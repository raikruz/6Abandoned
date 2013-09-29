#pragma strict
var bumpMap : Texture;
var bumpMap2 : Texture;
var bumpMap3 : Texture;
private var Cow_die = false;

private var startPos:Vector3;
private var elapsed=0;
private var t:float; 
function Start () 
{
	// initialize variables 
	gameObject.rigidbody.isKinematic = false;
	// initialize the velocity and start position 
	//var screenHeight:float   =   Screen.height;screenHeight*2;
	var randomValue:float = Random.value;
	if(randomValue<0.5)
	{
	    // from left to right
 		gameObject.rigidbody.velocity = new Vector3(50,0,0);
 		gameObject.rigidbody.position = new Vector3(0,randomValue,0); 
	}
 	else 
 	{
 		gameObject.rigidbody.velocity = new Vector3(-50,0,0);
 		gameObject.rigidbody.position = new Vector3(1,randomValue,0);
	}
 		
	//startPos.x = 0.5;
	//startPos.y = 0.87;
	//startPos.z = 0;

	// give random initilial direction (left or right)
	bumpMap = Resources.Load("bee_anim_1") as Texture;
 	bumpMap2  = Resources.Load("bee_anim_2") as Texture;
 	bumpMap3  = Resources.Load("bee_anim_3") as Texture;
}

function Update () 
{ 
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