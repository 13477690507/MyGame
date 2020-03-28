using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//报错窗口的控制
public class Warrningtext : MonoBehaviour {
    [SerializeField]
    private Text text;
    public void active(string Value)//传入使它打开面板的消息。
    {
        text.text = Value;
        gameObject.SetActive(true);//打开面板
    }
	public void close()
    {
        gameObject.SetActive(false);//关闭面板
    }
	
	
}
