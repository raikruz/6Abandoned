using UnityEngine;
using System.Collections;

public class Cow : MonoBehaviour {
	
	OTSprite cow;  
	bool Cow_die = false;
	OTObject landWater;
	OTObject landBoat;  
	float landBoatPosX;  
	Vector3 startPos;
	float k;
	Vector3 mouseDownPosition;
	// Use this for initialization
	void Start () {
		landWater = null;
		landBoat = null;
		landBoatPosX = 0f;
		
		// Get this chick's sprite
		cow = GetComponent<OTSprite>();
		cow.InitCallBacks(this);
		cow.frameIndex = 1;
		// initialize variables
		// initialize the velocity and start position
	 	cow.rigidbody.isKinematic = false;
		// initialize the velocity as 25 in -y direction
		cow.rigidbody.velocity = new Vector3(0, -25,0);
		
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
			cow.position = new Vector2(tmpX,tmpY);
			if (cow.outOfView)
			{
			 // Destroy the object
			 Destroy(cow.gameObject);
			}
			return;
		} else if (landWater != null) { 
			 // if it lands in water, let it die
			 cow.frameIndex = 0;
			 // set the die flag
			 Cow_die = true;
			 //cow.depth = 0.5; // on back
			 if (cow.outOfView)
			{
			 // Destroy the object
			 Destroy(cow.gameObject);
			}  
			return;
		}
		
		//This one is global detection
		//if (Input.GetMouseButtonDown(0))
		//{
	   	//	mouseDownPosition = Input.mousePosition;
	   	//	OT.print("Left Button down On at " + Input.mousePosition.x + "," + Input.mousePosition.y);
		//}
	  	//else if (Input.GetMouseButtonUp(0))
		//{
	   	//	OT.print("Left Button up On at " + Input.mousePosition.x + "," + Input.mousePosition.y);
		//}
		
		// respond to the left arrow
		if(Input.GetKey(KeyCode.LeftArrow) && Cow_die == false)
		{
			cow.frameIndex = 2;
			//transform.Translate(-30 * Time.deltaTime, 0, 0);
			startPos.x += -30 * Time.deltaTime;
		}
		// respond to the right arrow
		else if(Input.GetKey(KeyCode.RightArrow) && Cow_die == false)
		{
			cow.frameIndex = 3;
			//transform.Translate(30 * Time.deltaTime, 0, 0);
			startPos.x += 30 * Time.deltaTime;
		}
		// default down picture
		else if(Cow_die == false)
		{
			cow.frameIndex = 1;
		}
		// Reset the position according to interaction
		if (Cow_die == false){
			tmpX = startPos.x+(1f/((0.012f)+0.00005f*Time.time))* Mathf.Cos(tmpY*0.015f-k);
			cow.position = new Vector2(tmpX,tmpY);
		}
		
	
		// determine the border of the object's movement
		// the middle x position is: 
		if(cow.position.x  < - (Screen.width - cow.size.x)/2)
		{
			// show the Cow dead object 
			cow.frameIndex = 0;
			// set the die flag
			Cow_die = true;
		}
		else if(cow.position.x >  (Screen.width- cow.size.x)/2)
		{
			// show the Cow dead right object 
			cow.frameIndex = 4;
			//cow.depth += 0.5; // on back 
			// set the die flag
			Cow_die = true;
		} 
		if (cow.outOfView)
		{
		 	// Destroy the object
		 	Destroy(cow.gameObject);
		 	// Test Game Over function
			Application.LoadLevel(2);
	
		}
	}
	
	public void OnStay(OTObject owner)
	{
		OTObject obj = owner.collisionObject;  
		//var box:BoxCollider= null;
		//	box = GetComponent(BoxCollider);
		if(Cow_die == false
	     	&& (obj.name == "Boat0" || obj.name == "Boat1") 
	 		&& (obj.position.x+obj.size.x/2) >= (cow.position.x+cow.size.x/2)
	 		&& (obj.position.x-obj.size.x/2) <= (cow.position.x-cow.size.x/2)
	 		&& (obj.position.y+obj.size.y/2) >= (cow.position.y-cow.size.y/6))
	    {
	    	landBoat = owner.collisionObject;
	    	landBoatPosX = landBoat.position.x;
	    	//cow.depth = 0.5; // on back 
	    	cow.rigidbody.velocity = new Vector3(0, 0, 0);
	    	cow.collidable = false; // no need enter onStay any more
	    } 
	//    else if(collide with Water){
	//    	// show the Cow dead left object 
	//		cow.frameIndex = 0;
	//		//Cow.depth += 0.5; // on back 
	//		// set the die flag
	//		cow_die = true;
	//		cow.depth = 0.5; // on back  
	//		cow.collidable = false; // no need enter onStay any more
	//    }
	}
	//Move to event to main window so that mouse does not need click on object percisely
	//This one is local detection. set register input to true in designer
	//function OnInput(owner:OTObject):void
	//{
	//   	if (Input.GetMouseButtonDown(0))
	//	{
	//    	mouseDownPosition = Input.mousePosition;
	//    	OT.print("Left Button down On "+owner.name + " at " + mouseDownPosition.x + "," + mouseDownPosition.y);
	//	}
	//   	else if (Input.GetMouseButtonUp(0))
	//	{
	//    	OT.print("Left Button up On "+owner.name + " at " + mouseDownPosition.x + "," + mouseDownPosition.y);
	//	}
	//}
	//function onOutOfView(owner:OTObject)
	//{
	//    OT.DestroyObject(owner);
	//}
}
