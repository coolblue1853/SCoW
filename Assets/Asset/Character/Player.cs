using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
	Animator anime;

	float ChInRommSize = 2.383215f;

	public float runPower = 6f;
	Rigidbody2D rigid;
	Transform trans;




	// Use this for initialization
	void Start()
	{
		trans = gameObject.GetComponent<Transform>();
		rigid = gameObject.GetComponent<Rigidbody2D>();
		anime = gameObject.GetComponent<Animator>();


	}

    // Update is called once per frame
    private void Update()
	{
        if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.isRollet == false)
		{
			anime.SetBool("Dialog", false);
			runAndWalkChanger();
			Move();

		}
        else
        {
			anime.SetBool("Dialog", true);
		}
	}


	void runAndWalkChanger()
    {
		if(Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
		{
			anime.SetFloat("move", 1);
        }

		else if (Input.GetAxisRaw("Horizontal") == 0)
		{
			anime.SetFloat("move", 0);
		}
	}

	void Move()
	{
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetKey(KeyCode.Z))
		{
			moveVelocity = Vector3.left;
			transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
			//X값 스케일을 -1로 주어 좌우반전
		}
		else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetKey(KeyCode.Z) )
		{
			moveVelocity = Vector3.right;
			transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
			//X값 스케일을 1로 주어 다시 원위치 
		}

		if (Input.GetAxisRaw("Horizontal") < 0 )
		{

			moveVelocity = Vector3.left;

			transform.localScale = new Vector3(-ChInRommSize, ChInRommSize, 1);
			//X값 스케일을 -1로 주어 좌우반전
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			moveVelocity = Vector3.right;

			transform.localScale = new Vector3(ChInRommSize, ChInRommSize, 1);
			//X값 스케일을 1로 주어 다시 원위치 
		}


		transform.position += moveVelocity * runPower * Time.deltaTime;

	}





}