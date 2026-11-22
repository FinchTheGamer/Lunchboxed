
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
    public UnityEvent gameEnds;
    public UnityEvent backToMenuScreen;
    [SerializeField] private CanvasScript canvasScript;
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
        gameEnds.AddListener(canvasScript.OnGameOver);
        activateBackToMenuButton.AddListener(GameObject.FindGameObjectWithTag("MainUITag").GetComponent<CanvasScript>().ActivateBackToMenuButton);
        
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        //The GameOverScreen (or more accurately the YouWin screen) isn't being dragged along as a physical object..
    }
    public void runGameOver()
    {
        gameEnds.Invoke();
        transform.position = playerScript.transform.position; //that block just sets this object's position to the player's position
        transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        spriteRenderer.sprite = SplashScreenSpriteList[GAMEOVERINDEX]; //is the problem in RunGameVictory/runGameOver's publisher?
        //so the problem is something in this code
        //whoever is calling runGameOver is also calling something else that keeps the GameOverScreen tethered to the camera 
        //who calls runGameOver?
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
