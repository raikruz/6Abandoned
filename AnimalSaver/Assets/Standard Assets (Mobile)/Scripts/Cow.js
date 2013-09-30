#pragma strict
private var Cow:OTSprite;  
private var Cow_die = false;

private var startPos:Vector3;
private var k:float;
function Start () 
{
	// Get this cow's sprite
	Cow = GetComponent("OTSprite");
	Cow.InitCallBacks(this);
	Cow.frameIndex = 1;
	Cow.rigidbody.isKinematic = false;
	// initialize variables
	// initialize the velocity as 25 in -y direction
	Cow.rigidbody.velocity = new Vector3(0, -25,0);
 	
 	var randomValue1:float = Random.value;
 	// here 3*Cow.size.y is to give the gap on left and right. Please increase it to decrease difficulty, 
 	// because it will minimize the falling range on the sky 
 	// start position x should be random
 	var creationScreenwidth = Screen.width - 3*Cow.size.y;
	startPos.x =  creationScreenwidth*(randomValue1 - 0.5);
	startPos.y = (Screen.height- Cow.size.y)/2;
	startPos.z = 0;
	Cow.position = new Vector2(startPos.x , startPos.y);

	// give random initilial direction (left or right)
	var randomValue2:float = Random.value;
	if(randomValue2 < 0.5)
		k = startPos.y*0.015-(3.1415926/2);
	else
		k = startPos.y*0.015+(3.1415926/2);
}

function Update () 
{
	var targetPos: Vector3 = transform.position;
	var tmpY:float =  targetPos.y;
	var tmpX:float =  targetPos.x;
	
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Cow_die == false)
	{
		Cow.frameIndex = 2;
		//tmpX -= 30 * Time.deltaTime;
		startPos.x+=-30 * Time.deltaTime;

	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Cow_die == false)
	{
		Cow.frameIndex = 3;
		//tmpX += 30 * Time.deltaTime;
		startPos.x += 30 * Time.deltaTime;
	}
	// default down picture
	else if (Cow_die == false)
	{
		Cow.frameIndex = 1;
	}
	// Reset the position according to interaction
	if (Cow_die == false){
		tmpX = startPos.x+(1/((0.012)+0.00005*Time.time))* Mathf.Cos(tmpY*0.015-k);
		Cow.position = new Vector2(tmpX,tmpY);
	}
	
	// determine the border of the object's movement
	// the middle x position is: 
	if(Cow.position.x  < - (Screen.width - Cow.size.x)/2)
	{
		// show the Cow dead object 
		Cow.frameIndex = 0;
		// set the die flag
		Cow_die = true;
	}
	else if(Cow.position.x >  (Screen.width- Cow.size.x)/2)
	{
		// show the Cow dead right object 
		Cow.frameIndex = 4;
		// set the die flag
		Cow_die = true;
	} 
}