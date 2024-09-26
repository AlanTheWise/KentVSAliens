using UnityEngine.UI;
using UnityEngine;

public class PickGun : MonoBehaviour
{
    [SerializeField] GameObject gunCamera;
    [SerializeField] AudioSource pickGun;
    bool active;

    public static PickGun sharedInstance;

    private void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !active){
            active = true;
            activateGun();
        }
    }

    public void activateGun(){
        pickGun.Play();
        GameManager.sharedInstance.gunPicked = true;
        gunCamera.SetActive(true);
        UIController.sharedInstance.crossHair.GetComponent<Image>().enabled = true;
        UIController.sharedInstance.crossHair.GetComponent<Animator>().enabled = true;
        GameObject.Find("armaexposicion").SetActive(false);
    }
}
