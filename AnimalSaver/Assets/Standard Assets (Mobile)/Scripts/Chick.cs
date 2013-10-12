using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Chick : MonoBehaviour {
	
	OTSprite chick;  
	bool Chick_die = false;
	OTObject landBoat;  
	float landBoatPosX;  
	Vector3 startPos;
	float k;
	bool bWindAffected;
	float tWind;
	float deadTime;
	// Use this for initialization
	void Start () {
		landBoat = null;
		landBoatPosX = 0f;
		
		// Get this chick's sprite
		chick = GetComponent<OTSprite>();
		chick.InitCallBacks(this);
		chick.onInput = OnInput;
		chick.onEnter = OnCollided;
		chick.frameIndex = 0;
		// initialize variables
		// initialize the velocity and start position
	 	chick.rigidbody.isKinematic = false;
		// initialize the velocity as 25 in -y direction
		chick.rigidbody.velocity = new Vector3(0, -15,0);
		chick.size = new Vector2(Screen.width *0.0767f, Screen.width *.0767f*1.233f);
		
	 	float randomValue1 = Random.value;
	 	// here 3*Cow.size.y is to give the gap on left and right. Please increase it to decrease difficulty, 
	 	// because it will minimize the falling range on the sky 
	 	// start position x should be random
		var creationScreenwidth = Screen.width - 3*chick.size.y;
		startPos.x =  creationScreenwidth*(randomValue1 - 0.5f);
		startPos.y = Screen.height/2;
		startPos.z = 0;
		chick.position = new Vector2(startPos.x , startPos.y);
		
		// give random initilial direction (left or right)
		float randomValue2 = Random.value;
		if(randomValue2 < 0.5f)
			k = startPos.y*0.015f-(3.1415926f/2f);
		else
			k = startPos.y*0.015f+(3.1415926f/2f);
		
		bWindAffected = false;
		tWind = 0;
		deadTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = transform.position;
		float tmpY =  targetPos.y;
		float tmpX =  targetPos.x;
		
		// if it lands on one boat, let it move with boat
		if(landBoat != null) {
			tmpX += landBoat.position.x - landBoatPosX;
			landBoatPosX = landBoat.position.x;
			chick.position = new Vector2(tmpX,tmpY);
			if (chick.outOfView)
			{
			 // Destroy the object
			 Destroy(chick.gameObject);
			}
			return;
		}
		
		//Not working. Need to figure out how to call funtion of other objects or scripts
		//var other = Camera.main.GetComponent("main.cs");
		// Call the function DoSomething on the script
		//OT.print(other.WindDirection);
		//OT.print(Main.WindDirection);
		if(Chick_die == false)
		{
			if (bWindAffected)
			{
				// respond to the left arrow
				if(Main.WindDirection == "left")
				{
					chick.frameIndex = 1;
					startPos.x += -30 * Time.deltaTime;
					audio.Play();
				}
				else if(Main.WindDirection == "right")
				{
					chick.frameIndex = 3;
					startPos.x += 30 * Time.deltaTime;
					audio.Play();
				}
				else if(Main.WindDirection == "down")
				{
					chick.frameIndex = 0;
					tmpY = targetPos.y - 30 * Time.deltaTime;
					audio.Play();
				}
				else 
					chick.frameIndex = 0;
		
					
				if (Main.WindDirection == "left" || 
					Main.WindDirection == "right" ||
					Main.WindDirection == "down")
					tWind += Time.deltaTime;		
			}
		}
		else 
		{
			chick.frameIndex = 2;
			Main.gameOver = true;
			chick.rigidbody.velocity = new Vector3(0, chick.rigidbody.velocity.y  - 9.8f*(Time.time - deadTime), 0);
		}

		
		//if (tWind > Main.windDuration || Main.WindDirection == "none")
		if (bWindAffected && tWind > Main.windDuration)
		{
			bWindAffected = false;
			tWind = 0;
		}
		
		// Reset the position according to interaction
		if (Chick_die == false){
			tmpX = startPos.x+(1f/((0.006f)+0.00005f*Time.time))* Mathf.Cos(tmpY*0.015f-k);
			if (tmpX  < - (Screen.width - chick.size.x)/2)
				tmpX = -(Screen.width - chick.size.x)/2;
			if (tmpX >  (Screen.width- chick.size.x)/2)
				tmpX = (Screen.width- chick.size.x)/2;
			chick.position = new Vector2(tmpX,tmpY);
		}
		
		if (chick.outOfView)
		{
		 	// Destroy the object
		 	Destroy(chick.gameObject);
		 	// Test Game Over function
			Application.LoadLevel(1);
		}
	}

	public void OnCollided(OTObject owner)
	{
	    OTObject obj = owner.collisionObject;  
		if(Chick_die == false && (obj.name == "Boat0" || obj.name == "Boat1"))
	    {
	    	landBoat = owner.collisionObject;
	    	landBoatPosX = landBoat.position.x;
	    	chick.frameIndex = 0;
	    	chick.rigidbody.velocity = new Vector3(0, 0, 0);
	    	chick.collidable = false; // no need enter OnCollided any more
			
			Main.animalSaved++;
	    } else if(Chick_die == false && landBoat == null
				&& (obj.name == "BackGround_Bottom"  ||  obj.name.StartsWith("Bee_left")
			||obj.name.StartsWith("Bee_right") ||obj.name.StartsWith("Maneater_plant")))
		{
			deadTime = Time.time;
			// show the Cow dead left object 
			chick.frameIndex = 2;
			// set the die flag
			Chick_die = true;
			chick.collidable = false; // no need enter OnCollided any more
	    }
	}
	
	void OnInput(OTObject owner)
	{
		//OT.print("OnInput " + owner.name);
		bWindAffected = true;
	}
	
	//function OnCollision(owner:OTObject)
	//{
	//	// a collision occured
	//	OT.print(owner.name+" collided with "+owner.collisionObject.name+" at "+owner.collision.contacts[0].point);
	//}
	
	//function onOutOfView(owner:OTObject)
	//{
	//    OT.DestroyObject(owner);
	//}
	
}
