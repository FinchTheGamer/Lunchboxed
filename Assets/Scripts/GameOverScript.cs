using playerCode;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameOverScript : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    public void runGameOver()
    {
        transform.position = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().transform.position; //that block just sets this object's position to the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        spriteRenderer.enabled = true;
        GameObject.FindGameObjectWithTag("MainTileMapTag").GetComponent<TilemapRenderer>().enabled = false; //finds the Tilemap under Grid and turns its sprites off.
        
        
    }
}
