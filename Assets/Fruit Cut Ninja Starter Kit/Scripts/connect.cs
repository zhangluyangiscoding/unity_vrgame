using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine.UI;

public class connect : MonoBehaviour
{
    private int number;
    public GameObject Righthand;
    public GameObject Lefthand;
    // Start is called before the first frame update
    public Socket clientSocket;
    public string ipAddress = "127.0.0.1";
    public int portNumber = 8700;
    public int feedflag = 0;
    static public string feedmsg = "";
    public float connectInterval = 1;  // 链接间隔时间
    public float connectTime = 0;      // 当前链接时间
    public int connectCount = 0;       // 链接次数
    public bool isConnecting = false;  // 是否在连接中
    public float[] angle = new float[5];//用作全局变量的数组
    public bool status = true;         //开关判定
    private int isCanOpen = 1;
    public int selectRange = 0;
    //退出
    public GameObject escbtn;
    bool isStop = false;
    private bool isOK = false;
    bool isESC = false;
    bool isdaoju = false;
    bool isnaodian = false;
    bool isnaodianOK = false;
    bool isnaodianRE = false;
    public bool isrecive = false;
    //是否第一次接收到服务器的消息
    public bool isFirstReceive = false;
    // 是否第一次播放视频
    public bool isFirstVedio = true;
    // 是否可以播放
    DateTime videoStart = DateTime.Now;
    
    //第一次发送start
    public int is_start_sended = 0;

    public byte[] buffer;
    public byte[] buffer_st;
    
    void Start()
    {
        //调用开始连接
        Console.WriteLine("hello");
        ConnectedToServer();

    }

    // Update is called once per frame
    void Update()
    {
        SelectVideo();
        Debug.Log("connect.cs update ~~~ 2");
        ReceiveFormServer();
        //isFirstReceive = true;十分奇怪的改动 不知道为啥就可以运行
    }

    void OnDisable()
    {
        SendMessageToServer("stop");
    }

    void SelectVideo()
    {
        // 获取时间间隔，大于4s发送 flag=true
        int spanTime = GetSubSeconds(videoStart, DateTime.Now);
        Debug.Log("connect.cs update spanTime: " + spanTime + " isFirstVideo: " + isFirstVedio + 
            " isWriteData:" + GameObject.Find("hammer bloody").GetComponent<knifeRotate>().isWriteData);

        if (GameObject.Find("hammer bloody").GetComponent<knifeRotate>().isWriteData)
        {
            feedmsg = "";
            GameObject.Find("hammer bloody").GetComponent<knifeRotate>().isWriteData = false;
        }

        if (isFirstVedio || spanTime > 5)
        {
            int number = UnityEngine.Random.Range(0, 2);
            if (number == 0)
            {
                //selectRange = number;
                Righthand.SetActive(true);
                SendMessageToServer("start");
                is_start_sended = 1;
            }
            else
            {
                // selectRange = number;
                Lefthand.SetActive(true);
                SendMessageToServer("start");
                is_start_sended = 1;
            }
            videoStart = DateTime.Now;
            isFirstVedio = false;
        }
    }

    // --------------------
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

        // 返回间隔秒数（不算差的分钟和小时等，仅返回秒与秒之间的差）
        //return subTimer.Seconds;

        //返回相差时长（算上分、时的差值，返回相差的总秒数）
        return (int)subTimer.TotalSeconds;
    }
    // --------------------

    /// <summary>
    /// 链接到服务器
    /// </summary>
    public void ConnectedToServer()
    {
        connectCount++;//链接次数增加
        isConnecting = true;
        Debug.Log("这是第" + connectCount + "次链接");
        //如果客户端不为空
        if (clientSocket != null)
        {
            try
            {
                Debug.Log("客户端关闭");
                //断开连接，释放资源
                clientSocket.Disconnect(false);
                clientSocket.Close();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        //创建新的链接(固定格式)
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("这是第" + connectCount + "次链接" + "创建新的链接");

        //设置端口号和ip地址
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
        Debug.Log("这是第" + connectCount + "次链接" + "设置端口号和ip地址：" + ipAddress + " -- " + portNumber);

        //发起链接
        clientSocket.BeginConnect(endPoint, OnConnectCallBack, "");
        Debug.Log("这是第" + connectCount + "次链接" + "发起链接");
    }
    /// <summary>
    /// 开始链接的回调
    /// </summary>
    /// <param name="ar"></param>
    public void OnConnectCallBack(IAsyncResult ar)
    {
        Debug.Log("链接开始！！！！");
        if (clientSocket.Connected)
        {
            Debug.Log("链接成功");
            connectCount = 0;
            ReceiveFormServer();  //开启收消息
        }
        else
        {
            connectTime = 0;   //计时重置
        }
        isConnecting = false;
        //结束链接
        clientSocket.EndConnect(ar);
    }
    /// <summary>
    /// 从服务器接收消息
    /// </summary>
    public void ReceiveFormServer()
    {
        
        //定义缓冲池
        buffer = new byte[4];
        clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveFormServerCallBack, buffer);
        if (!isFirstReceive)
        {
           isrecive = false;
        }
    }
    public void ReceiveFormServerCallBack(IAsyncResult ar)//只在收到数据时触发
    {
        //结束接收
        int length = clientSocket.EndReceive(ar);
        byte[] buffer = (byte[])ar.AsyncState;
        Debug.Log("已重新读入缓冲区");
        isrecive = true;//判断这一次是从服务器接收到的消息，而不是接收上一次的消息
        isFirstReceive = true;
        Debug.Log("isrecive " + isrecive);
        Debug.Log("connect.vs get msg from server --- buffer: " + buffer);
        //将接收的数据转为字符串
        if (buffer != null)
        {
            feedmsg = System.Text.Encoding.UTF8.GetString(buffer, 0, length);
            //Debug.Log("connect.vs get msg from server --- buffer: " + buffer);
            Debug.Log("connect.vs get msg from server --- buffer_feedmsg: " + feedmsg);
            if (feedmsg.Trim() != "")
            {
                feedflag++;
                Debug.Log("feedflag: " + feedflag);
                if (int.Parse(feedmsg) >= 100)
                {
                    feedmsg = "0";
                    feedflag = 0;
                    isnaodian = false;
                    isnaodianOK = true; Debug.Log("成功");
                    isCanOpen++;
                }
                else if (feedflag > 500)
                {
                    feedmsg = "0";
                    feedflag = 0;
                    isnaodian = false;
                    isnaodianRE = true; Debug.Log("再来");
                }
                else
                {
                    ReceiveFormServer();   //开启下一次接收消息
                }
            }
            //滞空缓冲区
            //Array.Clear(buffer, 0, length);
            Debug.Log("已清空缓冲区");
            //feedmsg = "3";
            //Debug.Log("缓冲区更新后是" + buffer.ToString());
            buffer = null;
        } else
        {
            feedmsg = "3";
        }
        isFirstReceive = false;
    }
    /// <summary>
    /// 向服务器发送字符串信息
    /// </summary>
    /// <param name="msg"></param>
    public void SendMessageToServer(string msg)
    {
        //将字符串转成byte数组
        byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(msg);
        clientSocket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, SendMassageCallBack, 1);
    }
    /// <summary>
    /// 发送信息的回调
    /// </summary>
    /// <param name="ar"></param>
    public void SendMassageCallBack(IAsyncResult ar)
    {
        //关闭消息发送
        int length = clientSocket.EndSend(ar);
        Debug.Log("start已发送");
    }

}
