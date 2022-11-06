using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class knifeRotate : MonoBehaviour
{

    Vector3 pos = new Vector3(2.084f, 0.13f, 4.304f);
    // Start is called before the first frame update
    Transform weizhi;
    float start = 0f;
    // xxxxxx
    int a = 0;
    int judgeLeft = 0;
    Quaternion ababa;

    // Whether to play the video for the first time
    bool isFirstVedio = true;
    // Whether it can be played
    DateTime videoStart = DateTime.Now;
    // Read Data
    public bool isWriteData = false;

    void Start()
    {
        //weizhi = gameObject.GetComponent<Transform>();
        ababa = this.transform.rotation;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the time interval. If it is greater than 4s, send flag=true
        int spanTime = GetSubSeconds(videoStart, DateTime.Now);
        string judgeLeft = connect.feedmsg.Trim();
        Debug.Log("knifeRotate.cs FixedUpdate spanTime: " + spanTime + " isFirstVideo: " + isFirstVedio + " judgeLeft: " + judgeLeft);
        if (isFirstVedio || spanTime > 6)
        {
            judgeLeft = connect.feedmsg.Trim();
            if (judgeLeft == "1")
            {
                isWriteData = true;
                Debug.Log("knifeRotate.cs FixedUpdate --- judegLeft: " + judgeLeft + " a: " + a + " isWriteData" + isWriteData);
                if (a == 0)
                {
                    transform.RotateAround(pos, new Vector3(1f, 0f, 0f), -5f * Time.deltaTime * 1000);
                    // transform.RotateAround(pos, new Vector3(1f, 0f, 0f), Time.deltaTime * 7000);
                    a = 1;
                }
            }

            videoStart = DateTime.Now;
            isFirstVedio = false;
        }

        if (a == 1)
        {
            if (start <= 0.4f)
            {
                start += Time.deltaTime;
            }
            if (start > 0.4f)
            {
                transform.RotateAround(pos, new Vector3(1f, 0f, 0f), 5f * Time.deltaTime * 1000);
                //transform.rotation = weizhi.rotation;
                start = 0;
                a = 0;
            }
        }



        //if (!balala)
        //{
        //    string judgeLeft = GameObject.Find("Socket").GetComponent<connect>().feedmsg.Trim();
        //    if (judgeLeft == "1")
        //    {
        //        Debug.Log("knifeRotate.cs FixedUpdate --- judegLeft: " + judgeLeft + " a: " + a);
        //        balala = true;
        //    }
        //} else
        //{
        //    if (a == 0)
        //    {
        //        transform.RotateAround(pos, new Vector3(1f, 0f, 0f), -5f * Time.deltaTime * 1000);
        //        // transform.RotateAround(pos, new Vector3(1f, 0f, 0f), Time.deltaTime * 7000);
        //        a = 1;
        //    }
        //    if (a == 1)
        //    {
        //        if (start <= 0.4f)
        //        {
        //            start += Time.deltaTime;
        //        }
        //        if (start > 0.4f)
        //        {
        //            transform.RotateAround(pos, new Vector3(1f, 0f, 0f), 5f * Time.deltaTime * 1000);
        //            //transform.rotation = weizhi.rotation;
        //            start = 0;
        //            a = 0;
        //        }

        //    }
        //    balala = false;
        //}

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Rotate(new Vector3(0, 30*Time.deltaTime, 0));
            //this.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up * Time.deltaTime);
            //transform.RotateAround(new Vector3(-1.696f, 1.144f, 4.589f), new Vector3(1f, 0f, 0f), -5f*Time.deltaTime*100);

        }
    }

    /// <summary>
    /// 获取间隔秒数
    /// </summary>
    /// <param name="startTimer"></param>
    /// <param name="endTimer"></param>
    /// <returns></returns>
    public int GetSubSeconds(DateTime startTimer, DateTime endTimer)
    {
        TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

        TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

        TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

        // Returns the interval seconds (minutes and hours without difference, only the difference between seconds)
        //return subTimer.Seconds;

        //Return the difference duration (calculate the difference between minutes and hours, and return the total seconds of difference)
        return (int)subTimer.TotalSeconds;
    }
}
