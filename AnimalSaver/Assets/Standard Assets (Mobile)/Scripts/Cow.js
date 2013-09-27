#pragma strict
private var Cow_right : Texture;
private var Cow_left : Texture;
private var Cow_down : Texture;
private var Cow_dead : Texture;
private var Cow_r_dead : Texture;
private var Cow_die = false;

function Start () 
{
	// initialize picture
	Cow_right = Resources.Load("cow_right") as Texture;
    Cow_left = Resources.Load("cow_left") as Texture;
    Cow_down = Resources.Load("cow_down") as Texture;
    Cow_dead = Resources.Load("cow_dead") as Texture;
    Cow_r_dead = Resources.Load("cow_right_dead") as Texture;
}

function Update () 
{
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow) && Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_left;
		transform.Translate(-0.3 * Time.deltaTime, 0, 0);
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow) && Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_right;
		transform.Translate(0.3 * Time.deltaTime, 0, 0);
	}
	// default down picture
	else if (Cow_die == false)
	{
		gameObject.guiTexture.texture = Cow_down;
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