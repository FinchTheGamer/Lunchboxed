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
        itemCollected.AddListener(gameManagerScript.PlayerGetsACoin); // change this //why


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

        //do I exist? 
        Debug.Log(gameObject.name);
        //forget my name! License and registration, please!
        Debug.Log(gameObject.tag);
        
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
        //i need to figure out if this is never transmitting or if the receiver is never receiving


        // below is unrelated



        gameObject.SetActive(false); //HERE I SEE I UNDERSTAND
        //disableAllComponents();
    }
    private void disableAllComponents()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Key>().enabled = false;
    }
}
