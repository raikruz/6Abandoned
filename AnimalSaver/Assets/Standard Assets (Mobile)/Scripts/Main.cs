﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
    bool scrolling = false;             // scrolling notifier
	OTAnimatingSprite ManEatFlower; 
	System.Random random = new System.Random();
	static int boatSpeed = 2;
	static int boatNum = 2;
	static int waterSpeed = 1;
	static int waterNum = 2;
	static int level=1;
	static string[] animals = new string[] {"Cow", "Chick"};
	static string[] Obstacles = new string[] {"Bee_left","Bee_right","Maneater_plant"};
	static int[] maxObstacle = new int[] {1,1};
	static int[] numObstacle = new int[] {0,0};
	static int[] maxAnimal = new int[] {1,1};
	static int[] numAnimal = new int[] {0,0};
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
		initVariable();
		animalSaved = 0;
		gameOver = false;
		// resize filled sprites to match screen size
        Resize("BackGround", 0);
		Resize("BackGround_Bottom_0",1);
		Resize("BackGround_BottomWatertop_0", 3);
		Resize("BackGround_Bottom_1",1);
		Resize("BackGround_BottomWatertop_1", 3);
		Resize("BackGround_BottomgrassBorder",4);
		OTObject Bottom = OT.ObjectByName("BackGround_Bottom_0");
		OTObject grass = OT.ObjectByName("BackGround_Bottomgrass");
		grass.size = new Vector2(Screen.width , Screen.height*80f/1280f);
		grass.position = new Vector2(0,Bottom.position.y + Bottom.size.y/2 - grass.size.y/2);
		
		Resize("Score", 2);
		OTObject Waterbottom = OT.ObjectByName("BackGround_Bottom_1");
		Waterbottom.position = new Vector2(Waterbottom.position.x+ Waterbottom.size.x,Waterbottom.position.y);
        // set initialized notifier to true so we only initialize once.
		for (int i = 0; i < boatNum; ++i)
		{
			OTSprite boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
			boat.name = "Boat" + i.ToString();
			boat.size = new Vector2(Screen.width * 0.5f, Screen.width * 0.5f * 124 /357);
			//1/10 of boat height is under water
			boatposY = -(Screen.height/2 - Waterbottom.size.y  - boat.size.y*0.4f); // fixed value
			boat.position = new Vector2((Screen.width + boat.size.x) / 2 * (i + 1), boatposY);// -(Screen.height - boat.size.y * 2) / 2);
			//// only one boat shown at the same time
			//boat.position = new Vector2((Screen.width + boat.size.x) *(0.5f+i), boatposY); 
		}
		
		// water should move.
		OTObject Watertop = OT.ObjectByName("BackGround_BottomWatertop_1");
		Watertop.position = new Vector2(Watertop.size.x + Watertop.position.x, Watertop.position.y);
		
		t2 = 0;
		
		// create 
		InitWind();
	}
	
    void initVariable()
	{
		int i;
		for(i=0;i<numObstacle.Length;i++)
		{
				numObstacle[i]=0;
		}
			for(i=0;i<numAnimal.Length;i++)
		{
				numAnimal[i]=0;
		}
	}
	void InitWind()
	{	
		windLeft = OT.ObjectByName("WindLeft");
		windRight = OT.ObjectByName("WindRight");
		windDown = OT.ObjectByName("WindDown");
		windLeft.visible = false;
		windRight.visible = false;
		windDown.visible = false;
		tWind = 0;
		windDown.size = new Vector2(Screen.width * 0.1f, Screen.width * 0.2f);
		windLeft.size = new Vector2(windDown.size.y, windDown.size.x);
		windRight.size = new Vector2(windDown.size.y, windDown.size.x);
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

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			Application.LoadLevel(0); // Back to Main menu
		}

		level = (int)(Mathf.Log(animalSaved+1,2))+1;
		int randomNumber = random.Next(0, 2);
		setMaxAnimal();
		t += Time.deltaTime;
		
		t2 += Time.deltaTime;
		if (t > 5.0f)
		{
			if((randomNumber==0)&&(numAnimal[0]<maxAnimal[0])){
				OTSprite animal =  OT.CreateObject(animals[randomNumber]).GetComponent<OTSprite>();
				numAnimal[0]++;
							t = 0;

			}
			if((randomNumber==1)&&(numAnimal[1]<maxAnimal[1])){
				OTSprite animal =  OT.CreateObject(animals[randomNumber]).GetComponent<OTSprite>();
				numAnimal[1]++;
							t = 0;

			}
		}
		level = (int)(Mathf.Log(animalSaved+1,2))+1;
		setMaxObstacle();
		int randomNumberObs = random.Next(0, 3);
		if(t2 > 3f)
		{
			// Create Obstacle every 3 seconds
			if((randomNumberObs==0)&&(numObstacle[0]<maxObstacle[0])){
				OTSprite obj2 = OT.CreateObject(Obstacles[randomNumberObs]).GetComponent<OTSprite>();
				obj2.size = new Vector2(Screen.width / 12f, Screen.width / 12f * 196 /184); //184 / 196
				float positionY= random.Next(Screen.height/4,Screen.height*3/4); // obstacle is always generated on bottom 3/4 area to avoid kill animals soon after genrated.
				obj2.position = new Vector2((Screen.width + obj2.size.x) / 2 , ((Screen.height/2) - positionY));
				numObstacle[0]++;
			}
			if((randomNumberObs==1)&&(numObstacle[0]<maxObstacle[0])){
				OTSprite obj2 = OT.CreateObject(Obstacles[randomNumberObs]).GetComponent<OTSprite>();
				obj2.size = new Vector2(Screen.width / 12f, Screen.width / 12f * 196 /184); //184 / 196
				float positionY= random.Next(Screen.height/4,Screen.height*3/4);
				obj2.position = new Vector2((-Screen.width + obj2.size.x) / 2 , ((Screen.height/2) - positionY));
				numObstacle[0]++;
			}
			if((randomNumberObs==2)&&(numObstacle[1]<maxObstacle[1])){
				OTSprite obj2 = OT.CreateObject(Obstacles[randomNumberObs]).GetComponent<OTSprite>();
				obj2.size = new Vector2(Screen.width / 8f, Screen.width / 8f * 254 /205);  //205 / 254
				float positionX= random.Next(0,Screen.width);
				float positionY= random.Next(Screen.height/4,Screen.height*3/4);
				obj2.position = new Vector2(((Screen.width/2) - positionX ) , ((Screen.height/2) - positionY));
				numObstacle[1]++;
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
	
	public static void setMaxObstacle()
	{
			maxObstacle[0]= (int)(level/3);
			maxObstacle[1]= (int)(level/4);

	}
		public static void setMaxAnimal()
	{
			maxAnimal[0]= (int)(level/2);
			maxAnimal[1]= 2;

	}
	public static void decreaseObstacle(int choice)
	{
			numObstacle[choice]--;
	}
	public static void decreaseAnimal(int choice)
	{
			numAnimal[choice]--;
	}
}
