using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class LoginControl : MonoBehaviour {


    [SerializeField] InputField iptIP;
    [SerializeField] InputField iptPort;
    [SerializeField] Button btnConnection;
    [SerializeField] Text txtErrorMsg;
    [SerializeField] GameObject chitInstance;


    void Start () {
        
        //btnConnection.onClick.AddListener(()=> {
        //    NetMgr.Instance.Connection();
        //});
        btnConnection.onClick.AddListener(ConnectionClick);
        NetMgr.Instance.ConnectionHandle += ConnectionHandleCallback;
    }


    #region callback
    void ConnectionHandleCallback(HandleResult res) {
        if (res.code == 0)
        {
            GameObject chitPanel = GameObject.Instantiate(chitInstance);
            chitPanel.transform.SetParent(CommonUI.UIRoot, false);
            chitPanel.transform.localPosition = Vector3.zero;
            Destroy(gameObject);
            Debug.Log("登录成功");
        }
        else
        {
            Debug.LogError(string.Format("登录失败，错误码：{0},消息：{1}",res.code,res.message));
        }
    }
    #endregion


    void ConnectionClick()
    {
        if (string.IsNullOrEmpty(iptIP.text) ||
            string.IsNullOrEmpty(iptPort.text))
        {
            Debug.LogWarning("ip,port均不能为空");
            return;
        }
        InitArg();
        //连接服务器
        NetMgr.Instance.Connection();
    }

    void InitArg()
    {
        IPAddress ip = IPAddress.Parse(iptIP.text);
        int port = int.Parse(iptPort.text);
        NetMgr.Instance.InitNet(ip,port);
    }
	
	
	void Update () {
		
	}

    private void OnDestroy()
    {
        //当前场景销毁移除事件
        btnConnection.onClick.RemoveListener(ConnectionClick);

        //关闭连接
        NetMgr.Instance.Close();
    }
}
