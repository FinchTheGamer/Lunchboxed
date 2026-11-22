using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventoryScript : MonoBehaviour
{
    Key keyScript;
    EnemyScript thisLevelsBoss;
    [SerializeField] private DatabaseScriptableObject database;

    private List<string> backpack = new List<string> { };

    public List<string> Backpack { get { return backpack; } }
    public delegate void CollectACoinAction();
    public event CollectACoinAction OnCollectACoin;

    //crazy concept, but I think the PlayerInventory should control the CoinCount...
    //I'm not sure how many problems that'll cause, but that's okay, because I don't care!
    private int playerCoins = 0;
    public int PlayerCoins { get { return playerCoins; } private set { playerCoins = value; } }
    //so if anyone does want to change it, they need to send an event
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ((SceneManager.GetActiveScene().name != "Level Select") && (SceneManager.GetActiveScene().name != "Level Bootstrapper"))
        {
            keyScript = GameObject.FindGameObjectWithTag("KeyTag").GetComponent<Key>();
            keyScript.keyCollected += CollectAKeyHandler;
        }
        
    }
    void OnDisable()
    {
        keyScript.keyCollected -= CollectAKeyHandler; //brooo am I even connected
        //when disabled... when is it disabled??
    }
    public void CollectCoin()
    {
        PlayerCoins = PlayerCoins + 1; //I love private set //I could use PlayerCoins++ but I don't want to
        Debug.Log("Player Inventory says that PlayerCoins is " + PlayerCoins.ToString());
        OnCollectACoin.Invoke();
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
        reloadBossKeyConnections();
    }
    private void reloadBossKeyConnections()
    {
        //I know this is really really really lazy but the code for this is just going to leech off of reloadKeyConnections
        //any time that function runs, this will too
        try
        {
            thisLevelsBoss.OnGivePlayerKey += CollectAKeyHandler;
        }
        catch { Debug.Log("There isn't a boss in this level!"); }
    }
    public void ReloadKeyConnections()
    {
        reloadKeyConnections(); // i have reached the pinnacle of obfuscation/wrapping/encapsulation
    }

}
