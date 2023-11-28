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
    public GameObject fadeImageOb;
    public Image fadeImage;
    public TextMeshProUGUI fadeText;
    public TextMeshProUGUI text;

    
    IEnumerator FadeOutCorutine()
    {
        float 페이드카운트 = 1f;
        fadeImageOb.SetActive(true);
        yield return new WaitForSeconds(2f);
        while (페이드카운트 > 0)
        {
            페이드카운트 -= 0.025f;
            yield return new WaitForSeconds(0.01f);
            fadeImage.color = new Color(0, 0, 0, 페이드카운트);
            fadeText.color = new Color(255f, 255f, 255f, 페이드카운트);
        }

        Destroy(this.gameObject);
    }
    IEnumerator FadeInCorutine()
    {
        float 페이드카운트 = 0;
        fadeImageOb.SetActive(true);

        while (페이드카운트 < 1f)
        {
            페이드카운트 += 0.025f;
            yield return new WaitForSeconds(0.01f);
           fadeImage.color = new Color(0, 0, 0, 페이드카운트);
           fadeText.color = new Color(255f, 255f, 255f, 페이드카운트);
        }

        StartCoroutine(FadeOutCorutine());
    }
    private void Start()
    {
        StartCoroutine(FadeInCorutine());
    }
}
