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
    bool isSelectEnemy3 = false;

    bool isSetTrun = false;
    bool isSelectEnemy1 = false;
    bool isSelectEnemy2= false;
    void Update()
    {
        if (BattleManager.Instance.BattleState == "setTrun" && isSetTrun == false)
        {
            isSelectEnemy1 = false;
            isSelectEnemy2 = false;
            isSetTrun = true;
            spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);

        }
        if (BattleManager.Instance.BattleState == "selectEnemy")
        {
            isSetTrun = false;
               Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치 가져오기
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); // 마우스 위치에서 Raycast

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == this.transform.name)
                {
                    if (hit.collider.CompareTag("Enemy")) // Raycast가 무언가에 닿은 경우, 태그를 확인
                    {
                        BattleManager.Instance.onPointerEnemy = this.transform.name;

                        box2d.size = new Vector2(5f, 6.2f);

                        if (Input.GetMouseButton(0))
                        {

                            ButtonClick();
                        }
                    }

                }
            }
            else if (hit.collider == null)
            {
                Debug.Log(DataBaseManager.nowPlace);
                Debug.Log("확인중");
                BattleManager.Instance.onPointerEnemy = "";
                box2d.size = new Vector2(3f, 5.2f);
            }

 
            if (BattleManager.Instance.onPointerEnemy != "")
            {
                isSelectEnemy3 = false;
                if (BattleManager.Instance.onPointerEnemy != this.transform.name && isSelectEnemy2 == false)
                {
                    box2d.size = new Vector2(3f, 5.2f);

                    isSelectEnemy1 = false;
                    isSelectEnemy2 = true;
                    spriteRenderer.DOFade(0.1f, 0.3f).SetAutoKill(true);

                }
                else if (BattleManager.Instance.onPointerEnemy == this.transform.name && isSelectEnemy1 == false)
                {

                    isSelectEnemy1 = true;
                    isSelectEnemy2 = false;
                    spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);

                }
            }
            else if(BattleManager.Instance.onPointerEnemy == "" && isSelectEnemy3 == false)
            {

                isSelectEnemy1 = false;
                isSelectEnemy2 = false;
                isSelectEnemy3 = true;
                spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);
            }


            /*
             * 
             * 
             * 
            if (BattleManager.Instance.onPointerEnemy == "" && isSelectEnemy1 == false)
            {
                isSelectEnemy1 = true;
                isSelectEnemy2 = false;
                spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);
                if (this.spriteRenderer.color.a >= 0.1f && this.spriteRenderer.color.a != 1f)
                {

                    spriteRenderer.DOFade(1f, 0.3f).SetAutoKill(true);
                }

            }
            else if (BattleManager.Instance.onPointerEnemy != "" && isSelectEnemy2 == false)
            {
                isSelectEnemy1 = false;
                isSelectEnemy2 = true;
                if (BattleManager.Instance.onPointerEnemy != this.transform.name)
                {
                    spriteRenderer.DOFade(0.1f, 0.3f).SetAutoKill(true);
                    if (this.spriteRenderer.color.a <= 1f)
                    {


                    }


                }
            }
            */
        }
      

    }

}
