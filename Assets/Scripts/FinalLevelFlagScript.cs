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
        winGame.AddListener(GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>().WinGame); // this is a reference to the one in GameManager.
        //should there be a different winGame method in Game? it'd kinda be encapsulation mostly... but I'll add it. 
        winGame.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().thisPlayerWins);
        //should the finallevelflag tell the gameoverscript that we won? or should GAME?
        winGame.AddListener(GameObject.FindGameObjectWithTag("GameOverSplashScreenTag").GetComponent<GameOverScript>().RunGameVictory);
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
    
    //so this should boot back to the main menu
}
