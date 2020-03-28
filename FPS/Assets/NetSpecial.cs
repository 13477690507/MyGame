using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetSpecial : NetworkBehaviour
{
    public MaterialType.MaterialTypeEnum MaterialType;
    private float time = 0;
    private float endtime = 2f;

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
