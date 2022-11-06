using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime);
        //this.transform.Rotate(25 * Time.deltaTime, 0 ,0,Space.Self);
        this.transform.Rotate(50 * Time.deltaTime, 0, 0, Space.Self);
    }
}
