using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//subscribe to an onclick event in level select canvas script?

public class LevelButtonScript : MonoBehaviour
{
    private Button thisButton;
    public delegate void LevelButtonPressedAction(int buttonID);
    public static event LevelButtonPressedAction OnLevelButtonPressed; 
    /* Because it is static, it is shared by everyone with this script
       That means that if you subscribe to one, you subscribe to all!*/
    [SerializeField] private int thisButtonID = 1; //starts from 1
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnThisLevelButtonPress);
        //
    }
    
    private void OnThisLevelButtonPress()
    {
        Debug.Log("I ran! Get it? strait of hormuz lol anyways a button ran ");
        OnLevelButtonPressed.Invoke(thisButtonID); //wait i forgor who listen to this
        //the canvas will listen to this
        //but how can it listen to everyone at once?
    }
    //so I need to do the thing where I automate it probably

    
}
