using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {
	
	bool initialized = false;           // initialization notifier
    bool scrolling = false;             // scrolling notifier
	// Use this for initialization
	void Start () {
		// resize filled sprites to match screen size
        Resize("BackGround");
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
            sprite.size = new Vector2(Screen.width/2, Screen.height);
        }
    }


	 // application initialization
    void Initialize()
    {
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

	}
}
