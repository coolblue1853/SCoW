using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
public class IntelFloating : MonoBehaviour
{

    public float moveSpeed;
    public float destroyTime;

    public GameObject 페이드이미지오브젝트;
    public Image 페이드이미지;
    public TextMeshProUGUI 페이드글자;

    public TextMeshProUGUI Text;

    private Vector3 vector;
    IEnumerator 페이드아웃코루틴()
    {
        float 페이드카운트 = 1f;
        페이드이미지오브젝트.SetActive(true);
        yield return new WaitForSeconds(2f);
        while (페이드카운트 > 0)
        {
            페이드카운트 -= 0.025f;
            yield return new WaitForSeconds(0.01f);
            페이드이미지.color = new Color(0, 0, 0, 페이드카운트);
            페이드글자.color = new Color(255f, 255f, 255f, 페이드카운트);
        }

        Destroy(this.gameObject);
    }
    IEnumerator 페이드인코루틴()
    {
        float 페이드카운트 = 0;
        페이드이미지오브젝트.SetActive(true);

        while (페이드카운트 < 1f)
        {
            페이드카운트 += 0.025f;
            yield return new WaitForSeconds(0.01f);
           페이드이미지.color = new Color(0, 0, 0, 페이드카운트);
           페이드글자.color = new Color(255f, 255f, 255f, 페이드카운트);
        }

        StartCoroutine(페이드아웃코루틴());
    }
    private void Start()
    {
        StartCoroutine(페이드인코루틴());
    }
}
