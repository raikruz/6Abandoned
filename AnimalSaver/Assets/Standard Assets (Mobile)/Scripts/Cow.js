#pragma strict

//public var bullet:Transform;

public var speed:float = 20.0f;
public var speedy:float = 1.0f;
public var direction:int = 1;
public var startingLives:int = 3;

public var abc: Transform;
private var startPos: Vector3;
private var start_x:float = 0.5;
private var start_y:float = 0.87;
private var start_z:float = 0;
private var Cow_right : Texture;
private var Cow_left : Texture;
private var Cow_down : Texture;

private var remainingLives: int;

function Start () {
	remainingLives = startingLives;
	// initialize variables
	startPos.x = start_x;
	startPos.y = start_y;
	startPos.z = start_z;
	//
	Cow_right = Resources.Load("cow_right") as Texture;
    Cow_left = Resources.Load("cow_left") as Texture;
    Cow_down = Resources.Load("cow_down") as Texture;
    //Cow_down = Resources.Load("Standard Assets (Mobile)/Textures/cow_down") as Texture;
}

function Update () {
	var targetPos: Vector3 = transform.position;
	if (Input.GetKey(UnityEngine.KeyCode.LeftArrow)) {
	    // change the cow's picture to the left one
	    GameObject.Find("Cow_pre").guiTexture.texture = Cow_left;
		targetPos.x = transform.position.x - 0.02;
    	transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);  	
	}
	else if (Input.GetKey(UnityEngine.KeyCode.RightArrow)) {
	    // change the cow's picture to the right one
	    GameObject.Find("Cow_pre").guiTexture.texture = Cow_right;
		targetPos.x = transform.position.x + 0.02;
    	transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * 1.0f);
    }
    else
    {
    	GameObject.Find("Cow_pre").guiTexture.texture = Cow_down;
    }
		
    if (Input.GetKeyDown("space"))
    {
    	// need to implement sth...
    }
    targetPos.y = transform.position.y - speedy * direction/30;
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