using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    #region Variables and References

    [Header("Manager")]
    public static Manager instance;

    [Header("Can Move")]
    ThirdPersonMovement thirdPersonMovement;
    PlayerCombat playerCombat;
    public bool canMove;

    [Header("Spawning and Rounds")]
    public GameObject enemy;
    public int numberOfEnemies = 1;
    int spawnNumber = 1;
    int spawnPoint;

    [Header("TMP")]
    public TextMeshProUGUI health;
    public TextMeshProUGUI rounds;

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
        //references
        thirdPersonMovement = GameObject.Find("Third Person Player").GetComponent<ThirdPersonMovement>();
        playerCombat = GameObject.Find("Third Person Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        //calling methods
        CanMove();
        TMP();

        //checks for when all enemies are dead and begins next round
        if (numberOfEnemies <= 0)
        {
            Rounds();
        }
    }

    #endregion

    #region Can Move

    void CanMove()
    {
        //checks to see if movement is allowed
        if (!thirdPersonMovement.isJumping && !playerCombat.punching && !playerCombat.dead)
        {
            canMove = true;
        }

        else
        {
            canMove = false;
        }
    }

    #endregion

    #region Rounds And Instantiation

    void Rounds()
    {
        //heals player by 10 health per round
        playerCombat.Heal();

        //increases wave number, and so number of enemies to spawn
         spawnNumber++;

         //for loop to make Spawn happen spawnnumber amount of times
         for (int i = 0; i < spawnNumber; i++)
         {
             Spawns();
         }
        
    }

    void Spawns()
    {
        //finds random preselected spawn point
        spawnPoint = Random.Range(1, 6);
        //spawn points
        if (spawnPoint == 1)
        {
            Instantiate(enemy, new Vector3(84f, 1f, -2.7f), Quaternion.identity);
        }
        if (spawnPoint == 2)
        {
            Instantiate(enemy, new Vector3(83f, 1f, -18), Quaternion.identity);
        }
        if (spawnPoint == 3)
        {
            Instantiate(enemy, new Vector3(107f, 0f, -14f), Quaternion.identity);
        }
        if (spawnPoint == 4)
        {
            Instantiate(enemy, new Vector3(107f, 0f, 0f), Quaternion.identity);
        }
        if (spawnPoint == 5)
        {
            Instantiate(enemy, new Vector3(95f, 0f, 1f), Quaternion.identity);
        }

        //increases number of enemies alive by amount spawned
        numberOfEnemies++;
    }

    #endregion

    #region TMP

    void TMP()
    {
        //text
        health.text = "Health: "+ playerCombat.health;
        rounds.text = "Round: " + spawnNumber;
    }

    #endregion

}
