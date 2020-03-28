using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Candy candy;//
    public int columnNum = 10;
    public int rowNum = 7;
    public GameObject game;//
    private ArrayList CandyArr;//由每一行数组为元素组成的数组
    public AudioSource a;
    public AudioClip SW;
    public AudioClip EX;
    public AudioClip MA;

    //开始
    void Start()
    {   //
        a = this.GetComponent<AudioSource>();
        CandyArr = new ArrayList();//实例数组
        for (int j = 0; j < rowNum; j++)//列数
        {
            ArrayList tmp = new ArrayList();//定义并实例化数组，每一行的
            for (int i = 0; i < columnNum; i++)//行数
            {
                Candy c = AddCandy(j,i);//每一行中单个元素用cand接收
                tmp.Add(c);//添加数组至一行
            }
            CandyArr.Add(tmp);//添加每行数组至列表
            
        }
     
        //first check
        if (CheckMatches())
        {
            RemoveMatches();
        }
    }
    //为列表里每个数添加candy并设置父对象为gamecontroller
    private Candy AddCandy(int j,int i)//行数数，列数参数
    {
               object o = Instantiate(candy);//接收定义对象并实例化candy
                Candy c = o as Candy;//将对象转化为candy并用c接受
                c.transform.parent = game.transform;//c的父类设为game
                c.columnIndex = i;//c的列数为i
                c.rowIndex = j;//c的行数为j

                c.Updateposion();//赋予图片给每个数位置上
                c.game = this;
        return c;

    }
    //访问二维数组元素 位置
    private Candy GetCandy(int j,int i) {
        ArrayList tmp=CandyArr[j] as ArrayList;
        Candy c = tmp[i] as Candy;
        return c;
    }
    //设置二为素组元素       数组位置  candy 对象
    private void SetCandy(int j,int i,Candy c)
    {
        ArrayList tmp = CandyArr[j] as ArrayList;
        tmp[i] = c;
    }
    private Candy Ctr;//记录的candy点击
               //点击交换的方法
    public void Select(Candy c) {
        //Remove(c); return;
        if (Ctr == null)//第一次点击的记录
        {
            Ctr = c;
            //Ctr.Selected = true;
            return;
            
        }
        else {
            if (Mathf.Abs(Ctr.rowIndex-c.rowIndex)+Mathf.Abs(Ctr.columnIndex-c.columnIndex)==1)
            {
                
                StartCoroutine(JhCandy2(Ctr, c));

            }
            //Ctr.Selected = false;
            Ctr = null;
            
        }
      
       
    }
    IEnumerator JhCandy2(Candy c1, Candy c2)
    {
        JhCandy(c1, c2);//交换
        yield return new WaitForSeconds(0.4f);

        if (CheckMatches())
        {
            RemoveMatches();//消除
        }
        else
            JhCandy(c1, c2);//第二次点击的交换


    }
    //交换       candy 位置            
    private void JhCandy(Candy c1,Candy c2) {
        //play audio
        
        SetCandy(c1.rowIndex, c1.columnIndex,c2);//交换数组元素位置（参数）
        SetCandy(c2.rowIndex, c2.columnIndex, c1);
        int rowIndex = c1.rowIndex;//交换rowINdex
        c1.rowIndex = c2.rowIndex;
        c2.rowIndex = rowIndex;
        int columnIndex = c1.columnIndex;
        c1.columnIndex = c2.columnIndex;
        c2.columnIndex = columnIndex;
        //c1.Updateposion();//为c1加图片和加位置
        //c2.Updateposion();//为c2加图片和加位置
        c1.TweenToPosition();
        c2.TweenToPosition();
        a.PlayOneShot(SW);
    }
    //消除candy
    private void Remove(Candy c)
    {
        a.PlayOneShot(MA);
        c.Dispose();//
        
        int i = c.columnIndex;
        for (int j = c.rowIndex+1; j < rowNum; j++)//得到行数
        {
            Candy c2 = GetCandy(j,i);//访问数组属性
            c2.rowIndex--;   //
            c2.TweenToPosition();//移动方法
            //c2.Updateposion();
            SetCandy(j - 1,i,c2);//设置
        }
        Candy newc = AddCandy(rowNum-1,i);//
        newc.rowIndex = rowNum;
        newc.Updateposion();
        newc.rowIndex--;
        newc.TweenToPosition(); 
        SetCandy(rowNum - 1, i, newc);

     }
    //检测水平的有无可以消除的
    private bool CheckHorizeontalMatches() {
        //int j = rowNum - 1;
        bool result = false;
        for (int j = 0; j <rowNum; j++)
        {
          for (int i = 0; i <columnNum-2; i++)
        {
            if ((GetCandy(j,i).type==GetCandy(j,i+1).type)&& (GetCandy(j, i).type == GetCandy(j, i + 2).type))
            {
                    result = true;
                    Debug.Log(j+": "+i+" "+(i + 1)+" "+(i+2));
                AddMatch(GetCandy(j,i));
                AddMatch(GetCandy(j, i+1));
                AddMatch(GetCandy(j, i+2));
            } 
          }
        }
        
        return result;
    }
    private ArrayList Matches;//申明数组
    //添加数组
    private void AddMatch(Candy c) {

        if (Matches==null)//初始化
            Matches = new ArrayList();
        int index = Matches.IndexOf(c);//找到从0开始的索引
        if (index == -1)
            Matches.Add(c);//添加c到数组里
        
    }
    private void RemoveMatches() {//消除集合CANDY
        
        Candy tmp;
        for (int i = 0; i < Matches.Count; i++)//
        {
            tmp = Matches[i] as Candy;
            Remove(tmp);
        }
        Matches = new ArrayList();
        if (CheckMatches () )
        {
            RemoveMatches();
        }
    }
    //检测垂直的有无可以消除的
    private bool CheckVertiontalMatches() {
        bool result = false;
        for (int i = 0; i < columnNum; i++)
        {
            for (int j = 0; j < rowNum - 2; j++)
            {
                if ((GetCandy(j+1, i).type == GetCandy(j, i).type) && (GetCandy(j, i).type == GetCandy(j+2, i).type))
                {
                    result = true;
                    //Debug.Log(j + ": " + i + " " + (i + 1) + " " + (i + 2));
                    AddMatch(GetCandy(j, i));
                    AddMatch(GetCandy(j+1, i));
                    AddMatch(GetCandy(j+2, i));
                }
            }
        }
        return result;
    }

    //检测有无可以消除的
    private bool CheckMatches()
    {
        return CheckHorizeontalMatches()|| CheckVertiontalMatches();//有一个返回真都是真
    }
    //过0.5秒检测是否需要删除
    IEnumerator WaitAndCheck() {
        yield return new WaitForSeconds(0.5F);
        if (CheckMatches())
        {
            RemoveMatches();
        }
    }
}
