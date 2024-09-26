using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogo : MonoBehaviour
{
    [SerializeField] GameObject dialogoActivar, dialogoDesactivar;
    bool activated;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !activated){
            activated = true;
            dialogoActivar.gameObject.SetActive(true);
            if(dialogoDesactivar != null){
                dialogoDesactivar.gameObject.SetActive(false);
            }
            dialogoActivar.GetComponent<Dialogue>().StartDialogue();
        }
    }
}
