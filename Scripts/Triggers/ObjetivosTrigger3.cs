using UnityEngine;
using TMPro;

public class ObjetivosTrigger3 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objetivosText;
    [SerializeField] GameObject exclamacionImg; 
    bool active;       

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !active){
            if(GameObject.Find("Llave (1)") == null &&  GameObject.Find("Llave (2)") == null){
                active = true;
                GameObject.Find("PUERTABLOQUEADA_FINAL").GetComponent<Animator>().SetBool("abrir", true);
                GameObject.Find("DoorOpen").GetComponent<AudioSource>().Play();
                objetivosText.enabled = false;
                exclamacionImg.SetActive(false);
            }
        }
    }
}
