using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;


public class Timer : MonoBehaviour
{
    bool run = false;
    bool showTimeLeft = true;
    bool timeEnd = false;
    float startTime = 0.0f;
    float curTime = 0.0f;
    string curStrTime = string.Empty;
    bool pause = false;
    public float timeAvailable = 30f;
    public float showTime = 0;
    public Text guiTimer;
    public GameObject finishedUI;
    // Start is called before the first frame update
    public string ipAddress = "127.0.0.1";
    public int portNumber = 8700;
    private Socket clientSocket;
    private int connectCount;
    private bool isConnecting;
    private int connectTime;
    private string feedmsg;
    private int feedflag;
    private bool isnaodian;
    private bool isnaodianRE;
    private bool isnaodianOK;
    private int isCanOpen;

    /// <summary>
    /// 链接到服务器
    /// </summary>
    //public void ConnectedToServer()
    //{
    //    connectCount++;//链接次数增加
    //    isConnecting = true;
    //    Debug.Log("这是第" + connectCount + "次链接");
    //    //如果客户端不为空
    //    if (clientSocket != null)
    //    {
    //        try
    //        {
    //            Debug.Log("客户端关闭");
    //            //断开连接，释放资源
    //            clientSocket.Disconnect(false);
    //            clientSocket.Close();
    //        }
    //        catch (System.Exception e)
    //        {
    //            Debug.Log(e.ToString());
    //        }
    //    }

    //    //创建新的链接(固定格式)
    //    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    //    //设置端口号和ip地址
    //    EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);

    //    //发起链接
    //    clientSocket.BeginConnect(endPoint, OnConnectCallBack, "");
    //}

    ///// <summary>
    ///// 开始链接的回调
    ///// </summary>
    ///// <param name="ar"></param>
    //public void OnConnectCallBack(IAsyncResult ar)
    //{
    //    Debug.Log("链接开始！！！！");
    //    if (clientSocket.Connected)
    //    {
    //        Debug.Log("链接成功");
    //        connectCount = 0;
    //        ReceiveFormServer();  //开启收消息
    //    }
    //    else
    //    {
    //        connectTime = 0;   //计时重置
    //    }
    //    isConnecting = false;
    //    //结束链接
    //    clientSocket.EndConnect(ar);
    //}

    

    ///// <summary>
    ///// 从服务器接收消息
    ///// </summary>
    //public void ReceiveFormServer()
    //{
    //    //定义缓冲池
    //    byte[] buffer = new byte[512];
    //    clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveFormServerCallBack, buffer);
    //}
    ///// <summary>
    ///// 信息接收方法的回调
    ///// </summary>
    ///// <param name="ar"></param>
    //public void ReceiveFormServerCallBack(IAsyncResult ar)
    //{
    //    //结束接收
    //    int length = clientSocket.EndReceive(ar);
    //    byte[] buffer = (byte[])ar.AsyncState;
    //    //将接收的数据转为字符串
    //    feedmsg = System.Text.Encoding.UTF8.GetString(buffer, 0, length);
    //    feedflag++;
    //    Debug.Log("接受到的计算指标是：" + feedflag + '次' + feedmsg);

    //    if (int.Parse(feedmsg) >= 100)
    //    {
    //        feedmsg = "0";
    //        feedflag = 0;
    //        isnaodian = false;
    //        isnaodianOK = true; Debug.Log("成功");
    //        isCanOpen++;
    //    }
    //    else if (feedflag > 500)
    //    {
    //        feedmsg = "0";
    //        feedflag = 0;
    //        isnaodian = false;
    //        isnaodianRE = true; Debug.Log("再来");
    //    }
    //    else
    //    {
    //        ReceiveFormServer();   //开启下一次接收消息
    //    }


    //}
    ///// <summary>
    ///// 向服务器发送字符串信息
    ///// </summary>
    ///// <param name="msg"></param>
    //public void SendMessageToServer(string msg)
    //{
    //    //将字符串转成byte数组
    //    byte[] msgBytes = System.Text.Encoding.UTF8.GetBytes(msg);
    //    clientSocket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, SendMassageCallBack, 1);
    //}
    ///// <summary>
    ///// 发送信息的回调
    ///// </summary>
    ///// <param name="ar"></param>
    //public void SendMassageCallBack(IAsyncResult ar)
    //{
    //    //关闭消息发送
    //    int length = clientSocket.EndSend(ar);
    //    Debug.Log("start已发送");
    //}
    void Start()
    {
        //ConnectedToServer();
        RunTimer();
    }
    public void RunTimer()
    {
        run = true;
        startTime = Time.time;

    }
    public void PauseTimer(bool b)
    {
        pause = b;
    }
    public void EndTimer()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(pause)
        {
            startTime = startTime + Time.deltaTime;
            return;
        }
        if (run)
        {
            curTime = Time.time - startTime;

        }
        if(showTimeLeft)
        {
            showTime = timeAvailable - curTime;
            if(showTime<=0)
            {
                timeEnd = true;
                showTime = 0;
                //Application.Quit();
                //弹出界面，告诉用户游戏结束并且暂停游戏
                finishedUI.SetActive(true);
            }
            int minutes = (int)(showTime / 60);
            int seconds = (int)(showTime % 60);
            int fraction = (int)((showTime * 100) % 100);
            curStrTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            guiTimer.text = "Time:" + curStrTime;

        }
    }
}
