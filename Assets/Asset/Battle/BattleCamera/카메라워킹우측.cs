using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 카메라워킹우측 : MonoBehaviour
{

    public float moveSpeed;
    public float destroyTime;

    public Camera 카메라;

    public GameObject 오브젝트;
    Vector3 초기위치;
    private Vector3 vector;




    // Start is called before the first frame update
    void Start()
    {

        초기위치 = 오브젝트.transform.position;

    }









    public GameObject 일의자리몸통;
    public GameObject 룰렛머신온오프;




    int 랜덤일의자리;

    float 흔드는범위 = 0.03f;
    float 흔드는큰범위 = 0.06f;
    IEnumerator 카메라진동()
    {
        for (int i = 0; i < 20; i++)
        {

            랜덤일의자리 = Random.Range(1, 9);





            if (랜덤일의자리 == 1)
            {
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는범위, 일의자리몸통.transform.position.y - 흔드는범위, -341.134f);

                yield return new WaitForSeconds(0.025f);

                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는범위, 일의자리몸통.transform.position.y + 흔드는범위, -341.134f);

            }
            if (랜덤일의자리 == 2)
            {
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는범위, 일의자리몸통.transform.position.y + 흔드는범위, -341.134f);

                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는범위, 일의자리몸통.transform.position.y - 흔드는범위, -341.134f);

            }
            if (랜덤일의자리 == 3)
            {
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는큰범위, 일의자리몸통.transform.position.y - 흔드는범위, -341.134f);

                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는큰범위, 일의자리몸통.transform.position.y + 흔드는범위, -341.134f);

            }
            if (랜덤일의자리 == 4)
            {

                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는큰범위, 일의자리몸통.transform.position.y + 흔드는범위, -341.134f);
                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는큰범위, 일의자리몸통.transform.position.y - 흔드는범위, -341.134f);

            }
            if (랜덤일의자리 == 5)
            {

                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x, 일의자리몸통.transform.position.y + 흔드는큰범위,  -341.134f);
                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x, 일의자리몸통.transform.position.y - 흔드는큰범위,  -341.134f);

            }
            if (랜덤일의자리 == 6)
            {

                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x, 일의자리몸통.transform.position.y - 흔드는큰범위, -341.134f);
                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x, 일의자리몸통.transform.position.y + 흔드는큰범위, -341.134f);

            }
            if (랜덤일의자리 == 7)
            {

                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는큰범위, 일의자리몸통.transform.position.y, -341.134f);
                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는큰범위, 일의자리몸통.transform.position.y, -341.134f);

            }
            if (랜덤일의자리 == 8)
            {
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x + 흔드는큰범위, 일의자리몸통.transform.position.y, -341.134f);

                yield return new WaitForSeconds(0.025f);
                일의자리몸통.transform.position = new Vector3(일의자리몸통.transform.position.x - 흔드는큰범위, 일의자리몸통.transform.position.y, -341.134f);

            }







        }
    }


    float 카메라사이즈 = 5;
    public float 돌아가는각도 = 0;
    IEnumerator 카메라연출()
    {



        while (카메라.orthographicSize > 4.5)
        {
            카메라사이즈 = 카메라사이즈- 0.05f;
            돌아가는각도 = 돌아가는각도 + 0.5f;

            오브젝트.transform.rotation = Quaternion.Euler(0, 0, 돌아가는각도);
            카메라.orthographicSize = 카메라사이즈;
            yield return new WaitForSeconds(0.01f);

        }


        //카메라.orthographicSize = 4;
    }

    IEnumerator 카메라연출되돌리기()
    {



        while (카메라.orthographicSize < 5)
        {
            카메라사이즈 = 카메라사이즈 + 0.05f;
            돌아가는각도 = 돌아가는각도 - 0.5f;

            오브젝트.transform.rotation = Quaternion.Euler(0, 0, 돌아가는각도);
            카메라.orthographicSize = 카메라사이즈;
            yield return new WaitForSeconds(0.01f);

        }

        오브젝트.transform.rotation = Quaternion.Euler(0, 0, 0);
        카메라.orthographicSize = 5;

        //카메라.orthographicSize = 4;
    }





    IEnumerator 카메라연출적()
    {



        while (카메라.orthographicSize > 4.5)
        {
            카메라사이즈 = 카메라사이즈 - 0.05f;
            돌아가는각도 = 돌아가는각도 - 0.5f;

            오브젝트.transform.rotation = Quaternion.Euler(0, 0, 돌아가는각도);
            카메라.orthographicSize = 카메라사이즈;
            yield return new WaitForSeconds(0.01f);

        }


        //카메라.orthographicSize = 4;
    }

    IEnumerator 카메라연출적되돌리기()
    {



        while (카메라.orthographicSize < 5)
        {
            카메라사이즈 = 카메라사이즈 + 0.05f;
            돌아가는각도 = 돌아가는각도 + 0.5f;

            오브젝트.transform.rotation = Quaternion.Euler(0, 0, 돌아가는각도);
            카메라.orthographicSize = 카메라사이즈;
            yield return new WaitForSeconds(0.01f);

        }


        오브젝트.transform.rotation = Quaternion.Euler(0, 0, 0);
        카메라.orthographicSize = 5;


        //카메라.orthographicSize = 4;
    }











    bool 카메라연출을했는가 = false;
    // Update is called once per frame
    void Update()
    {








        if(DataBaseManager.카메라워킹온오프 == true)
        {


            if (카메라연출을했는가 == false)
            {
                //StartCoroutine(카메라진동());

                카메라연출을했는가 = true;
                StartCoroutine(카메라연출());
            }

            vector.Set(오브젝트.transform.position.x + (moveSpeed * Time.deltaTime), 오브젝트.transform.position.y, 오브젝트.transform.position.z);
            오브젝트.transform.position = vector;

            destroyTime -= Time.deltaTime;

            if (destroyTime <= 0)
            {
                StartCoroutine(카메라연출되돌리기());
                카메라연출을했는가 = false;
                destroyTime = 3;
                오브젝트.transform.position = 초기위치;
                DataBaseManager.카메라워킹온오프 = false;

            }
        }

        if (DataBaseManager.카메라왼쪽워킹온오프 == true)
        {
            if (카메라연출을했는가 == false)
            {
                StartCoroutine(카메라진동());
                카메라연출을했는가 = true;
                StartCoroutine(카메라연출적());
            }
            vector.Set(오브젝트.transform.position.x - (moveSpeed * Time.deltaTime), 오브젝트.transform.position.y, 오브젝트.transform.position.z);
            오브젝트.transform.position = vector;

            destroyTime -= Time.deltaTime;

            if (destroyTime <= 0)
            {
                StartCoroutine(카메라연출적되돌리기());
                카메라연출을했는가 = false;
                destroyTime = 3;
                오브젝트.transform.position = 초기위치;
                DataBaseManager.카메라왼쪽워킹온오프 = false;

            }
        }



    }



}
