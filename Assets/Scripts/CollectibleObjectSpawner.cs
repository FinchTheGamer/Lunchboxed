using UnityEngine;

public class CollectibleObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CollectibleObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createCollectibleObject(CollectibleObject, "Coin", 3, transform.position);
        createCollectibleObject(CollectibleObject, "Key", 0, new Vector2 (-10, 0));
    }

    public void createCollectibleObject(GameObject kind, string type, int typeIndex, Vector2 where)
    {
        GameObject newCollectibleObject = Instantiate(kind, where, Quaternion.identity);
        newCollectibleObject.GetComponent<CollectableObjectScript>().Type = type;
        newCollectibleObject.GetComponent<CollectableObjectScript>().TypeIndex = typeIndex;
    }
    
}
