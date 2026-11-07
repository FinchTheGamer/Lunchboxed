using UnityEngine;
using System.Collections;

using NUnit.Framework.Constraints;
public class ProjectileScript : MonoBehaviour
{
    //Key insight! The projectile seems to move, but only after the signal is broadcasted a second time. Why?
    private Rigidbody2D rigidbody;
    GameObject JellyKing;
    JellyKingScript jellyKingScript;
    SpriteRenderer spriteRenderer;
    private Vector3 moveTowardsDirection = new Vector3(5, 0, 0); //this is not WHERE to move to, but rather how fast. Is that confusing? Yeah! but it was handy to package the data that way.
    public Vector3 MoveTowards //yooo
    {
        get
        {
            return moveTowardsDirection;
        }
        set
        {
            if (moveTowardsDirection == Vector3.zero) //so someone outside is setting MoveTowards to zero
            {
                Debug.Log("Someone touched me!"); //so its right here! Someone is setting this to zero
            }
            
            moveTowardsDirection = value;
        }
    }
    
    private void SetMoveTowards(Vector3 newMoveTowards) //this never ran... at all?????S
    {
        
        MoveTowards = newMoveTowards;
        Debug.Log("MoveTowards changed!");
        
       
        //Debug.Log("Twas " + MoveTowards); //so its being updated...
        //this is running... but what's being passed to it?
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.yellow;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0f;
        gameObject.SetActive(true);
        
        JellyKing = GameObject.FindGameObjectWithTag("JellyKingTag");
        jellyKingScript = JellyKing.GetComponent<JellyKingScript>();
        
        //jellyKingScript.SetProjectileSpeed += SetMoveTowards; // null reference exception!! //maybe it can't listen! It is being instantiated at the sametime!
        
        moveTowardsDirection.x = moveTowardsDirection.x * jellyKingScript.Direction;
        
        StartCoroutine(Move());

    }
    //ugh
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("I hit something, but is it friend or foe?");
        
        
            //Debug.Log("I hit something!");
            StopCoroutine(Move());
            gameObject.SetActive(false);
    }
    
    
    private void OnDestroy()
    {
        //jellyKingScript.SetProjectileSpeed -= SetMoveTowards;
    }

   
    private IEnumerator Move()
    {
        while(gameObject.activeInHierarchy == true)
        {
            //Debug.Log("It's now " + MoveTowards);
            
            rigidbody.linearVelocity = MoveTowards;
            
            yield return null;
        }

    }
    //its not running at birth! That's what's going on!
    //how do I get it to update at birth???


}
