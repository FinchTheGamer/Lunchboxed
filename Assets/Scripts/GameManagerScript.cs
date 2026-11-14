using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using playerCode;
using System;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    private const string MEMORIAL = "I love you Lolo. I miss you. I'm sorry that we didn't talk more. You loved me so much. But I could never say it back.";
    public GameObject attackHitBox;
    public PlayerScript playerScript;
    public BoxCollider2D attackHitBoxCollider2D;
    public GameObject player;
    public Text text;
    [SerializeField] private Text coinCount;
    private int lives = 3;
    public GameObject TurnFlag;

    public UnityEvent playerDies;
    public UnityEvent perfectlySafeToReloadKeys;
    
    
    
    public int Coins { get; set; }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ConnectReferences();
        if (SceneManager.GetActiveScene().name == "Level 1") //bro I hard coded
        {
            OnFirstLevel();
        }
        

        

        
        //GOAL: when the player presses x, teleport the attackHitBox to the player.
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            attackHitBox.transform.position = player.transform.position + new Vector3(1, 0, 0);
        }
        //EXPENSIVE
        text.text = "Lives: " + playerScript.Lives.ToString(); //To String is expensive. Concatenation makes it even more expensive. Strings are immutable, so this allocated memory on the heap every frame
        coinCount.text = ": " + Coins.ToString();
        //try stringbuilder instead
        if (playerScript.Lives <= 0)
        {
            playerDies.Invoke();
            endGame();
        }
        if (playerScript.IsGameOver)
        {
            endGame();
        }
        
    } //wait how will I make any of this code work without... the player?
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //though, this should never be disabled really
    }
    private void OnFirstLevel()
    {
        playerScript.Lives = 3;
        Coins = 0;

        text.text = "Lives: " + playerScript.Lives.ToString();
        coinCount.text = ": " + Coins.ToString();
    }
    private void ConnectReferences()
    {
        attackHitBox.transform.position = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerScript = player.GetComponent<PlayerScript>();
        playerDies.AddListener(playerScript.Die);
        perfectlySafeToReloadKeys.AddListener(player.GetComponentInChildren<PlayerInventoryScript>().ReloadKeyConnections);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        safeToReloadKeys();
    }
    private void safeToReloadKeys()
    {
        perfectlySafeToReloadKeys.Invoke();
    }
    public void endLevelandLoadNextOne() //when this method is called, it needs to know when the scene is loaded.
    {
        //so what is going on
        Debug.Log("End Game");
        try
        {
            SceneManager.LoadScene("Level " + (playerScript.CurrentLevel + 1), LoadSceneMode.Single); // I need to write a function to fix this... but how can I fix that
            //wait til the scene is loaded? That'll need a wrapper...
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.Log(e);
            //the game should end
            endGame();

        }
        //its been a while since I've touched this code.

    }
    //public void PlayerTakesDamage()
    //{
    //    playerScript.Lives--;
    //}
    private void endGame()
    {
        Time.timeScale = 0f;
    }
    public void PlayerGetsACoin()
    {
        Coins++;
    }

    public void DebugInstantDeath()
    {
        playerScript.Lives = 0;
    }
}
    
