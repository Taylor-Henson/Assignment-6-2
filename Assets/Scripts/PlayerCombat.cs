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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(punchPoint.transform.position, sphereSize);
    }
}
