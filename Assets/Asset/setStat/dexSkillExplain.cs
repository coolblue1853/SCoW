using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dexSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void evasion_expl()
    {
        nameEX.text = "< ȸ�� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ������ ȸ�� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ���� ���ݿ��� ���� ��� �� �ֽ��ϴ�. ���� ����� �ִ� �� �� ����� �����մϴ�.";
    }
    public void deftness_expl()
    {
        nameEX.text = "< ������ >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ���� �����, �������� ��Ÿ���ϴ� .  �� �ɷ�ġ�� �������� �ս��� ������ �𸣰� ������ ��ġ�ų� �Ҹ����� ���� ������ ����� �ൿ�� �����մϴ�.";
    }
    public void rhetoric_expl()
    {
        nameEX.text = "< �𺯼� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� Ÿ���� ���� �����ϴ� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ��ȭ�� ���� Ÿ���� �����ϱ� �������ϴ�.";
    }
    public void stealth_expl()
    {
        nameEX.text = "< ���ż� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �ൿ�� ������� ������ ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �ٸ� ����鿡�� ��Ű�� �ʰ� �̵� �� �� �ֽ��ϴ�.";
    }
    public void Disguise_expl()
    {
        nameEX.text = "< ����� >";
        mainEX.text = "�� ���ġ�� �÷��̾� ���� �ɷ��� ������ ��Ÿ���ϴ�. �� �ɷ�ġ�� �������� �� �ɼ��ϰ� Ÿ���� �ൿ�̳� ������ �䳻�� �� �ֽ��ϴ�.";
    }
}
