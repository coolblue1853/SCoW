using System.Collections.Generic;
using NUnit.Framework;
using SCoW.Core.Save;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SCoW.Tests
{
    /// <summary>UX-12: 키 충돌 검출·재지정 저장 왕복·키 안내 글리프·엔딩 기록.</summary>
    public class Ux12Tests
    {
        private InputActionAsset _asset;

        [SetUp]
        public void SetUp()
        {
            InputActionAsset source = Resources.Load<InputActionAsset>("Input/SCoWInput");
            Assert.IsNotNull(source);
            _asset = BindingStore.Clone(source);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_asset);
            HintGlyphs.Resolver = null;
        }

        private InputAction Act(string map, string name)
        {
            return _asset.FindActionMap(map).FindAction(name);
        }

        // ---------- 충돌 ----------

        [Test]
        public void Default_bindings_have_no_new_conflicts()
        {
            Assert.AreEqual(0, BindingRules.NewConflicts(BindingStore.Entries(_asset)).Count);
        }

        [Test]
        public void Same_key_in_same_map_is_a_conflict()
        {
            InputAction map = Act("UI", "OpenMap");
            map.ApplyBindingOverride(BindingStore.FindIndex(map, false), "<Keyboard>/1"); // OpenIntel 의 키
            List<BindingConflict> found = BindingRules.NewConflicts(BindingStore.Entries(_asset));
            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("Keyboard", found[0].Scheme);
            Assert.IsTrue((found[0].ActionA == "OpenMap" && found[0].ActionB == "OpenIntel") || (found[0].ActionA == "OpenIntel" && found[0].ActionB == "OpenMap"));
        }

        [Test]
        public void Same_key_in_other_map_or_scheme_is_not_a_conflict()
        {
            InputAction map = Act("UI", "OpenMap");
            map.ApplyBindingOverride(BindingStore.FindIndex(map, false), "<Keyboard>/e"); // E 는 Gameplay/Interact
            Assert.AreEqual(0, BindingRules.NewConflicts(BindingStore.Entries(_asset)).Count);
            map.ApplyBindingOverride(BindingStore.FindIndex(map, true), "<Gamepad>/buttonSouth"); // 패드 A: UI 맵에는 없음
            Assert.AreEqual(0, BindingRules.NewConflicts(BindingStore.Entries(_asset)).Count);
        }

        [Test]
        public void Baseline_shared_keys_are_not_reported_but_new_ones_are()
        {
            // 기본에서 Portal·Present 가 F 를 나눠 쓴다(상황이 달라 경고하지 않음). Interact 가 F 를 가져가면 Gameplay 맵의 새 충돌이다.
            InputAction interact = Act("Gameplay", "Interact");
            interact.ApplyBindingOverride(BindingStore.FindIndex(interact, false), "<Keyboard>/f");
            List<BindingConflict> found = BindingRules.NewConflicts(BindingStore.Entries(_asset));
            Assert.AreEqual(2, found.Count); // Interact-Portal, Interact-Present
        }

        [Test]
        public void Synthetic_entries_ignore_case_and_blank_paths()
        {
            List<BindingEntry> e = new List<BindingEntry>
            {
                new BindingEntry("M", "A", "Keyboard", "<Keyboard>/a", "<Keyboard>/G"),
                new BindingEntry("M", "B", "Keyboard", "<Keyboard>/b", "<Keyboard>/g"),
                new BindingEntry("M", "C", "Keyboard", "", ""),
                new BindingEntry("M", "D", "Keyboard", "", ""),
            };
            Assert.AreEqual(1, BindingRules.NewConflicts(e).Count);
        }

        // ---------- 저장 왕복 ----------

        [Test]
        public void Overrides_survive_settings_roundtrip()
        {
            InputAction a = Act("Gameplay", "Interact");
            a.ApplyBindingOverride(BindingStore.FindIndex(a, false), "<Keyboard>/g");
            a.ApplyBindingOverride(BindingStore.FindIndex(a, true), "<Gamepad>/buttonWest");
            SettingsData d = new SettingsData { BindingOverridesJson = BindingStore.Capture(_asset) };
            Assert.IsNotEmpty(d.BindingOverridesJson);

            SettingsData back = JsonUtility.FromJson<SettingsData>(JsonUtility.ToJson(d)); // ES3 가 공개 필드를 쓰는 것과 같은 모양
            back.Sanitize();
            InputActionAsset fresh = BindingStore.Clone(Resources.Load<InputActionAsset>("Input/SCoWInput"));
            try
            {
                BindingStore.Restore(fresh, back.BindingOverridesJson);
                InputAction b = fresh.FindActionMap("Gameplay").FindAction("Interact");
                Assert.AreEqual("<Keyboard>/g", BindingStore.EffectivePath(b, false));
                Assert.AreEqual("<Gamepad>/buttonWest", BindingStore.EffectivePath(b, true));
                BindingStore.Restore(fresh, string.Empty);
                Assert.AreEqual("<Keyboard>/e", BindingStore.EffectivePath(b, false));
                Assert.AreEqual("<Gamepad>/buttonSouth", BindingStore.EffectivePath(b, true));
            }
            finally
            {
                Object.DestroyImmediate(fresh);
            }
        }

        [Test]
        public void No_override_captures_empty_and_old_settings_default_to_empty()
        {
            Assert.AreEqual(string.Empty, BindingStore.Capture(_asset));
            SettingsData old = JsonUtility.FromJson<SettingsData>("{\"SoundVolume\":0.3}");
            old.BindingOverridesJson = null;
            old.Sanitize();
            Assert.AreEqual(string.Empty, old.BindingOverridesJson);
        }

        [Test]
        public void Corrupt_json_falls_back_to_defaults()
        {
            InputAction a = Act("Gameplay", "Interact");
            a.ApplyBindingOverride(BindingStore.FindIndex(a, false), "<Keyboard>/g");
            BindingStore.Restore(_asset, "{ not json");
            Assert.AreEqual("<Keyboard>/e", BindingStore.EffectivePath(a, false));
        }

        // ---------- 글리프 ----------

        [Test]
        public void Friendly_labels()
        {
            Assert.AreEqual("E", BindingLabel.Friendly("<Keyboard>/e"));
            Assert.AreEqual("Esc", BindingLabel.Friendly("<Keyboard>/escape"));
            Assert.AreEqual("F5", BindingLabel.Friendly("<Keyboard>/f5"));
            Assert.AreEqual("A", BindingLabel.Friendly("<Gamepad>/buttonSouth"));
            Assert.AreEqual("D-Pad Up", BindingLabel.Friendly("<Gamepad>/dpad/up"));
            Assert.AreEqual("R3", BindingLabel.Friendly("<Gamepad>/rightStickPress"));
            Assert.AreEqual("-", BindingLabel.Friendly(null));
        }

        [Test]
        public void Default_effective_paths_match_hint_table()
        {
            Assert.AreEqual("E", BindingLabel.Friendly(BindingStore.EffectivePath(Act("Gameplay", "Interact"), false)));
            Assert.AreEqual("1", BindingLabel.Friendly(BindingStore.EffectivePath(Act("UI", "OpenIntel"), false)));
            Assert.AreEqual("Y", BindingLabel.Friendly(BindingStore.EffectivePath(Act("UI", "OpenIntel"), true)));
            Assert.AreEqual("LT", BindingLabel.Friendly(BindingStore.EffectivePath(Act("Gameplay", "DialogBacklog"), true)));
            Assert.AreEqual("D-Pad Up", BindingLabel.Friendly(BindingStore.EffectivePath(Act("Gameplay", "Portal"), true)));
        }

        [Test]
        public void Resolver_overrides_default_glyph_and_null_falls_back()
        {
            Assert.AreEqual("E", HintGlyphs.Get(HintAction.Interact, InputDeviceKind.Keyboard));
            HintGlyphs.Resolver = (a, d) => a == HintAction.Interact ? "G" : null;
            Assert.AreEqual("G", HintGlyphs.Get(HintAction.Interact, InputDeviceKind.Keyboard));
            Assert.AreEqual("B", HintGlyphs.Get(HintAction.Backlog, InputDeviceKind.Keyboard));
        }

        // ---------- 엔딩 ----------

        [Test]
        public void Ending_record_marks_once_and_ignores_unknown()
        {
            EndingRecord r = new EndingRecord();
            Assert.IsTrue(r.Mark(EndingCatalog.Symbol));
            Assert.IsFalse(r.Mark(EndingCatalog.Symbol));
            Assert.IsFalse(r.Mark("nope"));
            Assert.IsTrue(r.IsSeen(EndingCatalog.Symbol));
            Assert.IsFalse(r.IsSeen(EndingCatalog.Blood));
        }

        [Test]
        public void Ending_record_survives_roundtrip_and_sanitizes()
        {
            EndingRecord r = new EndingRecord();
            r.Mark(EndingCatalog.Blood);
            r.Mark(EndingCatalog.Nightmare);
            EndingRecord back = JsonUtility.FromJson<EndingRecord>(JsonUtility.ToJson(r));
            back.Sanitize();
            Assert.IsTrue(back.IsSeen(EndingCatalog.Blood));
            Assert.IsTrue(back.IsSeen(EndingCatalog.Nightmare));
            Assert.IsFalse(back.IsSeen(EndingCatalog.Ladder));

            EndingRecord broken = new EndingRecord();
            broken.Seen = new List<string> { EndingCatalog.Arrest, EndingCatalog.Arrest, null, string.Empty };
            broken.Sanitize();
            Assert.AreEqual(1, broken.Seen.Count);
            broken.Seen = null;
            broken.Sanitize();
            Assert.AreEqual(0, broken.Seen.Count);
        }

        [Test]
        public void Ending_topics_map_to_ids()
        {
            Assert.AreEqual(EndingCatalog.Symbol, EndingCatalog.FromTopic("InSewer", "Ending_BreakSymbol"));
            Assert.AreEqual(EndingCatalog.Arrest, EndingCatalog.FromTopic("InSewer", "Ending_Killing"));
            Assert.AreEqual(EndingCatalog.Blood, EndingCatalog.FromTopic("Keyword", "Symbol/Advent"));
            Assert.AreEqual(EndingCatalog.Nightmare, EndingCatalog.FromTopic("1st_DetectiveOffice", "FourthNightmare"));
            Assert.AreEqual(EndingCatalog.Ladder, EndingCatalog.FromJudge("SanCheck/EndingLedderSan"));
            Assert.IsNull(EndingCatalog.FromTopic("InSewer", "Sewer_FirstIn"));
            Assert.IsNull(EndingCatalog.FromJudge("SanCheck/Other"));
        }
    }
}
