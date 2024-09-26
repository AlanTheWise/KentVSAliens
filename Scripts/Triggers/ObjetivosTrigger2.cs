using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetivosTrigger2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objetivosText;
    [SerializeField] GameObject exclamacionImg;    

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            objetivosText.enabled = true;
            exclamacionImg.SetActive(true);
            objetivosText.text = "Consigue las tarjetas 0/2";
        }
    }
}
