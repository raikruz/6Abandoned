#pragma strict
private var Chick_right : Texture;
private var Chick_left : Texture;
private var Chick_down : Texture;

function Start()
{
	// Initialize the resource picture
	Chick_right = Resources.Load("chick_right") as Texture;
    Chick_left = Resources.Load("chick_left") as Texture;
    Chick_down = Resources.Load("chick_down") as Texture;
}

function Update () {
	// respond to the left arrow
	if(Input.GetKey(KeyCode.LeftArrow))
	{
		gameObject.guiTexture.texture = Chick_left;
		transform.Translate(-0.3 * Time.deltaTime, 0, 0);
	}
	// respond to the right arrow
	else if(Input.GetKey(KeyCode.RightArrow))
	{
		gameObject.guiTexture.texture = Chick_right;
		transform.Translate(0.3 * Time.deltaTime, 0, 0);
	}
	// default down picture
	else
	{
		gameObject.guiTexture.texture = Chick_down;
	}
}