using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Animator playerAnimator ;
	private BoxCollider2D playerBoxCollider ;
	private bool isJumping = false ; 
	private float jumpDuration = 0.6f ;
	private float jumpTime ;
	private string lastObjectName = null ;

	// Use this for initialization
	void Start () {
		playerAnimator = GetComponent<Animator> () ;
		playerBoxCollider = GetComponent<BoxCollider2D> () ;
	}

	public void IsDead()
	{
		playerAnimator.SetTrigger ("deadTrigger") ;
	}

	public void PlayerJump(bool jump)
	{
		if (isJumping == true)
			return ;
		
		if (jump) 
		{
			playerAnimator.SetTrigger ("jumpTrigger");
			jumpTime = Time.time + jumpDuration;
		}

		isJumping = jump;
	}

	// Update is called once per frame
	void Update () {
		
		if (isJumping && Time.time > jumpTime) 
		{
			isJumping = false ;
			PlayerJump (false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Clouds") 
		{
			if (lastObjectName != other.gameObject.transform.parent.name) 
			{
				CloudControl.instance.ManualSpawn ();
				lastObjectName = other.gameObject.transform.parent.name;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Clouds") 
		{
			Debug.Log ("Gaining Points");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Clouds" && !isJumping) 
		{
			Debug.Log ("Dead") ;
		}
	}
}

//Work on cloud collision