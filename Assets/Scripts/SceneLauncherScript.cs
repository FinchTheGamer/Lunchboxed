using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneLauncherScript : MonoBehaviour
{
    
    public void LaunchLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
}
