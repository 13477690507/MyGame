using UnityEngine;
using UnityEngine.UI;
public class PamcanMove : MonoBehaviour
{



    public float speed = 0.01f;
    //float moveX = 0;
    //float moveY = 0;
    //public Vector2 fristPos;
    //public Vector2 topos;
    
    private Vector2 dest = Vector2.zero;
    public Image up;
    public Image down;
    public Image left;
    public Image right;
    Vector2 dir;
   
    Vector3 upposition;
    Vector3 dowmposition;
    Vector3 leftposition;
    Vector3 rightposition;
    private void Start()
    {
        upposition= (Vector3)up.GetComponent<RectTransform>().position;
        dowmposition = down.GetComponent<RectTransform>().position;
        leftposition = left.GetComponent<RectTransform>().position;
        rightposition = right.GetComponent<RectTransform>().position;
        dest = transform.position;
        dir = Vector3.zero;
        
    }
    
    public void FixedUpdate()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (Vector3.Magnitude(Input.mousePosition - upposition) <= 60)
            {
                dest = (Vector2)transform.position + Vector2.up*0.5f;
                dir = Vector2.up;
            }
            if (Vector3.Magnitude(Input.mousePosition - dowmposition) <= 60)
            {
                dest = (Vector2)transform.position + Vector2.down*0.5f;
                dir = Vector2.down;
            }
            if (Vector3.Magnitude(Input.mousePosition - leftposition) <= 60)
            {
                dest = (Vector2)transform.position + Vector2.left*0.5f;
                dir = Vector2.left;
            }
            if (Vector3.Magnitude(Input.mousePosition - rightposition) <= 60)
            {
                dest = (Vector2)transform.position + Vector2.right*0.5f;
                dir = Vector2.right;
            }

            Vector2 temp = Vector2.MoveTowards(dest, transform.position, speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
       
       


        //if (Input.GetMouseButton(0))
        //{
        //    dest = (Vector2)transform.position + Vector2.up;
        //    transform.eulerAngles = new Vector3(0, 0, 90);

        //}
        //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        //{
        //    dest = (Vector2)transform.position + Vector2.down;
        //    //transform.eulerAngles = new Vector3(0, 0, -90);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    dest = (Vector2)transform.position + Vector2.left;
        //    //transform.eulerAngles = new Vector3(0, 0, 180);
        //}
        //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    dest = (Vector2)transform.position + Vector2.right;
        //    //transform.eulerAngles = new Vector3(0, 0, 0);
        //}

        //}

        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
        //}
        //    private bool Valid(Vector2 dir)
        //    {
        //    Vector2 pos = transform.position;
        //    RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        //    return (hit.collider == GetComponent<Collider2D>());

    }
//    public  void Up()
//    {
 

//            dir = Vector2.up;
//;
  
        
//        dest = (Vector2)transform.position + Vector2.up;
        
        
//    }
//    public void Down()
//    {
  
//            dir = Vector2.down;
//            IsRight = false;

//        dest = (Vector2)transform.position + Vector2.down;
//    }
//    public void Left()
//    {

//            dir = Vector2.left;

//        dest = (Vector2)transform.position + Vector2.left;
//    }
//    public void Right()
//    {
 
//            dir = Vector2.right;

//        dest = (Vector2)transform.position + Vector2.right;
//    }


}










































