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
        winGame.AddListener(GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>().endLevelandLoadNextOne);
        winGame.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().thisPlayerWins);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTag"))
        {
            winGame.Invoke();
            spriteRenderer.enabled = false;

            // code here to completely end the game
        }
    }
    
    
}
