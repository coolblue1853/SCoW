using System.Collections.Generic;
using System.Text;

namespace SCoW.Core.Story
{
    /// <summary>CSV 한 줄(대사 1개). 열 구성: ID, 캐릭터이름, 대사, 메모(없으면 빈 문자열).</summary>
    public sealed class StoryLine
    {
        public int SourceLine;
        public string Id = "";
        public string Speaker = "";
        public string Text = "";
        public string Memo = "";
    }

    /// <summary>파싱 결과. 오류는 예외 대신 Errors 에 쌓는다.</summary>
    public sealed class StoryCsvResult
    {
        public List<StoryLine> Lines = new List<StoryLine>();
        public List<string> Errors = new List<string>();
        public bool Ok { get { return Errors.Count == 0; } }
    }

    /// <summary>
    /// RFC4180 CSV 파서(쉼표·따옴표·CRLF/LF/CR·빈 줄·UTF-8 BOM). 따옴표는 필드 맨 앞일 때만 특수 취급하고,
    /// 필드 중간의 날 따옴표는 글자 그대로 둔다(옛 CSV 호환). 옛 표식 @ ^ ⓖ(..)ⓦ ⒳ 등은 해석하지 않고 원문 그대로 둔다.
    /// </summary>
    public static class StoryCsvParser
    {
        public static StoryCsvResult Parse(string text)
        {
            StoryCsvResult result = new StoryCsvResult();
            if (text == null)
            {
                result.Errors.Add("CSV 텍스트가 null");
                return result;
            }
            List<string[]> records = new List<string[]>();
            List<int> lineNos = new List<int>();
            ParseRecords(text, records, lineNos, result.Errors);
            bool first = true;
            for (int i = 0; i < records.Count; i++)
            {
                string[] f = records[i];
                if (first)
                {
                    first = false;
                    if (string.Equals(f[0].Trim(), "ID", System.StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                }
                if (f.Length < 3)
                {
                    result.Errors.Add(lineNos[i] + "행: 열 부족(" + f.Length + "열, 최소 3열)");
                    continue;
                }
                StoryLine line = new StoryLine();
                line.SourceLine = lineNos[i];
                line.Id = f[0];
                line.Speaker = f[1];
                line.Text = f[2];
                line.Memo = f.Length > 3 ? f[3] : "";
                result.Lines.Add(line);
            }
            return result;
        }

        /// <summary>열 수 제한 없는 원시 레코드 분해. 빈 줄은 건너뛴다. lineNos 는 레코드 시작 행(1부터).</summary>
        public static void ParseRecords(string text, List<string[]> records, List<int> lineNos, List<string> errors)
        {
            int p = 0;
            int n = text.Length;
            if (n > 0 && text[0] == '\uFEFF')
            {
                p = 1;
            }
            int line = 1;
            List<string> fields = new List<string>();
            StringBuilder sb = new StringBuilder();
            int recStart = 1;
            bool recHasContent = false;
            bool fieldQuoted = false;

            while (p <= n)
            {
                bool eof = p == n;
                char c = eof ? '\n' : text[p];
                if (!eof && c == '"' && sb.Length == 0 && !fieldQuoted)
                {
                    int startLine = line;
                    p++;
                    bool closed = false;
                    while (p < n)
                    {
                        char q = text[p];
                        if (q == '"')
                        {
                            if (p + 1 < n && text[p + 1] == '"')
                            {
                                sb.Append('"');
                                p += 2;
                                continue;
                            }
                            p++;
                            closed = true;
                            break;
                        }
                        if (q == '\n')
                        {
                            line++;
                        }
                        sb.Append(q);
                        p++;
                    }
                    if (!closed)
                    {
                        errors.Add(startLine + "행: 닫히지 않은 따옴표");
                        sb.Length = 0;
                        fields.Clear();
                        recHasContent = false;
                        break;
                    }
                    fieldQuoted = true;
                    recHasContent = true;
                    if (p < n && text[p] != ',' && text[p] != '\r' && text[p] != '\n')
                    {
                        errors.Add(line + "행: 닫는 따옴표 뒤에 글자가 이어짐");
                    }
                    continue;
                }
                if (c == ',' && !eof)
                {
                    fields.Add(sb.ToString());
                    sb.Length = 0;
                    fieldQuoted = false;
                    recHasContent = true;
                    p++;
                    continue;
                }
                if (c == '\r' || c == '\n')
                {
                    if (c == '\r' && p + 1 < n && text[p + 1] == '\n')
                    {
                        p++;
                    }
                    p++;
                    fields.Add(sb.ToString());
                    bool blank = !recHasContent && fields.Count == 1 && fields[0].Length == 0 && !fieldQuoted;
                    if (!blank)
                    {
                        records.Add(fields.ToArray());
                        lineNos.Add(recStart);
                    }
                    fields.Clear();
                    sb.Length = 0;
                    fieldQuoted = false;
                    recHasContent = false;
                    if (!eof)
                    {
                        line++;
                    }
                    recStart = line;
                    if (eof)
                    {
                        break;
                    }
                    continue;
                }
                sb.Append(c);
                recHasContent = true;
                p++;
            }
        }
    }
}
