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
    private GameObject attackHitBox;
    private PlayerScript playerScript;
    private BoxCollider2D attackHitBoxCollider2D;
    private GameObject player;
    private Text LivesText;
    private Text coinCount;
    private int lives = 3;
    private bool isStartScreen = true;
    //public GameObject TurnFlag; //why should I be spawning turnflags?

    public UnityEvent playerDies;
    public UnityEvent perfectlySafeToReloadKeys;
    
    
    
    public int Coins { get; set; }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().name == "Level 1") //bro I hard coded
        {
            isStartScreen = false;
            ConnectReferences();
            OnFirstLevel();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;





        //GOAL: when the player presses x, teleport the attackHitBox to the player.
    }
    
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //attackHitBox.transform.position = player.transform.position + new Vector3(1, 0, 0);
        //}
        if (!isStartScreen)
        {
            CheckIfGameOver();
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
        
        //remember to connect references
        playerScript.Lives = 3;
        Coins = 0;

        LivesText.text = "Lives: " + playerScript.Lives.ToString();
        coinCount.text = ": " + Coins.ToString();
    }
    private void ConnectReferences()
    {
        //attackHitBox.transform.position = new Vector3(0, 0, 0);
        //Player
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerScript = player.GetComponent<PlayerScript>();
        playerDies.AddListener(playerScript.Die);
        perfectlySafeToReloadKeys.AddListener(player.GetComponentInChildren<PlayerInventoryScript>().ReloadKeyConnections);
        playerScript.OnSetLiveCount += UpdateLiveCount;
        //Player
        //Lives Text
        LivesText = GameObject.FindGameObjectWithTag("LivesTextTag").GetComponent<Text>();
        //Lives Text
        //Coin Count
        coinCount = GameObject.FindGameObjectWithTag("CoinsCollectedTag").GetComponent<Text>();
        //Coin Count
        //TurnFlag Prefab
        //TurnFlag Prefab
    }
    private void UpdateCoinCount(int newCoinCount) //this shouldn't need happen at all
    {
        //EXPENSIVE
        
        //coinCount.text = ": " + Coins.ToString(); //it can't find it? 
        coinCount.text = ": " + newCoinCount;// 0 is temporary
        //alright alright alright alright we can do this so the player has the UI so they should broadcast the message
        //try stringbuilder instead
        //i hate this now we have to rebuild all this code
    }
    private void UpdateLiveCount(int newLiveCount)
    {
        //LivesText.text = "Lives: " + playerScript.Lives.ToString(); //To String is expensive. Concatenation makes it even more expensive. Strings are immutable, so this allocated memory on the heap every frame
        LivesText.text = "Lives: " + newLiveCount;
    }

    private void CheckIfGameOver()
    {
        if (playerScript.Lives <= 0)
        {
            playerDies.Invoke();
            endGame();
        }
        if (playerScript.IsGameOver)
        {
            endGame();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Level 1") //bro I hard coded
        {
            isStartScreen = false;
            ConnectReferences();
            OnFirstLevel();
        }
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
        UpdateCoinCount(Coins);
    }

    public void DebugInstantDeath()
    {
        playerScript.Lives = 0;
    }
    private void OnDestroy()
    {
        playerScript.OnSetLiveCount -= UpdateLiveCount;
    }
}
    
