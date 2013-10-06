﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Chick : MonoBehaviour {
	
	OTSprite chick;  
	bool Chick_die = false;
	OTObject landBoat;  
	float landBoatPosX;  
	Vector3 startPos;
	float k;
	// Use this for initialization
	void Start () {
		landBoat = null;
		landBoatPosX = 0f;
		
		// Get this chick's sprite
		chick = GetComponent<OTSprite>();
		chick.InitCallBacks(this);
		chick.frameIndex = 0;
		// initialize variables
		// initialize the velocity and start position
	 	chick.rigidbody.isKinematic = false;
		// initialize the velocity as 25 in -y direction
		chick.rigidbody.velocity = new Vector3(0, -15,0);
		
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
			// respond to the left arrow
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				chick.frameIndex = 1;
				startPos.x += -30 * Time.deltaTime;
				audio.Play();
			}
			// respond to the right arrow
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				chick.frameIndex = 3;
				startPos.x += 30 * Time.deltaTime;
				audio.Play();
			}
			// default down picture
			else 
			{
				chick.frameIndex = 0;
			}
			tmpX = startPos.x+(1f/((0.006f)+0.00005f*Time.time))* Mathf.Cos(tmpY*0.015f-k);
		
			// determine the border of the object's movement
			if(Mathf.Abs(gameObject.transform.position.x) > ((Screen.width - chick.size.x)/2))
			{
				tmpX = startPos.x-(1f/((0.006f)+0.00005f*Time.time))* Mathf.Cos(tmpY*0.015f-k);
			}
			// Reset the position according to interaction
			chick.position = new Vector2(tmpX,tmpY);
		} // end if (chick_die == false)
		else //Chick_die == true
		{
			//chick.position = new Vector2(tmpX,tmpY- 0.5f*9.8f*(Time.deltaTime)*(Time.deltaTime));
		}
		if (chick.outOfView)
		{
		 	// Destroy the object
		 	Destroy(chick.gameObject);
		 	// Test Game Over function
			Application.LoadLevel(1);
		}
	}
	public void OnStay(OTObject owner)
	{
	    OTObject obj = owner.collisionObject;  
		if(Chick_die == false
				&& (obj.name == "BackGround_Bottom" )
				&& (obj.position.y+obj.size.y/2) >= (chick.position.y-chick.size.y/2))
		{
			//landWater = owner.collisionObject;
	    	// show the Cow dead left object 
			chick.frameIndex = 2;
			// set the die flag
			Chick_die = true;
			chick.collidable = false; // no need enter onStay any more
	    }
		else if(Chick_die == false
	     	&& (obj.name == "Boat0" || obj.name == "Boat1") 
	 		&& (obj.position.x+obj.size.x/2) >= (chick.position.x+chick.size.x/2)
	 		&& (obj.position.x-obj.size.x/2) <= (chick.position.x-chick.size.x/2)
	 		&& (obj.position.y+obj.size.y/2) >= (chick.position.y-chick.size.y/6))
	    {
	    	landBoat = owner.collisionObject;
	    	landBoatPosX = landBoat.position.x;
	    	//chick.depth = 0.5; // on back 
	    	chick.rigidbody.velocity = new Vector3(0, 0, 0);
	    	chick.collidable = false; // no need enter onStay any more
	    } 
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