using System.Runtime.Serialization;
using playerCode;
using Unity.VisualScripting;
using UnityEngine;

public class ToyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //let's just build something fun for once
    //I'm so tired. I've been tired. And I'll need something to test movement when I partition the player... someday
    public Rigidbody2D rigidbody2D;
       
       
    public float frequency = 2.0f;     //so uhh its like how fast

    int ticker = 0;
    float tempY;
    Vector3 goal;
    private Vector3 originalScale;
    // Update is called once per frame
    float sineValue;
    float cosineValue;
    float scale;
    int shouldFlip = 1;
    GameObject player; //I think I lost my mind writing this script. It's likely I'll have to disable all this code;
    PlayerScript playerScript;
    public SpriteRenderer spriteRenderer;
    private string apology = "I'm sorry for spilling all your coffee";
    void Start()
    {
        originalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerScript = player.GetComponent<PlayerScript>();
        Destroy(gameObject); //ha suicidal code;
    }
    float thePlayersDirection = 1;
    void Update()
    {
        
        sineValue = Mathf.Sin(Time.time * 0.5f);
        cosineValue = Mathf.Cos(Time.time * 5f);
        if (cosineValue <= 0)
        {
            cosineValue = 1;
        }
        scale = 1 + (sineValue * sineValue);

        transform.localScale = originalScale * scale;
        switch (Random.Range(0, 3))
        {
            case 0: // r, g , b
                spriteRenderer.color = new Color(scale, spriteRenderer.color.g, spriteRenderer.color.b);
                break;
            case 1:
                spriteRenderer.color = new Color(spriteRenderer.color.r, scale, spriteRenderer.color.b);
                break;
            case 2:
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, scale);
                break;
        }
        
        
        goal = player.transform.position; //what if this was a shield?


        thePlayersDirection = SetPlayerDirection();

        goal.x += 5 * thePlayersDirection;
        transform.position = Vector3.MoveTowards(transform.position, goal , cosineValue); //what?
        // I need so much practice
        //this is really cool!
    }
    
    private float SetPlayerDirection() //so... so... tired
    {
        if(playerScript.playerDirection > 0) //what does this return?
        {
            Debug.Log("Returning: " + (1 * shouldFlip));
            return 1 * shouldFlip;
        }
        else if(playerScript.playerDirection < 0)
        {
            Debug.Log("Returning: " + (-1 * shouldFlip));
            return -1 * shouldFlip;
        }
        else
        {
            return thePlayersDirection;
        }
    }
    //HOW CAN I GET IT TO MOVE IN A CIRCLE
    //LIKE DREAMSHIELD

    //that'd be so cool!
}
