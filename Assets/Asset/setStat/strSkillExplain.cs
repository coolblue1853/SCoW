using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class strSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void martialarts_expl()
    {
        nameEX.text = "< ������ >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �Ǽ� ���� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �������� �����մϴ�.";
    }
    public void Gunshot_expl()
    {
        nameEX.text = "< ��ݼ� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �ѱ⸦ �ٷ�� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ������ ��Ȯ���� �������ϴ�..";
    }
    public void sword_expl()
    {
        nameEX.text = "< �˼� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ���˷��� �ٷ�� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ������ ����� �������� �����մϴ�.";
    }
    public void Listening_expl()
    {
        nameEX.text = "< ��� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� û���� �������� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ����, �ǽ����� ���ϴ� �Ҹ��� �� ���� �� �ֽ��ϴ�.";
    }
    public void Swiming_expl()
    {
        nameEX.text = "< ���� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �����ɷ�ġ�� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ���� ����� �� ������ �� �ӿ��� �ս��� �����Դϴ�.";
    }



}
