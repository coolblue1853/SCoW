using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class statExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void str_expl()
    {
        nameEX.text = "< �� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �ٷ��� ��Ÿ���ϴ�. �� �ɷ�ġ�� �������� �� ���� ���� �� �� ������ ���ſ� ������ �� �� �ֽ��ϴ�.    �� ��� ���ġ�� �ش� ���ġ�� ��ġ�� ������� �մϴ�.";
    }

    public void rest_expl ()
    {
        nameEX.text = "< - >";
        mainEX.text = "";
    }
}
