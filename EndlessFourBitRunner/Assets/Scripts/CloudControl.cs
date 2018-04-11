using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControl : MonoBehaviour {

	public static CloudControl instance ;

	public List<Clouds> clouds ;

	private GameObject[] cloudsGO ;
	private float timedd ;
	private int lastIndex ;
	private int randNumber ;
	private bool firstSpawn = true ;

	[System.Serializable]
	public class Clouds
	{
		public string tag ;
		public GameObject cloudPrefab ;
		public float pixelSkips ;
	}

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
		cloudsGO = new GameObject [clouds.Count] ;
		int index = 0 ;
		foreach (Clouds c in clouds) 
		{
			cloudsGO[index] = Instantiate (c.cloudPrefab) ;
			cloudsGO[index].SetActive(false) ;
			index++ ;
		}

		Spawn () ;
	}

	private void Spawn()
	{
		while (cloudsGO[randNumber].activeSelf) 
		{
			randNumber = Random.Range (0, clouds.Count);
		}

		if (firstSpawn != true)
			cloudsGO [randNumber].transform.position = new Vector3 (0.0f, cloudsGO[lastIndex].transform.position.y + clouds[lastIndex].pixelSkips + 1.0f, 0.0f) ; 

		cloudsGO [randNumber].SetActive (true) ;
		lastIndex = randNumber ;
		firstSpawn = false ;
	}
		
	private IEnumerator ScrollClouds()
	{
		foreach (GameObject activeClouds in cloudsGO) 
		{
			if (activeClouds.activeSelf) 
			{
				activeClouds.transform.position = Vector3.SmoothDamp (activeClouds.transform.position, GameManagerScript.instance.targetPos, ref GameManagerScript.instance.velocity, 0.4f, 1.0f, Time.deltaTime * GameManagerScript.instance.obstacleSpeedModifier) ;
			}
			IsCloudOffscreen (activeClouds) ;
		}
		yield return null ;
	}

	private void IsCloudOffscreen(GameObject activeClouds)
	{
		Vector2 camPos = Camera.main.WorldToViewportPoint (activeClouds.transform.position) ;
		if (camPos.y < -0.7f)
		{
			activeClouds.SetActive (false) ;		
			activeClouds.transform.position = Vector3.zero ;
		}
	}
		
	void Update()
	{
		StartCoroutine (ScrollClouds ()) ;
	}

	public void ManualSpawn()
	{
		Spawn () ;
	}

}