using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100, currentHealth;
    
    [SerializeField] AudioSource hurtSound, lowHealthSound, defeatSound;

    [SerializeField] Animator hurtBloodAnim;

    [SerializeField] GameObject menuMuerte;

    void Start()
    {
        currentHealth = maxHealth;

        UIController.sharedInstance.UpdatePlayerHealthImage(currentHealth);
    }

    private void Update() {
        if (currentHealth <= 15 && !lowHealthSound.isPlaying && !UIController.sharedInstance.pausePanelActive && !UIController.sharedInstance.deadMenuActive) lowHealthSound.Play();
    }

    public void ReceiveDamage(int amount){
        currentHealth -= amount;
        hurtSound.Play();

        if (currentHealth <= 15){
            hurtBloodAnim.SetTrigger("LowHealth");
        } else{ 
            hurtBloodAnim.SetTrigger("HurtBlood");
        }

        if (currentHealth <= 0) {
            // Jugador Muerto
            menuMuerte.SetActive(true);
            defeatSound.Play();
            Time.timeScale = 0f;
            UIController.sharedInstance.deadMenuActive = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            currentHealth = 0;
        }
        

        UIController.sharedInstance.UpdatePlayerHealthImage(currentHealth);
    }

    public void HealPlayer(int amount){
        lowHealthSound.Stop();
        hurtBloodAnim.SetTrigger("Idle");
        if(currentHealth > 0 && currentHealth < maxHealth){
            currentHealth += amount;
        }
        if(currentHealth > 100) currentHealth = 100;

        UIController.sharedInstance.UpdatePlayerHealthImage(currentHealth);
    }

    public bool canPickMedkit(){
        return currentHealth != maxHealth;
    }
}
