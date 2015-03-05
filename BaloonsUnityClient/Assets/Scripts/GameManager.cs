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
	[SerializeField] Canvas stageCanvas;
	List<GameObject> cachedBubbles = new List<GameObject>();
	
	int scores = 0;
	MyUI UI;
	
	IEnumerator Start () {
		UI = FindObjectOfType<MyUI>();
		UI.UpdateScores(scores);
		for (int i = 0; i < cacheInitialSize; i++)
		{
			CreateNewBubble();
		}
		while (true)
		{
		StartCoroutine(PutBaloon());
		if (cachedBubbles.Count < 2)
		{
			CreateNewBubble();
		}
		yield return new WaitForSeconds(baloonPutPeriod);
		}
	}
	
	void CreateNewBubble()
	{
		GameObject go = new GameObject("baloon");
		go.transform.SetParent(stageCanvas.transform);
		var spr = go.AddComponent<Image>();
		var move = go.AddComponent<BaloonMoveDestroy>();
		spr.sprite = Resources.Load<Sprite>("baloon");
		go.SetActive(false);
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
		//Create GameObject and add all components
		if (cachedBubbles.Count == 0) yield break;
		var go = cachedBubbles[0];
		cachedBubbles.Remove(go);
		var spr = go.GetComponent<Image>();
		var move = go.GetComponent<BaloonMoveDestroy>();
		RectTransform rect = go.GetComponent<RectTransform>();
		yield return null;
		//Set color
		spr.color = RandomColor();
		yield return null;
		//Set size and starting position
		var size = Random.Range(baloonMinSize,baloonMaxSize);
		float y = Screen.height/2 - size/2;
		float x = Random.Range(-Screen.width/2+size/2,Screen.width/2-size/2);
		rect.localPosition = new Vector3(x,y,0);
		rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,size);
		rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size);
		yield return null;
		//Set speed and give information about the size to baloon's script
		move.speed = baloonMaxSpeed*(baloonMaxSize - size)/(baloonMaxSize-baloonMinSize) + baloonMinSpeed;
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
		cachedBubbles.Add(go);
	}
}
