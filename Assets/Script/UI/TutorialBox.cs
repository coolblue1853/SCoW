using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class TutorialBox : MonoBehaviour
{
    public GameObject[] childObjects; // 자식 오브젝트들을 배열로 저장합니다.
    private int currentIndex = 0; // 현재 자식 오브젝트의 인덱스를 추적합니다.

    private void Start()
    {
        // 모든 자식 오브젝트를 비활성화합니다.
        foreach (GameObject childObject in childObjects)
        {
            childObject.SetActive(false);
        }
        // 첫 번째 자식 오브젝트를 활성화합니다.
        childObjects[currentIndex].SetActive(true);
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.ClickSoundPlay();
        // 현재 자식 오브젝트를 비활성화합니다.
        childObjects[currentIndex].SetActive(false);
        // 다음 자식 오브젝트의 인덱스를 계산합니다.
        currentIndex = (currentIndex + 1) % childObjects.Length;
        // 다음 자식 오브젝트를 활성화합니다.
        childObjects[currentIndex].SetActive(true);
    }
    public void OnBackButtonClick()
    {
        SoundManager.Instance.ClickSoundPlay();
        // 현재 자식 오브젝트를 비활성화합니다.
        childObjects[currentIndex].SetActive(false);
        // 이전 자식 오브젝트의 인덱스를 계산합니다.
        currentIndex = (currentIndex - 1 + childObjects.Length) % childObjects.Length;
        // 이전 자식 오브젝트를 활성화합니다.
        childObjects[currentIndex].SetActive(true);
    }
}

