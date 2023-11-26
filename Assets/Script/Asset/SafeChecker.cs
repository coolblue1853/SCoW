using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SafeChecker : MonoBehaviour
{
    public Sprite spriteBefore; // ���� �� ��������Ʈ
    public Sprite spriteAfter; // ���� �� ��������Ʈ
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ���� �� ��������Ʈ�� spriteA�� ����
        spriteRenderer.sprite = spriteBefore;
    }
    bool once = false;
    private void Update()
    {
        // ������ ������ �� ��������Ʈ ����
        if (DataBaseManager.getSafe == true && once == false)
        {
            once = true;
            spriteRenderer.sprite = spriteAfter;
        }
    }
}
