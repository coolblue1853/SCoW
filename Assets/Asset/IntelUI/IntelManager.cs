using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntelManager : MonoBehaviour
{

    public GameObject Character_B;
    public GameObject Event_B;
    public GameObject Place_B;

    public GameObject CharacterWindow;
    public GameObject EventWindow;
    public GameObject PlaceWindow;

    public GameObject UI;
    public GameObject IntelWindow;
    public GameObject FloatingTextObject;
    public GameObject FloatingPlace;

    public void AddIntelFloating(string Category, string tag, string detail)
    {
        if( tag == "행성대직렬")
        {
            //DataBaseManager.행성대직렬알리미 = true;
        }

        if(Category == "Event")
        {
            var clone = Instantiate(FloatingTextObject, FloatingPlace.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<IntelFloating>().Text.text = "<color=#B30000>("+ detail + ")</color>정보 획득".ToString();
            clone.transform.SetParent(UI.transform);
            clone.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    /*
    //사건
    public void 행성대직렬정보추가()
    {
        DataBaseManager.행성대직렬알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(행성대직렬 신문기사) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 불면증정보추가()
    {
        DataBaseManager.불면증알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(불면증 유행) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 불면증정보추가2()
    {
        DataBaseManager.불면증알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(병원의 불면증 환자) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 불면증정보추가3()
    {
        DataBaseManager.불면증알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(불면증의 원인) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 악몽정보추가()
    {
        DataBaseManager.악몽알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(에이든의 악몽) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 악몽정보추가2()
    {
        DataBaseManager.악몽알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(악몽의 내용) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 편집증정보추가()
    {
        DataBaseManager.편집증알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(편집증 유행) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 편집증정보추가2()
    {
        DataBaseManager.편집증알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(편집증의 원인) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }


    public void 실종사건추가()
    {

        DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(연속 실종 사건) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 실종사건추가2()
    {

        DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(취재 - 연속 실종) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 하수도의괴담추가()
    {

        //DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(에이든이 취재하던 것) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 비릿한냄새추가1()
    {

        //DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(비릿한 냄새 소문) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 미스캐토닉강의수질추가1()
    {

        //DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(강의 특이점) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 섬뜩한눈추가1()
    {

        //DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(하수도의 섬뜩한 눈) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 하수도노동자정보1추가()
    {

        //DataBaseManager.실종사건알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#B30000>(하수도노동자의 부당해고) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    //인물
    public void 엘라정보추가()
    {
        DataBaseManager.엘라알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(엘라 트리스) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 에이든정보추가()
    {
        DataBaseManager.에이든알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(에이든 트리스) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 에이든정보추가2()
    {
        DataBaseManager.에이든알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(에이든의 상태) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 에이든정보추가3()
    {
        DataBaseManager.에이든알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(에이든의 발언) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }

    //동료기자
    public void 동료기자정보추가1()
    {
        DataBaseManager.에이든알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(스탠리 큐브릭) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 노교수정보추가1()
    {
        DataBaseManager.에이든알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#1A6800>(신이난 노교수) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }









    //장소
    public void 병원정보추가()
    {
        DataBaseManager.병원알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(병원과 의뢰자) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 하수정비사무소정보추가1()
    {
        DataBaseManager.병원알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(하수정비사무소의 위치) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 아캄역정보추가()
    {
        DataBaseManager.아캄역알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(아컴역) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 의뢰자의집정보추가()
    {
        DataBaseManager.의뢰자의집알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(의뢰자의 집) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }
    public void 신문사정보추가()
    {
        DataBaseManager.의뢰자의집알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(남편의 직장) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }

    public void 강가정보추가()
    {
        DataBaseManager.의뢰자의집알리미 = true;
        var clone = Instantiate(플로팅텍스트오브젝트, 적플로팅위치.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<정보플로팅>().Text.text = "<color=#0026A9>(강과 교수) </color>정보 획득".ToString();
        clone.transform.SetParent(ui창.transform);   
        clone.transform.localScale = new Vector3(1, 1, 1);
    }

    */
    public void QuitIntelWindow()
    {
        //DataBaseManager.옵션창여부 = false;
        IntelWindow.SetActive(false);
    }
    

    public GameObject EventWindow_ON;
    public GameObject CharacterWindow_ON;
    public GameObject PlaceWindow_ON;

    public void Open_Character()
    {
        CharacterWindow_ON.SetActive(true);
        EventWindow_ON.SetActive(false);
        PlaceWindow_ON.SetActive(false);

        CharacterWindow.SetActive(true);
        EventWindow.SetActive(false);
        PlaceWindow.SetActive(false);
    }

    public void Open_Event()
    {
        CharacterWindow_ON.SetActive(false);
        EventWindow_ON.SetActive(true);
        PlaceWindow_ON.SetActive(false);

        CharacterWindow.SetActive(false);
        EventWindow.SetActive(true);
        PlaceWindow.SetActive(false);
    }
    public void OpenPlace()
    {
        CharacterWindow_ON.SetActive(false);
        EventWindow_ON.SetActive(false);
        PlaceWindow_ON.SetActive(true);

        CharacterWindow.SetActive(false);
        EventWindow.SetActive(false);
        PlaceWindow.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

 
    }

    private static IntelManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static IntelManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
}
