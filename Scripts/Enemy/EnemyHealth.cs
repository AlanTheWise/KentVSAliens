using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 30;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] CapsuleCollider headCol;
    [SerializeField] GameObject headshotText;
    [SerializeField] float destroyTime = 10f;
    Animator anim;
    AudioSource audioSource;

    AudioSource meleeDeathSound, flyDeathSound;

    EnemyBehaviour eb;
    [HideInInspector] public int currentHealth;
    bool isDead;

    void Start(){
        currentHealth = maxHealth;
        eb = GetComponent<EnemyBehaviour>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        meleeDeathSound = GameObject.Find("AlienMeleeDeath").GetComponent<AudioSource>();
        flyDeathSound = GameObject.Find("AlienFlyDeath").GetComponent<AudioSource>();
    }

    public void ReceiveDamage(int amount){
        if (isDead) return;
        
        currentHealth -= amount;
        audioSource.PlayOneShot(hurtSound, 0.25f);

        eb.chase = true;
        //anim.SetTrigger("Hurt");

        // Enemigo Muerto
        if (currentHealth <= 0) {
            currentHealth = 0;
            isDead = true;
            anim.SetTrigger("EnemyDead");
            GetComponent<CapsuleCollider>().enabled = false;
            if (headCol != null) headCol.enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            
            Destroy(gameObject, destroyTime);
        }
    }

    public void FlyDeathSound(){
        flyDeathSound.Play();
    }

    public void MeleeDeathSound(){
        meleeDeathSound.Play();
    }

    public void ShowHeadshotText(){
        headshotText.SetActive(true);
        Invoke("HideHeadshotText", 1f);
    }

    public void HideHeadshotText(){
        headshotText.SetActive(false);
    }
}
