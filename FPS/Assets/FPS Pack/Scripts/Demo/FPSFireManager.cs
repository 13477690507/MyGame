using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FPSFireManager : NetworkBehaviour
{
    public static bool IsShoot = false;
    //public ImpactInfo[] ImpactElemets = new ImpactInfo[0];

    public float BulletDistance = 100;//射程
                                      // public GameObject ImpactEffect;//特效
                                      // public GameObject FireFlash;//枪口火焰
    private Text BulletNum;  //弹夹子弹数
    public static int BulletCount = 30; //默认30发子弹
    private Text BulletLeft; //子弹总数
    public static int BulletAmount = 90;
    private float time;

    private Text MyScoreText;
    private int MyScore = 0;

    private void Start()
    {
        MyScoreText = GameObject.Find("MyScore").GetComponent<Text>();
        BulletNum = GameObject.Find("BulletNum").GetComponent<Text>();
        BulletLeft = GameObject.Find("BulletLeft").GetComponent<Text>();

    }
    void Update()
    {
        MyScoreText.text = MyScore.ToString();
        BulletNum.text = BulletCount.ToString();
        BulletLeft.text = BulletAmount.ToString();
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && PlayerAnimerController.ReloadingEnd)
        {
            if (time >= 0.1f)
            {
                CmdFireFlash();
                if (BulletCount > 0)
                {
                    BulletNum.color = Color.red;
                    BulletCount--;
                    time = 0;
                    IsShoot = true;
                    RaycastHit hit;

                    var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width, Screen.height) * 0.5f);
                    if (Physics.Raycast(ray, out hit, BulletDistance))
                    {

                        if (hit.collider.gameObject.GetComponent<MaterialType>().TypeOfMaterial == MaterialType.MaterialTypeEnum.Head)
                        {
                            CmdHP(hit.collider.gameObject.GetComponentInParent<NetworkIdentity>().netId, 80, hit.point, 20);
                        }
                        else
                        if (hit.collider.gameObject.GetComponent<MaterialType>().TypeOfMaterial == MaterialType.MaterialTypeEnum.Body)
                        {
                            CmdHP(hit.collider.gameObject.GetComponent<NetworkIdentity>().netId, 20, hit.point, 10);
                        }
                        else
                        {
                            CmdGetImpactEffect(hit.collider.gameObject, hit.point);
                        }


                        // var effectIstance = Instantiate(effect, hit.point, new Quaternion()) as GameObject;
                        //ImpactEffect.SetActive(false);
                        //ImpactEffect.SetActive(true);
                        //effectIstance.transform.LookAt(hit.point + hit.normal);
                        //Destroy(effectIstance, 4);
                    }
                }

            }
            else
            {
                time += Time.deltaTime;

            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || BulletCount <= 0 || !PlayerAnimerController.ReloadingEnd)
        {
            IsShoot = false;
            BulletNum.color = Color.white;
        }
    }

    //服务器生成枪口火焰
    [Command]
    void CmdFireFlash()
    {
        GameObject fire = Instantiate(Resources.Load<GameObject>("MuzzleFlash9"));
        fire.transform.position = transform.Find("Player1/Rifle/fire").position;
        fire.transform.forward = -transform.Find("Player1/Rifle").forward;
        NetworkServer.Spawn(fire);
    }

    //向服务器发送打到物体的netid,以及打到后服务器所需要执行的所有操作
    [Command]
    void CmdHP(NetworkInstanceId netID,int attack, Vector3 hitPos,int Score)
    {
        RpcHP(NetworkServer.FindLocalObject(netID),attack, hitPos, Score);
    }

    [ClientRpc]
    void RpcHP(GameObject go,int attack, Vector3 hitPos, int Score)
    {
        CmdGetImpactEffect(go, hitPos);
        if (go.GetComponent<BloodController>() != null)
        {
            go.GetComponent<BloodController>().HP -= attack;
            if (go.GetComponent<BloodController>().HP <= 0) {
                MyScore += Score;
            }
        }
       
    }


    [System.Serializable]
    public class ImpactInfo
    {
        public MaterialType.MaterialTypeEnum MaterialType;
        public GameObject ImpactEffect;
    }

    [Command]
    void CmdGetImpactEffect(GameObject impactedGameObject, Vector3 hit)
    {
       
        GameObject[] ImpactElemets = Resources.LoadAll<GameObject>("Impact");
        MaterialType materialType = impactedGameObject.GetComponent<MaterialType>();
        if (materialType != null)
        {
            foreach (var impactInfo in ImpactElemets)
            {
                if (impactInfo.GetComponent<NetSpecial>().MaterialType == materialType.TypeOfMaterial)
                {
                    GameObject go = Instantiate(impactInfo);
                    go.transform.localScale = Vector3.one;
                    go.transform.position = hit;
                    NetworkServer.Spawn(go);
                }
            }
        }
    }
}
