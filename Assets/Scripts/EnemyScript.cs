using playerCode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody;
    public GameObject player;
    
    public PlayerScript playerScript;
    public GameManagerScript gameManagerScript;
    
    [SerializeField] private float enemyMoveSpeed = 1;
    public Rigidbody2D playerRigidBody;

    
  
    
    private int direction = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        enemyRigidBody = gameObject.GetComponent<Rigidbody2D>(); //let's practice getting our components manually.

        

        playerScript = player.GetComponent<PlayerScript>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        enemyRigidBody.linearVelocity = new Vector2(enemyMoveSpeed * direction, enemyRigidBody.linearVelocityY);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), enemyMoveSpeed * Time.deltaTime);
        // the enemy will move from its position, to a new Vector3. This vector 3 has the x and z values of the player, but the y value of the enemy, so the enemy shouldn't fly.
        
        
    }
    public void turnAround()
    {
        direction = direction * -1; //if direction = 1, then it'll flip to -1. If direction = -1, then it'll flip to 1. Genius!
    }

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurnFlagTag"))
        {
            turnAround();
        }
        
    }
    private void followPlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            enemyRigidBody.linearVelocity = new Vector2((enemyMoveSpeed), enemyRigidBody.linearVelocityY); //we can equalize the direction real quick
        }
        else
        {
            enemyRigidBody.linearVelocity = new Vector2(enemyMoveSpeed * -1, enemyRigidBody.linearVelocityY);
        }
    }
}
