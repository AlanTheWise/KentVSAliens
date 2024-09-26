using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage2 : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Pared")){
            Destroy(this.gameObject);
        }

        print(other.gameObject.tag);
    }
}
