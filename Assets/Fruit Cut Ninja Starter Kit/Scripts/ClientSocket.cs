using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.Assertions;
using System.Threading;


public class ClientSocketD
{
    //每个数据包都有一个2byte表示长度的头部
    const int PACKAGE_LEN_BYTE = 2;

    Socket m_socket = null;
    byte[] m_receiveBuffer = new byte[1 << (PACKAGE_LEN_BYTE * 2) + 2];
    int m_receiveBufUsed = 0;

    //这里定义数据包的分发函数, 如解密、拆箱成对象、分发对应处理函数
    public delegate void Dispatch(byte[] szData);
    Dispatch m_dispatch = null;

    static private ClientSocketD m_instance;

    private ClientSocketD()
    {
    }

    static public ClientSocketD GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = new ClientSocketD();
        }
        return m_instance;
    }
    public bool IsConnect()
    {
        return m_socket == null;
    }

    public void Init(string server, int port, Dispatch handler = null)
    {
        m_dispatch = handler;
        IPHostEntry hostEntry = null;
        hostEntry = Dns.GetHostEntry(server);


        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, port);
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                m_socket = tempSocket;
                Debug.Log("connect success");
                break;
            }
            else
            {
                continue;
            }
        }
        return;
    }

    static int GetPackLen(byte[] szData)
    {
        return ((int)szData[0] << 4) + szData[1];
    }
    //打包，在数据包开头用两字节记录包的大小。
    static byte[] PackBig2(byte[] data)
    {
        int iLen = data.Length;
        Assert.IsFalse(iLen <= 0 || iLen > (1 << PACKAGE_LEN_BYTE * 8), "C pack too big");

        byte[] szPackage = new byte[PACKAGE_LEN_BYTE + iLen];
        szPackage[0] = (byte)((iLen & 0xf0) >> 4);
        szPackage[1] = (byte)((iLen & 0x0f));
        Array.Copy(data, 0, szPackage, PACKAGE_LEN_BYTE, iLen);

        return szPackage;
    }

    //解包
    public void UnPackDispatch()
    {
        if (m_receiveBufUsed > PACKAGE_LEN_BYTE)
        {
            int len = GetPackLen(m_receiveBuffer);
            int start = 0;
            while (len + PACKAGE_LEN_BYTE <= m_receiveBufUsed - start)
            {
                byte[] onePackage = new byte[len];
                Array.Copy(m_receiveBuffer, start + PACKAGE_LEN_BYTE, onePackage, 0, len);

                {
                    Debug.Log("Client: recv DATA=======================");
                    Debug.Log(Encoding.UTF8.GetString(onePackage));
                    Debug.Log("Client: recv END=======================");
                    if (m_dispatch != null)
                    {
                        m_dispatch(onePackage);
                    }
                }

                start = start + PACKAGE_LEN_BYTE + len;
            }
            if (start > 0)
            {
                Assert.IsTrue(m_receiveBufUsed - start >= 0, "client unknow error");
                if (m_receiveBufUsed - start != 0)
                {
                    Array.Copy(m_receiveBuffer, start, m_receiveBuffer, 0, m_receiveBufUsed - start);
                }
                m_receiveBufUsed -= start;
            }

        }
    }

    //异步发送
    public int SendAsync(byte[] data)
    {
        byte[] package = PackBig2(data);
        m_socket.BeginSend(package, 0, package.Length, SocketFlags.None, OnSend, package);
        return 0;
    }

    //异步接收
    public int ReceiveAsync()
    {
        if (null == m_socket)
        {
            Debug.LogError("C no init");
        }
        // 这个回调参数可定义成任意对象
        string strData = "receiveRecall";
        m_socket.BeginReceive(m_receiveBuffer, m_receiveBufUsed, m_receiveBuffer.Length - m_receiveBufUsed, SocketFlags.None, OnReceive, strData);
        return 0;
    }

    // 发送完成recall
    private void OnSend(IAsyncResult data)
    {
        byte[] bytesSend = (byte[])data.AsyncState;
        //获取得发送成功的字节数
        int iSendLen = m_socket.EndSend(data);
        Debug.Log(string.Format("C OnSend: sendLen {0:d},{1:d}", iSendLen, bytesSend.Length));
        Assert.IsTrue(bytesSend.Length == iSendLen, "net busy");
    }
    // 接收到缓存后recall
    private void OnReceive(IAsyncResult data)
    {
        // 可以看到每次线程id和主线程不同
        Debug.Log("C threadId:" + Thread.CurrentThread.ManagedThreadId.ToString());

        try
        {
            string msg = (string)data.AsyncState;
            //获取新接收数据的字节数, 小于等于BeginReceive指定的字节数
            int iReceiveLen = m_socket.EndReceive(data);

            if (iReceiveLen > 0)
            {

                m_receiveBufUsed += iReceiveLen;
                Debug.Log(string.Format("C recall {0:d},{1:d}", msg, iReceiveLen));
                UnPackDispatch();
            }
            else
            {

                Debug.Log("C wait================\n");
            }

            //缓冲区接收不了比他大的数据包，这个包指的是自己写的PackBig2打的包(服务器那边的)
            int LeftBuffLen = m_receiveBuffer.Length - m_receiveBufUsed;
            Assert.IsTrue(LeftBuffLen > 0, "C package too large" + LeftBuffLen);
            m_socket.BeginReceive(m_receiveBuffer, m_receiveBufUsed, LeftBuffLen, SocketFlags.None, OnReceive, "receiveRecall");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}

public class ClientSocket : MonoBehaviour
{
    private ClientSocketD m_clientSocketD = null;
    public int port = 8700;

    private float m_fPassTime = 0;//计时器
    public float m_fInterval = 4;//间隔时间
    public  int judge;//左右手判断

    // Start is called before the first frame update

    void Start()
    {
        m_clientSocketD = ClientSocketD.GetInstance();
        m_clientSocketD.Init("192.168.1.173", 8700);

        m_clientSocketD.SendAsync(Encoding.UTF8.GetBytes("start"));
        //m_clientSocketD.ReceiveAsync();
        int judge = m_clientSocketD.ReceiveAsync();
    }

    // Update is called once per frame


    void Update()
    {
        if (m_fPassTime > m_fInterval)
        {
            Debug.Log("C main threadId:" + Thread.CurrentThread.ManagedThreadId.ToString());

            m_fPassTime = 0;
            //每个4秒向matalb重新发送“start”，进行下一轮游戏
            m_clientSocketD.SendAsync(Encoding.UTF8.GetBytes("start" ));
            int judge = m_clientSocketD.ReceiveAsync();
        }
        m_fPassTime += Time.deltaTime;
    }
}
