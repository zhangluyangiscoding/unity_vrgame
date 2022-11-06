using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class knife : MonoBehaviour
{
    Vector2 screenInp;
    bool fire = false;
    bool fire_prev = false;
    bool fire_down = false;
    bool fire_up = false;
    public LineRenderer trail;
    Vector2 start, end;
    Vector3[] trailPositions = new Vector3[10];
    int index;
    float lineTimer = 1.0f;
    int linePart = 0;
    public int points;
    float trail_alpha = 0f;
    int raycastCount = 10;
    bool started = false;
    public GameObject[] splashPrefab;
    public GameObject[] splashFlatPrefab;
    RaycastHit hit;
    Collision collision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "hammer bloody (1)" || collision.gameObject.name == "hammer bloody")
        {
            if (this.gameObject.tag != "destroyed")
            {
                //Generate the part of the cut fruit
                this.gameObject.GetComponent<ObjectKill>().OnKill();

                //Delete the cut fruit
                Destroy(this.gameObject);


                //Prepare resources for broken fruits
                if (this.tag == "red") index = 0;
                if (this.tag == "yellow") index = 1;
                if (this.tag == "green") index = 2;
                //Fruit juice effect
                Vector3 splashPoint =transform.position;
                splashPoint.z = 4;
                Instantiate(splashPrefab[index], splashPoint, Quaternion.identity);
                splashPoint.z += 4;
                Instantiate(splashFlatPrefab[index], splashPoint, Quaternion.identity);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
