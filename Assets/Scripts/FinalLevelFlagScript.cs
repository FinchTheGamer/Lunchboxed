using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class FinalLevelFlagScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public UnityEvent winGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.purple;
        winGame.AddListener(GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>().endLevelandLoadNextOne); // this is a reference to the one in GameManager.
        winGame.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().thisPlayerWins);
    }
    //if you saw earlier, we made the other flags run by a cos. could a tan work? no... it'd expand toward infinity
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTag"))
        {
            winGame.Invoke(); //woah woah is it really being connected?
            spriteRenderer.enabled = false; 
            

            // code here to completely end the game
        }
    }
    
    
}
