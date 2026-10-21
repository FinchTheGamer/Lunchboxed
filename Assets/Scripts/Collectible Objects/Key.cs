using UnityEngine;

public class Key : CollectableObjectScript
{
    
    [SerializeField] private DatabaseScriptableObject database;
    private void Start()
    {
        Debug.Log("uhhh");
        gameManagerScript = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();
        itemCollected.AddListener(gameManagerScript.PlayerGetsACoin); // change this


        collectibleObjectSpriteList = database.CollectibleObjectSpriteDatabase; //this loads in all of them! Be careful! This is nowhere near best practice
        overrideType = true;
        if (!overrideType)
        {
            type = "Key";
            typeIndex = 0;
        }
        type = "Key";
        typeIndex = 0;
        setSpriteViaIndex(0);
    }

    public void KeyCollected()
    {
        Debug.Log("What key?");
    }

}
