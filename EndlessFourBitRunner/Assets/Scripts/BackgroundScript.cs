using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	public float backGroundSize = 14.7f ;

	private int bgIndex1 = 0 ;
	private int bgIndex2 = 1 ;
	private float maxTarget = -1000.0f ;
	private Transform[] layers ;
	private Vector3 targetPos ;
	private Vector3 velocity = Vector3.zero ;


	void Start()
	{
		targetPos = new Vector3 (0.0f, maxTarget, 0.0f) ;
		layers = new Transform[transform.childCount] ;

		for (int i = 0; i < transform.childCount; i++)
			layers [i] = transform.GetChild (i) ;

	}
		
	void Update()
	{
		if (!IsSpriteOnScreen (layers [bgIndex1].position) || !IsSpriteOnScreen (layers [bgIndex2].position))
			RepeatBG () ;
		
		StartCoroutine (Scroll (targetPos, GameManagerScript.instance.scrollSpeed)) ;
	}

	private IEnumerator Scroll(Vector3 target, float speed)
	{
		//transform.localPosition = Vector3.MoveTowards (transform.localPosition, target, Time.smoothDeltaTime * speed) ;
		transform.localPosition = Vector3.SmoothDamp (transform.localPosition, target, ref velocity, 0.4f, 1.0f, Time.deltaTime * GameManagerScript.instance.scrollSpeed) ;
		yield return null ;
	}

	private bool IsSpriteOnScreen(Vector3 spriteTransform)
	{
		bool onScreen = true ;
		Vector2 camPosition = Camera.main.WorldToViewportPoint (spriteTransform) ;
		if (camPosition.y < -1.2f)
			onScreen = false ;

		return onScreen ;
	}

	private void RepeatBG()
	{
		if (layers[bgIndex1].position.y < layers[bgIndex2].position.y) 
			layers [bgIndex1].position = new Vector3 (0.0f, layers [bgIndex2].position.y + backGroundSize, 0.0f);
		else
			layers [bgIndex2].position = new Vector3 (0.0f, layers [bgIndex1].position.y + backGroundSize, 0.0f);
	}

}
