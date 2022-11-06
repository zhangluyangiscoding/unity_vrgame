//some help class to make coding easier :)

static function ScrRect(x : float, y : float, wd : float, ht : float) : Rect
	{
	return Rect(x*Screen.width, y*Screen.height, wd*Screen.width, ht*Screen.height);
	}

static function ScrRect(x : float, y : float, size : float, xratio : float, yratio : float) : Rect
	{
	return Rect(x*Screen.width, y*Screen.height, size*Screen.width*xratio, size*Screen.width*yratio);
	}
		
static function ScrPos(x : float, y : float, size : float) : Vector2
	{
	return Vector2(x*Screen.width + size*Screen.width * 0.5f, y*Screen.height + size*Screen.width*0.5f);
	}	

static function ScrRect(x : float, y : float, size : float) : Rect
	{
	return Rect(x*Screen.width, y*Screen.height, size*Screen.width, size*Screen.width);
	}
	
static function ScrRectCenter(x : float, y : float, size : float) : Rect
	{
	return Rect(x*Screen.width - size*Screen.width * 0.5f, y*Screen.height - size*Screen.height * 0.5f, size*Screen.width, size*Screen.width);
	}	
	
static function ScrRectCenter(x : float, y : float, xsize : float, ysize : float) : Rect
	{
	return Rect(x*Screen.width - xsize*Screen.width * 0.5f, y*Screen.height - ysize*Screen.height * 0.5f, xsize*Screen.width, ysize*Screen.width);
	}		
	
static function ScrRectCenter2(x : float, y : float, xsize : float, ysize : float) : Rect
	{
	return Rect(x*Screen.width - xsize*Screen.width * 0.5f, y*Screen.height - ysize*Screen.height * 0.5f, xsize*Screen.width, ysize*Screen.height);
	}		
	
static function PlayAudioClip2D (clip : AudioClip, pitch : float, volume : float) {
    var go = new GameObject ("Fast Audio 2D");
    var source : AudioSource = go.AddComponent (AudioSource);
    source.pitch = pitch;
    source.clip = clip;
    source.volume = volume;
    source.Play ();
    Destroy (go, clip.length);
    return source;
}