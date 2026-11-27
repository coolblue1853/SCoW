using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SCoW.Core.Rules;
using SCoW.Core.State;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 전투 실플레이 검증 도구(에디터 플레이 모드 전용). 실제 키 입력 없이 공개 메서드·버튼 onClick·룰렛 내부 호출로 전투를 끝까지 진행한다.
/// execute_code 에서 BattlePlaytestDriver.Setup / Begin / Tail / Stop 을 부른다. 로그는 Logs/playtest/driver.log.
/// </summary>
public static class BattlePlaytestDriver
{
    private const BindingFlags Bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    private const string LogPath = "Logs/playtest/driver.log";

    /// <summary>attack(반격·공격), evade(회피), flee(도주), lose(HP 1 로 두고 막지 않기).</summary>
    public static string Mode = "attack";
    public static int Swaps;
    public static int Turns;
    private static GameObject _runner;

    /// <summary>캐릭터 생성 결과처럼 정상 상태를 만들고 파티 인원(1~3)을 맞춘다.</summary>
    public static string Setup(int party, int hp, int san)
    {
        GameState s = GameRoot.Instance.State;
        s.SetCounter("str", 60); s.SetCounter("dex", 60); s.SetCounter("intl", 70); s.SetCounter("luk", 55); s.SetCounter("weal", 50);
        s.SetCounter("hp", Mathf.Max(60, hp)); s.SetCounter("nowHP", hp); s.SetCounter("san", 65); s.SetCounter("nowSan", san);
        s.SetCounter("sewerEnemyCounter", 0);
        s.SetCounter("firstaidkit", 1);
        if (party >= 2) { PartyState.Join(s, CompanionDefs.Albert); }
        if (party >= 3) { PartyState.Join(s, CompanionDefs.Maeve); }
        DataBaseManager.nowPlace = "InSewer";
        return "party=" + PartyState.ActiveCompanions(s).Count + " hp=" + DataBaseManager.nowHP + " san=" + DataBaseManager.nowSan;
    }

    public static void HideDialog()
    {
        GameObject dm = (GameObject)typeof(DirectingManager).GetField("dialogManager").GetValue(DirectingManager.Instance);
        dm.SetActive(false);
        DataBaseManager.isActiveDialog1 = false;
    }

    /// <summary>하수도 전투를 연다(조우 번호 = 적 구성)하고 구동기를 켠다.</summary>
    public static string Begin(string mode, int encounter)
    {
        Mode = mode; Swaps = 0; Turns = 0;
        File.WriteAllText(LogPath, "");
        GameRoot.Instance.State.SetCounter("sewerEnemyCounter", encounter);
        DirectingManager.Instance.GotoBattleSewer();
        HideDialog();
        if (_runner != null) { Object.Destroy(_runner); }
        _runner = new GameObject("BattlePlaytestRunner");
        _runner.AddComponent<Runner>();
        return "begin " + mode + " enc=" + encounter;
    }

    public static void Stop()
    {
        if (_runner != null) { Object.Destroy(_runner); _runner = null; }
    }

    public static string Tail(int n)
    {
        string[] lines = File.ReadAllLines(LogPath);
        StringBuilder sb = new StringBuilder();
        for (int i = Mathf.Max(0, lines.Length - n); i < lines.Length; i++) { sb.AppendLine(lines[i]); }
        return sb.ToString();
    }

    private static void Log(string line)
    {
        File.AppendAllText(LogPath, Time.time.ToString("F1") + " " + line + "\n");
    }

    private static object F(object o, string n) { return o.GetType().GetField(n, Bf).GetValue(o); }
    private static void S(object o, string n, object v) { o.GetType().GetField(n, Bf).SetValue(o, v); }
    private static bool On(object o, string n) { GameObject g = F(o, n) as GameObject; return g != null && g.activeInHierarchy; }
    private static void M(object o, string n) { o.GetType().GetMethod(n, Bf).Invoke(o, null); }

    private static bool PumpBattleRollet()
    {
        BattleRollet br = BattleRollet.Instance;
        if (br == null) { return false; }
        if (On(br, "endButton")) { br.EndRollet(); return true; }
        if ((bool)F(br, "isActiveRollet"))
        {
            br.CancelInvoke(); S(br, "isClick", true); S(br, "isActiveRollet", false); br.Invoke("GetIntResult", 0.5f);
            return true;
        }
        if (On(br, "setUI") && (bool)F(br, "activeCheck")) { S(br, "activeCheck", false); br.ActiveRollet(); return true; }
        return false;
    }

    private static bool PumpRollet()
    {
        Rollet r = Rollet.Instance;
        if (r == null) { return false; }
        if (On(r, "endButton")) { r.EndRollet(); return true; }
        if ((bool)F(r, "isActiveRollet") && !(bool)F(r, "isClick")) { M(r, "FastStop"); return true; }
        if (On(r, "setUI") && (bool)F(r, "activeCheck")) { S(r, "activeCheck", false); r.ActiveRollet(); return true; }
        if (On(r, "sanRollet") && (bool)F(r, "activeCheck2")) { S(r, "activeCheck2", false); M(r, "ActiveSanRollet"); return true; }
        if (On(r, "endButton2")) { M(r, "EndSanRollet"); return true; }
        return false;
    }

    private sealed class Runner : MonoBehaviour
    {
        private string _last = "";
        private float _next;

        private void Update()
        {
            if (Time.time < _next) { return; }
            _next = Time.time + 0.3f;
            if (Mode == "stop") { return; }
            BattleManager bm = BattleManager.Instance;
            if (bm == null) { return; }
            PartyBattleDirector pbd = PartyBattleDirector.Instance;
            string st;
            if (PartyBattleDirector.Running)
            {
                st = "PARTY " + pbd.DebugState + " | " + Summary(pbd.Field);
            }
            else
            {
                st = bm.battleState + "|hp=" + DataBaseManager.nowHP + "|san=" + DataBaseManager.nowSan + "|" + bm.roundText.text;
                IList en = (IList)F(bm, "_enemies");
                foreach (object e in en) { EnemyState es = (EnemyState)e; st += "|" + es.Def.Id + "=" + es.NowHp; }
            }
            if (st != _last) { Log(st); _last = st; }
            if (PumpBattleRollet() || PumpRollet()) { return; }
            if (PartyBattleDirector.Running) { DrivePartyView(pbd.View); return; }
            DriveSolo(bm);
        }

        private static string Summary(BattleField f)
        {
            if (f == null) { return "-"; }
            StringBuilder sb = new StringBuilder();
            foreach (BattleSlot sl in f.Allies) { if (sl.Unit != null) { sb.Append(sl.Unit.Id + ":" + sl.Unit.Hp + "/san" + sl.Unit.San + "/" + sl.Unit.Status + "/r" + sl.Rank + " "); } }
            sb.Append("vs ");
            foreach (BattleSlot sl in f.Enemies) { if (sl.Unit != null) { sb.Append(sl.Unit.Id + ":" + sl.Unit.Hp + " "); } }
            return sb.ToString();
        }

        private static void DriveSolo(BattleManager bm)
        {
            if (bm.runAwayObject.activeSelf) { bm.RunYes(); return; }
            if (bm.playerChoiceUi.activeSelf && bm.battleState == BattlePhase.EnemyAttack)
            {
                if (Mode == "evade" || Mode == "flee") { bm.ChoiceEvasion(); } else { bm.ChoiceCounterAttack(); }
                return;
            }
            if (bm.battleState == BattlePhase.PlayerChoice && bm.playerActionUi.activeSelf)
            {
                if (Mode == "flee") { bm.RunAway(); return; }
                Click(bm.playerActionUi, "Attack");
            }
            else if (bm.battleState == BattlePhase.PlayerAttack)
            {
                Click(bm.playerAttackUi, "martialarts");
            }
            else if (bm.battleState == BattlePhase.SelectEnemy)
            {
                IList en = (IList)F(bm, "_enemies");
                string[] pref = { "DeepOneHybrid4", "DeepOne1", "DeepOneHybrid3" };
                GameObject[] sym = { bm.enemyTrunSymbol1, bm.enemyTrunSymbol2, bm.enemyTrunSymbol3 };
                for (int p = 0; p < pref.Length; p++)
                {
                    foreach (object e in en)
                    {
                        EnemyState es = (EnemyState)e;
                        if (es.NowHp > 0 && es.Def.Id == pref[p] && sym[p].activeSelf) { bm.onPointerEnemy = pref[p]; bm.SetEnemy(pref[p]); return; }
                    }
                }
            }
        }

        private static void Click(GameObject root, string name)
        {
            foreach (Button b in root.GetComponentsInChildren<Button>(true))
            {
                if (b.name == name) { b.onClick.Invoke(); return; }
            }
        }

        private static void DrivePartyView(PartyBattleView v)
        {
            if (v == null) { return; }
            string mode = v.DebugMode;
            if (mode == "Menu")
            {
                List<ActionOption> menu = (List<ActionOption>)F(v, "_menu");
                int idx = -1;
                bool response = menu.Count == 2 && menu[0].Label == "Dodge";
                if (response) { idx = Mode == "evade" ? 0 : 1; }
                else
                {
                    string want = "Attack";
                    PartyMember actor = (PartyMember)F(v, "_actor");
                    if (actor != null && actor.IsLeader && Swaps < 2) { want = "Swap"; }
                    if (actor != null && actor.IsLeader && Mode == "flee") { want = "Run"; }
                    for (int i = 0; i < menu.Count; i++)
                    {
                        if (menu[i].Label == want && menu[i].Enabled) { idx = i; }
                    }
                    if (idx < 0)
                    {
                        for (int i = 0; i < menu.Count; i++)
                        {
                            if ((menu[i].Label == "Attack" || menu[i].Label == "Shoot") && menu[i].Enabled) { idx = i; }
                        }
                    }
                    if (idx < 0)
                    {
                        for (int i = 0; i < menu.Count; i++)
                        {
                            if (menu[i].Enabled && menu[i].Label != "Run") { idx = i; break; }
                        }
                    }
                    if (want == "Swap" && idx >= 0 && menu[idx].Label == "Swap") { Swaps++; }
                    Turns++;
                }
                if (idx < 0) { idx = 0; }
                Log("PICK " + (idx < menu.Count ? menu[idx].Label : "?"));
                v.DebugAnswer(idx);
            }
            else if (mode == "Target")
            {
                v.DebugAnswer(0);
            }
        }
    }
}
