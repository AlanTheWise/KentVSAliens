using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    protected Transform goal;
    [SerializeField] protected float chaseDistance = 10;
    float agentSpeed;

    protected NavMeshAgent agent;
    Animator anim;

    protected float distanceToPlayer;
    protected bool attack;
    [HideInInspector] public bool chase;

    protected virtual void Start(){
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        agentSpeed = agent.speed;
    }

    protected virtual void Update(){
        AnimatorCheck();

        if(GetComponent<EnemyHealth>().currentHealth <= 0){
            attack = false;
            chase = false;
            return;
        } 

        distanceToPlayer = Vector3.Distance(transform.position, goal.position);
        if (distanceToPlayer <= chaseDistance){
            chase = true;
        } 

        if (chase) ChasePlayer(); 

        if (attack){
            agent.isStopped = true;
            Attack();
        } else{
            agent.isStopped = false;
        }
    }

    protected virtual void ChasePlayer(){
        agent.isStopped = false;
        agent.SetDestination(goal.position);
    }

    protected virtual void Attack(){
        transform.LookAt(new Vector3(goal.position.x, transform.position.y, goal.position.z));
    }

    void AnimatorCheck(){
        if (anim == null) return;

        anim.SetFloat("Velocity", agent.velocity.magnitude); 
        anim.SetBool("Attack", attack);
        anim.SetInteger("CurrentHealth", GetComponent<EnemyHealth>().currentHealth);  

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Locomotion")){ // Para idle o run
            agent.speed = agentSpeed;
        } else{
            agent.speed = agentSpeed / 2f;
        }
    }
}
