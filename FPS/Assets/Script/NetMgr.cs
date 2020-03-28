using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class NetMgr : MonoBehaviour
{
    //单例
    public static NetMgr Instance;

    //action无返回值的委托

    //连接回调
    public System.Action<HandleResult> ConnectionHandle;
    //接收到消息回调
    public System.Action<byte[]> RecvHandle;


    //客户端的socket对象
    Socket clinet;

    //服务器ip port
    IPAddress ip;
    int port;

    //接收数据的缓存，大小
    byte[] buffer;
    int size = 1024; //kb

    private void Awake()
    {
        //实例化数组
        buffer = new byte[size];
        Instance = this;
    }

    /// <summary>
    /// 初始化网络参数
    /// </summary>
    /// <param name="_ip"></param>
    /// <param name="_port"></param>
    public void InitNet(IPAddress _ip, int _port)
    {
        ip = _ip;
        port = _port;
    }

    /// <summary>
    /// 连接网络
    /// </summary>
    public void Connection()
    {
        try
        {
            //实例化socket对象
            clinet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint ep = new IPEndPoint(ip, port);
            clinet.Connect(ep);

            //处理接收消息
            clinet.BeginReceive(buffer,0,size,SocketFlags.None, RecvCallback,null);

            CallConnectionHandle(new HandleResult(0,string.Empty));
            //Debug.Log("连接成功");
        }
        catch (System.Exception ex)
        {
            //ex.message就是异常错误消息
            CallConnectionHandle(new HandleResult(100,ex.Message));
        }
    }


    void RecvCallback(System.IAsyncResult ar)
    {
        int len = clinet.EndReceive(ar);
        if (len < 1)
            return;

        //收到消息
        //System.Text.Encoding.UTF8.GetBytes(buffer, 0, len);
        if (RecvHandle != null)
        {
            byte[] temp = new byte[size];
            //复制数组
            System.Array.Copy(buffer, temp, len);
            RecvHandle(temp);
        }

        //继续处理接收消息
        clinet.BeginReceive(buffer, 0, size, SocketFlags.None, RecvCallback, null);
    }


    #region handle

    void CallConnectionHandle(HandleResult res)
    {
        if (ConnectionHandle != null)
        {
            ConnectionHandle(res);
        }
    }



    #endregion

    public void Close()
    {
        if (clinet == null || clinet.Connected == false)
            return;
        clinet.Shutdown(SocketShutdown.Both);
        clinet.Close();
    }
}


public class HandleResult
{
    public HandleResult(int _code, string _message) {
        this.code = _code;
        this.message = _message;

    }
    public int code { get; set; }
    public string message { get; set; }
}
