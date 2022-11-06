//used in fruits - after destroying - create some effects from prefabs

#pragma strict

var killed : boolean = false;
var prefab : GameObject[];

function Kill () {

if (killed) return;

killed = true;

for (var p in prefab) {
	var ins = GameObject.Instantiate(p,transform.position,Random.rotation);
	if (ins.rigidbody) {
		ins.rigidbody.velocity =  Random.onUnitSphere + Vector3.up;	
		//audio.PlayOneShot(sfx,1.0);
		ins.rigidbody.AddTorque(Random.onUnitSphere * 1,ForceMode.Impulse);
	}
}

}

