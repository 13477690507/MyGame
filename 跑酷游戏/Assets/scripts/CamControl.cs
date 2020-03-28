using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
    public GameObject player;
    void Start () {
        
    }

    void Update () {
        Move();


    }
   
    void Move()
    {
        float Xmove = Input.GetAxis("Mouse X");
        float Ymove = Input.GetAxis("Mouse Y");
        Vector3 dir =Vector3.Normalize( transform.position - player.transform.position);
        Vector3 norup = Vector3.Normalize(player.transform.up);
        Vector3 temp = transform.position;
        if (Vector3.Dot(dir, norup) >= 0.8)
        {
            transform.RotateAround(player.transform.position, player.transform.right, -0.2f);
        }
        else if (Vector3.Dot(dir, norup) <= 0.1)
        {
            transform.RotateAround(player.transform.position, player.transform.right, 0.2f);
        }
        else
        {
            transform.RotateAround(player.transform.position, player.transform.right, Ymove);
        }
        //transform.RotateAround(player.transform.position, player.transform.up, Xmove);
    }
}
