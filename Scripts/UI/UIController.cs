using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    public static UIController sharedInstance;

    [SerializeField] Text playerCurrentAmmoText;
    [SerializeField] Image HealthFillImg, notificacionPuerta;

    public int healthUpdate;

    // Menu Pausa
    [SerializeField] GameObject pausePanel, pauseMenuParent;
    public bool pausePanelActive, deadMenuActive;
    [SerializeField] Button btnContinue;
    Image crosshairImg;

    [HideInInspector] public GameObject crossHair;

    void Awake() {
        if(sharedInstance == null){
            sharedInstance = this;
        }
        crossHair = GameObject.Find("Crosshair");
        crosshairImg = GameObject.Find("Crosshair").GetComponent<Image>();
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        activatePauseMenu();
        deactivatePauseMenu();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !deadMenuActive && !GameManager.sharedInstance.endActivated){
            if (!pausePanelActive){
                activatePauseMenu();
            } else{
                deactivatePauseMenu();
            }
        }
    }

    void activatePauseMenu(){
        pausePanelActive = true;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        crosshairImg.enabled = false;
        btnContinue.Select();
        
        Time.timeScale = 0;
    }

    void deactivatePauseMenu(){
        pausePanelActive = false;
        pausePanel.SetActive(false);

        for(int i = 0; i < pauseMenuParent.transform.childCount; i++ ){
            pauseMenuParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        crosshairImg.enabled = GameManager.sharedInstance.gunPicked;

        Time.timeScale = 1;
    }

    // Activa la animacion para que la mirilla se ponga roja cuando el arma golpea un enemigo
    public void CrossHairHit(){
        crossHair.GetComponent<Animator>().SetTrigger("Hit");
    }

    public void UpdatePlayerHealthImage(float health)
    {
         HealthFillImg.fillAmount = health/100;
    }

    public void UpdatePlayerCurrentAmmoText(int ammo){
        playerCurrentAmmoText.text = ammo.ToString();
    }

    public void NotificacionPuertaCorutina(){
        //StartCoroutine(NotificacionPuerta());
    }

    /*public IEnumerator Notificacion(){
        notificacionPuerta.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        notificacionPuerta.transform.gameObject.SetActive(false); 
    }*/
}
