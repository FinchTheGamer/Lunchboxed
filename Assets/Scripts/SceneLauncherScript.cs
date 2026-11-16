using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLauncherScript : MonoBehaviour
{
    LevelSelectCanvasScript levelSelectCanvasScript;
    public delegate void SetCurrentLevelAction(int whatToSetCurrentLevelTo);
    public event SetCurrentLevelAction OnSetCurrentLevel;

       
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name != "Level Bootstrapper")
        {
            ConnectReferences();
            
        }
    }
    private void ConnectReferences()
    {
        //levelSelectCanvasScript.OnLaunchLevel -= LaunchLevel; //it can't find the level select canvas? oh
        //hasConnectedReferences = true;
        if (SceneManager.GetActiveScene().name == "Level Select")
        {
            levelSelectCanvasScript = GameObject.FindGameObjectWithTag("LevelSelectCanvasTag").GetComponent<LevelSelectCanvasScript>();
            levelSelectCanvasScript.OnLaunchLevel += LaunchLevel;
        }
    }
    public void LaunchLevel(int levelToLaunch = 1)
    {
        Debug.Log("Launch Level ran");
        OnSetCurrentLevel.Invoke(levelToLaunch);//null reference exception?? ohhh
        Debug.Log("Player Current Level updated from the Scene Launcher");
        SceneManager.LoadScene("Level"+ " " + levelToLaunch.ToString());
    }
    public void StartLevel1()
    {
        
        //encapsulation (I call it wrappers)
        LaunchLevel(1); //i need this for a button to work
        //I know its weird encapsulation
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if((scene.name != "Level Bootstrapper")) //maybe this will work (!hasConnectedReferences) 
        {
            ConnectReferences();
        }
    }

    void OnDestroy()
    {
        //levelSelectCanvasScript.OnLaunchLevel -= LaunchLevel;
        //null reference exception?? ohhh
    }
    //but when launching the scene we need to update the player's current level
    //so so so so what on earth am I doing
}
