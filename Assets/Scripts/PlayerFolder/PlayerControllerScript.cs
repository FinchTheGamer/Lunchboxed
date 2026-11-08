using System;
using Unity.VisualScripting;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
//should be swapped with delegates later...
public class PlayerControllerScript : MonoBehaviour
{
    private GameObject character;
    public GameObject Player
    {
        get
        {
            if (character != null)
            {
                return character;
                
            }
            else
            {
                Debug.Log("I don't see a player here...");
                return character;
            }

            
        }
        set
        {
            character = value;
        }
    }
    NewPlayerScript playerScript;
    Rigidbody2D characterRigidBody;

    PlayerAnimatorScript playerAnimatorScript;

    public delegate void SetPlayerAnimationBool(string name, bool trueOrFalse);
    public event SetPlayerAnimationBool setPlayerAnimationBool; // i suck at naming things

    public delegate void SetPlayerSpriteDirectionHandler(bool isPlayerFacingBackwards);
    public event SetPlayerSpriteDirectionHandler OnSetPlayerSpriteDirection;

    //time for a lesson On delegates! See below!
    private bool isTouchingGround = false;
    
    private float playerDirection;
    private int jumpStrength;
    private int speed;

    //decoupled code!
    
    void Start()
    {
        ConnectReferences();
        jumpStrength = 7;
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
        
    }
    private void OnDestroy() //could do this for OnDisable, don't feel like it
    {
        Player.GetComponent<NewPlayerScript>().ReturnRecoil -= Recoil;
    }
    private void MovementHandler()
    {
        isTouchingGround = playerScript.IsTouchingGround;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isTouchingGround)
        {
            characterRigidBody.linearVelocity = new Vector2(0, 1) * jumpStrength; //setting this value equal to a vector will be different to adding a vectory onto the value
        }
        playerDirection = Input.GetAxis("Horizontal");
        characterRigidBody.linearVelocity = new Vector2(playerDirection * speed, characterRigidBody.linearVelocityY);
        if (characterRigidBody.linearVelocityX < 0)
        {
            //playerSpriteRenderer.flipX = false;
            OnSetPlayerSpriteDirection.Invoke(true);
            //FlipCharacterSpriteDirection.Invoke();
            
        }
        else if (characterRigidBody.linearVelocityX > 0)
        {
            //playerSpriteRenderer.flipX = false;
            //brooo what am I even doing
            OnSetPlayerSpriteDirection.Invoke(false);
            //FlipCharacterSpriteDirection.Invoke();
        }
        
        if (playerDirection != 0 && !(playerAnimatorScript.IsFalling))
        {
            //playerAnimator.SetBool("isRunning", true);
            setPlayerAnimationBool.Invoke("isRunning", true);
        }
        else
        {
            //playerAnimator.SetBool("isRunning", false);
            setPlayerAnimationBool.Invoke("isRunning", false);
        }
    }
    private void ConnectReferences()
    {
        Player = gameObject.transform.parent.transform.gameObject; //our game object's transform's parent's transform's game object (so just the parent of this game object)
        characterRigidBody = Player.GetComponent<Rigidbody2D>();
        playerScript = Player.GetComponent<NewPlayerScript>();
        playerAnimatorScript = Player.GetComponentInChildren<PlayerAnimatorScript>();
        Player.GetComponent<NewPlayerScript>().ReturnRecoil += Recoil;
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



        characterRigidBody.linearVelocity = new Vector3(Convert.ToInt32(differenceOfX) * speed, Convert.ToInt32(differenceOfY) * speed, Convert.ToInt32(differenceOfZ) * speed);
        //Ah! But that makes that zero!
        //instead, use these boolean to set a final number (dizzying amount of failed code and variables I used like once. I could reuse them!)
    }

    public void Recoil(Vector3 thingyouhit)
    {
        recoil(thingyouhit);
    }
    //we are either going to need a million different parameters in one event or a million different events
}


/*
    How to name a delegate/event:
    Delegates should have Handler or Action at the end
    Events should start with On because they are actions
    SetPlayerAnimationBoolAction
    OnSetPlayerAnimationBool
 */