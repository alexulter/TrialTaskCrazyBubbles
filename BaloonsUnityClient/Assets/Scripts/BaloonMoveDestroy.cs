using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Moves the baloon and destroys it when needed;
/// </summary>
public class BaloonMoveDestroy : MonoBehaviour, IPointerDownHandler  {

	public float speed = 1;
	public int size = 10;
	float yEdge;
	GameManager gm;
	
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		//gameObject.SetActive(false);
	}
	
	void Start () {
		//where bubble will die
		yEdge = -Camera.main.orthographicSize;//+ size*Camera.main.orthographicSize/2/Screen.height;
	}
	
	void LateUpdate () {
		//moving the bubble
		transform.Translate(new Vector3(0,-speed*Time.smoothDeltaTime,0));
		if (transform.position.y < yEdge) Remove();
	}
	
	//Actions on click on the bubble
	public void OnPointerDown(PointerEventData data)
	{
		gm.IncreaseScores(size);
		Remove();
	}
	
	public void Remove()
	{
		gm.RemoveBubble(gameObject);
	}
}
