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
		renderer.material.SetTexture("_MainTex", bumpMap);
	}
	else if(elapsed==5)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap2);
	}else if(elapsed==10)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap3);
	}
	else if(elapsed==15)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap4);
	}else if(elapsed==20)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap5);
	}	else if(elapsed==25)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap6);

	}
}


