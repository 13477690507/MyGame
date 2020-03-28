using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireFlash : NetworkBehaviour
{
    private float time = 0;
    private float endtime = 1f;

    void Start()
    {

    }

    [ServerCallback]
    void Update()
    {
        time += Time.deltaTime;
        if (time > endtime)
        {
            NetworkServer.Destroy(gameObject);
        }

    }
}
