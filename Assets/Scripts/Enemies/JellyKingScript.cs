using UnityEngine;
using UnityEngine.Events;
using static Key;
//what if we can make the enemy shoot fireballs?

public class JellyKingScript : EnemyScript
{
    //super important! "Original Prefab" when pulling a modified prefab into the editor means that this is a whole new prefab, unrelated to the first. Just happens to have had similar code
    //"Prefab variant" means changes to the original that aren't explicitly overwritten in the variant will be applied to the variant
    //the latter is useful for slightly modified objects, like Enemy and FastEnemy. 
    //the former is useful for things that are totally unrelated, like Enemy and DestroyableBox or Beholder or Ghoul
    private float enemyMoveSpeed = 2;
    private UnityEvent kickStartProjectile;
    [SerializeField] private DatabaseScriptableObject database;
    public delegate void JellyKingDelegate(Vector3 moveDirection);
    public event JellyKingDelegate setProjectileTargetDestination;

    private Vector3 moveDirectionOfNewProjectiles = new Vector3(-5, 0, 0);
    void Start()
    {
        JellyKingDelegate sendMoveDirection = (moveDirection) => { }; //creates a quick lil lambda that just returns the MoveDirection
    }
    void Update()
    {
        ShootFireBall();
    }

    private void ShootFireBall()
    {
        Debug.Log("What's going on?");
        GameObject NewProjectile = Instantiate(database.projectile);
        setProjectileTargetDestination?.Invoke(moveDirectionOfNewProjectiles);
        // so this is moving, but it gets stuck moving. Instead, create an event and subscribe to the event when the newbie is created, which tells the newbie to move itself
        
    }
    //can't wait to build the FireBall! How will it work?
    //this is just a hook for now, so we create the fire
}
