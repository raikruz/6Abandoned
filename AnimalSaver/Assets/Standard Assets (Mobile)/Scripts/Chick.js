#pragma strict
private var chick:OTSprite;  
private var Chick_die = false;

private var startPos:Vector3;
private var k:float;
function Start()
{
	// Get this chick's sprite
	chick = GetComponent("OTSprite");
	chick.InitCallBacks(this);
	chick.frameIndex = 0;
	// initialize variables
	// initialize the velocity and start position
 	chick.rigidbody.isKinematic = false;
	// initialize the velocity as 25 in -y direction
	chick.rigidbody.velocity = new Vector3(0, -15,0);
	
 	var randomValue1:float = Random.value;
 	// here 3*Cow.size.y is to give the gap on left and right. Please increase it to decrease difficulty, 
 	// because it will minimize the falling range on the sky 
 	// start position x should be random
	var creationScreenwidth = Screen.width - 3*chick.size.y;
	startPos.x =  creationScreenwidth*(randomValue1 - 0.5);
	startPos.y = (Screen.height- chick.size.y)/2;
	startPos.z = 0;
	chick.position = new Vector2(startPos.x , startPos.y);
	
	// give random initilial direction (left or right)
	var randomValue2:float = Random.value;
	if(randomValue2 < 0.5)
		k = startPos.y*0.015-(3.1415926/2);
	else
		k = startPos.y*0.015+(3.1415926/2);
}

function Update () {
	var targetPos: Vector3 = transform.position;
	var tmpY:float =  targetPos.y;
	var tmpX:float =  transform.position.x;
	
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Chick_die == false)
	{
		chick.frameIndex = 1;
		//transform.Translate(-30 * Time.deltaTime, 0, 0);
		startPos.x += -30 * Time.deltaTime;
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Chick_die == false)
	{
		chick.frameIndex = 3;
		//transform.Translate(30 * Time.deltaTime, 0, 0);
		startPos.x += 30 * Time.deltaTime;
	}
	// default down picture
	else if(Chick_die == false)
	{
		chick.frameIndex = 0;
	}
	// Reset the position according to interaction
	if (Chick_die == false){
		tmpX = startPos.x+(1/((0.006)+0.00005*Time.time))* Mathf.Cos(tmpY*0.015-k);
		chick.position = new Vector2(tmpX,tmpY);
	}
	

	// determine the border of the object's movement
	if(Mathf.Abs(gameObject.transform.position.x) > ((Screen.width - chick.size.x)/2))
	{
		// show the Cow dead object 
		chick.frameIndex = 2;
		// set the die flag
		Chick_die = true;
	}
	
	if (chick.outOfView)
	{
	 // Destroy the this the object
	 Destroy(chick.gameObject);
	 // Test Game Over function
	 Application.LoadLevel(1);
	}

}

function OnCollision(owner:OTObject)
{
	// a collision occured
	OT.print(owner.name+" collided with "+owner.collisionObject.name+" at "+owner.collision.contacts[0].point);
}

//function onOutOfView(owner:OTObject)
//{
//    OT.DestroyObject(owner);
//}