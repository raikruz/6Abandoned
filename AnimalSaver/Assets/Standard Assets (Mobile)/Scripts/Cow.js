#pragma strict
private var Cow_right : Texture;
private var Cow_left : Texture;
private var Cow_down : Texture;

function Start () 
{
	// initialize picture
	Cow_right = Resources.Load("cow_right") as Texture;
    Cow_left = Resources.Load("cow_left") as Texture;
    Cow_down = Resources.Load("cow_down") as Texture;
}

function Update () 
{
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow))
	{
		gameObject.guiTexture.texture = Cow_left;
		transform.Translate(-0.2 * Time.deltaTime, 0, 0);
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow))
	{
		gameObject.guiTexture.texture = Cow_right;
		transform.Translate(0.2 * Time.deltaTime, 0, 0);
	}
	// default down picture
	else
	{
		gameObject.guiTexture.texture = Cow_down;
	}
    
}