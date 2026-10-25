using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using playerCode;
using System;

public class GameManagerScript : MonoBehaviour
{
    public GameObject attackHitBox;
    public PlayerScript playerScript;
    public BoxCollider2D attackHitBoxCollider2D;
    public GameObject player;
    public Text text;
    [SerializeField] private Text coinCount;
    [SerializeField] private int lives = 3;
    public GameObject TurnFlag;

    public UnityEvent playerDies;
    
    
    public int Lives
    {
        get { return lives; }
        set { lives = value; if (value < 1) { lives = 0; } }
    }
    public int Coins { get; set; }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        attackHitBox.transform.position = new Vector3(0, 0, 0);
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerScript = player.GetComponent<PlayerScript>();
        playerDies.AddListener(playerScript.Die);
        
        Lives = 3;
        Coins = 0;
        text.text = "Lives: " + Lives.ToString();
        coinCount.text = ": " + Coins.ToString();
        Instantiate(TurnFlag, new Vector2(5f, -3.5f), Quaternion.identity);
        //GOAL: when the player presses x, teleport the attackHitBox to the player.
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            attackHitBox.transform.position = player.transform.position + new Vector3(1, 0, 0);
        }
        //EXPENSIVE
        text.text = "Lives: " + Lives.ToString(); //To String is expensive. Concatenation makes it even more expensive. Strings are immutable, so this allocated memory on the heap every frame
        coinCount.text = ": " + Coins.ToString();
        //try stringbuilder instead
        if (Lives <= 0)
        {
            playerDies.Invoke();
            endGame();
        }

        
    }
    public void endLevelandLoadNextOne()
    {
        Debug.Log("End Game");
        try
        {
            SceneManager.LoadScene("Level " + (playerScript.CurrentLevel + 1), LoadSceneMode.Single); // I need to write a function to fix this... but how can I fix that
        }
        catch(Exception e)
        {
            Debug.Log(e);
            //the game should end
        }
        

    }
    public void PlayerTakesDamage()
    {
        Lives--;
    }
    private void endGame()
    {
        Time.timeScale = 0f;
    }
    public void PlayerGetsACoin()
    {
        Coins++;
    }
}
    
