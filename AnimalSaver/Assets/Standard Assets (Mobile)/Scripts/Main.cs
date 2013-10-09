using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	OTAnimatingSprite ManEatFlower; 
	System.Random random = new System.Random();
	static int boatSpeed = 2;
	static int boatNum = 2;
	static string[] animals = new string[] {"Cow", "Chick"};
	static string[] Obstacles = new string[] {"Bee_left","Bee_right","Maneater_plant"};
	Vector3 mousePosition;
	Vector3 mouseUpPosition;
	static string windDirection = "none";
	OTObject windLeft;
	OTObject windRight;
	OTObject windDown;
	float t;
	float t2;
	float tWind = 0;
	public static float windDuration = 2.0f;
	public static uint animalSaved;
	public static bool gameOver;
	// Use this for initialization
	void Start () {

		animalSaved = 0;
		gameOver = false;
		// resize filled sprites to match screen size
        Resize("BackGround", 0);
		Resize("BackGround_Bottom",1);
		Resize("Score", 2);
        // set initialized notifier to true so we only initialize once.
		for (int i = 0; i < boatNum; ++i)
		{
			OTSprite boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
			boat.name = "Boat" + i.ToString();
			boat.size = new Vector2(Screen.width / 2.0f, Screen.height / 5.0f);
			boat.position = new Vector2((Screen.width + boat.size.x) / 2 * (i + 1), -(Screen.height - boat.size.y * 2) / 2);			
		}
		
//		for (int i = 0; i < Screen.width;)
//		{
//			OTSprite flower = OT.CreateObject(Obstacles[0]).GetComponent<OTSprite>();
//			flower.position = new Vector2(-(Screen.width - flower.size.x) / 2 + i, -(Screen.height - flower.size.y) /2);
//			i += (int)flower.size.x;
//		}
		t2 = 0;
		
		windLeft = OT.ObjectByName("WindLeft");
		windRight = OT.ObjectByName("WindRight");
		windDown = OT.ObjectByName("WindDown");
		// create 
	}
	

    // This method will resize the a FilledSprite ( provided by name )
    // to match the current view (resolution).
    void Resize(string spriteName, int type)
    {
        // Lookup the FilledSprite using its name.
        OTObject sprite = OT.ObjectByName(spriteName);
        if (sprite != null)
        {
            // We found the sprite so lets size it to match the screen's resolution
            // We will assume the OTView.zoom factor is set to zero (no zooming));
            switch(type)
			{
			case 0: // background
            	sprite.size = new Vector2(Screen.width, Screen.height);
				break;
			case 1:  // bottom
				sprite.size = new Vector2(Screen.width, Screen.height / 6.0f);
				sprite.position = new Vector2(0, -(Screen.height - sprite.size.y) / 2);
				break;
			case 2:
				sprite.size = new Vector2(Screen.width, Screen.height / 10);
				sprite.position = new Vector2(0, (Screen.height - sprite.size.y) / 2);
				break;
			default:
				break;
			}
        }
    }

	 // application initialization
    void Initialize()
    {
//		 // Get reference to gun animation sprite
//        ManEatFlower = OT.ObjectByName("ManEatFlowerSprite") as OTAnimatingSprite;
//		
//        // Set gun animation finish delegate
//        // HINT : We could use sprite.InitCallBacks(this) as well.
//        // but because delegates are the C# way we will use this technique
//        ManEatFlower.onAnimationFinish = OnAnimationFinish;
//		
//		// temporary
//		ManEatFlower.Play();
        // set our initialization notifier - we only want to initialize once
        initialized = true;
	}
	// Update is called once per frame
	void Update () 
	{
		if(!initialized)
		{
			Initialize();
			return;
		}
		int randomNumber = random.Next(0, 2);
		t += Time.deltaTime;
		
		t2 += Time.deltaTime;
		if (t > 5.0f)
		{
			
			OTSprite animal =  OT.CreateObject(animals[randomNumber]).GetComponent<OTSprite>();
			t = 0;
		}
		int randomNumberObs = random.Next(0, 3);
		if(t2 > 3f)
		{
			// Create Obstacle every 3 seconds
			OTSprite obj2 = OT.CreateObject(Obstacles[randomNumberObs]).GetComponent<OTSprite>();
			
			t2 = 0;
			boatSpeed = random.Next(0,5);
		}
		
		for (int i = 0; i < boatNum; ++i)
		{
			string boatName = "Boat" + i.ToString();
			OTSprite boat = OT.ObjectByName(boatName).GetComponent<OTSprite>();
			boat.position = new Vector2(boat.position.x - boatSpeed, boat.position.y);
			if (boat.position.x < -(Screen.width + boat.size.x) / 2)
				boat.position = new Vector2((Screen.width + boat.size.x) / 2, -(Screen.height - boat.size.y * 2) / 2);
		}
		
		UpdateWindDirection();
		//OT.print(windDirection);
		DrawWind();	
	}
	
	bool IsWindBegin(ref Vector2 pos)
	{
		foreach(Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				pos = Camera.main.ScreenToWorldPoint(touch.position);
				return true;
			}
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return true;
		}
		
		return false;	
	}
	
	bool IsWindEnd(ref Vector2 pos)
	{
		foreach(Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Ended)
			{
				pos = Camera.main.ScreenToWorldPoint(touch.position);
				return true;
			}
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return true;
		}
		
		return false;	
	}
	
	void UpdateWindDirection()
	{
		Vector2 tmpPos = new Vector2();
		if (IsWindBegin(ref tmpPos))
		{
			windDirection = "none";
			mousePosition = tmpPos;
			//OT.print(mousePosition.x + "," + mousePosition.y);
		}
		else if (IsWindEnd(ref tmpPos))
		{
			mouseUpPosition = tmpPos;
			//OT.print(mouseUpPosition.x + "," + mouseUpPosition.y);
			float xPos = mouseUpPosition.x - mousePosition.x;
			float yPos = mouseUpPosition.y - mousePosition.y;
			
			if (Mathf.Abs(yPos) > Mathf.Abs(xPos))
			{
				if (yPos < 0)
					windDirection = "down";
				if (yPos > 0)
					windDirection = "up";
			}
			else
			{
				if (xPos < 0)
					windDirection = "left";
				else if (xPos > 0)
					windDirection = "right";
				else 
					windDirection = "none";
			}
			
			tWind = 0;
		}
		
		if (windDirection != "none")
			tWind += Time.deltaTime;
		
		if (tWind > windDuration)
			windDirection = "none";			
	}
	
	void DrawWind()
	{
		OTObject wind = null;
		switch (windDirection)
		{
		case "left":
			{
				windRight.visible = false;
				windDown.visible = false;
				wind = windLeft;
			}
			break;
		case "right":
			{
				windDown.visible = false;
				windLeft.visible = false;
				wind = windRight;
			}
			break;
		case "down":
			{
				windRight.visible = false;
				windLeft.visible = false;
				wind = windDown;
			}
			break;
		default:
			{
				windRight.visible = false;
				windLeft.visible = false;
				windDown.visible = false;
			}
			break;
		}
		
		if (wind != null)
		{
			wind.position = new Vector2 ((mousePosition.x + mouseUpPosition.x) / 2, (mousePosition.y + mouseUpPosition.y) / 2);
			wind.visible = true;
		}
	}
		
	public static string WindDirection
	{
		get
		{
			return windDirection;
		}
	}
	
	public void OnAnimationFinish(OTObject owner)
   {
//        if (owner == ManEatFlower)
//        {
//           // Because the only animation that finishes will be the gun's 'shoot' animation frameset
//            // we know that we have to switch to the gun's looping 'idle' animation frameset
//            ManEatFlower.PlayLoop("Start");
//        }
    }
}
