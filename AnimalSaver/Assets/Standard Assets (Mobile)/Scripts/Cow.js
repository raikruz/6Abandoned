#pragma strict
private var Cow_right : Texture;
private var Cow_left : Texture;
private var Cow_down : Texture;
private var Cow_dead : Texture;
private var Cow_r_dead : Texture;
private var Cow_die = false;

private var startPos:Vector3;
private var k:float;
function Start () 
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
		k = startPos.y*10-(3.1415926/2);
	else
		k = startPos.y*10+(3.1415926/2);
	
	Cow_right = Resources.Load("cow_right") as Texture;
    Cow_left = Resources.Load("cow_left") as Texture;
    Cow_down = Resources.Load("cow_down") as Texture;
    Cow_dead = Resources.Load("cow_dead") as Texture;
    Cow_r_dead = Resources.Load("cow_right_dead") as Texture;
}

function Update () 
{
	var targetPos: Vector3 = transform.position;
	
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_left;
		transform.Translate(-0.3 * Time.deltaTime, 0, 0);
		startPos.x+=-0.3 * Time.deltaTime;

	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_right;
		transform.Translate(0.3 * Time.deltaTime, 0, 0);
		startPos.x += 0.3 * Time.deltaTime;
	}
	// default down picture
	else if (Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_down;
	}
	// Reset the position according to interaction
	if (Cow_die == false){
		targetPos.x = startPos.x+(1/(1.2+0.25*Time.time))* Mathf.Cos(targetPos.y*10-k);
		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
	}
	
	// determine the border of the object's movement
	// the middle x position is: x = 0.5
	if(gameObject.transform.position.x < 0.25)
	{
		// show the Cow dead object 
		gameObject.guiTexture.texture = Cow_dead;
		// set the die flag
		Cow_die = true;
	}
	else if(gameObject.transform.position.x > 0.8)
	{
		// show the Cow dead right object 
		gameObject.guiTexture.texture = Cow_r_dead;
		// set the die flag
		Cow_die = true;
	} 
}