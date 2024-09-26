using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] bool isLlave1, isBotiquin, isBateria;
    [SerializeField] int botiquinHealAmount = 40, bateriaAmmo = 75;
     [SerializeField] AudioSource pickUpSound;

    void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player"))
        {
            if (isBotiquin){
                if (!other.GetComponent<PlayerHealth>().canPickMedkit()) return;
                other.GetComponent<PlayerHealth>().HealPlayer(botiquinHealAmount);
            }
            if (isBateria){
                other.GetComponentInChildren<PlayerGun>().AddAmmo(bateriaAmmo);
            }
            pickUpSound.Play();
            Destroy(gameObject);
        }
    }
}
