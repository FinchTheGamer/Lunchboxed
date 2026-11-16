using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelSelectCanvasScript : MonoBehaviour
{
    private GameObject StartScreenPanel;
    private GameObject LevelSelectScreenPanel;
    private GameObject PlayButtonCollider;
    private GameObject SceneLauncher;
    
    

    public delegate void LaunchLevelAction(int levelTolaunch);
    public event LaunchLevelAction OnLaunchLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConnectToPanels();
        SceneLauncher = GameObject.FindGameObjectWithTag("SceneLauncherTag");
        PlayButtonCollider = GameObject.FindGameObjectWithTag("PlayButtonColliderTag");
        LevelButtonScript.OnLevelButtonPressed += OnLevelButtonClick; //but when it is a static, you have to use the type instead of a specific instance
        //what 
    }
    private void ConnectToPanels()
    {
        StartScreenPanel = transform.Find("Start Screen Panel").gameObject;
        LevelSelectScreenPanel = transform.Find("Level Selection Menu Panel").gameObject;
    }
    //so when the button is pressed we are going to open up the panel
    public void OpenLevelSelectMenu()
    {
        //First we have to hide the previous menu, so let's shove it all into a panel
        StartScreenPanel.SetActive(false);
        PlayButtonCollider.SetActive(false);
        //And then we open up a 
        LevelSelectScreenPanel.SetActive(true);
    }//
    public void CloseLevelSelectMenu()
    {
        LevelSelectScreenPanel.SetActive(false); //so we close the level select screeen
        //and open the main one again
        StartScreenPanel.SetActive(true);
        PlayButtonCollider.SetActive(true);
    }
    public void OnLevelButtonClick(int buttonID)
    {
        OnLaunchLevel.Invoke(buttonID); //this isn't connected. Just one more error and we should be good this week
        // do we call the game manager to do something?
        //no we should call the scenelauncher to launch a scene i hate this chain
    }
    private void OnDestroy()
    {
        //I want to disconnect
        //LevelButtonScript.OnLevelButtonPressed -= OnLevelButtonClick
    }
}

