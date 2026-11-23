using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    //how are we going to make this?
    //do we make an enum with button directions and just pass one event with enum
    [SerializeField] DatabaseScriptableObject database;
    public delegate void LeftRightMovementInputAction(float axis);
    public event LeftRightMovementInputAction OnLeftRightMovementInput; //argh argh argh args args args
    public delegate void JumpMovementInputAction(bool playerWantsToJump);
    public event JumpMovementInputAction OnJumpMovementInput;
    private float playerDirection;
    private bool doesPlayerWantToJump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ugh I don't know how to get the enum!! 
        //uh oh! What do I do???!?!?!? This was... easier than expected!!!
        //I guess we prep mobile friendly dev

    }
    void Update() //maybe this will be very expensive... ah well not MY problem
    { //hmm we'll need to check if the user is a mobile player or desktop player... later... we'll build the code beforehand
        KeyboardMovementHandler();
        Debug.Log($"Player Direction = {playerDirection}");
    }
    private void KeyboardMovementHandler()
    {
        if (playerDirection != Input.GetAxis("Horizontal"))
        {
            playerDirection = Input.GetAxis("Horizontal");
            OnLeftRightMovementInput?.Invoke(playerDirection); //yelling into the void here...
            
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))) //we don't need to know if the player can jump, we just need to know if they want to
        {
            doesPlayerWantToJump = true;
            OnJumpMovementInput.Invoke(doesPlayerWantToJump);
            //setting this value equal to a vector will be different to adding a vectory onto the value
            //how will we stop the player from wanting to jump? oh...
            Debug.Log("Player wants to jump");
        } //we'll need to invent a way...
        if (!(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            doesPlayerWantToJump = false;
            OnJumpMovementInput.Invoke(doesPlayerWantToJump);
            Debug.Log("Player doesn't want to jump");
        }
        //but hackatime gives me no time to breathe and think
        //every 2 minutes I have to mush again
    }
    private void MobileMovementHandler()
    {
        //stump? //yeah pretty much
        //this is going to be empty
        //I don't actually need to call the methods below because they are called by onclick
    }
    public void OnLeftButtonPress()
    {
        //alright after these are connected we need to update inputs
        playerDirection = -1;
        OnLeftRightMovementInput.Invoke(playerDirection);
        Debug.Log("Left button was pressed");
    }
    public void OnRightButtonPress()
    {
        playerDirection = 1;
        OnLeftRightMovementInput.Invoke(playerDirection);
        Debug.Log("Right button was pressed");
    }
    public void OnUpButtonPress()
    {
        doesPlayerWantToJump = true;
        OnJumpMovementInput.Invoke(doesPlayerWantToJump);
        Debug.Log("Up button was pressed");
    }
    public void OnDownButtonPress()
    {
        //TEMPT
        doesPlayerWantToJump = false;
        playerDirection = 0;
        Debug.Log("Down button was pressed");
    }
    //the finale
    //this code is a skeleton for now

}
