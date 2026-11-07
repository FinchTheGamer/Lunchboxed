using System.Collections;
using playerCode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    
    public PlayerScript playerScript;
    public GameManagerScript gameManagerScript;
    
    protected int direction = -1;
    private float enemyMoveSpeed = 1;
    private int enemyHealth = 1;
    public int EnemyHealth
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
    
    public Rigidbody2D playerRigidBody;
    protected Vector3 localScale;
    
    public enum EnemyModifierType
    {
        Normal,
        Fast,
        Strong
    }

    struct EnemyTypeAttributes
    {
        public int enemyTypeSpeed;
        public int enemyTypeDamage;
        public int enemyTypeHealth;
        //Colour is funny spelling but I think I'll use color so no one thinks I'm a Briton
        public Color enemyTypeColor;
        
        public EnemyTypeAttributes(int speed, int damage, int health, Color colour): this() //what on earth does this() do?
        {
            enemyTypeSpeed = speed;
            enemyTypeDamage = damage;
            enemyTypeHealth = health; //if something goes wrong, let's be honest, it's this
            enemyTypeColor = colour;
        }

    }

    public EnemyModifierType enemyModifierType = EnemyModifierType.Normal; //hashmap would be helpful to return the modifiers
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        enemyRigidBody = gameObject.GetComponent<Rigidbody2D>(); //let's practice getting our components manually.
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //what kind of enemy are we?
        

        playerScript = player.GetComponent<PlayerScript>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();
        
        Hashtable enemyTypeHashmap = new Hashtable() //each kind of enemy equals a certain set of statistics, packaged neatly into the EnemyTypeAttributes struct
        {
            {EnemyModifierType.Normal, new EnemyTypeAttributes(1, 1, 1, spriteRenderer.color) },//ugh but I'll need a struct tooo
            {EnemyModifierType.Fast, new EnemyTypeAttributes(2, 1, 1, new Color(spriteRenderer.color.r, 0.5f, spriteRenderer.color.b))},//modify health over the entire game so that this speed can be halved
            {EnemyModifierType.Strong, new EnemyTypeAttributes(1, 1, 2, Color.red) } //same for the strength... everything in this game will need to go up by ten or so
        };

        //I'm beginning to hate coding
        EnemyTypeAttributes enemyTypeAttributes = (EnemyTypeAttributes)enemyTypeHashmap[enemyModifierType]; //our type attributes equal the one from the hashtable
        Debug.Log("I am " + enemyModifierType + " and my colour is "+ enemyTypeAttributes.enemyTypeColor); //what??
        enemyMoveSpeed = enemyTypeAttributes.enemyTypeSpeed; //sets speed
        spriteRenderer.color = enemyTypeAttributes.enemyTypeColor; //sets colour

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
        direction = direction * -1; //if direction = 1, then it'll flip to -1. If direction = -1, then it'll flip to 1. Genius!\
        localScale = transform.localScale; //so that we can modify it, because localscale.x is normally read only
        localScale.x *= -1;
        transform.localScale = localScale; 
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
