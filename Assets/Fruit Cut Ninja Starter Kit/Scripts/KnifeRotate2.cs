using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
public class KnifeRotate2 : MonoBehaviour
{
    Vector3 pos = new Vector3(2.084f, 0.13f, 4.304f);
    // Start is called before the first frame update
    Transform weizhi;
    float start = 0f;
    // xxxxxx
    int a = 0;
    int judgeLeft = 0;
    Quaternion ababa;

    // 是否第一次播放视频
    bool isFirstVedio = true;
    // 是否可以播放
    DateTime videoStart = DateTime.Now;
    // 读取数据
    public bool isWriteData = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int spanTime = GetSubSeconds(videoStart, DateTime.Now);
        string judgeLeft = connect.feedmsg.Trim();
        //GameObject.Find("Socket").GetComponent<connect>().feedmsg.Trim();
        Debug.Log("knifeRotate.cs FixedUpdate spanTime: " + spanTime + " isFirstVideo: " + isFirstVedio + " judgeLeft: " + judgeLeft);
        if (isFirstVedio || spanTime > 6)
        {
            judgeLeft = connect.feedmsg.Trim();
            if (judgeLeft == "2")
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