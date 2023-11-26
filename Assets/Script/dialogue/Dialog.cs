using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [Header("카메라의 타겟팅 대상")]
    public Transform tfTarget;

    [Tooltip("대사 치는 캐릭터이름")]
    public string name;

    [Tooltip("대사내용")]
    public string[] contexts;

    [HideInInspector]
    public string[] spriteName;

}
[System.Serializable]
public class DialogEvent
{
    public string name;
    public Vector2 line;
    public Dialog[] dialogs;
}
