using UnityEngine;

public class FlyingEnemy : EnemyBehaviour
{
    [SerializeField] float spaceWithPlayer = 5;
    // Para el disparo
    [SerializeField] float timeBetweenShots = 5, bulletSpeed = 10;
    [SerializeField] GameObject enemyBullet, bulletSpawnPoint;
    [SerializeField] ParticleSystem spitEffect;
    AudioSource attackSound;
    float bulletTime;
    GameObject bulletObj, player;

    protected override void Start() {
        base.Start();
        attackSound = GameObject.Find("FlyAttack").GetComponent<AudioSource>();
    }

    protected override void Update(){
        base.Update();
        if (distanceToPlayer <= spaceWithPlayer){
            attack = true;
        } else{
            attack = false;
        }

        if(bulletObj != null){
            bulletObj.transform.position = Vector3.MoveTowards(bulletObj.transform.position, player.transform.position, bulletSpeed * Time.deltaTime);
        }
    }

    protected override void Attack(){ 
        base.Attack();      
        ShootPlayer();
    }

    void ShootPlayer(){
        bulletTime -= Time.deltaTime;
        if(bulletTime > 0) return;
        attackSound.Play();
        spitEffect.Play();
        bulletTime = timeBetweenShots;
        bulletObj = Instantiate(enemyBullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation) as GameObject;
        player = GameObject.Find("Player");
        //Destroy(bulletObj, 2f);
    }
}

