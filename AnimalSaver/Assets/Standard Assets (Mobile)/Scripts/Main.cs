using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	//bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	
	// Cow picture;
	Texture cow_left = null;
	Texture cow_right = null;
	Texture cow_down = null;
	// Chick picture
	Texture chick_left = null;
	Texture chick_right = null;
	Texture chick_down = null;
	
	// Use this for initialization
	void Start () {
		// resize filled sprites to match screen size
        //Resize("BackGround");
        // set initialized notifier to true so we only initialize once.
        //initialized = true;
		
		// Cow picture;
		cow_left = Resources.Load("cow_left") as Texture;
		cow_right = Resources.Load("cow_right") as Texture;
		cow_down = Resources.Load("cow_down") as Texture;
		// Chick picture
		chick_left = Resources.Load("chick_left") as Texture;
		chick_right = Resources.Load("chick_right") as Texture;
		chick_down = Resources.Load("chick_down") as Texture;
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
            // We will assume the OTView.zoom factor is set to zero (no zooming)
			Debug.Log(Screen.width);
            sprite.size = new Vector2(Screen.width, Screen.height);
        }
    }
	
	static string[] animals = new string[] {"Cow_pre", "Chick_pre"};
	System.Random random = new System.Random();
	// Update is called once per frame
	float t;
	int index = 0;
	Texture animal_texture = null;
	// Update is called once per frame
	void Update () 
	{
		int randomNumber = random.Next(0, 2);
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
	    {
			if(randomNumber == 0)
			{
				animal_texture = cow_left;
			}
			else
			{
				animal_texture = chick_left;
			}
	    }
	    else if (Input.GetKeyDown(KeyCode.RightArrow))
	    {
			if(randomNumber == 0)
			{
				animal_texture = cow_right;
			}
			else
			{
				animal_texture = chick_right;
			}
	    }
		else
		{
			if(randomNumber == 0)
			{
				animal_texture = cow_left;
			}
			else
			{
				animal_texture = chick_down;
			}
		}
		
		t += Time.deltaTime;
		if (t > 1.2f)
		{
			
			GameObject obj = OT.CreateObject(animals[randomNumber]);
			obj.guiTexture.texture = animal_texture;
			//OTSprite boat = OT.CreateObject("Boat").GetComponent<OTSprite>();
			//boat.position = new Vector2(50, 100);
			t = 0;
		}
	}
}
