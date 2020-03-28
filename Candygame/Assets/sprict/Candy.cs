using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

    public int rowIndex = 0;//y,
    

    public int columnIndex = 0;//x
    public float xoff = -4.5f;
    public float yoff = 3f;
    public int CandytypeNum = 0;
    public GameObject[] size;//定义对象的数组存入candy的背景
    private GameObject bg;//candy的背景对象
    public int type;
    public GameController game;
    private SpriteRenderer sr;
    //public bool Selected {
    //    set {
    //        if (sr!=null)
    //        {
    //            //if (value) sr.color = Color.blue;
    //            //else sr.color = Color.white;
    //            sr.color = value ? Color.blue : Color.white;
    //        }
    //    }


    //}
    
    void Start() {
    }
    //添加背景图片
    private void AddRandomBG() {
        if (bg != null)//背景为空则返回s
        {
            return;
        }
        type = Random.Range(0,Mathf.Min( CandytypeNum,size.Length));//随机一个从零到数组末尾的数
        bg = (GameObject)Instantiate(size[type]);//用背景bg接收实例的单个对象
        bg.transform.parent = this.transform;//设置bg的父类为Candy
        sr=bg.GetComponent<SpriteRenderer>();
    }
    //移动
    public void TweenToPosition() {
        AddRandomBG();
        iTween.MoveTo(this.gameObject, iTween.Hash("x", columnIndex + xoff,
            "y", rowIndex - yoff, "time", 0.3f));

    }
    void Update() {

    }//点击事件
    void OnMouseDown()
    {
        game.Select(this);
    }

    private void OnMouseOver()
    {
        
         sr.color = Color.blue;
           
    }
    private void OnMouseExit() {
        sr.color = Color.white;
    }
    //
    public void Updateposion()
    {
        AddRandomBG();//调用添加背景
        //添加背景的位置
        transform.position = new Vector3(columnIndex + xoff, rowIndex - yoff, -2);


    }
    //删除
    public void Dispose() {
        game = null;
        Destroy(bg.gameObject);//删除对象
        Destroy(this.gameObject);//删除此对对象

    }
}
