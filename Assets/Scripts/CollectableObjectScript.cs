using UnityEngine;
using UnityEngine.Events;
public class CollectableObjectScript : MonoBehaviour
{
    [SerializeField] protected string type;
    [SerializeField] protected int typeIndex;
    [SerializeField] protected bool overrideType = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    protected Sprite[] collectibleObjectSpriteList;
    [SerializeField] private Key keyScript;
    protected GameManagerScript gameManagerScript;
    public UnityEvent itemCollected;
    private PlayerInventoryScript playerInventoryScript;

    float sine;
   
    private Vector3 SITTINGPOSITION;
    public string Type
    {
        get { return type; }
        set { type = value; }
    }
    public int TypeIndex
    {
        get { return typeIndex; }
        set { typeIndex = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start() //protected so Unity doesn't throw an error and virtual so it can be overwritten by a child class
    {
       
        gameManagerScript = GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>();
        playerInventoryScript = GameObject.FindGameObjectWithTag("PlayerInventoryTag").GetComponent<PlayerInventoryScript>();
        //itemCollected.AddListener(gameManagerScript.PlayerGetsACoin); don't need this anymore
        itemCollected.AddListener(playerInventoryScript.CollectCoin); //this is all going to have to be rewritten to add different coin denominations...
        //Wait it won't! Because GAME can just get the player coin count alone!
        //okay so this will ping the player to collect a coin, and that will tell the game manager to update the script
        //wouldn't it be kind of expensive to do it this way?
        //ugh but the other way around doesn't sound better! Having the player subscribe to every single coin sounds a lot worse

        collectibleObjectSpriteList =  Resources.LoadAll<Sprite>("Collectibles"); //this loads in all of them! Be careful! This is nowhere near best practice //its not finding anything!
        
        
        if (!overrideType)
        {
            type = "Coin";
            typeIndex = 1;
        }
        setSpriteViaIndex(typeIndex);
    }

    private void Awake()
    {
        SITTINGPOSITION = transform.position; //dangerous to set things in awake, ain't it
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
        {
            //hit
            collectThisItem();
            
        }
    }
    private void FixedUpdate() //what on earth is fixed update?
    {
        
        sine = Mathf.Sin(Time.time);
        //WE GOT THE JUICE IT FEELS GOOD I DON'T LIKE HOW THEY BOB IN UNION
        
        transform.position = new Vector3(transform.position.x, (((SITTINGPOSITION.y) + (sine * 0.1f))), transform.position.z); //gotta change that y value
        //guh all of them are shifting up whyy
        //why minus 1? I don't know, but for some reason it works half the time
    }
    protected virtual void collectThisItem()
    {
        itemCollected.Invoke();
        gameObject.SetActive(false);
        //so we invoke item collected
        //Its like changing a linked list in a way
        //this invoke needs to point to a different method and the subscriber method needs to listen to a different invoke
        //that's kinda cool
        //and afterwards, run away
    }
    protected void setSpriteViaIndex(int spriteIndex)
    {
        if (spriteIndex >= 0)
        {
            spriteRenderer.sprite = collectibleObjectSpriteList[spriteIndex];
        }
    }
    private void OnDestroy()
    {
        collectibleObjectSpriteList = null; //nullptr
        //all unused assets must be set to null before this statement below 
        Resources.UnloadUnusedAssets(); // <-- this one
    }
}
