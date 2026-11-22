using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasScript : MonoBehaviour
{
    GameManagerScript GAME;
    GameObject BackToMenuGameObject;
    Button BackToMenuButton;
    Image BackToMenuButtonImage;
    Text BackToMenuButtonText;

    [SerializeField] GameObject EndGamePanel;
    [SerializeField] Text FinalScoreText;
    private int totalCoinCount;
    private int enemiesStomped;
    private int powerupScore;
    private int finalScore;

    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        //putting too many things on dontdestroyonload might be a problem in the future for memory
        EndGamePanel.SetActive(false);
        //BackToMenuButton
        BackToMenuGameObject = GameObject.FindGameObjectWithTag("BackToMenuButtonTag");
        BackToMenuButton = BackToMenuGameObject.GetComponent<Button>();
        //BackToMenuButton
        GAME = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>(); //this needs to be run in the first level, so be careful

        BackToMenuButtonText = BackToMenuGameObject.GetComponentInChildren<Text>();
        BackToMenuButton.onClick.AddListener(GAME.ReturnToMainMenu);
        BackToMenuButton.onClick.AddListener(GAME.restartGame);

        BackToMenuButtonImage = BackToMenuGameObject.GetComponentInChildren<Image>();

        BackToMenuButton.enabled = false;
        BackToMenuButtonImage.enabled = false;
        BackToMenuButtonText.enabled = false;
        //wait but if its off who will turn it on? ugh this is a can of worms
        //I guess someone will have to turn it on?
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ActivateBackToMenuButton() //we'll need plenty more in the future
    {
        BackToMenuButton.enabled = true;
        BackToMenuButtonImage.enabled = true;
        BackToMenuButtonText.enabled = true;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) //we call a lot of these
    {
        ConnectBackToMenuButtonReferences();
        if(scene.name == "Level Select") //I think that's kinda dangerous
        {
            BackToMenuButton.enabled = false;
            BackToMenuButtonImage.enabled = false;
            BackToMenuButtonText.enabled = false;
            gameObject.GetComponent<Canvas>().enabled = (false); //who will reactivate this?
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true; 
        }
    }
    private void ConnectBackToMenuButtonReferences()
    {
        BackToMenuGameObject = GameObject.FindGameObjectWithTag("BackToMenuButtonTag");
        BackToMenuButton = BackToMenuGameObject.GetComponent<Button>();
        BackToMenuButtonText = BackToMenuGameObject.GetComponentInChildren<Text>();
        BackToMenuButton.onClick.AddListener(GAME.ReturnToMainMenu);
        BackToMenuButton.onClick.AddListener(OnResetGame);
        BackToMenuButtonImage = BackToMenuGameObject.GetComponentInChildren<Image>();
    }
    public void OnGameOver()
    {
        //we need to connect to the game manager, then they send us an update to everything?
        //wait this is a canvas it should be fine for this to be coupled with the GameManager a little tighter
        totalCoinCount = GAME.GetPlayerCoinCount();
        enemiesStomped = GAME.GetPlayerEnemiesStomped();
        powerupScore = GAME.GetPlayerPowerupsCollected();
        finalScore = (totalCoinCount * 100) + (enemiesStomped * 100) + (powerupScore * 100) + GAME.GetPlayerBonusScore(); //I know I could just do 100(coins*enemiesStomped*powerups) but I might mod the formula later
        FinalScoreText.text = $"\tFinal Score: {finalScore}\r\n\t-Coins: {totalCoinCount} -Enemies stomped: {enemiesStomped}\r\n\t-Powerups: {powerupScore} -Bonus: {GAME.GetPlayerBonusScore()}"; //THAT BETTER WORK
        EndGamePanel.SetActive(true);
    }
    public void OnResetGame()
    {
        EndGamePanel.SetActive(false);
    }

}
