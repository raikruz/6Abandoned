﻿#pragma strict

var normalTxt : Texture2D;
var hoverTxt : Texture2D;

// mouse enter function
function OnMouseEnter()
{
	guiTexture.texture = hoverTxt;
}
// mouse hover exit function
function OnMouseExit()
{
	guiTexture.texture = normalTxt;
}

// mouse down function
function OnMouseDown()
{
	// play sound
	audio.Play();
	// exit the game
	Application.Quit();
}