using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseScriptableObject", menuName = "Scriptable Objects/DatabaseScriptableObject")]
public class DatabaseScriptableObject : ScriptableObject
{
    public Vector2[] respawnLocations = {new Vector2(-10, -3), new Vector2(-10, 4), new Vector2(60,60), new Vector2(62,62) }; //but if this is wrong, how is it loading the correct spawn data?
    public Sprite[] CollectibleObjectSpriteDatabase;
    private void OnEnable()
    {
        CollectibleObjectSpriteDatabase = Resources.LoadAll<Sprite>("Collectibles");
    }
}
