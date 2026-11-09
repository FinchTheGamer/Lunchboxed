using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    GameObject player;
    NewPlayerScript playerScript;
    PlayerMovement playerMovement;
    SpriteRenderer playerSpriteRenderer;
    private  Animator playerAnimator;
    private bool isFalling; // I'm not sure the event updating this is being connected
    public bool IsFalling
    {
        get { return isFalling; }
        private set { value = isFalling; }
    }
    private bool isFacingRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConnectReferences();
    }

    
    private void ConnectReferences()
    {
        player = transform.parent.gameObject;
        playerScript = player.GetComponent<NewPlayerScript>();

        playerMovement = player.GetComponentInChildren<PlayerMovement>();

        //if we are falling
        playerScript.PlayerCurrentAnimationBool += SetCurrentPlayerFallingState; //we are subscribing!

        //is this even connected?
        // is this below if we are running? we need better names
        playerMovement.setPlayerAnimationBool += SetCurrentPlayerAnimation;
        //Player's current direction
        playerMovement.OnSetPlayerSpriteDirection += SetPlayerSpriteDirection;

        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        playerScript.PlayerCurrentAnimationBool -= SetCurrentPlayerAnimation;
        playerMovement.setPlayerAnimationBool -= SetCurrentPlayerAnimation;
        playerMovement.OnSetPlayerSpriteDirection -= SetPlayerSpriteDirection;
    }
    private void SetCurrentPlayerAnimation(string name, bool ciertoOFalso)
    {
        //this is if the player is running
        //Debug.Log(name + " says " + IsFalling );
        //so we know at some point, IsRunning is becoming True
        

        playerAnimator.SetBool(name, ciertoOFalso); //this'll throw an error so fast
        //Debug.Log(name);
        //Debug.Log(playerAnimator.GetBool(name));
    }

    private void SetCurrentPlayerFallingState(string name, bool ciertoOFalso)
    {
        //Debug.Log("yooo animation is like sick man");
        //if (name == "isFalling") { IsFalling = ciertoOFalso; } // is this expensive?
        IsFalling = ciertoOFalso;
        playerAnimator.SetBool(name, ciertoOFalso);
    }
    private void SetPlayerSpriteDirection(bool isFacingRightward) //not to be confused with the global
    {
        isFacingRight = isFacingRightward;
        playerSpriteRenderer.flipX = isFacingRight;
    }
    // Update is called once per frame
    
}
