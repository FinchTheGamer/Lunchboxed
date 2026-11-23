using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;

using System.Runtime.CompilerServices;

public class GameManagerScript : MonoBehaviour
{
    private const string MEMORIAL = "I love you Lolo. I miss you. I'm sorry that we didn't talk more. You loved me so much. But I could never say it back.";
    private GameObject attackHitBox;
    private PlayerScript playerScript;
    private PlayerInventoryScript playerInventoryScript;
    private BoxCollider2D attackHitBoxCollider2D;
    private GameObject player;
    private Text LivesText;
    private Text coinCount; //we need to update the bonusScore sometimes
    
    private int bonusScore = 0;
    private bool isStartScreen = true;
    private Scene lastScene;
    //public GameObject TurnFlag; //why should I be spawning turnflags?

    public UnityEvent perfectlySafeToReloadKeys;
    private GameObject doesPlayerAlreadyExist;
    public GameObject DoesPlayerAlreadyExist { get { return doesPlayerAlreadyExist; } set { doesPlayerAlreadyExist = value; } }
    
    public int Coins { get; set; }
    
    void Start()
    {
        //DEBUG 
        Coins = 0;
        //DEBUG
        lastScene = SceneManager.GetActiveScene();

        DontDestroyOnLoad(gameObject);
        Debug.Log("Will we run the first level?");
        
        if (!((SceneManager.GetActiveScene().name == "Level Select") || (SceneManager.GetActiveScene().name == "Level Bootstrapper"))) //bro I hard coded
        {
            isStartScreen = false;
            Debug.Log(isStartScreen);
            ConnectReferences();
            OnFirstLevel();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;





        //GOAL: when the player presses x, teleport the attackHitBox to the player.
    }
    
    //wait how will I make any of this code work without... the player?
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //though, this should never be disabled really
    }
    public void ReturnToMainMenu()
    {
        isStartScreen = true;
        SceneManager.LoadScene("Level Select");
    }
    private void OnFirstLevel()
    {
        
        //remember to connect references
        //playerScript.Lives = 3;
        

        LivesText.text = "Lives: " + playerScript.Lives.ToString();
        coinCount.text = ": " + playerInventoryScript.PlayerCoins.ToString(); //now we have to get this with delegates... on second thought... I might have caused myself more trouble
    }
    private void ConnectReferences()
    {
        //attackHitBox.transform.position = new Vector3(0, 0, 0);
        ConnectPlayerReferences();
        //Lives Text
        LivesText = GameObject.FindGameObjectWithTag("LivesTextTag").GetComponent<Text>();
        //Lives Text
        //Coin Count
        coinCount = GameObject.FindGameObjectWithTag("CoinsCollectedTag").GetComponent<Text>();
        //Coin Count
        //TurnFlag Prefab
        //TurnFlag Prefab
    }
    private void ConnectPlayerReferences()
    {
        //Player
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerScript = player.GetComponent<PlayerScript>();


        playerInventoryScript = player.GetComponentInChildren<PlayerInventoryScript>();
        playerInventoryScript.OnCollectACoin += PlayerGetsACoin;


        
        perfectlySafeToReloadKeys.AddListener(player.GetComponentInChildren<PlayerInventoryScript>().ReloadKeyConnections);
        playerScript.OnSetLiveCount += UpdateLiveCount;
        
        doesPlayerAlreadyExist = player;
        //Player
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

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!((SceneManager.GetActiveScene().name == "Level Select") || (SceneManager.GetActiveScene().name == "Level Bootstrapper"))) //bro I hard coded
        {
            isStartScreen = false;
            ConnectReferences();
            OnFirstLevel();
            int x = 1;
            
            if (!(lastScene.name == "Level Select"))
            {
                bonusScore += (100/x/x/x); //bonus score for winning more levels
                //ha see I can do math as well
                Debug.Log("Bonus score distributed");
            }
        }
        safeToReloadKeys();
        //someone is reloadeding the player to the first scene... ah  I see its that OnFirstLevel thing
        lastScene = scene;
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
        catch(Exception e)
        {
            Debug.Log(e);
            //the game should end
            endGame(); //due to the commented out code I'm kind of running this for no reason

        }
        //its been a while since I've touched this code.

    }
    //old code gone
    public void WinGame()
    {
        Debug.Log("You win!");
        //endLevelandLoadNextOne();
    }
    private void endGame()
    {
        //Time.timeScale = 0f;
        //and then after ending the game... whta??
        //what boots the GameOverScren anyways?
        //what is this even for??

    }
    
    public void restartGame() //restartGame is the one who is resetting the player's lives to 3
    {
        playerScript.Lives = 3;
    }
    public void PlayerGetsACoin()
    {
        
        //we could call the player right here, or we could have a chain of delegates and events. This is clearly the superior solution
        /* For future reference:
         Coin ------> calls Player to update the coin count number ------> calls GAME to update the coin count
         This'll be better for the eventual multiplayer update that is definitely coming and I won't quit the project before hand definitely*/
        UpdateCoinCount(playerInventoryScript.PlayerCoins);
        Debug.Log(playerInventoryScript.PlayerCoins);
    }

    public void DebugInstantDeath()
    {
        playerScript.Lives = 0;
    }
    private void OnDestroy()
    {
        Debug.Log(playerScript == null);
        
        playerScript.OnSetLiveCount -= UpdateLiveCount;
        playerInventoryScript.OnCollectACoin -= PlayerGetsACoin;
        
    }
    public int GetPlayerCoinCount()
    {
        return playerInventoryScript.PlayerCoins; //wow so so so so bad for practices but idc                                      
    }
    public int GetPlayerEnemiesStomped()
    {
        return playerScript.EnemiesStomped;
        //enmies stomped returned
    }
    public int GetPlayerPowerupsCollected()
    {
        //I'm so so sorry
        return playerScript.PowerupScore; //because the power up score is going to be in hundreds, so remember to manipulate it in the CanvasScript
    }
    public int GetPlayerBonusScore()
    {
        return bonusScore; // what does the triple  /// mean?
        //some games have a bonus score, so I felt that  having one as well  could be useful for
        //some kind of timer feature where a faster time would grant more score? I don't know
    }

}
    
