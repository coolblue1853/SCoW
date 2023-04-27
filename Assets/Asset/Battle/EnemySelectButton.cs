using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{

    public void ButtonClick()
    {
        BattleManager.Instance.SetEnemy(this.transform.name);
    }
    Vector3 m_vecMouseDownPos;

    void Update()
    {
#if UNITY_EDITOR
        // ���콺 Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
#else
        // ��ġ ��
        if (Input.touchCount > 0)
#endif
        {

#if UNITY_EDITOR
            m_vecMouseDownPos = Input.mousePosition;
#else
            m_vecMouseDownPos = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif


            // ���콺 Ŭ�� ��ġ�� ī�޶� ��ũ�� ��������Ʈ�� �����մϴ�.
            Vector2 pos = Camera.main.ScreenToWorldPoint(m_vecMouseDownPos);

            // Raycast�Լ��� ���� �ε�ġ�� collider�� hit�� ���Ϲ޽��ϴ�.
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                if(BattleManager.Instance.BattleState == "selectEnemy")
                {
                    ButtonClick();
                }

            }

        }
    }
}
