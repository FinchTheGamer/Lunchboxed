using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using static Key;
using System;

//what if we can make the enemy shoot fireballs?

public class JellyKingScript : EnemyScript
{
    //super important! "Original Prefab" when pulling a modified prefab into the editor means that this is a whole new prefab, unrelated to the first. Just happens to have had similar code
    //"Prefab variant" means changes to the original that aren't explicitly overwritten in the variant will be applied to the variant
    //the latter is useful for slightly modified objects, like Enemy and FastEnemy. 
    //the former is useful for things that are totally unrelated, like Enemy and DestroyableBox or Beholder or Ghoul
    private float enemyMoveSpeed = 2;
    private UnityEvent kickStartProjectile;
    private Transform projectileSpawnLocation;
    private Rigidbody2D rigidbody2D; //I DIDN'T KNOW YOU COULD HAVE NUMBERS IN THE VARIABLE NAME
    
    [SerializeField] private DatabaseScriptableObject database;

    public event Action<Vector3> SetProjectileSpeed;
    [SerializeField] private bool DoesThisBossBestowAKey = false;
    [SerializeField] private string KeyThisBossBestows = "00000000";


    private int enemyScore = 1000;
    
    //public delegate void JellyKingDelegate(float moveDirectionX, float moveDirectionY, float moveDirectionZ);
    //public event JellyKingDelegate setProjectileTargetDestination;
    
    
    private Vector3 moveDirectionOfNewProjectiles = new Vector3(-12, 1, 0);
    public int Direction
    {
        get
        {
            return direction;
        }
        set
        {
            if(value != 0)
            {
                value = direction;
            }
            
        }
    }


    void Start()
    {
        
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        //JellyKingDelegate sendMoveDirection = (moveDirectionX,moveDirectionY,moveTowardsZ) => { }; //creates a quick lil lambda that just returns the MoveDirection
        projectileSpawnLocation = transform.Find("ProjectileSpawnLocation");
        StartCoroutine(StartJellyKingAttack()); //it can't find its own coroutine? //now it just stoppped calling the coroutine
        ConnectReferences();
        EnemyHealth = 3;
        Debug.Log($"EnemyHealth = {EnemyHealth}");
        
    }
    void Update()
    {
        enemyRigidBody.linearVelocity = new Vector2(enemyMoveSpeed * direction, enemyRigidBody.linearVelocityY);
        //Debug.Log($"This boss's health is {enemyHealth}");
        //if(enemyHealth == 1)
        //{
        //    Debug.Log("Bruh why are you one?");
        //}
    }
    
    private void ShootFireBall()
    {
        //we need to get the enemy's current direction
        
        moveDirectionOfNewProjectiles.x = moveDirectionOfNewProjectiles.x * direction;
        
        GameObject NewProjectile = Instantiate(database.projectile, projectileSpawnLocation.position, Quaternion.identity);
        //SetProjectileSpeed?.Invoke(moveDirectionOfNewProjectiles); //it should kinda just shoot a fireball without moving //it looks as though its not receiving the new position?\
        //I'M GETTING A NULL REFERENCE EXCEPTION
        
       //the projectile hasn't been born yet... i hate this i hate this i hate this i hate this i can't solve it


        // so this is moving, but it gets stuck moving. Instead, create an event and subscribe to the event when the newbie is created, which tells the newbie to move itself
        // MoveTowards
    }
    protected override void damageThisEnemy(int damageToBeDealt)
    {
        StartCoroutine(FlashRed());
        EnemyHealth -= damageToBeDealt; //why does this work? I think the method is grabbing the EnemyScript's health instead of the JellyKing's health
        
    }
    private void ConnectReferences()
    {
        //so you see, a lot of stuff should be moving to this in the future. why haven't I moved it yet? I like pain! sooo
        enemyHurtBoxScript = gameObject.GetComponentInChildren<EnemyHurtBoxScript>();
        enemyHurtBoxScript.OnThisEnemyDies += ThisEnemyDies;
        enemyHurtBoxScript.OnDamageThisEnemy += damageThisEnemy;
    }
    protected override void ThisEnemyDies()
    {
        BestowPlayerKey();
        Debug.Log("Bleh");
        Destroy(gameObject);
        
    }
    //can't wait to build the FireBall! How will it work?
    //this is just a hook for now, so we create the fire
    private IEnumerator StartJellyKingAttack()
    {
        while(true)
        {
            Debug.Log("is this running?");
            ShootFireBall();
            yield return new WaitForSeconds(2.0f);
        }
    }
    private void BestowPlayerKey()
    {
        //player.GetComponentInChildren<PlayerInventoryScript>(); //we need to send a signal for the player to get a key
        if (DoesThisBossBestowAKey)
        {
            Debug.Log($"Key being bestowed: {KeyThisBossBestows}");
            OnGivePlayerKey?.Invoke(KeyThisBossBestows); 
        }

    }


}
