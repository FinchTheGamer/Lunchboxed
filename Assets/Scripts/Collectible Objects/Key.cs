using UnityEngine;

public class Key : CollectableObjectScript
{
    
    [SerializeField] private DatabaseScriptableObject database;

    [SerializeField] string thisKey = "22222222";
    public delegate void KeyDelegate(string valueToSend);

    public event KeyDelegate keyCollected;
    
    private void Start()
    {
        KeyDelegate returnKeyData = (thisKey) => { };

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collectThisItem();
    }

    protected override void collectThisItem()
    {
        Debug.Log("This ran");
        itemCollected.Invoke();
        keyCollected?.Invoke(thisKey); //SO FOR SOMEREASON THIS IS FAILING
        gameObject.SetActive(false);
    }
}
