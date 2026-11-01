using UnityEngine;
using System.Collections;
public class ProjectileScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    
    private Vector3 moveTowardsDirection; //this is not WHERE to move to, but rather how fast. Is that confusing? Yeah! but it was handy to package the data that way.
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTowardsDirection = transform.position;
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        JellyKingScript jellyKingScript = GameObject.FindGameObjectWithTag("JellyKingTag").GetComponent<JellyKingScript>();
        jellyKingScript.setProjectileTargetDestination += SetMoveTowards;
        StartCoroutine(Move());
    }

    private void OnBecameInvisible()
    {
        StopCoroutine(Move());
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
            transform.position = new Vector3((transform.position.x + moveTowardsDirection.x), (transform.position.y + moveTowardsDirection.y), (transform.position.z + moveTowardsDirection.z));
            yield return null;
        }

    }



}
