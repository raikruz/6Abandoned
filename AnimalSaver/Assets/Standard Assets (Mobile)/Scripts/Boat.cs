using UnityEngine;
using System.Collections;

public class Boat : MonoBehaviour {

	// Use this for initialization
	OTSprite sprite;
	void Start () {
		sprite = GetComponent<OTSprite>();
		sprite.InitCallBacks(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollision(OTObject owner)
	{
		// a collision occured
		OT.print(owner.name+" collided with "+owner.collisionObject.name+" at "+owner.collision.contacts[0].point);
	}
}
