/// <summary>
/// 코드가 직접 만드는 UI 문구의 한 자리. I2 Localization 은 쓰지 않는다(대사창 log·auto·TEXT 는 영문 고정 표기, 씬 문구만 I2 키).
/// 새 문구는 여기에 추가하고, 번역이 필요해지면 이 클래스만 I2 조회로 바꾼다.
/// </summary>
public static class UiText
{
    public const string DialogLog = "log";
    public const string DialogAuto = "auto";
    public const string DialogAutoOn = "AUTO ON";
    public const string DialogSkip = "skip";
    public const string DialogSkipOn = "SKIP ON";
    // TODO I2: 영문 고정 표기
}
