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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(gameObject);

        BackToMenuGameObject = GameObject.FindGameObjectWithTag("BackToMenuButtonTag");
        BackToMenuButton = BackToMenuGameObject.GetComponent<Button>();

        GAME = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>(); //this needs to be run in the first level, so be careful

        BackToMenuButtonText = BackToMenuGameObject.GetComponentInChildren<Text>();
        BackToMenuButton.onClick.AddListener(GAME.ReturnToMainMenu);

        BackToMenuButtonImage = BackToMenuGameObject.GetComponentInChildren<Image>();

        BackToMenuButton.enabled = false;
        BackToMenuButtonImage.enabled = false;
        BackToMenuButtonText.enabled = false;
        //wait but if its off who will turn it on? ugh this is a can of worms
        //I guess someone will have to turn it on?
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ActivateBackToMenuButton()
    {
        BackToMenuButton.enabled = true;
        BackToMenuButtonImage.enabled = true;
        BackToMenuButtonText.enabled = true;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
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

        BackToMenuButtonImage = BackToMenuGameObject.GetComponentInChildren<Image>();
    }

}
