using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    [SerializeField] int tickDamage;
    bool inLava;
    [SerializeField] float tickPeriod;
    float counter;

    private void Update() {
        if (inLava){
            counter += Time.deltaTime;
            if (counter >= tickPeriod){
                counter = 0;
                GameObject.Find("Player").GetComponent<PlayerHealth>().ReceiveDamage(tickDamage);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")){
            inLava = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            inLava = false;
        }
    }

}
