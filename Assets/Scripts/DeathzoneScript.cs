using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class DeathzoneScript : MonoBehaviour
{
    public GameObject GAME;
    public GameManagerScript gameManagerScript;
    public UnityEvent playerTouchedDeathzone;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GAME = GameObject.FindGameObjectWithTag("GAMETag");
        gameManagerScript = GAME.GetComponent<GameManagerScript>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //that's our sprite! So cool!
        playerTouchedDeathzone.AddListener(gameManagerScript.DebugInstantDeath);

        spriteRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTag"))
        {
            playerTouchedDeathzone.Invoke();
        }
    }

}
