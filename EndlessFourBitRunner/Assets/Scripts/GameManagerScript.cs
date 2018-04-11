using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance ;
	public Vector3 velocity = Vector3.zero ;
	public float bgSpeedModifier = 1.0f ;
	public float obstacleSpeedModifier = 1.3f ;
	public Vector3 targetPos ;
	public Text spdText ;

	private float maxTarget = -100.0f ;

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

	void Start()
	{
		targetPos = new Vector3 (0.0f, maxTarget, 0.0f) ;
	}

	public void SpeedChange(bool incSpeed) 
	{
		if (incSpeed)
			bgSpeedModifier += 1 ;
		else
			bgSpeedModifier -= 1 ;
	}

	void Update()
	{
		spdText.text = "Spd: " + bgSpeedModifier ;	
	}

}
