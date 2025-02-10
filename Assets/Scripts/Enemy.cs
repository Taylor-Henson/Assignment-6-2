using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Variables and References

    [Header("Movement/NavMesh")]
    public NavMeshAgent agent;
    public Transform player;

    [Header("Attacking/Dying")]
    public Transform attackPoint;
    public PlayerCombat playerCombat;
    public LayerMask playerMask;
    bool attackCooldown;
    bool dead;

    [Header("Animation")]
    public Animator anim;

    States state;

    #endregion

    #region Start and Update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //references
        player = GameObject.Find("Third Person Player").GetComponent<Transform>();
        playerCombat = GameObject.Find("Third Person Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        //calling methods
        Logic();

        //state machine
        if (!Physics.CheckSphere(transform.position, 1.5f, playerMask))
        {
            state = States.Walking;
        }
        else
        {
            state = States.Attacking;
        }
    }

    #endregion

    #region States

    public enum States
    { 
        //lists states
        Walking,
        Attacking,
    }

    void Logic()
    {
        //defines which state the enemy is in
        if (state == States.Walking)
        {
            Movement();
        }
        else
        {
            Attacking();
        }
    }

    #endregion

    #region Movement

    void Movement()
    {
        if (!dead)
        {
            //uses NavMesh to make enemy walk to player
            agent.SetDestination(player.transform.position);
            anim.SetTrigger("Walk");
        }
    }

    #endregion

    #region Attacking

    void Attacking()
    {
        if (!attackCooldown && !dead)
        { 
            //animates
            anim.SetTrigger("Punch");

            //sets cooldown
            attackCooldown = false;
            Invoke("Cooldown", 2);
        }
    }

    void Hit()
    {
        //through animation event checks sphere for player mask
        if (Physics.CheckSphere(attackPoint.transform.position, 0.5f, playerMask))
        {
            //accesses player script to make it take damage
            playerCombat.TakeDamage();
        }
    }
    void Cooldown()
    {
        //undos cooldown
        attackCooldown = false;
    }

    #endregion

    #region Death

    public void Die()
    {
        //animation
        if (!dead)
        {
            anim.SetTrigger("Die");
        }
        
        //disables other functions
        dead = true;
        anim.SetBool("Dead", true);
    }

    #endregion

}
