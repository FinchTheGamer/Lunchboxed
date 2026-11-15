
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;

public class GameOverScript : MonoBehaviour
{
    //this'll also double as the win screen. Will I be changing this? No.
    SpriteRenderer spriteRenderer;
    PlayerScript playerScript;
    private Sprite[] SplashScreenSpriteList;
    public UnityEvent activateBackToMenuButton;
    private const int GAMEOVERINDEX = 0; //0 is Game Over, 1 is You Win
    private const int YOUWININDEX = 1; //hmmm the player is falling while triggering YOUWIN
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareSprites();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        spriteRenderer.sprite = SplashScreenSpriteList[GAMEOVERINDEX]; 

        playerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>();

        activateBackToMenuButton.AddListener(GameObject.FindGameObjectWithTag("MainUITag").GetComponent<CanvasScript>().ActivateBackToMenuButton);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void runGameOver()
    {
        transform.position = playerScript.transform.position; //that block just sets this object's position to the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        spriteRenderer.sprite = SplashScreenSpriteList[GAMEOVERINDEX];
        spriteRenderer.enabled = true;
        activateBackToMenuButton.Invoke();
        //GameObject.FindGameObjectWithTag("MainTileMapTag").GetComponent<TilemapRenderer>().enabled = false; //finds the Tilemap under Grid and turns its sprites off. //this should be gone
        //who on earth is creating the player?\
        //so it turns out that because the player is, too, in the first scene, loading back to that scene finds... a second player
        
    } //the PROBLEM IS THAT GAME IS SETTING TIMESCALE TO 0, STOPPING EVERYTHING, SO YOU SHOULD LIKE FIX THAT
    public void RunGameVictory()
    {
        transform.position = new Vector3(playerScript.transform.position.x, playerScript.transform.position.y, -9); //ha I compressed it
        spriteRenderer.sprite = SplashScreenSpriteList[YOUWININDEX]; //i've become numb to how much time I'm spending on Terra
        spriteRenderer.enabled = true;
        activateBackToMenuButton.Invoke();
        
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Resources.UnloadUnusedAssets();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Level Select")
        {
            spriteRenderer.enabled = false;
        }
    }
    private void PrepareSprites()
    {
        SplashScreenSpriteList = Resources.LoadAll<Sprite>("SplashScreens");
        
    }
    

    
}
