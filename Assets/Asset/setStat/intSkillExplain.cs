using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class intSkillExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update
    public void Medicine_expl()
    {
        nameEX.text = "< �������� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ���������� ������ ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ��ó�� �м��Ͽ� ������ ã�Ƴ��ų�, �� ���� ������ ����� �� �ֽ��ϴ�. ���� �ش� ����� ����ؼ� �λ��� ġ���� �� �ֽ��ϴ�.";
    }
    public void analysis_expl()
    {
        nameEX.text = "< �ڷ�м� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ���� �м� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� å�̳� �Ź���� ����, ���� ������ ���� �� �ֽ��ϴ�.";
    }
    public void psychology_expl()
    {
        nameEX.text = "< �ɸ��Ǵ� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� Ÿ���� �ɸ����¸� �м��ϴ� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ������ ��������, �������� �ϰ��ִ��� �ƴ��� ��Ȯ�ϰ� �Ǵ��� �� �ֽ��ϴ�.";
    }
    public void psychotherapy_expl()
    {
        nameEX.text = "< ����ġ�� >";
        mainEX.text = "�� ���ġ�� �÷��̾ �ɸ�,���� ġ�� �ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ������ �ر��� �ִ� ����� ������ų �� �ֽ��ϴ�. �ش� ����� ����ؼ� �ڽ��� ���źر� ��ġ�� ���ҽ�ų �� �ֽ��ϴ�.";
    }
    public void occult_expl()
    {
        nameEX.text = "< ����Ʈ�� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ����Ʈ ������ ������ ��Ÿ���ϴ�. �� �ɷ�ġ�� �������� ��н����� ����, �ΰ� ����, Ȥ�� �ź��п� �����մϴ�.";
    }
}
