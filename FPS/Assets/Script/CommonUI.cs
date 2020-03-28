using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具类
/// </summary>
public class CommonUI : MonoBehaviour
{


    static Transform uiRoot;
    /// <summary>
    /// 找到UI的GameObject
    /// </summary>
	public static Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                uiRoot = GameObject.Find("Canvas").transform;
            }

            return uiRoot;
        }
    }
}
