using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Variables and References

    [Header("Manager")]
    public static Manager instance;

    [Header("Can Move")]
    ThirdPersonMovement thirdPersonMovement;
    PlayerCombat playerCombat;
    public bool canMove;

    #endregion

    #region Singleton, Start and Update

    void Awake()
    {
        if (instance == null)
        {
            // if instance is null, store a reference to this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
            //print("do not destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            //print("do destroy");
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thirdPersonMovement = GameObject.Find("Third Person Player").GetComponent<ThirdPersonMovement>();
        playerCombat = GameObject.Find("Third Person Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        CanMove();
    }

    #endregion

    #region Can Move

    void CanMove()
    {
        //checks to see if movement is allowed
        if (!thirdPersonMovement.isJumping && !playerCombat.punching)
        {
            canMove = true;
        }

        else
        {
            canMove = false;
        }
    }

    #endregion

}
