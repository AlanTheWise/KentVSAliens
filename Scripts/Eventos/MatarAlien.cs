using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarAlien : MonoBehaviour
{
    [SerializeField] GameObject dialogo;
    [SerializeField] GameObject dialogodesactivado;
    [SerializeField] AudioSource doorOpen, glitchSound;

    public static MatarAlien sharedInstance;

    private void Awake() {
        if (sharedInstance == null){
            sharedInstance = this;
        }
    }

    public void KillAlienEventStart(){
        glitchSound.Play();
        GetComponent<Animator>().SetTrigger("disparado");
        this.gameObject.tag = "Untagged";
        doorOpen.Play();
        dialogo.SetActive(true);
        dialogo.GetComponent<Dialogue>().StartDialogue();
        GameObject.Find("puerta_tutorial").GetComponent<Animator>().SetBool("abrir", true);
        dialogodesactivado.SetActive(false);
    }
}
