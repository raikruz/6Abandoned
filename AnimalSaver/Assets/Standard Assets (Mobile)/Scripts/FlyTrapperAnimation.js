#pragma strict


var start= 0;
var elapsed=0;
var flyTrapper:OTSprite;  

function Start () {
	start=Time.time;
}

function Update () {
	elapsed=Time.time-start;
	flyTrapper = GetComponent(OTSprite);
	if(elapsed==0)
	{
		 audio.Play();
		 flyTrapper.frameIndex=0; 
	}
	else if(elapsed==2)
	{	
		 audio.Play();
		 flyTrapper.frameIndex=1; 
	}else if(elapsed==4)
	{	
		 audio.Play();
		 flyTrapper.frameIndex=2; 
	}
	else if(elapsed==6)
	{	
		 audio.Play();
		 flyTrapper.frameIndex=3; 
	}else if(elapsed==8)
	{	
		 audio.Play();
		 flyTrapper.frameIndex=4; 
	}	else if(elapsed==12)
	{	
		 audio.Play();
		 flyTrapper.frameIndex=5; 

	}
}


