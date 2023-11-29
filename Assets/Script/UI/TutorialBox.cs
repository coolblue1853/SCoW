using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class TutorialBox : MonoBehaviour
{
    public GameObject[] childObjects; // �ڽ� ������Ʈ���� �迭�� �����մϴ�.
    private int currentIndex = 0; // ���� �ڽ� ������Ʈ�� �ε����� �����մϴ�.

    private void Start()
    {
        // ��� �ڽ� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        foreach (GameObject childObject in childObjects)
        {
            childObject.SetActive(false);
        }
        // ù ��° �ڽ� ������Ʈ�� Ȱ��ȭ�մϴ�.
        childObjects[currentIndex].SetActive(true);
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.ClickSoundPlay();
        // ���� �ڽ� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        childObjects[currentIndex].SetActive(false);
        // ���� �ڽ� ������Ʈ�� �ε����� ����մϴ�.
        currentIndex = (currentIndex + 1) % childObjects.Length;
        // ���� �ڽ� ������Ʈ�� Ȱ��ȭ�մϴ�.
        childObjects[currentIndex].SetActive(true);
    }
    public void OnBackButtonClick()
    {
        SoundManager.Instance.ClickSoundPlay();
        // ���� �ڽ� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        childObjects[currentIndex].SetActive(false);
        // ���� �ڽ� ������Ʈ�� �ε����� ����մϴ�.
        currentIndex = (currentIndex - 1 + childObjects.Length) % childObjects.Length;
        // ���� �ڽ� ������Ʈ�� Ȱ��ȭ�մϴ�.
        childObjects[currentIndex].SetActive(true);
    }
}

