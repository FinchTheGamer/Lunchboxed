using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventoryScript : MonoBehaviour
{
    Key keyScript;
    [SerializeField] private DatabaseScriptableObject database;

    private List<string> backpack = new List<string> { };

    public List<string> Backpack { get { return backpack; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyScript = GameObject.FindGameObjectWithTag("KeyTag").GetComponent<Key>();
        keyScript.keyCollected += CollectAKeyHandler;
    }
    void OnDisable()
    {
        keyScript.keyCollected -= CollectAKeyHandler;
    }
    private void CollectAKeyHandler(string txt)
    {
        //txt should be the key so
        //either this isn't receiving or the sender isn't transmitting...
        Debug.Log(txt);
        backpack.Add(txt);
    }
    private void reloadKeyConnections()
    {
        try
        {


            Debug.Log(SceneManager.GetActiveScene().name); // at least it correctly knows the scene!

            //null means the tag exists, but no one in the scene has it

            Debug.Log(GameObject.FindWithTag("KeyTag")); // this returns null
            Debug.Log(GameObject.FindGameObjectWithTag("KeyTag").name); // I need to fix this...
                                                                        // we need more tests!
                                                                        //the code stops here. It cannot find GameObject.FindGameObjectWithTag("KeyTag)
                                                                        //why can't it find that? // I think Key2 might not even exist.

            Key keyScript = GameObject.FindGameObjectWithTag("KeyTag").GetComponent<Key>(); //so we are getting a null reference exception here. What line?
            //keyScript.keyCollected += CollectAKeyHandler;


            //this problem is clear. This is failing to connect. but why? I don't understand
            //so apparently, if any key is collected, then this code will all fail. Why?
            //THE THING WE ARE REFERENCING DISABLES ITSELF //this is frustrating
            //WHAT IF WE ONLY DISABLE THE SCRIPT?
            //well disabling the script didn't help either
            //gosh darn these keys are failing again
            Debug.Log("Key Connections Reloaded!");
            Debug.Log(keyScript.name + "Key connected!");
        }
        catch (Exception e)
        {
            Debug.Log(e);

        }
    }

    public void ReloadKeyConnections()
    {
        reloadKeyConnections(); // i have reached the pinnacle of obfuscation/wrapping/encapsulation
    }

}
