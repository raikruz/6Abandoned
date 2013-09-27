using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	//bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	
	// Use this for initialization
	void Start () {
		// resize filled sprites to match screen size
        //Resize("BackGround");
        // set initialized notifier to true so we only initialize once.
        //initialized = true;
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
	// Update is called once per frame
	void Update () 
	{
		int randomNumber = random.Next(0, 2);
		
		t += Time.deltaTime;
		if (t > 1.2f)
		{
			
			GameObject obj = OT.CreateObject(animals[randomNumber]);
			//obj.guiTexture.texture = animal_texture;
			t = 0;
		}
	}
}
