//change GUIText color

#pragma strict
var color : Color;

function Awake () {

GetComponent(GUIText).material.color = color;

}