using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Moves the baloon and destroys it when needed;
/// </summary>
public class BaloonMoveDestroy : MonoBehaviour, IPointerDownHandler  {

	RectTransform rect;
	public float speed = 1;
	public int size = 10;
	GameManager gm;
	public GameObject particles;
	
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		gameObject.SetActive(false);
	}
	
	void Start () {
		rect = GetComponent<RectTransform>();
	}
	
	void LateUpdate () {
		//moving the bubble
		rect.localPosition += new Vector3(0,-speed*Time.smoothDeltaTime,0);
		//where bubble will die
		var yEdge = -Screen.height/2 + size/2;
		if (rect.localPosition.y < yEdge) Remove();	
	}
	
	//Actions on click on the bubble
	public void OnPointerDown(PointerEventData data)
	{
		gm.IncreaseScores(size);
		Remove();
	}
	
	void Remove()
	{
		gm.RemoveBubble(gameObject);
	}
}
