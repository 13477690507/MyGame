using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DeathController : NetworkBehaviour
{
    public static GameObject player;
    private float time;
    private float relifeTime = 6f;
    private GameObject[] a;


    void Start()
    {
        a = GameObject.FindGameObjectsWithTag("StartPosition");
    }

    
    void Update()
    {
        if (player == null)
            return;
        if (player.activeSelf == false)
        {

            time += Time.deltaTime;
            if (time > relifeTime)
            {
                CmdSetActive();
                time = 0;
            }
        }
    }

    public static void FindPlayer(GameObject go)
    {
        player = go;
    }

    [Command]
    void CmdSetActive()
    {
        RpcSetActive();
    }

    [ClientRpc]
    void RpcSetActive()
    {
        player.SetActive(true);
        print(player);
        player.GetComponent<BloodController>().HP = 100;
        player.transform.position = a[Random.Range(0, a.Length - 1)].transform.position;
    }

}

