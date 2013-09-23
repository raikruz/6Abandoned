#pragma strict

public var speedx:float = 20.0f;
public var speedy:float = 1.0f;
public var direction:int = 1;
public var startingLives:int = 3;

private var startPos: Vector3;
private var start_x:float = 0.5;
private var start_y:float = 0.87;
private var start_z:float = 0;

private var Chick_right : Texture;
private var Chick_left : Texture;
private var Chick_down : Texture;

private var remainingLives: int;

function Start () {
	//remainingLives = startingLives;
	// initialize variables
	startPos.x = start_x;
	startPos.y = start_y;
	startPos.z = start_z;
	//
	Chick_right = Resources.Load("chick_right") as Texture;
    Chick_left = Resources.Load("chick_left") as Texture;
    Chick_down = Resources.Load("chick_down") as Texture;
}

function Update () {
	var targetPos: Vector3 = transform.position;
	if (Input.GetKey(UnityEngine.KeyCode.LeftArrow)) {
	    // change the Chick's picture to the left one
	    GameObject.Find("Chick_pre").guiTexture.texture = Chick_left;
		targetPos.x = transform.position.x - 0.02;
    	transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
    	
    	
	}
	else if (Input.GetKey(UnityEngine.KeyCode.RightArrow)) {
	    // change the chick's picture to the right one
	    GameObject.Find("Chick_pre").guiTexture.texture = Chick_right;
		targetPos.x = transform.position.x + 0.02;
    	transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
    }
    else
    {
    	GameObject.Find("Chick_pre").guiTexture.texture = Chick_down;
    }
		
    if (Input.GetKeyDown("space"))
    {
    	// need to implement sth...
    }
    targetPos.y = transform.position.y - speedy * direction/100;
    transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
}

function OnGUI() {
    //GUI.Label (Rect (70, 5, 120, 20), "Lives Remaining: " + remainingLives);
}

function Hit() {
	remainingLives--;
	
	//Instantiate(explosion, transform.position, Quaternion.identity);
	
	if (remainingLives < 0) {
		Destroy(gameObject);
		GameObject.Find("GameScript").SendMessage("PlayerDead");
	}
}