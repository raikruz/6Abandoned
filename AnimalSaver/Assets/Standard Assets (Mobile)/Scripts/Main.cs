using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	OTSprite boat;
	OTAnimatingSprite ManEatFlower; 
	System.Random random = new System.Random();
	int boatSpeed = 2;
	static string[] animals = new string[] {"Cow", "Chick"};
	static string[] Obstacles = new string[] {"Maneatingplant_ani","Bee_ani"};
	
	// Use this for initialization
	void Start () {
		// resize filled sprites to match screen size
        Resize("BackGround");
        // set initialized notifier to true so we only initialize once.
		boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
		boat.position = new Vector2(Screen.width / 2, -(Screen.height - boat.size.y) / 2);
		boatSpeed = random.Next(2,6);
		for (int i = 0; i < Screen.width;)
		{
			OTSprite flower = OT.CreateObject(Obstacles[0]).GetComponent<OTSprite>();
			flower.position = new Vector2(-(Screen.width - flower.size.x) / 2 + i, -(Screen.height - flower.size.y) /2);
			i += (int)flower.size.x;
		}
		t2 = 0;
		
		// create 
	}
	

    // This method will resize the a FilledSprite ( provided by name )
    // to match the current view (resolution).
    void Resize(string spriteName)
    {
        // Lookup the FilledSprite using its name.
        OTObject sprite = OT.ObjectByName(spriteName);
        if (sprite != null)
        {
            // We found the sprite so lets size it to match the screen's resolution
            // We will assume the OTView.zoom factor is set to zero (no zooming));
            sprite.size = new Vector2(Screen.width, Screen.height);
        }
    }

	// Update is called once per frame
	float t;
	float t2;
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
		if (t > 1.2f)
		{
			
			OTSprite animal =  OT.CreateObject(animals[randomNumber]).GetComponent<OTSprite>();
			t = 0;
		}

		if(t2 > 3f)
		{
			// Create Obstacle every 3 seconds
			OTSprite obj2 = OT.CreateObject(Obstacles[1]).GetComponent<OTSprite>();
			
			t2 = 0;
		}
		boat.position = new Vector2(boat.position.x - boatSpeed, boat.position.y);
		if (boat.position.x < -Screen.width / 2)
		{
			boat.position = new Vector2(Screen.width / 2, -(Screen.height - boat.size.y) / 2);
			boatSpeed = random.Next(2,6);
			print(boatSpeed);
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
