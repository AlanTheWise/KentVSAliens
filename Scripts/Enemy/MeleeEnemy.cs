using UnityEngine;

public class MeleeEnemy : EnemyBehaviour
{
    [SerializeField] float acceleration = 10, deceleration = 60, decelerationDistance = 4;
    AudioSource attackSound;
    float attackDistance;

    protected override void Start() {
        base.Start();
        attackSound = GameObject.Find("MeleeAttack").GetComponent<AudioSource>();
    }

    protected override void Update(){
        base.Update();
        attackDistance = (PlayerMovement.sharedInstance.GetStandingLayerName() == "Box") ? 4 : agent.stoppingDistance + 0.25f;

        if(distanceToPlayer <= attackDistance){
            attack = true;
        } else{
            attack = false;
        }

        if (agent.hasPath){
            agent.acceleration = (agent.remainingDistance < decelerationDistance) ? deceleration : acceleration; 
        }
    }

    public void AttackSound(){
        if(!attackSound.isPlaying) attackSound.Play();  
    }
}
