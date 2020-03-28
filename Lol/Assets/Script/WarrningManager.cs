using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WarrningManager : MonoBehaviour {
    //将字符串添加到列表
    public static List<string> errors = new List<string>();
    [SerializeField]
    private Warrningtext windowtext;
    void Update()
    {
        if (errors.Count>0)
        {
            string err = errors[0];//将添加的第一位列表的字符串付给err
            errors.RemoveAt(0);//删除第一位列表字符串
            windowtext.active(err);//将err传入错误面板
        }
    }
	
}
