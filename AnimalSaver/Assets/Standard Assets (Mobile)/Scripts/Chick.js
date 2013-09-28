#pragma strict
private var Chick_right : Texture;
private var Chick_left : Texture;
private var Chick_down : Texture;
private var Chick_dead : Texture;
private var Chick_die = false;

private var startPos:Vector3;
private var k:float;
function Start()
{
	// initialize variables
	// initialize the velocity and start position
 	gameObject.rigidbody.velocity = new Vector3(0, -0.25,0);
	startPos.x = 0.5;
	startPos.y = 0.87;
	startPos.z = 0;
	// give random initilial direction (left or right)
	var randomValue:float = Random.value;
	if(randomValue<0.5)
		k = startPos.y*8-(3.1415926/2);
	else
		k = startPos.y*8+(3.1415926/2);
	// Initialize the resource picture
	Chick_right = Resources.Load("chick_right") as Texture;
    Chick_left = Resources.Load("chick_left") as Texture;
    Chick_down = Resources.Load("chick_down") as Texture;
    Chick_dead = Resources.Load("chick_dead") as Texture;
}

function Update () {
	var targetPos: Vector3 = transform.position;
	
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_left;
		transform.Translate(-0.3 * Time.deltaTime, 0, 0);
		startPos.x += -0.3 * Time.deltaTime;
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_right;
		transform.Translate(0.3 * Time.deltaTime, 0, 0);
		startPos.x += 0.3 * Time.deltaTime;
	}
	// default down picture
	else if(Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_down;
	}
	
	// Reset the position according to interaction
	if (Chick_die == false){
		targetPos.x = startPos.x+(1/(1+0.25*Time.time))* Mathf.Cos(targetPos.y*8-k);
		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
	}
	
	// determine the border of the object's movement
	// the middle x position is: x = 0.5
	if(gameObject.transform.position.x < 0.25 || gameObject.transform.position.x > 0.8)
	{
		// show the Cow dead object 
		gameObject.guiTexture.texture = Chick_dead;
		// set the die flag
		Chick_die = true;
	}
}