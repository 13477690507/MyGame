using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }


    }
    public bool isSupaerPamcan=false;
    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject startPland;
    public GameObject AnStart;
    public GameObject gamePland;
    public GameObject gameover;
    public GameObject wind;
    public AudioClip startClip;
    public Text remainText;
    public Text nowText;
   
    private int pacdotNum = 0;//吃了多少
    private int nowEat = 0;//剩余的
    //private int score = 0;//分数   

    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    private List<GameObject> pacdotGos = new List<GameObject>();
    private void Awake()
    {
        _instance = this;
        Screen.SetResolution(1024,768,false);
        int tempCount = rawIndex.Count;
        for (int i = 0; i < tempCount; i++)
        {
            int temp = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[temp]);//添加的序号上的值
            rawIndex.RemoveAt(temp);//移除

        }
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Respawn"))
        {
           
            pacdotGos.Add(t);
        }
        pacdotNum = pacdotGos.Count;
    }
    
    private void Start()
    {
        SetGameState(false);
        //Invoke("CreateSuperPacdot",5f);
     
    }
    private void Update()
    {
        if (gamePland.activeInHierarchy)
        {
            remainText.text = "Remain:\n\n" + (pacdotNum - nowEat);//总数减去吃掉的=剩余的
            nowText.text = "Eaten:\n\n" + nowEat;
        }
        if (nowEat==pacdotNum&&pacman.GetComponent<PamcanMove>().enabled==true)//豆子吃掉数和总数相同并且pacmMOVE的脚本停止调用
        {
            gamePland.SetActive(false);
            Instantiate(wind);
            StopAllCoroutines();
            SetGameState(false);
        }
        if (nowEat==pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                 SceneManager.LoadScene(0);
            }
            
        }
    }
    public void OnStartButton() {
        startPland.SetActive(false);
        StartCoroutine(PlayStarPland());
        AudioSource.PlayClipAtPoint(startClip,Vector3.zero);
        

    }
    public void OnExitButton() {
        Application.Quit();

    }
    IEnumerator PlayStarPland()
    {
        GameObject go = Instantiate(AnStart);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        Invoke("CreateSuperPacdot", 10f);
        gamePland.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
   IEnumerator RecoveryEnemy()
    {
        yield return new WaitForSeconds(3f);
        DisFreezeEnemy();
        isSupaerPamcan = false;
       
    }
    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        pacdotGos.Remove(go);
    }
    public void OnEatSuperPacdot() {
        Invoke("CreateSuperPacdot", 10f);
        isSupaerPamcan = true;
        FreezeEnemy();
        //Invoke("RecoveryEnemy", 4f);
        StartCoroutine(RecoveryEnemy());
    }
    private void CreateSuperPacdot()
    {
        if (pacdotGos.Count<5)
        {
            return;
        }
        int temp = Random.Range(0, pacdotGos.Count);
        pacdotGos[temp].transform.localScale=new Vector3(3,3,3);
        pacdotGos[temp].GetComponent<Pacdot>().isSuperPacdot = true;
        
    }
    private void FreezeEnemy()
    {
       
            blinky.GetComponent<EnemyWay>().enabled = false;
            clyde.GetComponent<EnemyWay>().enabled = false;
            inky.GetComponent<EnemyWay>().enabled = false;
            pinky.GetComponent<EnemyWay>().enabled = false;
            blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f,0.7f,0.7f);
            clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
            inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
            pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
       
    }
    private void DisFreezeEnemy(){
        blinky.GetComponent<EnemyWay>().enabled = true;
            clyde.GetComponent<EnemyWay>().enabled = true;
            inky.GetComponent<EnemyWay>().enabled = true;
            pinky.GetComponent<EnemyWay>().enabled = true;
            blinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

    }
    private void SetGameState(bool state) {
        pacman.GetComponent<PamcanMove>().enabled = state;
        blinky.GetComponent<EnemyWay>().enabled = state;
        clyde.GetComponent<EnemyWay>().enabled = state;
        inky.GetComponent<EnemyWay>().enabled = state;
        pinky.GetComponent<EnemyWay>().enabled = state;

    }
}
