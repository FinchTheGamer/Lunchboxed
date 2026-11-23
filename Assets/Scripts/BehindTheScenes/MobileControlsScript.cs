using UnityEngine;
using UnityEngine.Events;
public class MobileControlsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //time to grind out unity events...
    public UnityEvent OnLeftButtonPress;
    //you might be wondering why I'm bothering to make this script at all if I'm just going to forward an event
    public UnityEvent OnRightButtonPress;
    //I don't know
    public UnityEvent OnUpButtonPress;
    //Do you know?
    public UnityEvent OnDownButtonPress;
    private PlayerControllerScript playerControllerScript;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerControllerScript = GameObject.FindGameObjectWithTag("PlayerTag").GetComponentInChildren<PlayerControllerScript>();
        ConnectReference();
        
    }
    private void ConnectReference()
    {
        OnLeftButtonPress.AddListener(playerControllerScript.OnLeftButtonPress);
        OnRightButtonPress.AddListener(playerControllerScript.OnRightButtonPress);
        OnUpButtonPress.AddListener(playerControllerScript.OnDownButtonPress);
        OnDownButtonPress.AddListener(playerControllerScript.OnUpButtonPress);
    }
    public void OnLeftButtonClick()
    {
        //very redundant
        OnLeftButtonPress.Invoke();
    }
    public void OnRightButtonClick()
    {
        OnRightButtonPress.Invoke();
    } //bro I can't fix these no matter what i do
    public void OnUpButtonClick()
    {
        //deutschland reference? l*a*nder reference (couldn't type umlaut l+ratio)
        OnUpButtonPress.Invoke();
        //The German Lander are essentially states of Germany. Germany has been  decentralized for a very long time now, barring the Third Reich
    }
    public void OnDownButtonClick()
    {
        OnDownButtonPress.Invoke();
    }

    
}
