using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChitControl : UIBase {


    protected override void OnAwake()
    {
        Debug.Log("这是子类的awake");
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        Debug.Log("这是子类的update");
    }

    protected override void DoMessage(string msg)
    {
        Debug.Log(msg);
    }
}
