using System.Collections;
using UnityEngine;

public class SpiritScript : EnemyScript
{
    private float sine;
    private bool shouldChasePlayer = true;
    private Vector3 SITTINGPOSITION;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        enemyRigidBody.gravityScale = 0;
        spriteRenderer.color = new Color(0,1,0,0.5f);
        SITTINGPOSITION = transform.position;
        //StartCoroutine(ChasePlayer());
    }
    // I want it to bob along a sine wave
    void Update()
    {
        sine = Mathf.Sin(Time.time);
        //Debug.Log("Normal sine: " + sine);
        //Debug.Log("Sprite sine: " + (transform.position.y + sine));
        transform.position = new Vector3(transform.position.x, (((SITTINGPOSITION.y) + (sine * 0.5f))), transform.position.z);
        
    }

    private IEnumerator ChasePlayer()
    {
        while (shouldChasePlayer)
        {
            transform.position = new Vector3(transform.position.x, Vector3.MoveTowards(transform.position, player.transform.position, 0.1f).x, transform.position.z);
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("PlayerTag"))
        { // I <3 nested if
            if (shouldChasePlayer)
            {
                shouldChasePlayer = false; //bro she not chasing player
            }

        }
    }
}
