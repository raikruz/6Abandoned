using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	OTAnimatingSprite ManEatFlower; 
	System.Random random = new System.Random();
	static int boatSpeed = 2;
	static int boatNum = 2;
	static int waterSpeed = 1;
	static int waterNum = 2;
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
	float boatposY = 0f; // fixed and initialized at Start() function
	public static float windDuration = 2.0f;
	public static uint animalSaved;
	public static bool gameOver;
	// Use this for initialization
	void Start () {

		animalSaved = 0;
		gameOver = false;
		// resize filled sprites to match screen size
        Resize("BackGround", 0);
		Resize("BackGround_Bottom_0",1);
		Resize("BackGround_BottomWatertop_0", 3);
		Resize("BackGround_Bottom_1",1);
		Resize("BackGround_BottomWatertop_1", 3);
		Resize("BackGround_BottomgrassBorder",4);
		Resize("BackGround_Bottomgrass",4);
		OTObject Bottom = OT.ObjectByName("BackGround_Bottom_0");
		OTObject grass = OT.ObjectByName("BackGround_Bottomgrass");
		grass.position = new Vector2(grass.position.x,-(Screen.height-Bottom.size.y - grass.size.y)/2);
			
		Resize("Score", 2);
		OTObject Waterbottom = OT.ObjectByName("BackGround_Bottom_1");
		Waterbottom.position = new Vector2(Waterbottom.position.x+ Waterbottom.size.x,Waterbottom.position.y);
        // set initialized notifier to true so we only initialize once.
		for (int i = 0; i < boatNum; ++i)
		{
			OTSprite boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
			boat.name = "Boat" + i.ToString();
			boat.size = new Vector2(Screen.width * 0.6f, Screen.width * 0.6f * 124 /357);
			//boat.position = new Vector2((Screen.width + boat.size.x) / 2 * (i + 1), -(Screen.height - boat.size.y * 2) / 2);
			//1/4 of boat height is under water
			boatposY = -(Screen.height/2 - Waterbottom.size.y  - boat.size.y*0.25f); // fixed value
			boat.position = new Vector2((Screen.width + boat.size.x) / 2 * (i + 1), boatposY);
		}
		
		// water should move.

		
		
		OTObject Watertop = OT.ObjectByName("BackGround_BottomWatertop_1");
		Watertop.position = new Vector2(Watertop.size.x + Watertop.position.x, Watertop.position.y);
		
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
			case 1:  // bottom water should be initialized before Top ater
				sprite.size = new Vector2(Screen.width * 2 , Screen.height * 152f /1280f);
				sprite.position = new Vector2(0, -(Screen.height - sprite.size.y) / 2);
				break;
			case 3: // top water
			{
				sprite.size = new Vector2(Screen.width* 2, Screen.height* 54f/1280 );//1600f
				OTObject Bottomwater = OT.ObjectByName("BackGround_Bottom_0");
				if(Bottomwater != null) {
					float tmpy = Bottomwater.position.y + Bottomwater.size.y/2 + sprite.size.y/2 -2;
					sprite.position = new Vector2(0, tmpy);
				}
				break;
			}
			case 4: // grass fixed at the bottom
				sprite.size = new Vector2(Screen.width, Screen.height *82f/800f);
				sprite.position = new Vector2(0, -(Screen.height - sprite.size.y) / 2);
				break;
			case 2: // Top score pic
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
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			Application.LoadLevel(0); // Back to Main menu
		}

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
			if(randomNumberObs==0){
				obj2.size = new Vector2(Screen.width / 12f, Screen.width / 12f * 184 /196);
				float positionY= random.Next(Screen.height/2,Screen.height); // obstacle is always generated on bottom to avoid kill animals soon after genrated.
				obj2.position = new Vector2((Screen.width + obj2.size.x) / 2 , ((Screen.height/2) - positionY));
			}
			if(randomNumberObs==1){
				obj2.size = new Vector2(Screen.width / 12f, Screen.width / 12f * 184 /196);
				float positionY= random.Next(Screen.height/2,Screen.height);
				obj2.position = new Vector2((Screen.width + obj2.size.x) / 2 , ((Screen.height/2) - positionY));
			}
			if(randomNumberObs==2){
				obj2.size = new Vector2(Screen.width / 8f, Screen.width / 8f * 205 /254);
				float positionX= random.Next(0,Screen.width);
				float positionY= random.Next(Screen.height/2,Screen.height);
				obj2.position = new Vector2(((Screen.width/2) - positionX ) , ((Screen.height/2) - positionY));
			}

			t2 = 0;
			boatSpeed = random.Next(0,5);
		}
		
		for (int i = 0; i < boatNum; ++i)
		{
			string boatName = "Boat" + i.ToString();
			OTSprite boat = OT.ObjectByName(boatName).GetComponent<OTSprite>();
			boat.position = new Vector2(boat.position.x - boatSpeed, boat.position.y);
			if (boat.position.x < -(Screen.width + boat.size.x) / 2)
				boat.position = new Vector2((Screen.width + boat.size.x) / 2, boatposY);
		}
		for (int i = 0; i < waterNum; ++i)
		{
			string waterName = "BackGround_Bottom_" + i.ToString();
			OTObject water = OT.ObjectByName(waterName).GetComponent<OTSprite>();
			water.position = new Vector2(water.position.x - waterSpeed, water.position.y);
			if (water.position.x < -(Screen.width + water.size.x) / 2)
				water.position = new Vector2((Screen.width + water.size.x) / 2, water.position.y);
		}
		for (int i = 0; i < waterNum; ++i)
		{
			string waterName = "BackGround_BottomWatertop_" + i.ToString();
			OTObject water = OT.ObjectByName(waterName).GetComponent<OTSprite>();
			water.position = new Vector2(water.position.x - waterSpeed, water.position.y);
			if (water.position.x < -(Screen.width + water.size.x) / 2)
				water.position = new Vector2((Screen.width + water.size.x) / 2, water.position.y);
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
