using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemySelectButton : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D box2d;
    private void Awake()
    {
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>();
        box2d = this.transform.GetComponent<BoxCollider2D>();
    }
    public void ButtonClick()
    {
        BattleManager.Instance.SetEnemy(this.transform.name);
    }
    public void ButtonEnter()
    {


    }


    void Update()
    {
        Debug.Log(BattleManager.Instance.onPointerEnemy);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 가져오기
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // 마우스 위치에서 Raycast

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == this.transform.name)
            {
                if (hit.collider.CompareTag("Enemy")) // Raycast가 무언가에 닿은 경우, 태그를 확인
                {
                    BattleManager.Instance.onPointerEnemy = this.transform.name;
                    box2d.size = new Vector2 (5f, 6.2f);
                }

            }
        }
        if (hit.collider == null)
        {
            BattleManager.Instance.onPointerEnemy = "";
            box2d.size = new Vector2(3f, 5.2f);
        }




        if (BattleManager.Instance.onPointerEnemy == "")
        {
            if(this.spriteRenderer.color.a >= 0.1f && this.spriteRenderer.color.a != 1f)
            {
                Debug.Log(6);
                spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);
            }

        }
        if (BattleManager.Instance.onPointerEnemy != "")
        {
            if (BattleManager.Instance.onPointerEnemy != this.transform.name)
            {
                if(this.spriteRenderer.color.a <= 1f)
                {
                    Debug.Log(7);
                    spriteRenderer.DOFade(0.1f, 0.3f).SetAutoKill(true);
                }


            }
        }

    }

}
