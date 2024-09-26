using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaTrigger : MonoBehaviour
{
    [SerializeField] GameObject palancaOff, palancaOn, escaleras;
    [SerializeField] AudioSource leverSound;
    bool activated;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !activated){
            leverSound.Play();
            palancaOff.SetActive(false);
            palancaOn.SetActive(true);
            escaleras.SetActive(true);
            activated = true;
        }
    }
}
