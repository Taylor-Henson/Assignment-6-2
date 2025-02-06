using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    #region Variables and References

    [Header("General")]
    public Animator anim;

    [Header("Punching")]
    public Transform punchPoint;
    public LayerMask enemyMask;
    float sphereSize = 0.4f;
    public bool punching;

    [Header("Dying")]
    public float health = 100;
    public bool dead;

    #endregion

    #region Start and Update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoPunch();
        Health();
    }

    #endregion

    #region Punch

    void DoPunch()
    {
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

        if (Physics.CheckSphere(position, sphereSize, enemyMask))
        {
            print("hit");
        }
    }

    void EndPunch()
    {
        punching = false;
    }

    #endregion

    #region Health and Dying

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(punchPoint.transform.position, sphereSize);
    }
}
