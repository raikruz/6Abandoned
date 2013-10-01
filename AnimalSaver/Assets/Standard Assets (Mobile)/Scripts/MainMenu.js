#pragma strict

function Start () {

}

function Update () {

}

function OnGUI() {
	if (GUI.Button(Rect(Screen.width / 2.2, Screen.height / 1.5 , 120, 50), "Play")) {
		Application.LoadLevel(1);
	}
	else if (GUI.Button(Rect(Screen.width / 2.2, Screen.height / 1.5 + 70, 120, 50), "Quit")) {
		Application.LoadLevel(1);
	}
}