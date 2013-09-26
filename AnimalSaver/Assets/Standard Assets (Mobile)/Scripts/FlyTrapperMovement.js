#pragma strict

var direction : int ;
var level : int;
var speedFactor:float;
var start = 0 ;
var elapsed = 0;
var countdown:int;
function Start () {
	speedFactor=0.1;
	start = Time.time;
	for(var i=1;i<level;i++)
		{
			speedFactor = speedFactor*1.5;
	}	
}

function Update() {
		// Move the object to the right relative to the camera 1 unit/second.
		elapsed = Time.time - start;
		if(elapsed==countdown)
		{
			transform.Translate(Vector3.back * (0.5), Camera.main.transform);
		}
}