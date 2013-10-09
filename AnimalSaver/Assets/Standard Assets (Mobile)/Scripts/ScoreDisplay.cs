using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.alignment = GUI.skin.label.alignment;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = Color.yellow;
		OTSprite sprite = GetComponent<OTSprite>();
		GUI.Label (new Rect (0, 10, sprite.size.x - 8, 20), Main.animalSaved.ToString(), style);	
	}
}
