// Use this on a guiText or guiTexture object to automatically have them
// adjust their aspect ratio when the game starts.

var m_NativeRatio = 1.3333333333333;
    
function Awake() {    
var currentRatio = (Screen.width+0.0) / Screen.height;
transform.localScale.x *= m_NativeRatio / currentRatio;
}