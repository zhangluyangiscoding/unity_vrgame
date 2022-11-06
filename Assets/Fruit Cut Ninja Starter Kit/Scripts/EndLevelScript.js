//after time out - this scripts block game and show finish screen

#pragma strict
var done : boolean = false;
var completed : boolean = false;
var finishedGUI : GameObject;
var guiPoints : GUIText;
var mouseControl : MouseControl;

function Awake () {
	guiPoints = GameObject.Find("GUI/Points").GetComponent(GUIText);	
	mouseControl = GetComponent(MouseControl);
}

function LateUpdate () {

	guiPoints.text = "Points: "+mouseControl.points.ToString();
		
	if (done && !completed) {
		GameObject.Find("FruitDispenser").GetComponent(FruitDispenser).pause = true;
		completed = true;
		GameObject.Find("MainScripts").GetComponent(Timer).EndTimer();
		GameObject.Find("GUI").SetActiveRecursively(false);
		finishedGUI.SetActiveRecursively(true);
		GetComponent(MouseControl).enabled = false;
		GameObject.Find("FinishedGUI/Timer").GetComponent(GUIText).text = "Points : "+mouseControl.points.ToString();
		
	}



}

