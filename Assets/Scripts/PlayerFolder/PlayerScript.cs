using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//THIS IS THE NEW PLAYER
public class PlayerScript : MonoBehaviour
{
    //Is it okay for this to double as a general controller and a hurtbox?
    [SerializeField] private DatabaseScriptableObject database;
    public UnityEvent reloadKeyConnections;
    public UnityEvent gameOver;
    public UnityEvent takeDamage;
    //should the player control them taking damage? no... the GAME should rip the damage from the player
    //public delegate void TakeDamageAction(int damage);
    //public event TakeDamageAction OnTakeDamage; I spent time writing these before realizing the game manager only really needs to read
    public delegate void ReturnRecoilDelegate(Vector3 wheretheenemyis);
    //still don't get delegates, never will
    public event ReturnRecoilDelegate ReturnRecoil;
    public delegate void SetPlayerCurrentAnimationBool(string animationName, bool ciertoOFalso);
    public event SetPlayerCurrentAnimationBool PlayerCurrentAnimationBool;
    public delegate void SetLiveCountAction(int newLiveCount);
    public event SetLiveCountAction OnSetLiveCount; //get the GAME to subscribe to this
    //public delegate void SetCoinCountAction(int newCoinCount);
    //public event SetCoinCountAction OnSetCoinCount; //the player... for some reason... doesn't control their coin count... the coin does... this is a travesty
    // we should flay whoever wrote all this
    //I wrote all this
    private GameManagerScript GAME;
    private GameObject playerInventory;
    private PlayerInventoryScript playerInventoryScript;
    private Rigidbody2D playerRigidBody;
    private bool isTouchingGround;
    public bool IsTouchingGround
    {
        get
        {
            return isTouchingGround; //anyone can check isTouchingGround
        }
        private set
        {
            value = isTouchingGround; //no one gets to change isTouchingGround
        }
    }
    private bool isFalling;
    public bool IsFalling { get { return isFalling; } private set { isFalling = value; } }
    
    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } private set { isGameOver = value; } }
    private bool isInvincibile = false;
    private float invincibilityFramesRemaining = 0; //defaults to 1
    private int currentLevel = 0;
    public int CurrentLevel //don't need to do it manually but I did it anyways
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
        }
    }

    private int lives = 3;
    public int Lives // turns out moving this to the player didn't take much work at all!
    {
        get { return lives; }
        set { lives = value; if (value < 1) { lives = 0; } } // I could do something funny here and make lives equal abs(lives)
    } //doesn't work that bad at all!

    private void recoil(Vector3 locationOfEnemy)
    {
        ReturnRecoil?.Invoke(locationOfEnemy); //doesn't throw a fit if its null
    }
    public void Die()
    {
        transform.position = database.respawnLocations[currentLevel];
        isGameOver = true;
        gameOver.Invoke(); //calls game over
    }
    public void newLevel()
    {
        currentLevel++; //this needs to be the only time that the level is updated.

        

        transform.position = database.respawnLocations[currentLevel]; // okay so it shifts to the next level
                                                                      //alright alright so the above is running but the below isn't
        //reloadKeyConnections(); hmm gotta send a message to reloadKeyConnections();
        reloadKeyConnections.Invoke();
        //this is so stupid what on earth is going on... wait what if key uhhh connections are being loaded before the new level drops? do I need to set it up in the game?
    }
    
    private void ConnectReferences()
    {
        //Debug.Log(transform.parent.gameObject); //we can't find our dad
        //we are the dad! // or the mother?
        playerInventory = GameObject.Find("PlayerInventory"); //hopefully this returns our sibling, PlayerInventory
        // it can't find the playerInventory
        playerInventoryScript = playerInventory.GetComponent<PlayerInventoryScript>();
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        GAME = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();
        reloadKeyConnections.AddListener(playerInventoryScript.ReloadKeyConnections); 
        gameOver.AddListener(GameObject.FindGameObjectWithTag("GameOverSplashScreenTag").GetComponent<GameOverScript>().runGameOver); //polished up
        if (takeDamage == null)
        {
            takeDamage = new UnityEvent(); //if this event isn't already assigned, then begin to assign it
        }
        //takeDamage.AddListener(GAME.PlayerTakesDamage);
    }
    public void thisPlayerWins() { Debug.Log("I win"); transform.position = new Vector2(0, 100); isGameOver = true; gameOver.Invoke(); }
    void Start()
    {
        ConnectReferences();
        DontDestroyOnLoad(gameObject);
    }
    private void TakeDamage(int damageInt)
    {
        Lives -= damageInt;
        OnSetLiveCount.Invoke(Lives);
    }
    private void Update()
    {
        if (isInvincibile) // if the player is invincible
        {
            if (invincibilityFramesRemaining == 0) //and is no longer supposed to invincible
            {
                isInvincibile = false; //then don't be invincible
            }
            else if (invincibilityFramesRemaining <= 0) //and has a timer that is below 0
            {
                invincibilityFramesRemaining = 0; //then fix the timer
            }
            else //and still has i-frames remaining
            {
                invincibilityFramesRemaining -= Time.deltaTime; //then tick the timer down
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject refers to the parent if the parent and child share a RigidBody
        //collision.collider.gameObject refers to the parent or child, depending on who was hit.

        //playerAnimator.SetBool("isFalling", false);
        isTouchingGround = ((collision.gameObject.layer == 6));
        PlayerCurrentAnimationBool?.Invoke("isFalling", false);
        isFalling = false;
        if (collision.gameObject.layer == 7 && !(collision.collider.gameObject.CompareTag("EnemyHurtBoxTag")) && (invincibilityFramesRemaining == 0))
        {
            //good thing I seperated them earlier
            Debug.Log(collision.gameObject.GetComponent<EnemyScript>().EnemyDamage);
            TakeDamage(collision.gameObject.GetComponent<EnemyScript>().EnemyDamage);
            //how are we going to pass the damage...
            Debug.Log("The player has taken damage!");
            Vector3 location_of_enemy = collision.transform.position;
            recoil(location_of_enemy);
            isInvincibile = true;
            invincibilityFramesRemaining = 1;

        }
        if ((collision.gameObject.CompareTag("ObstacleTag") || collision.gameObject.layer == 9) && (invincibilityFramesRemaining == 0))
        {
            TakeDamage(1); //defaults to 1...

            Vector3 location_of_enemy = collision.transform.position;
            recoil(location_of_enemy);
            isInvincibile = true;
            invincibilityFramesRemaining = 1;
        }
        if (collision.gameObject.layer == 7 && collision.collider.gameObject.CompareTag("EnemyHurtBoxTag"))
        {
            playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocityX, 5);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isTouchingGround = ((collision.gameObject.layer == 6)); //hmm... how often should this run? I don't want to force isTouchingGround to constantly flip!
        //THEN AGAIN
        if (isTouchingGround)
        {
            //playerAnimator.SetBool("isFalling", false);
            PlayerCurrentAnimationBool?.Invoke("isFalling", false);
            isFalling = false;
        }
        if (collision.gameObject.layer == 7 && !(collision.collider.gameObject.CompareTag("EnemyHurtBoxTag")) && (invincibilityFramesRemaining == 0))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyScript>().EnemyDamage);
            Debug.Log("The player has taken damage!");
            isInvincibile = true;
            invincibilityFramesRemaining = 1;

        }
        if (collision.gameObject.CompareTag("ObstacleTag") && (invincibilityFramesRemaining == 0)) // if an obstacle has been hit
        {
            TakeDamage(1);

            Vector3 location_of_enemy = collision.transform.position;
            recoil(location_of_enemy);
            isInvincibile = true;
            invincibilityFramesRemaining = 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerCurrentAnimationBool?.Invoke("isFalling", true); //this is our key point! apparently, its never being set to true
        isFalling = true;
        //why won't it be being set to true? is it updating at all? it appears to be...
        if (isTouchingGround && collision.gameObject.layer == 6)
        {
            isTouchingGround = false;
        }
        if (collision.gameObject.CompareTag("MoveableTileTag"))
        {
            transform.SetParent(null, worldPositionStays: true);
            DontDestroyOnLoad(gameObject);
            isTouchingGround = false;
        }
    }


}
