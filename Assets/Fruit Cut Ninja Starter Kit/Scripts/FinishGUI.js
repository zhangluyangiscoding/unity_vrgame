//gui script for finish gui window

#pragma strict
var skin : GUISkin;

function LoadMenu()
{
/*
		yield WaitForSeconds(1.0);
		Application.LoadLevel("menu");	
*/		
}

function RestartLevel()
{
		yield WaitForSeconds(1.0);
		Application.LoadLevel("game");	
}

function NextLevel()
{
		yield WaitForSeconds(1.0);
		SharedSettings.LoadLevel = SharedSettings.LoadLevel + 1;
		Application.LoadLevel("game");	
}

function OnGUI() {

GUI.skin = skin;

if (GUI.Button(HelpClass.ScrRectCenter2(0.5,0.6,0.3,0.075),"Menu")) {
LoadMenu();
}

if (GUI.Button(HelpClass.ScrRectCenter2(0.5,0.7,0.3,0.075),"Restart")) {
RestartLevel();
}
}