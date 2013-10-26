using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Cow : MonoBehaviour {
	
	OTSprite cow;  
	bool Cow_die = false;
	//OTObject landWater;
	OTObject landBoat;  
	float landBoatPosX;  
	Vector3 startPos;
	float k;
	Vector3 mouseDownPosition;
	bool bWindAffected;
	float tWind;
	float deadTime;
	float dragstep;
	AudioSource sound;
	AudioSource died;
	//AudioSource generated;
	// Use this for initialization
	void Start () {
		//landWater = null;
		landBoat = null;
		landBoatPosX = 0f;
		
		// Get this chick's sprite
		cow = GetComponent<OTSprite>();
		cow.InitCallBacks(this);
		cow.onInput = OnInput;	
		cow.onEnter = OnCollided;
		cow.frameIndex = 1;
		// initialize variables
		// initialize the velocity and start position
	 	cow.rigidbody.isKinematic = false;
		// initialize the velocity as 25 in -y direction
		cow.rigidbody.velocity = new Vector3(0, -50,0);
		cow.size = new Vector2(Screen.width *0.255f, Screen.width *0.255f*1.333f);
		
	 	float randomValue1 = Random.value;
	 	// here 3*Cow.size.y is to give the gap on left and right. Please increase it to decrease difficulty, 
	 	// because it will minimize the falling range on the sky 
	 	// start position x should be random
		var creationScreenwidth = Screen.width - 3*cow.size.y;
		startPos.x =  creationScreenwidth*(randomValue1 - 0.5f);
		startPos.y = Screen.height/2;
		startPos.z = 0;
		cow.position = new Vector2(startPos.x , startPos.y);
		
		// give random initilial direction (left or right)
		float randomValue2 = Random.value;
		if(randomValue2 < 0.5f)
			k = startPos.y*0.015f-(3.1415926f/2f);
		else
			k = startPos.y*0.015f+(3.1415926f/2f);
		
		bWindAffected = false;
		tWind = 0;
		deadTime = 0f;
		dragstep = Screen.height * 0.05f;
		
		AudioSource[] aSources = GetComponents<AudioSource>();
    	sound = aSources[0];//Drag
    	died = aSources[1];//dead
		

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = transform.position;
		float tmpY =  targetPos.y;
		float tmpX =  targetPos.x;
		//tmpY = startPos.y +  cow.rigidbody.velocity.y * Time.time;
		
		// if it lands on one boat, let it move with boat
		if(landBoat != null) {
			tmpX += landBoat.position.x - landBoatPosX;
			landBoatPosX = landBoat.position.x;
			cow.position = new Vector2(tmpX,tmpY);
			if (cow.outOfView)
			{
			 // Destroy the object
			 Destroy(cow.gameObject);
			 Main.decreaseAnimal(0);
			}
			return;
		}

		if(Cow_die == false)
		{
			if (bWindAffected)
			{
				//OT.print(Main.WindDirection);
				// respond to the left arrow
				if(Main.WindDirection == "left")
				{
					cow.frameIndex = 2;
					startPos.x += -dragstep * Time.deltaTime;
					sound.Play();
				}
				else if(Main.WindDirection == "right")
	
				{
					cow.frameIndex = 3;
					startPos.x += dragstep * Time.deltaTime;
					sound.Play();			
				}
				else if(Main.WindDirection == "down")
				{
					cow.frameIndex = 1;
					//startPos.y -= 30 * Time.deltaTime;
					tmpY -=  dragstep * Time.deltaTime;
					startPos.y -=  dragstep * Time.deltaTime;
					sound.Play();
				}
				else
					cow.frameIndex = 1;
				// Reset the position according to interaction	
				
				if (Main.WindDirection == "left" || 
					Main.WindDirection == "right" ||
					Main.WindDirection == "down")
					tWind += Time.deltaTime;
			}
		}
		else
		{
			cow.frameIndex = 0;
			Main.gameOver = true;
		}
	
			
		//if (tWind > Main.windDuration || Main.WindDirection == "none")
		if (bWindAffected && tWind > Main.windDuration)
		{
			bWindAffected = false;
			tWind = 0;
		}
		
		// Reset the position according to interaction
		if (Cow_die == false){
			tmpX = startPos.x+(1f/((0.012f)+0.00005f*Time.time))* Mathf.Cos(tmpY*0.015f-k);
			if (tmpX  < - (Screen.width - cow.size.x)/2)
				tmpX = -(Screen.width - cow.size.x)/2;
			if (tmpX >  (Screen.width- cow.size.x)/2)
				tmpX = (Screen.width- cow.size.x)/2;
			//tmpY = startPos.y -  cow.rigidbody.velocity.y * Time.time;
			cow.position = new Vector2(tmpX,tmpY);

		} // end if (cow_die == false)
		else //Cow_die == true
		{
			cow.rigidbody.velocity = new Vector3(0, cow.rigidbody.velocity.y  - 9.8f*(Time.time - deadTime), 0);
		}

		if (cow.outOfView)
		{
		 	// Destroy the object
		 	Destroy(cow.gameObject);
			Main.decreaseAnimal(0);
		 	// Game Over if any animal died
			if(Cow_die == true)
				Application.LoadLevel(2);
	
		}
	}

	public void OnCollided(OTObject owner)
	{
		OTObject obj = owner.collisionObject; 
		if(Cow_die == false
	     	&& (obj.name == "Boat0" || obj.name == "Boat1")) 
	    {
			// make sure the animal is ON the boat
			if((cow.position.x-cow.size.x/2)< (obj.position.x-obj.size.x/2))
				cow.position = new Vector2(obj.position.x-obj.size.x/2+ cow.size.x/2,cow.position.y);
			else if((cow.position.x+cow.size.x/2)> (obj.position.x+obj.size.x/2))//-0.25*obj.size.x is a tolerance
				cow.position = new Vector2(obj.position.x+obj.size.x/2- cow.size.x/2-0.25f*obj.size.x,cow.position.y);
	    	landBoat = owner.collisionObject;
	    	landBoatPosX = landBoat.position.x;
	    	cow.frameIndex = 1;
	    	cow.rigidbody.velocity = new Vector3(0, 0, 0);
	    	cow.collidable = false; // no need enter OnCollided any more
			
			Main.animalSaved++;
	    } else if(Cow_die == false && landBoat == null
				&& (obj.name.StartsWith("BackGround_Bottom")  ||  obj.name.StartsWith("Bee_left")
			||obj.name.StartsWith("Bee_right") ||obj.name.StartsWith("Maneater_plant")))
			{
				died.Play();
				deadTime = Time.time;
		    	// show the Cow dead left object 
				cow.frameIndex = 0;
				// set the die flag
				Cow_die = true;
				cow.collidable = false; // no need enter onStay any more
		    }

	}
	//Move to event to main window so that mouse does not need click on object percisely
	//This one is local detection. set register input to true in designer
	void OnInput(OTObject owner)
	{
		//OT.print("OnInput " + owner.name);
		bWindAffected = true;
	}
	
}
