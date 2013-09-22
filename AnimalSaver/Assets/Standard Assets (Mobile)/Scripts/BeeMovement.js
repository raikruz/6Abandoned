#pragma strict

var direction : int ;

function Start () {

}

function Update() {
		// Move the object to the right relative to the camera 1 unit/second.
		if(direction==0)
		{
			transform.Translate(Vector3.right * (Time.deltaTime*0.1), Camera.main.transform);
		}
		if(direction==1)
		{
			transform.Translate(Vector3.left * (Time.deltaTime*0.1), Camera.main.transform);
		}
}