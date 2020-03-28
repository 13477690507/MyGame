using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerNetController : NetworkBehaviour {
    //public FPSFireManager fireManager;
    //public Camera Playercamera;
    //public AudioListener Playeraudio;
    //public FirstPersonController controller;
    //public PlayerAnimerController playerAnimer;
    //public GameObject UI;

    void Start()
    {
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<FPSFireManager>().enabled = false;
            gameObject.GetComponentInChildren<AudioListener>().enabled = false;
            gameObject.GetComponentInChildren<FirstPersonController>().enabled = false;
            gameObject.GetComponentInChildren<PlayerAnimerController>().enabled = false;
            gameObject.GetComponentInChildren<BloodController>().enabled = false;
        }
        
        if (isLocalPlayer)
        {
            gameObject.name = "Me";
            transform.Find("Canvas").gameObject.SetActive(true);
            transform.Find("EventSystem").gameObject.SetActive(true);
        }
        
    }
	
	
	void Update () {

	}

    
}
