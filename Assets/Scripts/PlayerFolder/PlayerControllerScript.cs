using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
//should be swapped with delegates later...
public class PlayerControllerScript : MonoBehaviour
{
    GameObject character;
    public GameObject Character
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

    Rigidbody2D characterRigidBody;
    private bool isTouchingGround = false;
    private float playerDirection;
    private int jumpStrength;
    private int speed;

    //decoupled code!
    private UnityEvent FlipCharacterSpriteDirection;
    void Start()
    {
        Character = GameObject.FindGameObjectWithTag("PlayerTag");
        characterRigidBody = Character.GetComponent<Rigidbody2D>();
        jumpStrength = 7;
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
    }
    private void MovementHandler()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isTouchingGround)
        {
            characterRigidBody.linearVelocity = new Vector2(0, 1) * jumpStrength; //setting this value equal to a vector will be different to adding a vectory onto the value
        }
        playerDirection = Input.GetAxis("Horizontal");
        characterRigidBody.linearVelocity = new Vector2(playerDirection * speed, characterRigidBody.linearVelocityY);
        if (characterRigidBody.linearVelocityX < 0)
        {
            //playerSpriteRenderer.flipX = false;
            //FlipCharacterSpriteDirection.Invoke();
        }
        else if (characterRigidBody.linearVelocityX > 0)
        {
            //playerSpriteRenderer.flipX = false;
            //FlipCharacterSpriteDirection.Invoke();
        }
    }

    //we are either going to need a million different parameters in one event or a million different events
}
