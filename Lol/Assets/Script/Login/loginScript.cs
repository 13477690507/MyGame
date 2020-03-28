   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginScript : MonoBehaviour {
    #region 登陆面板部分
    [SerializeField]
    private InputField accountInputField;
    [SerializeField]
    private InputField passwordInputField;
    #endregion

    [SerializeField]
    private Button loginGameButt;//登陆成功

    public GameObject regPlend;//进入注册面板


    #region 注册面板部分
    [SerializeField]
    private InputField regAccount;

    [SerializeField]
    private InputField rePass;

    [SerializeField]
    private InputField regNotarizePass;
    #endregion

    [SerializeField]
    private Button regButt;//注册成功


    
    
    public void loginOnClick()
    {
        if (accountInputField.text.Length ==0||accountInputField.text.Length>11)
        {
            WarrningManager.errors.Add("账号不合法");//为错误面板添加字符串。
            return;
        }
        if (passwordInputField.text.Length == 0 || passwordInputField.text.Length > 11)
        {
            WarrningManager.errors.Add("密码不合法");
            return;
        }
        //登陆成功并关闭登陆按钮
        loginGameButt.interactable = false;
    }
    public void RegOnClick()
    {
        regPlend.SetActive(true);
    }
    public void QuitReg()
    {
        regPlend.SetActive(false);
    }
    public void Reg()
    {
        if (regAccount.text.Length == 0 || regAccount.text.Length > 11)
        {
            WarrningManager.errors.Add("账号不合法");
            return;
        }
        if (rePass.text.Length == 0 || rePass.text.Length > 11)
        {
            WarrningManager.errors.Add("密码不合法");
            return;
        }
        if (!rePass.text.Equals(regNotarizePass.text))
        {
            WarrningManager.errors.Add("密码不一致");
            return;
        }
        //注册成功并关闭注册按钮。
        Debug.Log("注册成功");
        regButt.interactable = false;
    }

}
