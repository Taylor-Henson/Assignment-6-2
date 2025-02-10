using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Variables and References

    [Header("General")]
    public Animator anim;
    public Enemy enemy;

    [Header("Punching")]
    public Transform punchPoint;
    public LayerMask enemyMask;
    float sphereSize = 1f;
    public bool punching;

    [Header("Dying")]
    public float health = 100;
    public bool dead;

    #endregion

    #region Start and Update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //referneces
        enemy = GameObject.Find("EnemyFinished").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //calling methods
        DoPunch();
        Health();
    }

    #endregion

    #region Punch

    void DoPunch()
    {
        //makes punch happen
        if (Input.GetButtonDown("Fire1") && Manager.instance.canMove)
        {
            anim.SetTrigger("Punch");
            punching = true;
        }
    }

    void PunchTrigger()
    {
        //checks sphere for hit
         Vector3 position = punchPoint.transform.position;

        //creates array of classes (objects) inside of hitsphere
        Collider[] hitColliders = (Physics.OverlapSphere(position, sphereSize, enemyMask));

        //for every class inside of hitColliders create "enemy"
        foreach (Collider enemy in hitColliders)
        {
            //access that classes gameobject, its enemy script component, and the die method inside of that
            enemy.gameObject.GetComponent<Enemy>().Die();

            //decreases number of enemies alive when an enemy is killed
            Manager.instance.numberOfEnemies--;
        }
    }

    void EndPunch()
    {
        punching = false;
    }

    #endregion

    #region Health and Dying

    public void TakeDamage()
    {
        //player takes damage
        health -= 20;
    }
    void Health()
    {
        //kills player and stops them moving once health is <= 0
        if (health <= 0 && !dead)
        {
            anim.SetTrigger("Die");
            dead = true;
        }
    }

    #endregion

}
