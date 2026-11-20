using System;
using System.Collections.Generic;

namespace SCoW.Core.Story
{
    /// <summary>
    /// 노드 본문 참조 `text`. 형식은 `CSV파일:시작ID-끝ID`(끝 포함) 또는 `CSV파일:ID`(한 줄).
    /// 파일 = Resources 의 CSV 이름(확장자 없음). ID = CSV 첫 열 값이며 옛 인스펙터 interactionEvent.line(x,y) 와 같은 숫자다
    /// (옛 줄 번호 = 비어 있지 않은 ID 줄의 등장 순서 = 현재 CSV 에서는 ID 값). 한 CSV 안에서 ID 는 유일해야 한다.
    /// </summary>
    public sealed class StoryTextRef
    {
        public string File = "";
        public string FromId = "";
        public string ToId = "";

        public static StoryTextRef Parse(string src, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(src)) { error = "text 비어 있음"; return null; }
            int colon = src.IndexOf(':');
            if (colon <= 0 || colon == src.Length - 1) { error = "text 형식 '파일:시작-끝' 아님: " + src; return null; }
            StoryTextRef t = new StoryTextRef();
            t.File = src.Substring(0, colon).Trim();
            string ids = src.Substring(colon + 1).Trim();
            int dash = ids.IndexOf('-');
            if (dash < 0)
            {
                t.FromId = ids;
                t.ToId = ids;
            }
            else
            {
                t.FromId = ids.Substring(0, dash).Trim();
                t.ToId = ids.Substring(dash + 1).Trim();
            }
            if (t.File.Length == 0 || t.FromId.Length == 0 || t.ToId.Length == 0)
            {
                error = "text 형식 '파일:시작-끝' 아님: " + src;
                return null;
            }
            return t;
        }
    }

    /// <summary>해석된 대사 한 줄의 위치(CSV 파일 + ID).</summary>
    public struct StoryLineRef
    {
        public string File;
        public string Id;

        public override string ToString() { return File + ":" + Id; }
    }

    public sealed class StoryResolution
    {
        public string NodeId = "";
        public List<StoryLineRef> Lines = new List<StoryLineRef>();
    }

    /// <summary>
    /// Topic → 첫 맞는 Case → (Pre 효과 적용) → 노드 → (OnEnter 효과) → 본문 줄 목록. 엔진 참조 없음.
    /// 본문이 풀리지 않으면(파일·ID 없음) 효과를 적용하지 않고 null 과 오류를 돌려준다. Topic·Case 가 없으면 null(오류 아님).
    /// </summary>
    public static class StoryResolver
    {
        public static StoryResolution Resolve(StoryScript script, string topicId, IStateReader r, IStateWriter w,
            Func<string, IList<StoryLine>> csv, List<string> errors)
        {
            StoryCase c = script.Pick(topicId, r);
            if (c == null) { return null; }
            StoryNode node;
            if (!script.Nodes.TryGetValue(c.NodeId, out node)) { errors.Add("없는 노드 '" + c.NodeId + "'"); return null; }

            StoryResolution res = new StoryResolution();
            res.NodeId = node.Id;
            if (node.TextKey.Length > 0 && !ExpandText(node.TextKey, csv, res.Lines, errors)) { return null; }

            EffectRunner.ApplyAll(c.Pre, w, r, null);
            EffectRunner.ApplyAll(node.OnEnter, w, r, null);
            return res;
        }

        private static bool ExpandText(string text, Func<string, IList<StoryLine>> csv, List<StoryLineRef> output, List<string> errors)
        {
            string err;
            StoryTextRef t = StoryTextRef.Parse(text, out err);
            if (t == null) { errors.Add(err); return false; }
            IList<StoryLine> lines = csv(t.File);
            if (lines == null) { errors.Add("CSV 를 찾지 못함: " + t.File); return false; }
            int from = IndexOfId(lines, t.FromId, 0);
            if (from < 0) { errors.Add(t.File + ": 시작 ID '" + t.FromId + "' 없음"); return false; }
            int to = IndexOfId(lines, t.ToId, from);
            if (to < 0) { errors.Add(t.File + ": 끝 ID '" + t.ToId + "' 없음(시작 뒤)"); return false; }
            for (int i = from; i <= to; i++)
            {
                StoryLineRef lr = new StoryLineRef();
                lr.File = t.File;
                lr.Id = lines[i].Id;
                output.Add(lr);
            }
            return true;
        }

        private static int IndexOfId(IList<StoryLine> lines, string id, int start)
        {
            for (int i = start; i < lines.Count; i++)
            {
                if (lines[i].Id == id) { return i; }
            }
            return -1;
        }
    }
}
