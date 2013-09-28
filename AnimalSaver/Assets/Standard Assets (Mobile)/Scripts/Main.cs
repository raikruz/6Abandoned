using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	//bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	
	// Use this for initialization
	void Start () {
		// resize filled sprites to match screen size
        Resize("BackGround");
        // set initialized notifier to true so we only initialize once.
        //initialized = true;
		OTSprite boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
		boat.position = new Vector2(Screen.width / 2, -Screen.height / 2.5f);
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
	
	static string[] animals = new string[] {"Cow_pre", "Chick_pre"};
	static string[] Obstacles = new string[] {"Maneatingplant_ani"};
	System.Random random = new System.Random();
	// Update is called once per frame
	float t;
	float t2;
	// Update is called once per frame
	void Update () 
	{
		int randomNumber = random.Next(0, 2);
		t += Time.deltaTime;
		
		t2 += Time.deltaTime;
		if (t > 1.2f)
		{
			
			GameObject obj = OT.CreateObject(animals[randomNumber]);
			t = 0;
		}
		if(t2 > 3f)
		{
			// Create Obstacle every 3 seconds
			OTSprite obj2 = OT.CreateObject(Obstacles[0]).GetComponent<OTSprite>();
			
			t2 = 0;
		}
		
		OTSprite boat = OT.ObjectByName("boat-1").GetComponent<OTSprite>();
		boat.position = new Vector2(boat.position.x - 2, boat.position.y);
		if (boat.position.x < -Screen.width / 2)
			boat.position = new Vector2(Screen.width / 2, -Screen.height / 2.5f);
	}
}
