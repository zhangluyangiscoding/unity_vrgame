using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.005f;
    GameObject ins;
    float perTime = 1f;
    float start = 0f;
    int number;
    public GameObject prefab;
    public bool is_really_reieve;
    public bool is_really_reieve1;
    public float timee = 0f;
    bool if_chip = false;
    // Update is called once per frame

    private void Start()
    {
        //number = 1;
        number = Random.Range(0, 2);
    }
    void Update()
    {
        start += Time.deltaTime;
        if (start >= perTime)
        {
            //transform.Translate(0, 0, -speed * Time.deltaTime);
            if (number == 0)
            {
                if (start >= 4f)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                }
                else
                {
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
            }
            else if (number == 1)
            {
                if (start >= 4f)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                }
                else
                {
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
            }
        }
        is_really_reieve = GameObject.Find("hammer bloody (1)").GetComponent<NewKnifeRotate2>().judge_down;
        is_really_reieve1 = GameObject.Find("hammer bloody").GetComponent<NewKnifeRotate1>().judge_down;
        //if (is_really_reieve || is_really_reieve1)
        //{
        //    if_chip = true;
        //}

        //if(if_chip == true)
        //{
        //    timee += Time.deltaTime;
        //    if (timee > 1f)
        //    {
        //        GameObject.Destroy(prefab);
        //        if_chip = false;
        //        timee = 0f;
        //    }
        //}

        // transform.Translate(0,0, speed * Time.deltaTime);
        //    ins.GetComponent<Rigidbody>().AddForce(new Vector3(speed,0,0));
    }

}
