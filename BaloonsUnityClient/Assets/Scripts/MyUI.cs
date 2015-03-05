using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyUI : MonoBehaviour {

	[SerializeField] Text scoresText;
	
	
	public void UpdateScores(int scores)
	{
		scoresText.text = scores.ToString();
	}
}
