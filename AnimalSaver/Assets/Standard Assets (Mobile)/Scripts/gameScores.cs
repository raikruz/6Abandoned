using UnityEngine;
using System.Collections;

public class gameScores : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.guiText.text=Main.animalSaved.ToString();
	}
}
