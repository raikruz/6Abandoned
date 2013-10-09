using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	OTSprite sprite;
	void Start () {
		sprite = GetComponent<OTSprite>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.alignment = GUI.skin.label.alignment;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = Color.yellow;
		style.fontSize = (int)(sprite.size.y / 4);
		GUI.Label (new Rect (0, 0, sprite.size.x * 0.97f, sprite.size.y * 0.8f), Main.animalSaved.ToString(), style);	
	}
}
