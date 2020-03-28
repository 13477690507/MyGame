using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebirth : MonoBehaviour {

    float rebirthtime = 8;
    float time = 0;
	void Start () {
		
	}
	
	void Update () {
        if (GameObject.Find("Me") == null)
        {
            Debug.Log(123);
            time += Time.deltaTime;
            if (time > rebirthtime)
            {
                

                time = 0;
            }
        }
	}
}
