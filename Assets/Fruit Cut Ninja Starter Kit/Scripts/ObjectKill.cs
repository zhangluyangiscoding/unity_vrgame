using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectKill : MonoBehaviour
{
    // Start is called before the first frame update
    bool killed = false;
    public GameObject[] prefab;
    public float scale = 1f;
    public void OnKill()
    {
        if (killed)
        {
            return;
        }
        foreach (GameObject go in prefab)
        {
            GameObject ins = Instantiate(go, transform.position, Random.rotation) as GameObject;
            Rigidbody rd = ins.GetComponent<Rigidbody>();
            if (rd != null)
            {
                //rd.velocity = Random.onUnitSphere + Vector3.up;
                rd.velocity = Random.onUnitSphere + Vector3.up;
                rd.AddTorque(Random.onUnitSphere * scale, ForceMode.Impulse);
            }

        }
        killed = true;
    }
}
