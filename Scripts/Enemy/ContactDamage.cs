using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().ReceiveDamage(damage);
        }
    }

}
