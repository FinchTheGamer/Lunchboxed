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
        Debug.Log("List of keys: ");
        foreach (string key in Backpack)
        {
            Debug.Log(key);
        }
    }
    void OnDisable()
    {
        keyScript.keyCollected -= CollectAKeyHandler; //brooo am I even connected
    }
    private void CollectAKeyHandler(string txt)
    {
        //txt should be the key so
        //either this isn't receiving or the sender isn't transmitting...
        Debug.Log("Key I just got: " + txt);
        backpack.Add(txt);
    }
    private void reloadKeyConnections()
    {
        try
        {


            Debug.Log("Current scene = " + SceneManager.GetActiveScene().name); // at least it correctly knows the scene!
            Debug.Log("Attempting to reload key connections now!");
            Key PREVIOUSKEY = keyScript;

            //null means the tag exists, but no one in the scene has it

            //Debug.Log(GameObject.FindWithTag("KeyTag")); // this returns null
            //Debug.Log(GameObject.FindGameObjectWithTag("KeyTag").name); // I need to fix this...
                                                                        // we need more tests!
                                                                        //the code stops here. It cannot find GameObject.FindGameObjectWithTag("KeyTag)
                                                                        //why can't it find that? // I think Key2 might not even exist.
            
            keyScript = GameObject.FindGameObjectWithTag("KeyTag").GetComponent<Key>(); //so we are getting a null reference exception here. What line?
            if (keyScript == null)
            {
                Debug.Log("New key... is null... so there isn't even a new key");
            }
            else if (keyScript == PREVIOUSKEY)
            {
                Debug.Log("Its the same gosh darn key!");
            }
            //it found Level 4, so its not a problem with finding it...
            //gahhhh
            //keyScript.keyCollected += CollectAKeyHandler;


            //this problem is clear. This is failing to connect. but why? I don't understand
            //so apparently, if any key is collected, then this code will all fail. Why?
            //THE THING WE ARE REFERENCING DISABLES ITSELF //this is frustrating
            //WHAT IF WE ONLY DISABLE THE SCRIPT?
            //well disabling the script didn't help either
            //gosh darn these keys are failing again
            //PREVIOUSKEY.keyCollected -= CollectAKeyHandler; //risque? I could but I don't want to so I won't

            keyScript.keyCollected += CollectAKeyHandler;
            Debug.Log("Key Connections Reloaded!");
            Debug.Log(keyScript.name + " connected!");
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
