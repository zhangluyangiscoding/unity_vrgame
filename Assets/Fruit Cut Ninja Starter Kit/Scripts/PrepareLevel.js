//show couting down before game starts

#pragma strict
var GetReady : GameObject;
var GO : GameObject;
var started : boolean;
var fpsgo : GameObject;

function Awake() {
	//Set game time
	GetComponent(Timer).timeAvailable = SharedSettings.ConfigTime;
}

function PrepareRoutine()
{
yield WaitForSeconds(1.0);
GetReady.active = true;
yield WaitForSeconds(2.0);
GetReady.active = false;
GO.active = true;
GetComponent(Timer).RunTimer();
started = true;
yield WaitForSeconds(1.0);
GO.active = false;
}

function Start () {

	GameObject.Find("GUI/LevelName").GetComponent(GUIText).text = SharedSettings.LevelName[SharedSettings.LoadLevel-1]; 

	PrepareRoutine();
}


