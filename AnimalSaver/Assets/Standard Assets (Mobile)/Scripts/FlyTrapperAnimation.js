#pragma strict

var bumpMap : Texture;
var bumpMap2 : Texture;
var bumpMap3 : Texture;
var bumpMap4 : Texture;
var bumpMap5 : Texture;
var bumpMap6 : Texture;
var image : int;
var start= 0;
var elapsed=0;
function Start () {
	start=Time.time;
}

function Update () {
	elapsed= Time.time -start;
	if(elapsed==0)
	{
		 gameObject.guiTexture.texture = bumpMap; 
	}
	else if(elapsed==2)
	{	
		 gameObject.guiTexture.texture = bumpMap2; 
	}else if(elapsed==4)
	{	
		 gameObject.guiTexture.texture = bumpMap3; 
	}
	else if(elapsed==6)
	{	
		 gameObject.guiTexture.texture = bumpMap4; 
	}else if(elapsed==8)
	{	
		 gameObject.guiTexture.texture = bumpMap5; 
	}	else if(elapsed==12)
	{	
		 gameObject.guiTexture.texture = bumpMap6; 

	}
}


