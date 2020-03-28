using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInit : MonoBehaviour {

    [SerializeField] GameObject[] uis;


    private void Start()
    {
        Set(0);
    }



    public void Set(int index)
    {
        if (uis == null || uis.Length == 0)
            return;

        if (uis.Length <= index)
            return;

        uis[index].SetActive(true);
    }


}
