using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;
using NUnit.Framework.Constraints;
public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    
    private Vector3 moveTowardsDirection = new Vector3(-10, 0, 0); //this is not WHERE to move to, but rather how fast. Is that confusing? Yeah! but it was handy to package the data that way.
    public Vector3 MoveTowards 
    {
        get
        {
            return moveTowardsDirection;
        }
        set
        {
            moveTowardsDirection = value;
        }
    }

    private void SetMoveTowards(Vector3 newMoveTowards)
    {
        MoveTowards = newMoveTowards;
        //this is running... but what's being passed to it?
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0f;
        gameObject.SetActive(true);
        GameObject JellyKing = GameObject.FindGameObjectWithTag("JellyKingTag");
        JellyKingScript jellyKingScript = JellyKing.GetComponent<JellyKingScript>();
        jellyKingScript.setProjectileTargetDestination += SetMoveTowards;
        
        StartCoroutine(Move());

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I hit something, but is it friend or foe?");
        
        
            Debug.Log("I hit something!");
            StopCoroutine(Move());
            gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        
    }

    
    private IEnumerator Move()
    {
        while(gameObject.activeInHierarchy == true)
        {
            
            rigidbody.linearVelocity = moveTowardsDirection;
            
            yield return null;
        }

    }



}
