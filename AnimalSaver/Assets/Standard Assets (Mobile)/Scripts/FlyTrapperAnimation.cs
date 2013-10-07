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
		if(elapsed<2)
		{
			 audio.Play();
			 flyTrapper.frameIndex=0; 
		}
		else if(elapsed<4)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=1; 
		}else if(elapsed<6)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=2; 
		}
		else if(elapsed<8)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=3; 
		}else if(elapsed<12)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=4; 
		}	else if(elapsed<14)
		{	
			 audio.Play();
			 flyTrapper.frameIndex=5; 
	
		} else if(elapsed<16)
			{	
			 audio.Play();
			 flyTrapper.frameIndex=6; 
	
		}else
		{
			 	Destroy(flyTrapper.gameObject);
		}
	}
}
