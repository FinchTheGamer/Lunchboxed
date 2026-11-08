using System.Collections.Generic;
using UnityEngine;

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

}
