#pragma strict
public var playbtn : Texture2D = null;
public var exitbtn : Texture2D = null;

function Start () {

}

function Update () {

}

function OnGUI() {
	if (GUI.Button(Rect(Screen.width / 2.2, Screen.height / 1.3 , 120, 50), "Play Again")) {
		Application.LoadLevel(1);
	}
	else if (GUI.Button(Rect(Screen.width / 2.2, Screen.height / 1.3 + 70, 120, 50), exitbtn)) {
		Application.LoadLevel(1);
	}
}