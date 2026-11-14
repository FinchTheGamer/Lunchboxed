using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLauncherScript : MonoBehaviour
{
    public void LaunchLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
}
