using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
	Animator anime;
	float chInRommSize = 2.383215f;
	float runPower = 14f;
	Rigidbody2D rigid;
	Transform trans;
	// Use this for initialization
	void Start()
	{
		trans = gameObject.GetComponent<Transform>();
		rigid = gameObject.GetComponent<Rigidbody2D>();
		anime = gameObject.GetComponent<Animator>();
	}
	public void CheckChangeCoat()
    {
		anime.SetBool("Coat", false);
	}
	public void CheckChangeNomal()
	{
		anime.SetBool("Nomal", false);
	}
	bool coatCheckOn = false;
	bool coatCheckOff = false;
	// Update is called once per frame
	private void Update()
	{
		if (DataBaseManager.WearCoat == true && coatCheckOff == false)
		{
			coatCheckOn = false;
			coatCheckOff = true;
			anime.SetBool("IsChange", true);
			anime.SetBool("Coat", true);
		}
		if (DataBaseManager.WearCoat == false && coatCheckOn == false)
        {
			coatCheckOn = true;
			coatCheckOff = false;
			anime.SetBool("Nomal", true);
			anime.SetBool("IsChange", false);
		}
			if (DataBaseManager.isActiveDialog1 == false && DataBaseManager.StoryDirecting == false && DataBaseManager.isRollet == false && DataBaseManager.isDirecting == false && DataBaseManager.isDirecting == false && DataBaseManager.isOpenUi == false )
		{
			anime.SetBool("Dialog", false);
			RunAndWalkChanger();
			Move();
		}
        else
        {
			anime.SetBool("Dialog", true);
		}
		if(DataBaseManager.Helplessness == true && runPower == 14f)
        {
			runPower = 9.8f;
        }
	}
	void RunAndWalkChanger()
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
		if (Input.GetAxisRaw("Horizontal") < 0 )
		{
			DataBaseManager.workSound = true;
			moveVelocity = Vector3.left;

			transform.localScale = new Vector3(-chInRommSize, chInRommSize, 1);
			//X값 스케일을 -1로 주어 좌우반전
		}
		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			DataBaseManager.workSound = true;
			moveVelocity = Vector3.right;

			transform.localScale = new Vector3(chInRommSize, chInRommSize, 1);
			//X값 스케일을 1로 주어 다시 원위치 
		}
        else
        {
			DataBaseManager.workSound = false;
		}
		transform.position += moveVelocity * runPower * Time.deltaTime;
	}
}