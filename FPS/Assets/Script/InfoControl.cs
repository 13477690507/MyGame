using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoControl : UIBase {

    [SerializeField] InputField iptUserName;
    [SerializeField] InputField iptPassword;
    [SerializeField] InputField iptNick;
    [SerializeField] Button btnLogin;
    [SerializeField] Button btnRegion;
    

    protected override void OnStart()
    {
        btnLogin.onClick.AddListener(btnLoginClick);
        btnRegion.onClick.AddListener(btnRegionClick);
    }

    #region click
    void btnLoginClick()
    {//登录

       
    }

    void btnRegionClick()
    {
        string userName = iptUserName.text.Trim();
        string password = iptPassword.text.Trim();
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return;
        }

        string dataPack = (int)MessageType.Region + "," + userName + "," + password + "," + iptNick.text;
        NetMgr.Instance.Send(dataPack);
    }
    #endregion


    protected override void DoMessage(Msg msg)
    {
        Debug.Log("收到消息" + msg);
        if (msg.id == (int)MessageType.Region)
        {
            string[] msgArra = msg.content.Split(',');
            int result = int.Parse(msgArra[1]);
            if (result == 0)
            {
                Debug.Log("注册成功");
            }
            else if (result == 1)
            {
                Debug.Log("注册失败，用户名重复");
            }else
            {
                Debug.Log("注册失败");
            }
        }
    }

    protected override void Destroy()
    {
        btnLogin.onClick.RemoveListener(btnLoginClick);
        btnRegion.onClick.RemoveListener(btnRegionClick);
    }


}
