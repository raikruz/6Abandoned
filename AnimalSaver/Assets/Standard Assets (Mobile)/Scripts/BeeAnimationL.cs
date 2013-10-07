using UnityEngine;
using System.Collections;

public class BeeAnimationL : MonoBehaviour {
	
	float start=0;
	int elapsedAnimation=0;
	OTSprite bee; 
	// Use this for initialization
	void Start () {
		start=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedAnimation++;
		bee = GetComponent<OTSprite>();
	
		if(elapsedAnimation%3==0)
		{
			 bee.frameIndex = 1;
		}
		else if(elapsedAnimation%3==1)
		{	
			bee.frameIndex = 2;
		}else if(elapsedAnimation%3==2)
		{	
			bee.frameIndex = 0;
		}
	}
}
