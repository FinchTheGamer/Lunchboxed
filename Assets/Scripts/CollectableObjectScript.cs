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
        itemCollected.AddListener(gameManagerScript.PlayerGetsACoin);
        


        collectibleObjectSpriteList =  Resources.LoadAll<Sprite>("Collectibles"); //this loads in all of them! Be careful! This is nowhere near best practice
        //Debug.Log(collectibleObjectSpriteList.Length);
        if (!overrideType)
        {
            type = "Coin";
            typeIndex = 3;
        }
        setSpriteViaIndex(typeIndex);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag"))
        {
            //hit
            collectThisItem();
            
        }
    }
    protected virtual void collectThisItem()
    {
        itemCollected.Invoke();
        gameObject.SetActive(false);
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
