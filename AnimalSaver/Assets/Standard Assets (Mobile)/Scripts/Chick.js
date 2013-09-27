#pragma strict
private var Chick_right : Texture;
private var Chick_left : Texture;
private var Chick_down : Texture;
private var Chick_dead : Texture;
private var Chick_die = false;

function Start()
{
	// Initialize the resource picture
	Chick_right = Resources.Load("chick_right") as Texture;
    Chick_left = Resources.Load("chick_left") as Texture;
    Chick_down = Resources.Load("chick_down") as Texture;
    Chick_dead = Resources.Load("chick_dead") as Texture;
}

function Update () {
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_left;
		transform.Translate(-0.3 * Time.deltaTime, 0, 0);
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_right;
		transform.Translate(0.3 * Time.deltaTime, 0, 0);
	}
	// default down picture
	else if(Chick_die == false)
	{
		gameObject.guiTexture.texture = Chick_down;
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