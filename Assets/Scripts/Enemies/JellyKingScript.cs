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
    
    private  int  enemyHealth = 3;
    public new int  EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
        set
        {
            enemyHealth = value;
        }
    }
    public delegate void JellyKingDelegate(float moveDirectionX, float moveDirectionY, float moveDirectionZ);
    public event JellyKingDelegate setProjectileTargetDestination;
    
    
    private Vector3 moveDirectionOfNewProjectiles = new Vector3(-5, 0, 0);
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
        JellyKingDelegate sendMoveDirection = (moveDirectionX,moveDirectionY,moveTowardsZ) => { }; //creates a quick lil lambda that just returns the MoveDirection
        projectileSpawnLocation = transform.Find("ProjectileSpawnLocation");
        StartCoroutine(StartJellyKingAttack()); //it can't find its own coroutine? //now it just stoppped calling the coroutine
        
        
    }
    
    private void ShootFireBall()
    {
        //we need to get the enemy's current direction
        
        moveDirectionOfNewProjectiles.x = moveDirectionOfNewProjectiles.x * direction;
        
        GameObject NewProjectile = Instantiate(database.projectile, projectileSpawnLocation.position, Quaternion.identity);
        
        setProjectileTargetDestination?.Invoke(moveDirectionOfNewProjectiles.x, moveDirectionOfNewProjectiles.y, moveDirectionOfNewProjectiles.z); //it should kinda just shoot a fireball without moving //it looks as though its not receiving the new position?\
        //I'M GETTING A NULL REFERENCE EXCEPTION
        
       


        // so this is moving, but it gets stuck moving. Instead, create an event and subscribe to the event when the newbie is created, which tells the newbie to move itself
        // MoveTowards
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
    

}
