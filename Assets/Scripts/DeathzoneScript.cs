using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class DeathzoneScript : MonoBehaviour
{
    public GameObject GAME;
    public GameManagerScript gameManagerScript;
    public UnityEvent playerTouchedDeathzone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GAME = GameObject.FindGameObjectWithTag("GAMETag");
        gameManagerScript = GAME.GetComponent<GameManagerScript>();

        playerTouchedDeathzone.AddListener(gameManagerScript.DebugInstantDeath);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTag"))
        {
            playerTouchedDeathzone.Invoke();
        }
    }

}
