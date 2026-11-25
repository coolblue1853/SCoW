using System;
using System.Collections.Generic;
using SCoW.Core.Ui;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 입력 에셋의 키 재지정(UX-12) 도우미. 에셋 인스턴스를 인자로 받아 GameInput 과 테스트가 같이 쓴다.
/// 저장 형식은 Input System 의 SaveBindingOverridesAsJson 문자열(SettingsData.BindingOverridesJson).
/// </summary>
public static class BindingStore
{
    private const string PadPrefix = "<Gamepad>";
    private const string KeyPrefix = "<Keyboard>";

    /// <summary>액션에서 재지정 대상 바인딩 번호. 키보드는 첫 &lt;Keyboard&gt; 단일 바인딩, 패드는 첫 &lt;Gamepad&gt; 단일 바인딩. 없으면 -1.</summary>
    public static int FindIndex(InputAction action, bool pad)
    {
        if (action == null)
        {
            return -1;
        }
        string prefix = pad ? PadPrefix : KeyPrefix;
        for (int i = 0; i < action.bindings.Count; i++)
        {
            InputBinding b = action.bindings[i];
            if (b.isComposite || b.isPartOfComposite || string.IsNullOrEmpty(b.path))
            {
                continue;
            }
            if (b.path.StartsWith(prefix, StringComparison.Ordinal))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>재지정이 반영된 컨트롤 경로. 바인딩이 없으면 null.</summary>
    public static string EffectivePath(InputAction action, bool pad)
    {
        int i = FindIndex(action, pad);
        return i < 0 ? null : action.bindings[i].effectivePath;
    }

    /// <summary>재지정 결과를 JSON 으로. 재지정이 하나도 없으면 빈 문자열(= 기본 키).</summary>
    public static string Capture(InputActionAsset asset)
    {
        if (asset == null || !HasOverride(asset))
        {
            return string.Empty;
        }
        return asset.SaveBindingOverridesAsJson();
    }

    /// <summary>JSON 을 에셋에 적용한다. 빈 값이면 기본 키로 되돌린다. 손상된 JSON 도 기본 키로 돌아가며 예외를 던지지 않는다.</summary>
    public static void Restore(InputActionAsset asset, string json)
    {
        if (asset == null)
        {
            return;
        }
        asset.RemoveAllBindingOverrides();
        if (string.IsNullOrEmpty(json))
        {
            return;
        }
        try
        {
            asset.LoadBindingOverridesFromJson(json);
        }
        catch (Exception e)
        {
            Debug.LogWarning("BindingStore 복원 실패, 기본 키 사용: " + e.Message);
            asset.RemoveAllBindingOverrides();
        }
    }

    public static bool HasOverride(InputActionAsset asset)
    {
        foreach (InputActionMap map in asset.actionMaps)
        {
            for (int i = 0; i < map.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(map.bindings[i].overridePath))
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>충돌 검출용 목록: 합성(composite)과 그 부품을 뺀 단일 바인딩 전부.</summary>
    public static List<BindingEntry> Entries(InputActionAsset asset)
    {
        List<BindingEntry> list = new List<BindingEntry>();
        if (asset == null)
        {
            return list;
        }
        foreach (InputActionMap map in asset.actionMaps)
        {
            for (int i = 0; i < map.bindings.Count; i++)
            {
                InputBinding b = map.bindings[i];
                if (b.isComposite || b.isPartOfComposite || string.IsNullOrEmpty(b.path) || string.IsNullOrEmpty(b.action))
                {
                    continue;
                }
                string scheme = b.path.StartsWith(PadPrefix, StringComparison.Ordinal) ? "Gamepad" : "Keyboard";
                list.Add(new BindingEntry(map.name, b.action, scheme, b.path, b.effectivePath));
            }
        }
        return list;
    }

    /// <summary>테스트·도구용: 에셋을 복제한다(원본 Resources 에셋에 재지정이 새지 않게).</summary>
    public static InputActionAsset Clone(InputActionAsset source)
    {
        InputActionAsset copy = ScriptableObject.CreateInstance<InputActionAsset>();
        copy.LoadFromJson(source.ToJson());
        return copy;
    }
}
