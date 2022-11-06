using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class NewKnifeRotate1: MonoBehaviour
{
    Vector3 pos = new Vector3(2.084f, 0.13f, 4.304f);
    // Start is called before the first frame update
    Transform weizhi;
    float start = 0f;
    public float speed = 1000f;
    // xxxxxx
    int a = 0;
    //string judgeLeft = "3";
    //int judgeLeft = 0;
    Quaternion ababa;

    // 是否第一次播放视频
    bool isrecv;
    bool isFirstVedio = true;
    // 是否可以播放
    DateTime videoStart = DateTime.Now;
    // 读取数据
    public bool isWriteData = false;
    public int spanTime;
    public float ttime = 0f;
    public float timee = 0f;
    public bool judge_down = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        judge_down = false;
        // if (isFirstVedio == false)
        isrecv = GameObject.Find("Socket").GetComponent<connect>().isrecive;
        Debug.Log("isrecv: " + isrecv);
        String judgeLeft;
        spanTime = GetSubSeconds(videoStart, DateTime.Now);
        if (!isrecv)
        {
            judgeLeft = "3";
        }
        else
        {
            judgeLeft = connect.feedmsg.Trim();
        }
        //GameObject.Find("Socket").GetComponent<connect>().feedmsg.Trim();
        //Debug.Log("knifeRotate.cs FixedUpdate spanTime: " + spanTime + " isFirstVideo: " + isFirstVedio + " judgeLeft: " + judgeLeft);
        // judgeLeft = connect.feedmsg.Trim();
        if (Input.GetKey(KeyCode.A))
        //if (isFirstVedio || spanTime >= 6)
        {
            isFirstVedio = false;
            //int spanTime = GetSubSeconds(videoStart, DateTime.Now);
            //judgeLeft = connect.feedmsg.Trim();
            //Debug.Log("knifeRotate.cs FixedUpdate --- judegLeft: " + judgeLeft + " a: " + a + " isWriteData" + isWriteData);
            //judgeLeft = "3";

            //videoStart = DateTime.Now;
            
                if (a == 0)
                {
                    transform.RotateAround(pos, new Vector3(1f, 0f, 0f), -5f * Time.deltaTime * 1000);
                    // transform.RotateAround(pos, new Vector3(1f, 0f, 0f), Time.deltaTime * 7000);

                    a = 1;
                    videoStart = DateTime.Now;
                }
                //isFirstVedio = true;
        

            
        }


        //if (a == 1)//锤子平面移动
        //{
        //    transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        //    //transform.Translate(-speed * Time.deltaTime * 100, 0, 0);
        //    ttime += Time.deltaTime;
        //    if (ttime > 0.8f)
        //    {
        //        a = 2;
        //        ttime = 0f;
        //    }
        //}

        //if (a == 2)//锤子平面返回
        //{
        //    transform.Translate(Vector3.right * (-speed) * Time.deltaTime, Space.World);
        //    //transform.Translate(speed * Time.deltaTime * 100, 0, 0);
        //    timee += Time.deltaTime;
        //    if (timee > 0.8f)
        //    {
        //        a = 3;
        //        timee = 0f;
        //    }
        //}


        if (a == 1)
        {
            if (start <= 0.4f)
            {
                start += Time.deltaTime;
            }
            if (start > 0.4f)
            {
                //
                transform.RotateAround(pos, new Vector3(1f, 0f, 0f), 5f * Time.deltaTime * 1000);
                //transform.rotation = weizhi.rotation;
                start = 0;
                isrecv = false;
                judge_down = true;
                a = 0;
            }

        }
    }

    public int GetSubSeconds(DateTime startTimer, DateTime endTimer)
    {
        TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

        TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

        TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

        // 返回间隔秒数（不算差的分钟和小时等，仅返回秒与秒之间的差）
        //return subTimer.Seconds;

        //返回相差时长（算上分、时的差值，返回相差的总秒数）
        return (int)subTimer.TotalSeconds;
    }
}