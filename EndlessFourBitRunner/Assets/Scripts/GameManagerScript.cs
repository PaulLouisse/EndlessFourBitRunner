using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance ;
	public float scrollSpeed = 1 ;
	public Text spdText ;

	void Awake()
	{
		if (instance == null) 
		{
			instance = this ; 
		}
		else if (instance != this)
		{
			Destroy (gameObject) ;
		}
	}

	public void SpeedChange(bool incSpeed) 
	{
		if (incSpeed)
			scrollSpeed += 1 ;
		else
			scrollSpeed -= 1 ;
	}

	void Update()
	{
		spdText.text = "Spd: " + scrollSpeed;	
	}

}
