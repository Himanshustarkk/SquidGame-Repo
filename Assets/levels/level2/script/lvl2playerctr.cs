using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class lvl2playerctr : MonoBehaviour
{
	[Header(" Control Settings ")]
	public Rigidbody thisRigidbody;
	public SimpleJoystick joystick;
	public float moveSpeed;
	public float RotSpeed;
	public float maxX;
	public float maxZ;
	bool move;
	public static bool canMove;
	public bool GmRun, die, chwya, win, stay,kick;
	GameObject enemplayer;
	public Image img;
	// Start is called before the first frame update
	void Start()
	{

		// Store some values
		Application.targetFrameRate = 60;
		canMove = true;
		img = transform.GetChild(1).GetChild(1).gameObject.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		//joystick.
		if (GmRun && !die && !win)
		{
			if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
			{
				
				//if (FindObjectOfType<enemCtr>().animcor)
				//{
				//	die = true;
				//	//StartCoroutine(dieplayer());
				//}
				// Move Player
				move = true;
				stay = false;
				GetComponent<Animator>().Play("run");
				transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
				GetComponent<Animator>().speed = 1;
			}
			else
			{
				if(move)
                {
					GetComponent<Animator>().Play("idle1");
				}
				move = false;
				
				
				//GetComponent<Animator>().speed = 1;
			}

			if (enemplayer != null)
			{
				if (Vector3.Distance(transform.position, enemplayer.transform.position) <= 1.5f)
				{
					kick = true;
				}
				else
                {
					kick = false;
				}
			}

			if (img.fillAmount <= 0 && !die)
			{
				die = true;
				StartCoroutine(dieplayer());
				//Destroy(gameObject, 3f);
			}
		}

		if (FindObjectOfType<UiManager>().t <= 0 && !die && !win)
		{
			win = true;
			StartCoroutine(winplayer());
		}
		//if (die && !chwya)
		//{
		//	if (joystick.HorizintalAxis.Value != 0 || joystick.VerticalAxis.Value != 0)
		//	{
		//		// Move Player
		//		move = true;
		//		GetComponent<Animator>().Play("run");
		//		transform.forward = new Vector3(joystick.HorizintalAxis.Value * Time.deltaTime, 0, joystick.VerticalAxis.Value * Time.deltaTime);
		//		GetComponent<Animator>().speed = 1;
		//	}
		//	else
		//	{
		//		move = false;
		//		GetComponent<Animator>().speed = 0;
		//	}
		//}



	}

	private void FixedUpdate()
	{
		if (GmRun && !die && !win)
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
			pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
			transform.position = pos;

			if (canMove)
			{
				if (move)
					Move();
				else
					thisRigidbody.velocity = Vector3.zero;
			}
		}


		if (die && !chwya)
		{
			Vector3 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -maxX, maxX);
			pos.z = Mathf.Clamp(pos.z, -maxZ, maxZ);
			transform.position = pos;

			if (canMove)
			{
				if (move)
					Move();
				else
					thisRigidbody.velocity = Vector2.zero;


			}
		}
	}


	public void Move()
	{
		Vector3 movement = new Vector3(joystick.HorizintalAxis.Value, 0, joystick.VerticalAxis.Value);
		movement *= moveSpeed * Time.deltaTime;

		thisRigidbody.velocity = movement;
	}


	public void kicking()
    {
		if(!stay && !die && !win)
        {
			stay = true;
			StartCoroutine(keppkicking());
		}
		
    }

    IEnumerator keppkicking()
    {
        float a = 0;
        do
        {
            int bb = Random.Range(1, 4);
            GetComponent<Animator>().Play("kick" + bb.ToString());
            GetComponent<Animator>().speed = 2;
			if(kick && enemplayer!=null)
            {
				transform.LookAt(enemplayer.transform);
				enemplayer.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Image>().fillAmount -= 0.1f;
            }
            yield return new WaitForSeconds(0.6f);
        } while (stay && !win && !die);

    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player"  && !collision.gameObject.GetComponent<lvl2playerAi>().die)
		{
			enemplayer = collision.gameObject;
		}
	}

    private void OnCollisionStay(Collision collision)
    {
		if (collision.gameObject.tag == "Player" && !collision.gameObject.GetComponent<lvl2playerAi>().die)
		{
			enemplayer = collision.gameObject;
		}
	}
    


	IEnumerator winplayer()
	{

        //=========================================================================================================================================================== for Update GameScore and GG Coins

        GrandAdManager.isWinOrLoseLevel = "win";
        GrandAdManager.TotalGGCoinsEarned += 1;
		GrandAdManager.TotalScore += 100;
        APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);

        //=========================================================================================================================================================== for Update GameSc

        transform.eulerAngles = new Vector3(0, 180, 0);
		FindObjectOfType<UiManager>().wineffet.SetActive(true);
		GetComponent<Animator>().Play("win");
		GetComponent<Animator>().speed = 1;
		SoundManager.instance.Play("win");
		yield return new WaitForSeconds(5f);
	///	Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowInterstitial();
		FindObjectOfType<UiManager>().winpanel.SetActive(true);
		Debug.Log("Getting Levels Details" + gamemanager.instance.getLevel());
	

	}

	IEnumerator dieplayer()
	{



		//=========================================================================================================================================================== for Update GameScore and GG Coins

		GrandAdManager.isWinOrLoseLevel = "loss";
		APIManager.Instance.UpdateGameScore(GrandAdManager.TotalScore, GrandAdManager.isWinOrLoseLevel, gamemanager.instance.getLevel() + 1, GrandAdManager.TotalGGCoinsEarned);

		//============================================================================================================================================================ 




		win = true;
		GameObject gm = Instantiate(FindObjectOfType<UiManager>().bloodeefect, transform);
		gm.transform.localPosition = new Vector3(0, 1.3f, 0);
		int bb = Random.Range(1, 5);
		GetComponent<Animator>().Play("die" + bb.ToString());
		GetComponent<Animator>().speed = 1;
		SoundManager.instance.Play("lose");
		yield return new WaitForSeconds(7f);
		//Gley.MobileAds.Internal.MobileAdsExample.Instance.ShowRewardedVideo();
		FindObjectOfType<UiManager>().losepanel.SetActive(true);
	}
}
