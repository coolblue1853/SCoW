using System.Collections.Generic;

namespace SCoW.Core.Story
{
    public sealed class StoryScript
    {
        public string Id = "";
        public List<StoryTopic> Topics = new List<StoryTopic>();
        public Dictionary<string, StoryNode> Nodes = new Dictionary<string, StoryNode>();

        /// <summary>이 id 의 Topic 이 있는가(Case 가 맞는지는 보지 않는다). 키워드 대사의 "NPC/*" 기본 토픽 대체 판단용.</summary>
        public bool HasTopic(string topicId)
        {
            for (int i = 0; i < Topics.Count; i++)
            {
                if (Topics[i].Id == topicId) { return true; }
            }
            return false;
        }

        /// <summary>위에서부터 첫 번째로 맞는 Case 의 노드 id. 없으면 null.</summary>
        public StoryCase Pick(string topicId, IStateReader s)
        {
            for (int i = 0; i < Topics.Count; i++)
            {
                if (Topics[i].Id != topicId) { continue; }
                List<StoryCase> cases = Topics[i].Cases;
                for (int j = 0; j < cases.Count; j++)
                {
                    if (cases[j].When.Eval(s)) { return cases[j]; }
                }
            }
            return null;
        }
    }

    public sealed class StoryTopic
    {
        public string Id = "";
        public List<StoryCase> Cases = new List<StoryCase>();
    }

    public sealed class StoryCase
    {
        public Condition When = Condition.Always;
        public Effect[] Pre = new Effect[0];
        public string NodeId = "";
    }

    public sealed class StoryChoice
    {
        public string TextKey = "";
        public Condition When = Condition.Always;
        public string Next = "";
    }

    public sealed class StoryNode
    {
        public string Id = "";
        public string Speaker = "";
        /// <summary>대사 본문 CSV 의 ID(파일은 StoryScript.Id 와 같은 이름 또는 TextFile).</summary>
        public string TextKey = "";
        public string Sprite = "";
        public Effect[] OnEnter = new Effect[0];
        public StoryChoice[] Choices = new StoryChoice[0];
        public string Next = "";
        /// <summary>판정이 있으면 "skill,difficulty".</summary>
        public string CheckId = "";
        /// <summary>
        /// 판정 노드의 결과별 다음(노드 id 또는 Topic id. Topic 이면 Case 조건으로 다시 고른다). 하나라도 있으면 판정 노드다(StoryResolver.ResolveJudge).
        /// 대성공/대실패 칸이 비면 각각 OnSuccess/OnFail 로 간다(옛 InteractionController.RetrunDialogResult 는 대성공=성공, 대실패=실패로 같이 다뤘다).
        /// </summary>
        public string OnSuccess = "";
        public string OnFail = "";
        public string OnCritSuccess = "";
        public string OnCritFail = "";

        public bool IsJudge
        {
            get { return OnSuccess.Length > 0 || OnFail.Length > 0 || OnCritSuccess.Length > 0 || OnCritFail.Length > 0; }
        }
    }
}
