using System.Text;

namespace SCoW.Core.Rules
{
    /// <summary>
    /// 캐릭터 생성 화면의 판단(UX-4, 순수 C#). 굴림 규칙·점수 계산은 건드리지 않고, 남은 점수 안내와 요약 문구만 만든다.
    /// 영문/한글 문구는 호출자가 고른다(글꼴에 한글이 없을 수 있음). TODO(I2): 새 키로 옮긴다.
    /// </summary>
    public static class CharacterCreation
    {
        /// <summary>스킬 점수를 다 썼으면 시작할 수 있다(옛 SkillMark 규칙 그대로).</summary>
        public static bool AllSpent(int strLeft, int intLeft, int dexLeft)
        {
            return strLeft == 0 && intLeft == 0 && dexLeft == 0;
        }

        /// <summary>남은 점수 경고 한 줄. 다 썼으면 빈 문자열.</summary>
        public static string Remaining(int strLeft, int intLeft, int dexLeft, bool korean)
        {
            if (AllSpent(strLeft, intLeft, dexLeft))
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(korean ? "남은 점수 " : "Points left ");
            Append(sb, "STR", strLeft);
            Append(sb, "INT", intLeft);
            Append(sb, "DEX", dexLeft);
            return sb.ToString().TrimEnd();
        }

        private static void Append(StringBuilder sb, string name, int left)
        {
            if (left != 0)
            {
                sb.Append(name).Append(' ').Append(left).Append(' ');
            }
        }

        /// <summary>능력치 요약 두 줄(위: STR INT DEX, 아래: HP MP SAN LUK 재산).</summary>
        public static string Summary(int str, int intl, int dex, int hp, int mp, int san, int luk, int weal)
        {
            return "STR " + str + "  INT " + intl + "  DEX " + dex + "\nHP " + hp + "  MP " + mp + "  SAN " + san + "  LUK " + luk + "  $" + weal;
        }
    }
}
