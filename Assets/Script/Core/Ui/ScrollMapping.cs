namespace SCoW.Core.Ui
{
    /// <summary>
    /// 목록 스크롤 입력 매핑(UX-9). 휠·W/S·방향키·패드 십자키를 한 가지 "한 칸 이동" 값으로 합친다.
    /// 반환: -1 = 위(이전), +1 = 아래(다음), 0 = 없음. 키 입력은 위/아래가 동시에 오면 0(상쇄).
    /// 휠은 위로 굴리면 양수. 키가 이번 프레임에 눌렸다면 키가 우선한다.
    /// </summary>
    public static class ScrollMapping
    {
        /// <summary>이 값 이하의 휠 변화는 무시한다(트랙패드 미세 떨림).</summary>
        public const float WheelDeadZone = 0.1f;

        public static int Resolve(bool navUp, bool navDown, float wheelY)
        {
            if (navUp || navDown)
            {
                if (navUp && navDown)
                {
                    return 0;
                }
                return navUp ? -1 : 1;
            }
            if (wheelY > WheelDeadZone)
            {
                return -1;
            }
            if (wheelY < -WheelDeadZone)
            {
                return 1;
            }
            return 0;
        }
    }
}
