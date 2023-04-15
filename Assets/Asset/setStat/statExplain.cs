using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class statExplain : MonoBehaviour
{
    public Text nameEX;
    public Text mainEX;
    // Start is called before the first frame update


    public void str_expl()
    {
        nameEX.text = "< �� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �ٷ��� ��Ÿ���ϴ�. �� �ɷ�ġ�� �������� �� ���� ���� �� �� ������ ���ſ� ������ �� �� �ֽ��ϴ�. �� ��� ���ġ�� �ش� ���ġ�� ��ġ�� ������� �մϴ�.";
    }
    public void int_expl()
    {
        nameEX.text = "< ���� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� �����ɷ��� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �� ���� ���� �޾�����, ������ ���� ���ص��� �����ϴ�. ���� ��� ���ġ�� �ش� ���ġ�� ��ġ�� ������� �մϴ�.";
    }
    public void dex_expl()
    {
        nameEX.text = "< ��ø >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ����� ������ ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �� �����ϰ� �����ϸ� �����ϴ�. ��ø ��� ���ġ�� �ش� ���ġ�� ��ġ�� ������� �մϴ�.";
    }

    //-------
    public void hp_expl()
    {
        nameEX.text = "< ü�� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ��ü�� �ǰ����� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �� ưư�ϸ� �ܺ��� ����, �Ƿηκ��� �� �� ��ƿ �� �ֽ��ϴ�. �� ��ġ�� 0�� �Ǹ� �÷��̾�� ����մϴ�.";
    }
    public void mp_expl()
    {
        nameEX.text = "< ���ŷ� >";
        mainEX.text = "�� ���ġ�� �÷��̾ �̼� ���Ҹ� ������ �� �ִ� ���ŷ��� ��Ÿ���ϴ�. �� �ɷ�ġ�� ���� ���� �� ���� ����� ��ƿ�� �ֽ��ϴ�."; 
    }
    public void san_expl()
    {
        nameEX.text = "< �̼� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ������ �ǰ����� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� �� ���� ���� ������ ����� ������ �� �ֽ��ϴ�. �� ��ġ�� 0�� �Ǹ� �÷��̾�� ����մϴ�.";

    }
    public void luck_expl()
    {
        nameEX.text = "< ��� >";
        mainEX.text = "�� ���ġ�� �÷��̾ �󸶳� ���� �ִ����� ��Ÿ���ϴ�.  �� �ɷ�ġ�� �������� ������ ���� ȸ���� Ȯ���� �þ�ϴ�.";
    }

    public void wealth_expl()
    {
        nameEX.text = "< ��� >";
        mainEX.text = "�� ���ġ�� �÷��̾��� ������ ��Ÿ���ϴ�.  �ش� �ɷ�ġ�� ����ŭ �޷�($)�� ������ �����մϴ�.";
    }




    public void rest_expl ()
    {
        nameEX.text = "< - >";
        mainEX.text = "";
    }
}
