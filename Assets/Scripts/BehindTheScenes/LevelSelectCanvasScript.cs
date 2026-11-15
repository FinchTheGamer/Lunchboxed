using UnityEngine;

public class LevelSelectCanvasScript : MonoBehaviour
{
    private GameObject StartScreenPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConnectToPanels();
    }
    private void ConnectToPanels()
    {
        StartScreenPanel = transform.Find("Start Screen Panel").gameObject;
    }
    //so when the button is pressed we are going to open up the panel
    public void OpenLevelSelectMenu()
    {
        //First we have to hide the previous menu, so let's shove it all into a panel
        StartScreenPanel.SetActive(false);
        //And then we open up a 
    }//
}
