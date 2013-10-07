using UnityEngine;
using System.Collections;

public class BeeMovementR : MonoBehaviour {
	float positionX;
	float positionY;
	double start=0;
	int level =1;
	double speedFactor;
	int elapsed = 0;
	OTSprite bee;  
	// Use this for initialization
	void Start () {
		start=Time.time;
		speedFactor=0.1;
		bee = GetComponent<OTSprite>();
		for(var i=1;i<level;i++)
		{
				speedFactor = speedFactor*1.5;
		}	
	}
	
	// Update is called once per frame
	void Update () {
		positionX = bee.position.x;
		positionY = bee.position.y;
		positionX++;
		bee.position = new Vector2(positionX,positionY);
	}
}
