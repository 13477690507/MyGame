using UnityEngine;
using System.Collections;

public class CharectorAnimationControl : MonoBehaviour {
    [SerializeField] GameObject player;
    private Animator animator;
    public bool Istrue;
    float Yplace;
    void Start () {
        animator = this.GetComponent<Animator>();
        Yplace = transform.position.y;
        
    }

    void Update () {
        Move();
        Shoot();
        Slid();
        Rowover();
    }
    void AnimationControl()
    {
        
    }
    void Move()
    {
        player.transform.position += (player.transform.forward * 0.1f+ player.transform.right*Input.GetAxisRaw("Horizontal")*0.1f);
        
        //animator.SetFloat("vertical", ((Input.GetAxisRaw("Vertical")) +1)*0.5f);
        animator.SetFloat("horizental", ((Input.GetAxisRaw("Horizontal")) + 1) * 0.5f);
        //float Xmove = Input.GetAxis("Mouse X");

        Vector3 target = transform.position + new Vector3(0, 1.5f, 0);
        //transform.RotateAround(transform.up, Xmove*0.1f);
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("MouseClick", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("MouseClick", false);
        }
    }
    void Slid()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * -100);
            animator.SetBool("Sdown", true);
        }
        else
        {
            animator.SetBool("Sdown", false);
        }
    }
    void Rowover()
    {
        AnimatorStateInfo StateInfo = animator.GetCurrentAnimatorStateInfo(0);
        print(StateInfo.nameHash);
        if (Input.GetKeyDown(KeyCode.W)&&(transform.position.y- Yplace<0.1f))
        {
            
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
            animator.SetBool("Wdown", true);
        }
        else
        {
            animator.SetBool("Wdown", false);
        }
    }
}
