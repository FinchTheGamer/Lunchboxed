using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseScriptableObject", menuName = "Scriptable Objects/DatabaseScriptableObject")]
public class DatabaseScriptableObject : ScriptableObject
{
    public Vector2[] respawnLocations = {new Vector2(-10, -3), new Vector2(-10, 4), new Vector2(-12,-7), new Vector2(-4, -3), new Vector2(-58, -15) }; //but if this is wrong, how is it loading the correct spawn data?
    public Sprite[] CollectibleObjectSpriteDatabase;
    private void OnEnable()
    {
        CollectibleObjectSpriteDatabase = Resources.LoadAll<Sprite>("Collectibles");
    }
}
