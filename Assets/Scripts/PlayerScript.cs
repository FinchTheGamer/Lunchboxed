using System;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace playerCode
{
    public class PlayerScript : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public Rigidbody2D playerRigidBody;
        public int speed;
        public int jumpStrength; 
        public SpriteRenderer playerSpriteRenderer;
        public Animator playerAnimator;
        public float playerDirection;
        [SerializeField] private bool isTouchingGround = false;
        public UnityEvent takeDamage;
        public UnityEvent gameOver;

        private bool isGameOver = false;
        private bool isInvincibile = false;
        [SerializeField] private float invincibilityFramesRemaining = 0; //defaults to 1
        public GameManagerScript GAME;
        
        [SerializeField] private DatabaseScriptableObject database;

        private List<string> backpack = new List<string> { };

        public List<string> Backpack {  get { return backpack; } }
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
        //private Vector2[] spawnCoordinates = {new Vector2(0,0), new Vector2(-10, 4)};
        
        void Start()
        {
            
            DontDestroyOnLoad(gameObject);
            GAME = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();

            Key keyScript = GameObject.FindGameObjectWithTag("KeyTag").GetComponent<Key>();
            keyScript.keyCollected += CollectAKeyHandler;

            speed = 5;
            jumpStrength = 7;
            playerAnimator.SetBool("isFalling", true);
            
            if (takeDamage == null)
            {
                takeDamage = new UnityEvent(); //if this event isn't already assigned, then begin to assign it
            }
            takeDamage.AddListener(GAME.PlayerTakesDamage);
            gameOver.AddListener(GameObject.FindGameObjectWithTag("GameOverSplashScreenTag").GetComponent<GameOverScript>().runGameOver); //polished up
        }

        // Update is called once per frame
        void Update()
        {
            if (!isGameOver)
            {
                movementHandler();
            }
            if (isGameOver)
            {
                playerRigidBody.bodyType = RigidbodyType2D.Static;
                playerAnimator.enabled = false;
                playerSpriteRenderer.enabled = false;
            }
            
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
            
            //tests confirm that the code is skipping straight from 0 to 2
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //collision.gameObject refers to the parent if the parent and child share a RigidBody
            //collision.collider.gameObject refers to the parent or child, depending on who was hit.

            playerAnimator.SetBool("isFalling", false);

            if (collision.gameObject.layer == 7 && !(collision.collider.gameObject.CompareTag("EnemyHurtBoxTag")) && (invincibilityFramesRemaining == 0))
            {
                takeDamage.Invoke();
                Debug.Log("The player has taken damage!");
                Vector3 location_of_enemy = collision.transform.position;
                recoil(location_of_enemy);
                isInvincibile = true;
                invincibilityFramesRemaining = 1;
                
            }
            if (collision.gameObject.CompareTag("ObstacleTag") && (invincibilityFramesRemaining == 0))
            {
                takeDamage.Invoke();

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

            isTouchingGround = collision.gameObject.layer == 6;
            if(isTouchingGround)
            {
                playerAnimator.SetBool("isFalling", false);
            }
            if (collision.gameObject.layer == 7 && !(collision.collider.gameObject.CompareTag("EnemyHurtBoxTag")) && (invincibilityFramesRemaining == 0))
            {
                takeDamage.Invoke();
                Debug.Log("The player has taken damage!");
                isInvincibile = true;
                invincibilityFramesRemaining = 1;

            }
            if (collision.gameObject.CompareTag("ObstacleTag") && (invincibilityFramesRemaining == 0)) // if an obstacle has been hit
            {
                takeDamage.Invoke();

                Vector3 location_of_enemy = collision.transform.position;
                recoil(location_of_enemy);
                isInvincibile = true;
                invincibilityFramesRemaining = 1;
            }

        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            playerAnimator.SetBool("isFalling", true);
            if (isTouchingGround && collision.gameObject.layer == 6)
            {
                isTouchingGround = false;
            }
        }
        public void newLevel()
        {
            currentLevel++; //this needs to be the only time that the level is updated.
            Debug.Log(currentLevel);
            transform.position = database.respawnLocations[currentLevel]; // how out of bounds? database.respawnLocations 0 and 1 should be fine??
        }
        private void OnTriggerEnter2D(Collider2D collision) //remember that this code assumes that the HurtBox isn't a trigger, but the turn box is!
        {
            if (collision.CompareTag("EnemyTag") || collision.CompareTag("EnemyTurnHurtBoxTag"))
            {
                takeDamage.Invoke();
                isInvincibile = true;
                invincibilityFramesRemaining = 1;
            }
        }
        private void movementHandler()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && isTouchingGround)
            {
                playerRigidBody.linearVelocity = new Vector2(0, 1) * jumpStrength; //setting this value equal to a vector will be different to adding a vectory onto the value
            }
            playerDirection = Input.GetAxis("Horizontal");
            playerRigidBody.linearVelocity = new Vector2(playerDirection * speed, playerRigidBody.linearVelocityY);
            if (playerRigidBody.linearVelocityX < 0)
            {
                playerSpriteRenderer.flipX = true;
            }
            else if (playerRigidBody.linearVelocityX > 0)
            {
                playerSpriteRenderer.flipX = false;
            }
            
            
            if (playerDirection != 0 && !playerAnimator.GetBool("isFalling"))
            {
                    playerAnimator.SetBool("isRunning", true);
            }
            else
            {
                    playerAnimator.SetBool("isRunning", false);
            }
           


        }
        public void Die()
        {
            transform.position = database.respawnLocations[currentLevel];
            isGameOver = true;
            gameOver.Invoke();
        }
        private void recoil(Vector3 positionOfObjectThatYouHit)
        {
            float differenceOfX = (transform.position.x - positionOfObjectThatYouHit.x) * -1; //let's say we are at 90 and they are at 85. That means they are on our left, and this returns -5
            float differenceOfY = (transform.position.y - positionOfObjectThatYouHit.y) * -1;
            float differenceOfZ = (transform.position.z - positionOfObjectThatYouHit.z) * -1; // I've reversed the directions to hopefully make it more intuitive
            //can we remove the difference of this? perhaps renaming would actually be better.
            Vector3 directionOfImpact = new Vector3(differenceOfX, differenceOfY, differenceOfZ); //so if the impact came from the north west, it'll be positive, negative, zero
            Debug.Log(directionOfImpact.ToString() + " from recoil");
            //bool isDirectionOfImpactRight = differenceOfX > 0; // so by our example, this should be false
            //bool isDirectionOfImpactUp = differenceOfY > 0;
            //bool isDirectionOfImpactForward = differenceOfZ > 0;
            //first the x coordinate
            if (differenceOfX > 0)
            {
                differenceOfX = 1;
            }
            else if (differenceOfX == 0)
            {
                differenceOfX = 0; //there really isn't any reason to do this but
            }
            else
            {
                differenceOfX = -1;
            }
            //and now the y coordinate
            if (differenceOfY > 0)
            {
                differenceOfY = 1;
            }
            else if (differenceOfY == 0)
            {
                differenceOfY = 0;
            }
            else
            {
                differenceOfY = -1;
            }
            //and finally the z coordinate
            if (differenceOfZ > 0)
            {
                differenceOfZ = 1;
            }
            else if (differenceOfZ == 0)
            {
                differenceOfZ = 0;
            }
            else
            {
                differenceOfZ = -1;
            }
            //at this point in time, differenceOfX,Y,Z will now only be -1, 0, and 1



            playerRigidBody.linearVelocity = new Vector3(Convert.ToInt32(differenceOfX) * speed, Convert.ToInt32(differenceOfY) * speed, Convert.ToInt32(differenceOfZ) * speed); 
            //Ah! But that makes that zero!
            //instead, use these boolean to set a final number (dizzying amount of failed code and variables I used like once. I could reuse them!)
        }

        private void CollectAKeyHandler(string txt)
        {
            //txt should be the key so
            Debug.Log(txt);
            backpack.Add(txt);
        }
        public void thisPlayerWins() { Debug.Log("I win"); transform.position = new Vector2(0, 100); isGameOver = true; }
        

    }
}
