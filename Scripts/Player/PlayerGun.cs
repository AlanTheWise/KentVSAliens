using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] int damage = 10, maxAmmo = 50;
    [SerializeField] float overHeatSpeed = 0.05f, timeBetweenShots = 0.2f;
    float unHeatSpeed, shootTimer;
    [SerializeField] Image overHeatImg, overHeatAlert;
    [SerializeField] AudioSource shootSound, overchargeSound, enemyHitSound, headshotSound;
    [SerializeField] ParticleSystem laserParticle, laserImpact, bloodEffect;
    [SerializeField] GameObject bulletPrefab, bulletShootPos;
    GameObject bulletObj;

    bool maxOverHeatReached;
    int currentAmmo;
    Transform cam;
    Animator camAnim;

    void Start() {
        cam = transform.parent;
        currentAmmo = maxAmmo;
        overHeatImg.fillAmount = 0f;
        shootTimer = timeBetweenShots;
        camAnim = cam.GetComponent<Animator>();
        UIController.sharedInstance.UpdatePlayerCurrentAmmoText(currentAmmo);
        StartCoroutine("UnHeatGun");
    }

    void Update()
    {
        if (UIController.sharedInstance.pausePanelActive || !GameManager.sharedInstance.gunPicked || UIController.sharedInstance.deadMenuActive) return;
        
        shootTimer -= Time.deltaTime;

        if (shootTimer < 0) shootTimer = 0;

        if (Input.GetButton("Fire1") && currentAmmo > 0 && !maxOverHeatReached && shootTimer <= 0){
            Shoot();
            shootTimer = timeBetweenShots;
        } 
    }

    void Shoot(){
        RaycastHit hit;

        camAnim.SetTrigger("shake");

        shootSound.Play();

        if (Physics.Raycast(cam.position, cam.forward, out hit)){
            laserImpact.transform.position = hit.point;
            laserImpact.Play();

            laserParticle.transform.LookAt(hit.point);
            laserParticle.Play();

            bulletObj = Instantiate(bulletPrefab, bulletShootPos.transform.position, bulletPrefab.transform.rotation);
            bulletObj.GetComponent<PlayerBullet>().bulletDir = hit.point;

            if(hit.transform.CompareTag("Enemy")){
                //enemyHitSound.Play();
                ParticleSystem bloodEffectObject = Instantiate(bloodEffect, hit.point, bloodEffect.transform.rotation);
                bloodEffectObject.Play();

                hit.transform.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(damage);
                UIController.sharedInstance.CrossHairHit();
            }
            if(hit.transform.CompareTag("EnemyTutorial")){
                MatarAlien.sharedInstance.KillAlienEventStart();
            }
            if(hit.transform.CompareTag("EnemyHead")){
                headshotSound.Play();
                //enemyHitSound.Play();
                ParticleSystem bloodEffectObject = Instantiate(bloodEffect, hit.point, bloodEffect.transform.rotation);
                bloodEffectObject.Play();
                hit.transform.parent.gameObject.GetComponent<EnemyHealth>().ReceiveDamage(damage * 2);
                hit.transform.parent.gameObject.GetComponent<EnemyHealth>().ShowHeadshotText();
                UIController.sharedInstance.CrossHairHit();
            }
        }
        ManageAmmo();

        // Sobrecargar el arma
        if (overHeatImg.fillAmount < 1){
            overHeatImg.fillAmount += overHeatSpeed;
        }
    }

    public void AddAmmo(int amount){
        currentAmmo += amount;
        if(currentAmmo > maxAmmo){
            currentAmmo = maxAmmo;
        }
        UIController.sharedInstance.UpdatePlayerCurrentAmmoText(currentAmmo);
    }

    void ManageAmmo(){
        currentAmmo--;
        if(currentAmmo <= 0){
            currentAmmo = 0;
        }
        UIController.sharedInstance.UpdatePlayerCurrentAmmoText(currentAmmo);
    }

    IEnumerator UnHeatGun(){
        while (true){
            float currentOverheatAmount = overHeatImg.fillAmount;
            // Cuanto mas sobrecargada, mas rapido se descarga
            if (currentOverheatAmount == 0){
                unHeatSpeed = 0;
            } else if(currentOverheatAmount <= 0.25f){
                unHeatSpeed = 0.05f;
            } else if(currentOverheatAmount <= 0.5f){
                unHeatSpeed = 0.1f;
            } else if(currentOverheatAmount <= 0.75f){
                unHeatSpeed = 0.15f;
            } else if(currentOverheatAmount < 1){
                unHeatSpeed = 0.2f;
            } else if(currentOverheatAmount == 1 && !maxOverHeatReached){
                maxOverHeatReached = true;
                overHeatAlert.transform.gameObject.SetActive(true);
                overchargeSound.Play();
                Invoke("DisableMaxOverHeat", 1f);
            }

            // Bajar la barra
            if (currentOverheatAmount > 0 && !maxOverHeatReached){
                overHeatImg.fillAmount -= unHeatSpeed;
            }
            yield return new WaitForSeconds(0.75f);
        }
    }

    void DisableMaxOverHeat(){
        overHeatAlert.transform.gameObject.SetActive(false);
        maxOverHeatReached = false;
        overHeatImg.fillAmount -= unHeatSpeed; // para que no entre en el ultimo if de unheatgun()
    }
}
