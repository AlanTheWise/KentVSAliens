using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaTrigger : MonoBehaviour
{
    [SerializeField] Animator puertaAnim;
    bool active;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !active){
            active = true;
            puertaAnim.SetBool("abrir", true);
            GameObject.Find("DoorOpen").GetComponent<AudioSource>().Play();
        }
    }
}
