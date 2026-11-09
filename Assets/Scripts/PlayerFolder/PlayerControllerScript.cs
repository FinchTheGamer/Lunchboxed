using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    //how are we going to make this?
    //do we make an enum with button directions and just pass one event with enum
    [SerializeField] DatabaseScriptableObject database;
    public delegate void LeftRightMovementInputAction(float axis);
    public event LeftRightMovementInputAction OnLeftRightMovementInput; //argh argh argh args args args
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ugh I don't know how to get the enum!!

    }
    private void ConnectReferences()
    {
        
    }
    
    //this code is a skeleton for now
    /*
     You might be wondering why this is a skeleton. Its because Terra didn't count any of the time I spend moving prefabs and testing the game or instantiating new prefabs or tweaking values in editor
     Terra only counts whacking letters into my keyboard. This is preposterous. Do you want me to spend 3 hours colouring a brick?
     
     */
}
