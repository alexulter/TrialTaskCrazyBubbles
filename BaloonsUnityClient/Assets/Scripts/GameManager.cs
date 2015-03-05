using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int baloonMinSize = 10;
	public int baloonMaxSize = 100;
	public float baloonPutPeriod = 0.3f;//0.3f;
	public float baloonMaxSpeed = 120;
	public float baloonMinSpeed = 40;
	public int cacheInitialSize = 20;
	public GameObject baloonPref;
	List<GameObject> activeBubbles = new List<GameObject>();
	List<GameObject> cachedBubbles = new List<GameObject>();
	
	Canvas stageCanvas;
	int scores = 0;
	MyUI UI;
	
	IEnumerator Start () {
		UI = FindObjectOfType<MyUI>();
		UI.UpdateScores(scores);
		for (int i = 0; i < cacheInitialSize; i++)
		{
			GetNewBaloon();
			yield return null;
		}
		
		while (true)
		{
			StartCoroutine(PutBaloon());
			if (cachedBubbles.Count < 2) 
			{
				GetNewBaloon();
			}
			yield return new WaitForSeconds(baloonPutPeriod);
		}
		

		
	}
	
	void GetNewBaloon()
	{
		GameObject go = (GameObject)Instantiate(baloonPref);
		go.SetActive(false);
		go.transform.SetParent(transform);
		cachedBubbles.Add(go);
	}
	
	/// <summary>
	/// Increases the scores, depending on the size of the bubble.
	/// </summary>
	/// <param name="size">Size of bubble</param>
	public void IncreaseScores(int size)
	{	
		scores += (baloonMaxSize-size)*(baloonMaxSize-size)/10 + 1;
		UI.UpdateScores(scores);
	}
	
	/// <summary>
	/// Generates a bubble with random parameters.
	/// </summary>
	/// <returns>A new bubble.</returns>	
	IEnumerator PutBaloon()
	{
		var go = cachedBubbles[0];
		cachedBubbles.Remove(go);
		activeBubbles.Add(go);
		var spr = go.GetComponent<SpriteRenderer>();
		var move = go.GetComponent<BaloonMoveDestroy>();
		yield return null;
		
		
		//Create GameObject and add all components

		
		//Set color
		spr.color = RandomColor();
		yield return null;
		//Set size and starting position
		//var size = Random.Range(baloonMinSize,baloonMaxSize);
		var size = 0;
		float y = Camera.main.orthographicSize - size/2;
		var width = Camera.main.orthographicSize*Camera.main.aspect;
		float x = Random.Range(-width+size/2,width-size/2);
		go.transform.localPosition = new Vector3(x,y,0);
		//transform.localScale = new Vector3(size / spr.bounds.size.x, size / spr.bounds.size.y, 1);
		yield return null;
		//Set speed and give information about the size to baloon's script
		move.speed = 1;//baloonMaxSpeed*(baloonMaxSize - size)/(baloonMaxSize-baloonMinSize) + baloonMinSpeed;
		move.size = size;
		go.SetActive(true);
		yield break;
	}
	
	/// <summary>
	/// Randoms the color.
	/// </summary>
	/// <returns>The color.</returns>
	Color RandomColor()
	{	
		return new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
	}
	
	public void RemoveBubble(GameObject go)
	{
		go.SetActive(false);
		activeBubbles.Remove(go);
		cachedBubbles.Add (go);
	}
}
