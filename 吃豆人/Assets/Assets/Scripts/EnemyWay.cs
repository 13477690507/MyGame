using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyWay : MonoBehaviour
{
    public GameObject[] Ways;
    private int Index = 0;
    public float Speed = 0.2f;
    private List<Vector3> ways = new List<Vector3>();
    private Vector3 stratPos;
    private void Start()
    {
        stratPos = transform.position + new Vector3(0,3,0);
        LoadAPath(Ways[GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder-2]]);//随机一条路径
    }

    private void FixedUpdate()
    {
        if (transform.position != ways[Index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, ways[Index], Speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            Index++;
            if (Index>=ways.Count)
            {
                Index = 0;//go
                LoadAPath(Ways[Random.Range(0,Ways.Length)]);
            }
            
        }
        Vector2 dir = ways[Index] - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (GameManager.Instance.isSupaerPamcan)
            {
                transform.position = stratPos - new Vector3(0, 3, 0);
                Index = 0;
            }
            else
            {
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePland.SetActive(false);
                Instantiate(GameManager.Instance.gameover);
                Invoke("Restrat", 3f);
            }
        }
    }
    private void LoadAPath(GameObject go)
    {
        ways.Clear();
        //遍历对象数组，添加随即一个途径
        foreach (Transform i in go.transform)
        {
            ways.Add(i.position);
        }
        ways.Insert(0,stratPos);
        ways.Add(stratPos);

    }
    private void Restrat()
    {
        SceneManager.LoadScene(0);//加载本场景
    }

}
