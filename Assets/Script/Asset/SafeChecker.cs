using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SafeChecker : MonoBehaviour
{
    public Sprite spriteBefore; // 변경 전 스프라이트
    public Sprite spriteAfter; // 변경 후 스프라이트
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 시작 시 스프라이트를 spriteA로 설정
        spriteRenderer.sprite = spriteBefore;
    }
    bool once = false;
    private void Update()
    {
        // 조건을 충족할 때 스프라이트 변경
        if (DataBaseManager.getSafe == true && once == false)
        {
            once = true;
            spriteRenderer.sprite = spriteAfter;
        }
    }
}
