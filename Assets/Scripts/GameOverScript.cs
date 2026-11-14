
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameOverScript : MonoBehaviour
{
    
    SpriteRenderer spriteRenderer;
    PlayerScript playerScript;
    public UnityEvent activateBackToMenuButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        playerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>();

        activateBackToMenuButton.AddListener(GameObject.FindGameObjectWithTag("MainUITag").GetComponent<CanvasScript>().ActivateBackToMenuButton);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void runGameOver()
    {
        transform.position = playerScript.transform.position; //that block just sets this object's position to the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        spriteRenderer.enabled = true;
        activateBackToMenuButton.Invoke();
        //GameObject.FindGameObjectWithTag("MainTileMapTag").GetComponent<TilemapRenderer>().enabled = false; //finds the Tilemap under Grid and turns its sprites off. //this should be gone
        //who on earth is creating the player?\
        //so it turns out that because the player is, too, in the first scene, loading back to that scene finds... a second player
        
    } //the PROBLEM IS THAT GAME IS SETTING TIMESCALE TO 0, STOPPING EVERYTHING, SO YOU SHOULD LIKE FIX THAT
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Level Select")
        {
            spriteRenderer.enabled = false;
        }
    }
}
