using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI基类
/// </summary>
public class UIBase : MonoBehaviour {

    //把接收到的消息，存到队列
    Queue<string> msgQueue = new Queue<string>();

    private void Awake()
    {
        //注册消息，当收到消息时，触发回调事件
        NetMgr.Instance.RecvHandle += RecvHandleCallback;
        OnAwake();
    }

    void RecvHandleCallback(byte[] buffer)
    {
        //把字节数组转换成字符串
        string message = System.Text.Encoding.UTF8.GetString(buffer);
        //把字符串消息加入到消息队列
        msgQueue.Enqueue(message);
    }


    void Start () {
        OnStart();
   
	}

	void Update () {
        OnUpdate();
        if (msgQueue.Count > 0) //消息队列里是否有消息
        {
            //取出一条消息
            //Dequeue向消息队列里取出一条消息
            DoMessage(msgQueue.Dequeue());
        }
	}


    protected virtual void DoMessage(string msg)
    {

    }

    private void OnDestroy()
    {
        NetMgr.Instance.RecvHandle -= RecvHandleCallback;
        Destroy();
    }

    protected virtual void OnAwake() { }

    protected virtual void OnStart() { }

    protected virtual void OnUpdate() { }

    protected virtual void Destroy() { }

}
