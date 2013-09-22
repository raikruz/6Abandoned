#pragma strict

var bumpMap : Texture;
var bumpMap2 : Texture;
var bumpMap3 : Texture;
var image : int;
function Start () {
}

function Update () {
	if(image==0)
	{
		renderer.material.SetTexture("_MainTex", bumpMap);
		UpdateImageDelay();
	}
	else if(image==1)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap2);
		UpdateImageDelay();
	}else if(image==2)
	{	
		renderer.material.SetTexture("_MainTex", bumpMap3);
		UpdateImageDelay();
	}

}

function UpdateImageDelay () {
	yield WaitForSeconds(2);
	if(image==0)
	{
		image=1;
	}
	else if(image==1)
	{
		image=2;
	}else if(image==2)
	{
		image=0;
	}
}
