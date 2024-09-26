using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController sharedInstance;
    public bool active;
    public Transform checkpointPos;
    public GameObject gunCamera;

    private void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(checkpointPos);
    }

    private void Start() {
        if (SceneManager.GetActiveScene().name == "MainMenu"){
            active = false;
            return;
        }
        CheckpointController.sharedInstance.tpToCheckpoint();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu"){
            active = false;
            return;
        }
    }

    public void tpToCheckpoint(){
        if (active){
            GameObject.Find("Player").transform.position = checkpointPos.position;
            PickGun.sharedInstance.activateGun();
        }
    }
}
