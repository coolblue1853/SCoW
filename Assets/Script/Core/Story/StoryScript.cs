using System.Collections.Generic;

namespace SCoW.Core.Story
{
    public sealed class StoryScript
    {
        public string Id = "";
        public List<StoryTopic> Topics = new List<StoryTopic>();
        public Dictionary<string, StoryNode> Nodes = new Dictionary<string, StoryNode>();

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
        public string OnSuccess = "";
        public string OnFail = "";
        public string OnCritFail = "";
    }
}
