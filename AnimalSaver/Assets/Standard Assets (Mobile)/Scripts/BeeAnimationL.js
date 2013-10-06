#pragma strict

var bumpMap : Texture;
var bumpMap2 : Texture;
var bumpMap3 : Texture;
var start= 0;
var elapsedAnimation=0;
var bee:OTSprite;  
function Start () {
	start=Time.time;
}

function Update () {
	elapsedAnimation++;
	bee = GetComponent(OTSprite);
	
		if(elapsedAnimation%3==0)
		{
			 bee.frameIndex = 1;
		}
		else if(elapsedAnimation%3==1)
		{	
			bee.frameIndex = 2;
		}else if(elapsedAnimation%3==2)
		{	
			bee.frameIndex = 0;
		}


}