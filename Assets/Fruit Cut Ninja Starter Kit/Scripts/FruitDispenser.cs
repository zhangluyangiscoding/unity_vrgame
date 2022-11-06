using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDispenser : MonoBehaviour
{
    
    public GameObject[] fruits;
    public GameObject bomb;
    public float y;
    public float powerScale;
    public bool pause = false;
    bool started = false;
    public float timer=2.5f;
    public int if_start_sended = 0;
    //判断是否是第一次接收到
    public bool is_first_start = false;

    public bool is_really_reieve;
    public bool is_really_reieve1;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pause) return;
        timer -= Time.deltaTime;
        if(timer<=0&&!started)
        {
            timer = 0f;
            started = true;
        }
        if(started)
        {
            if (SharedSetting.LoadLevel == 0)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 1f;//水果出现的间隔
                }
            }
            else
            if (SharedSetting.LoadLevel == 1)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 2.0f;
                }
            }
            else
            if (SharedSetting.LoadLevel == 2)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 1.75f;
                }
            }
            else
            if (SharedSetting.LoadLevel == 3)
            {
                if (timer <= 0)
                {
                    FireUp();
                    timer = 1.5f;
                }
            }
        }
        //Spawn(false);
        //GameObject go = GameObject.Find("ins");

    }
    void FireUp()
    {
        if (pause) return;
        Spawn(false);
        if(SharedSetting.LoadLevel==2&&Random.Range(0,10)<2)
        {
            Spawn(false);

        }
        if (SharedSetting.LoadLevel == 3 && Random.Range(0, 10) < 4)
        {
            Spawn(false);

        }
        if (SharedSetting.LoadLevel == 1 && Random.Range(0, 100) < 10)
        {
            Spawn(true);

        }
        if (SharedSetting.LoadLevel == 2 && Random.Range(0, 100) < 20)
        {
            Spawn(true);

        }
        if (SharedSetting.LoadLevel == 3 && Random.Range(0, 100) < 30)
        {
            Spawn(true);

        }

    }

     
   
    void Spawn(bool isBomb)
    {
        int Done;
        
        //float x = Random.Range(1.15f, 1.15f);
        //z = Random.Range(2.17f, 2.17f);
        float x = 1.15f;
        y = 1.7f;
        GameObject ins;


        if_start_sended = GameObject.Find("Socket").GetComponent<connect>().is_start_sended;
        is_really_reieve = GameObject.Find("hammer bloody (1)").GetComponent<NewKnifeRotate2>().judge_down;
        is_really_reieve1 = GameObject.Find("hammer bloody").GetComponent<NewKnifeRotate1>().judge_down;
        Debug.Log("is_really_reieve" + is_really_reieve);

        ins = Instantiate(fruits[Random.Range(0, fruits.Length)], transform.position + new Vector3(x, y, 1.33f), Quaternion.Euler(0.0f, 2.0f, 0.0f)) as GameObject;
        is_first_start = true;
       

      

    }

    //void OnTriggerEnter(Collider other)
    //void OnTriggerEnter(Collider other)
    //{
    //    Destroy(other.gameObject);
        
    //}

  
}
