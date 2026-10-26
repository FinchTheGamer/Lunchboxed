using playerCode;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameOverScript : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    PlayerScript playerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        playerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>();
        DontDestroyOnLoad(gameObject);
    }
    public void runGameOver()
    {
        transform.position = playerScript.transform.position; //that block just sets this object's position to the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        spriteRenderer.enabled = true;
        //GameObject.FindGameObjectWithTag("MainTileMapTag").GetComponent<TilemapRenderer>().enabled = false; //finds the Tilemap under Grid and turns its sprites off. //this should be gone
        
        
    }
}
