#pragma strict

var level =1;
var speedFactor:float;
var start = 0 ;
var elapsed = 0;
var bee:OTSprite;  
var origin : Vector3;

function Start () {
	speedFactor=0.1;
	start = Time.time;
	for(var i=1;i<level;i++)
	{
			speedFactor = speedFactor*1.5;
	}	
	origin=bee.position;
}

function Update() {
		// Move the object to the right relative to the camera 1 unit/second.
		elapsed = Time.time - start;
		bee = GetComponent(OTSprite);
		bee.position.x--;

}