using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Moves the baloon and destroys it when needed;
/// </summary>
public class BaloonMoveDestroy : MonoBehaviour, IPointerDownHandler  {

	RectTransform rect;
	public float speed = 1;
	public int size = 10;
	float yEdge;
	GameManager gm;
	
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		gameObject.SetActive(false);
	}
	
	void Start () {
		rect = GetComponent<RectTransform>();
		//where bubble will die
		yEdge = -Screen.height/2 + size/2;
	}
	
	void LateUpdate () {
		//moving the bubble
		rect.localPosition += new Vector3(0,-speed*Time.smoothDeltaTime,0);
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
		Destroy(gameObject);
	}
}
