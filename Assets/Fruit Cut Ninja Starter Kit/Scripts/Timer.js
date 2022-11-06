//timer script - can count or countdown (originaly used as countdown)

#pragma strict
var run : boolean = false;
var end : boolean = true;
var showTimeLeft : boolean = false;
var timeAvailable : float = 120; 
var timeEnd : boolean;

var startTime : float;
var endTime : float;
var curTime : float;
var curStrTime : String;
var pause : boolean = false;

var guiTimer : GUIText;

function Awake() {
	guiTimer = GameObject.Find("GUI/Timer").GetComponent(GUIText);
}

function PauseTimer(bool : boolean)
{
	pause = bool;
}

function EndTimer() {
if (end) return;
run = false;
end = true;
endTime = Time.time;

curTime = endTime - startTime;

   var minutes : int = curTime / 60;
   var seconds : int = curTime % 60;
   var fraction : int = (curTime * 100) % 100;

   curStrTime = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction); 

}

function RunTimer() {
	run = true;
	end = false;
	startTime = Time.time;
}

function Update () {

	if (pause) {
		startTime = startTime + (Time.deltaTime);
		return;
	}	

if (run) {
   curTime = Time.time - startTime;
   } else {
   curTime = endTime - startTime;
   }

   var showTime = curTime;

   if (showTimeLeft) {
   	showTime = timeAvailable - curTime;
   	if (showTime<=0) {
   	timeEnd = true;
   	showTime = 0;
   	GetComponent(EndLevelScript).done = true;
   	}
  
   }

   var minutes : int = showTime / 60;
   var seconds : int = showTime % 60;
   var fraction : int = (showTime * 100) % 100;

   curStrTime = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction); 
   
guiTimer.text = "Time : "+curStrTime;    


}