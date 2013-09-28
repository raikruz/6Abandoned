#pragma strict
private var maneatingplant_anim : Texture;
private var maneatingplant_anim_Mounth : Texture;
private var maneatingplant_dead : Texture;
private var Cow_die = false;

private var startPos:Vector3;
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
	maneatingplant_anim = Resources.Load("maneatingplant_anim_5") as Texture;
 	maneatingplant_anim_Mounth  = Resources.Load("maneatingplant_attack") as Texture;
 	maneatingplant_dead   = Resources.Load("maneatingplant_dead") as Texture;
}

function Update () 
{ 
	t += Time.deltaTime; 
	if(t>3f)
		t = 0;
	else if(t>2.2f)
		gameObject.guiTexture.texture = maneatingplant_anim_Mounth; 
	else if (t<1f )
		 gameObject.guiTexture.texture = maneatingplant_anim; 
	else
		  gameObject.guiTexture.texture =  maneatingplant_dead;
}