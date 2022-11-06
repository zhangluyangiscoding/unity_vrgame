// make text glow - example from pause menu

#pragma strict

function Update () {

GetComponent(GUIText).material.color.a = Mathf.PingPong(Time.time,0.5)+0.5;

}