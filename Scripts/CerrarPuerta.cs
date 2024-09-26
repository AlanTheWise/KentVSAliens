using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarPuerta : MonoBehaviour
{
    [SerializeField] Animator puertaAnim;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            puertaAnim.SetBool("cerrar", true);
        }
    }
}
