//fruit dispenser - dispense fruits with differend speed and time and gravitation

#pragma strict
var fruits : GameObject[];
var bomb : GameObject;

var z : float;
var sfx : AudioClip;
var pause : boolean = false;
var timer : float = 3.0;
private var started : boolean = false;
private var powerMod : float;

function Awake() {

if (SharedSettings.LoadLevel==1) {
	Random.seed = 356355;
	Physics.gravity.y = -2;
	powerMod = 1.0;
}
if (SharedSettings.LoadLevel==2) {
	Random.seed = 12411245;
	Physics.gravity.y = -3;
	powerMod = 0.825;
}
if (SharedSettings.LoadLevel==3) {
	Random.seed = 898979;
	Physics.gravity.y = -4;
	powerMod = 0.70;
}
if (SharedSettings.LoadLevel==4) {
	Random.seed = 64223459;
	Physics.gravity.y = -5;
	powerMod = 0.625; 
}

}

function Update() {

if (pause) return;

timer -= Time.deltaTime;

if (timer<=0.0 && !started) {
timer = 0.0;
started = true;
}

if (started) {

if (SharedSettings.LoadLevel==1) {
	if (timer<=0.0) {
		FireUp();
		timer = 2.0;
	}
}

if (SharedSettings.LoadLevel==2) {
	if (timer<=0.0) {
		FireUp();
		timer = 1.75;
	}
}

if (SharedSettings.LoadLevel==3) {
	if (timer<=0.0) {
		FireUp();
		timer = 1.5;
	}
}

if (SharedSettings.LoadLevel==4) {
	if (timer<=0.0) {
		FireUp();
		timer = 1.0;
	}
}

}

}

function Spawn(isbomb : boolean) {

	var x : float = Random.Range(-4.5,4.5);
	z += 1;
	if (z>=5.0) z = 0;
	var ins : GameObject;
	
	if (!isbomb) 
	ins = GameObject.Instantiate(fruits[Random.Range(0,fruits.length)],transform.position + Vector3(x,0,z),Random.rotation);
	else
	ins = GameObject.Instantiate(bomb,transform.position + Vector3(x,0,z),Random.rotation);

	var power = Random.Range(1.5,1.8) * -Physics.gravity.y * 1.5 * powerMod;

	var direction = Vector3(-x * 0.05 * Random.Range(0.3,0.8),1,0);
	direction.z = 0.0;

	ins.rigidbody.velocity =  direction * power;
	
	audio.PlayOneShot(sfx,1.0);
	ins.rigidbody.AddTorque(Random.onUnitSphere * 0.1,ForceMode.Impulse);

}

function FireUp () {

	if (pause) return;

	Spawn(false);
	
	if (SharedSettings.LoadLevel==2 && Random.Range(0,10)<2) {
		Spawn(false);
	}

	if (SharedSettings.LoadLevel==3 && Random.Range(0,10)<4) {
		Spawn(false);
	}

	if (SharedSettings.LoadLevel==4 && Random.Range(0,10)<4) {
		Spawn(false);
	}
	if (SharedSettings.LoadLevel==4 && Random.Range(0,10)<2) {
		Spawn(false);
	}

	//bomby
	if (SharedSettings.LoadLevel==2 && Random.Range(0,100)<10) {
		Spawn(true);
	}
	if (SharedSettings.LoadLevel==3 && Random.Range(0,100)<20) {
		Spawn(true);
	}
	if (SharedSettings.LoadLevel==4 && Random.Range(0,100)<30) {
		Spawn(true);
	}


}

    function OnTriggerEnter (other : Collider) {
        Destroy(other.gameObject);
    }