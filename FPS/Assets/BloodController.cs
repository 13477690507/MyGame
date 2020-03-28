using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class BloodController : NetworkBehaviour
{
    private Slider bloodBG;
    private Slider blood;

    public int HP = 100;

    private float relifeTime = 8f;
    private float time = 0;
   

   
    private void Start()
    {
        if (isLocalPlayer)
        {
            bloodBG = transform.Find("Canvas/Panel/BloodBG").GetComponent<Slider>();
            blood = transform.Find("Canvas/Panel/Blood").GetComponent<Slider>();
            DeathController.FindPlayer(gameObject);
        }

        

    }

    // [ServerCallback]
    void Update()
    {
        if (blood == null)
            return;
        blood.value = HP;
        if (bloodBG.value > blood.value)
        {
            bloodBG.value -= 0.2f;
        }
        else
        {
            bloodBG.value = blood.value;
        }
        if (HP <= 0)
        {
            CmdDestroy();
        }

    }

    [Command]
    void CmdDestroy()
    {
        RpcDestroy();
    }

    [ClientRpc]
    void RpcDestroy()
    {
        GameObject DeathBody = Instantiate(Resources.Load<GameObject>("DeathBody"));
        DeathBody.transform.position = gameObject.transform.position - Vector3.up;
        gameObject.SetActive(false);
    }


}
