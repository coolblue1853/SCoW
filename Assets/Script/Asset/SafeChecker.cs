using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeChecker : MonoBehaviour
{
    public Sprite spriteA; // ���� �� ��������Ʈ
    public Sprite spriteB; // ���� �� ��������Ʈ
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ���� �� ��������Ʈ�� spriteA�� ����
        spriteRenderer.sprite = spriteA;
    }
    bool once = false;
    private void Update()
    {
        // ������ ������ �� ��������Ʈ ����
        if (DataBaseManager.getSafe == true && once == false)
        {
            once = true;
            spriteRenderer.sprite = spriteB;
        }
    }
}
