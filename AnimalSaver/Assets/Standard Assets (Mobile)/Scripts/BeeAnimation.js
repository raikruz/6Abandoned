#pragma strict

var bumpMap : Texture;
var bumpMap2 : Texture;
var bumpMap3 : Texture;
var start= 0;
var elapsed=0;
function Start () {
	start=Time.time;
}

function Update () {
	elapsed++;
	if(elapsed%3==0)
	{
		 gameObject.guiTexture.texture = bumpMap; 
	}
	else if(elapsed%3==1)
	{	
		 gameObject.guiTexture.texture = bumpMap2; 
	}else if(elapsed%3==2)
	{	
		 gameObject.guiTexture.texture = bumpMap3; 
	}
}


