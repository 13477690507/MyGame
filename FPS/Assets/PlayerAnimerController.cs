using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerAnimerController : NetworkBehaviour
{
    Animator Player;
    Text BulletNum;
    public static bool ReloadingEnd = true;

    private int Num;

    void Start()
    {
        BulletNum = GameObject.Find("BulletNum").GetComponent<Text>();
        Player = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        if (isLocalPlayer)
        {
            Num = int.Parse(BulletNum.text);
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x == 0 && y == 0)
            {
                Player.SetInteger("MoveSate", 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Player.SetInteger("MoveSate", 2);
                }
                else
                {
                    Player.SetInteger("MoveSate", 1);
                }
            }
            //死亡
            if (gameObject.GetComponent<BloodController>().HP <= 0)
            {
                Player.SetBool("Death", true);
            }
            //跳跃
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.SetTrigger("IsJump");
            }
            //换弹
            if ((Input.GetKeyDown(KeyCode.R) || (Num == 0 && Input.GetMouseButton(0) && Input.GetMouseButton(1))) && FPSFireManager.BulletAmount != 0)
            {
                Player.SetBool("Relaoding", true);
                ReloadingEnd = false;
            }
            //瞄准
            if (Input.GetMouseButton(1))
            {
                Player.SetBool("ShotAim", true);
                if (Input.GetMouseButton(0) && Num > 0)
                {
                    Player.SetBool("Fire", true);
                }
                else
                {
                    Player.SetBool("Fire", false);
                }
            }
            else
            {
                Player.SetBool("Fire", false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                Player.SetBool("ShotAim", false);
            }
        }
    }




    //换弹
    public void Reloading()
    {
        if (FPSFireManager.BulletAmount >= (30 - FPSFireManager.BulletCount))
        {
            FPSFireManager.BulletAmount -= (30 - FPSFireManager.BulletCount);
            FPSFireManager.BulletCount = 30;
        }
        else
        {
            FPSFireManager.BulletCount += FPSFireManager.BulletAmount;
            FPSFireManager.BulletAmount = 0;
        }
        if (isLocalPlayer)
        {
            Player.SetBool("Relaoding", false);
            ReloadingEnd = true;
        }
    }
}
