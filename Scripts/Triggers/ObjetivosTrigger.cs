using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetivosTrigger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objetivosText;
    [SerializeField] GameObject luzRoja1, luzRoja2, luzVerde1, luzVerde2, luzRojaPuerta1, luzVerdePuerta1, exlamacionImg;
    bool active;
    bool active2;

    void Update()
    {
        if (GameObject.Find("Llave") == null && GameObject.Find("puerta_primera_llave") != null && !active){
            objetivosText.text = "Abre la puerta";
            active = true;
        }

        if(GameObject.Find("Llave (1)") == null &&  GameObject.Find("Llave (2)") == null){
            objetivosText.text = "Consigue las tarjetas 2/2. Abre la puerta";
            luzRoja2.SetActive(false);
            luzVerde2.SetActive(true);
            luzRoja1.SetActive(false);
            luzVerde1.SetActive(true);
        } else if(GameObject.Find("Llave (1)") == null &&  GameObject.Find("Llave (2)") != null){
            luzRoja1.SetActive(false);
            luzVerde1.SetActive(true);
            objetivosText.text = "Consigue las tarjetas 1/2";
        } else if(GameObject.Find("Llave (1)") != null &&  GameObject.Find("Llave (2)") == null){
            luzRoja2.SetActive(false);
            luzVerde2.SetActive(true);
            objetivosText.text = "Consigue las tarjetas 1/2";
        }

        if(GameObject.Find("Llave") == null){
            luzRojaPuerta1.SetActive(false);
            luzVerdePuerta1.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            if (GameObject.Find("puerta_primera_llave") != null)
            {
                objetivosText.enabled = true;
                exlamacionImg.SetActive(true);
            } 

            if(GameObject.Find("Llave") == null && !active2 /*&& GameObject.Find("puerta_primera_llave") != null*/){
                objetivosText.enabled = false;
                active2 = true;
                exlamacionImg.SetActive(false);
                CheckpointController.sharedInstance.active = true;
                GameObject.Find("puerta_primera_llave").GetComponent<Animator>().SetBool("abrir", true);
                GameObject.Find("DoorOpen").GetComponent<AudioSource>().Play();
            }
        }


    }
}
