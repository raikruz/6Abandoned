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
		renderer.material.SetTexture("_MainTex", bumpMap);
	}
	else if(elapsed%3==1)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap2);
	}else if(elapsed%3==2)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap3);
	}
}


