using System.Collections;
using SCoW.Core.Save;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 설정창의 "키"(키 재지정)·"엔딩"(본 엔딩 목록) 탭(UX-12).
/// 키 탭: 동작마다 키보드·패드 키를 보이고, 행을 고르면 다음 입력으로 바꾼다. 조작 3경로:
/// 마우스 = 행 가운데 조금 오른쪽(x 80) 기준 왼쪽 클릭은 키보드·오른쪽 클릭은 패드 키 재지정, 키보드 = A/D 로 키보드(A)·패드(D) 선택 후 E 또는 Enter 는 마지막 입력 장치 기준,
/// 패드 = A 로 패드 키 재지정(D-Pad 좌우로 키보드/패드 선택). 재지정 중 Esc(패드는 Start)는 취소.
/// 결과는 즉시 settings.es3 에 저장하고, 기본 키에는 없던 충돌은 맨 아래 줄에 경고한다. 문구는 SaveText.Pick 임시(TODO I2).
/// </summary>
public sealed partial class SettingsView
{
    private const float KeyRowH = 26f;
    private const float KeyRowStep = 28f;
    private const float KeyRowY0 = 44f;
    private static readonly Color WarnColor = new Color(0.95f, 0.45f, 0.35f, 1f);

    private void BuildKeysTab()
    {
        for (int i = 0; i < GameInput.RebindSlots.Length; i++)
        {
            AddKeyRow(i);
        }
        Row reset = NewRow(KeysTab, SaveText.Pick("기본 키로 되돌리기", "Reset to Default Keys"));
        reset.Label.fontSize = 18;
        reset.Label.rectTransform.sizeDelta = new Vector2(RowW - 36f, KeyRowH);
        reset.Activate = () =>
        {
            Click();
            GameInput.ResetBindings();
            RefreshKeysTab();
        };
        reset.Root.GetComponent<RowPointer>().OnClick = e => reset.Activate();
        Row warn = NewRow(KeysTab, string.Empty);
        warn.Label.fontSize = 16;
        warn.Label.rectTransform.sizeDelta = new Vector2(RowW - 36f, KeyRowH);
        warn.Refresh = () =>
        {
            System.Collections.Generic.List<string> lines = GameInput.ConflictLines();
            if (lines.Count == 0)
            {
                warn.Label.color = TextColor;
                warn.Label.text = SaveText.Pick("키 충돌 없음", "No key conflicts");
            }
            else
            {
                warn.Label.color = WarnColor;
                warn.Label.text = SaveText.Pick("키 충돌: ", "Key conflict: ") + string.Join(", ", lines.ToArray());
            }
        };
    }

    private void AddKeyRow(int slot)
    {
        GameInput.RebindSlot s = GameInput.RebindSlots[slot];
        Row row = NewRow(KeysTab, SaveText.Pick(s.LabelKo, s.LabelEn));
        row.Label.fontSize = 18;
        row.Value = MakeValueText(row.Root.transform);
        row.Value.rectTransform.sizeDelta = new Vector2(320f, KeyRowH);
        row.Value.resizeTextForBestFit = false; // 줄바꿈 없는 Overflow 라 맞춤 줄이기가 안 듣는다: 작은 고정 크기
        row.Value.fontSize = 13;
        row.ValueText = () => "KB " + GameInput.SlotLabel(slot, false) + "   Pad " + GameInput.SlotLabel(slot, true);
        row.Refresh = () => RefreshRow(row);
        row.Adjust = dir => StartRebind(row, slot, dir > 0);
        row.Activate = () => StartRebind(row, slot, GameInput.LastDevice == SCoW.Core.Ui.InputDeviceKind.Gamepad);
        row.Root.GetComponent<RowPointer>().OnClick = e =>
        {
            RectTransform rt = (RectTransform)row.Root.transform;
            Vector2 local;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, e.position, e.pressEventCamera, out local);
            StartRebind(row, slot, local.x > 80f);
        };
    }

    private void StartRebind(Row row, int slot, bool pad)
    {
        if (GameInput.IsRebinding)
        {
            return;
        }
        Click();
        row.Value.text = pad
            ? SaveText.Pick("패드 버튼을 누르세요 (Start 취소)", "Press a pad button (Start cancels)")
            : SaveText.Pick("키를 누르세요 (Esc 취소)", "Press a key (Esc cancels)");
        if (!GameInput.BeginRebind(slot, pad, changed => StartCoroutine(EndRebindNextFrame())))
        {
            RefreshRow(row);
        }
    }

    // 취소 키(Esc·Start)가 같은 프레임에 설정창 닫기·메뉴로 새지 않도록 입력을 한 프레임 뒤에 다시 켠다.
    private IEnumerator EndRebindNextFrame()
    {
        yield return null;
        GameInput.EndRebind();
        RefreshKeysTab();
    }

    private void RefreshKeysTab()
    {
        for (int i = 0; i < _tabRows[KeysTab].Count; i++)
        {
            Row r = _tabRows[KeysTab][i];
            if (r.Refresh != null)
            {
                r.Refresh();
            }
        }
    }

    private void BuildEndingsTab()
    {
        for (int i = 0; i < EndingCatalog.All.Length; i++)
        {
            EndingInfo info = EndingCatalog.All[i];
            Row row = NewRow(EndingsTab, "???");
            row.Value = MakeValueText(row.Root.transform);
            row.Label.rectTransform.sizeDelta = new Vector2(380f, RowH);
            row.Refresh = () =>
            {
                bool seen = EndingService.IsSeen(info.Id);
                row.Label.text = seen ? SaveText.Pick(info.TitleKo, info.TitleEn) : "???";
                row.Value.rectTransform.sizeDelta = new Vector2(120f, RowH);
                row.Value.text = seen ? SaveText.Pick("달성", "Seen") : "-";
            };
        }
    }
}
