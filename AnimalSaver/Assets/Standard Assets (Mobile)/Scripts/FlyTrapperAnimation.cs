using UnityEngine;
using System.Collections;

public class FlyTrapperAnimation : MonoBehaviour {
	 float start= 0;
	float elapsed=0;
	OTSprite flyTrapper;
	// Use this for initialization
	void Start () {
		start=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		elapsed=Time.time-start;
		flyTrapper = GetComponent<OTSprite>();
		if(elapsed<0.5)
		{
			 audio.Play();
			 flyTrapper.frameIndex=0; 
		}
		else if(elapsed<1)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=1; 
		}else if(elapsed<1.5)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=2; 
		}
		else if(elapsed<2)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=3; 
		}else if(elapsed<2.5)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=5; 
		}	else if(elapsed<3)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=4; 
			flyTrapper.collidable=true;
	
		} else if(elapsed<8)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=5;
			flyTrapper.collidable=false;
		}else if(elapsed<8.5)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=3; 
	
		}else if(elapsed<9)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=2; 
	
		}else if(elapsed<9.5)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=1; 
	
		}else if(elapsed<10)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=0; 
	
		}else
		{
			 	Destroy(flyTrapper.gameObject);
				Main.decreaseObstacle(2);

		}
	}
}
