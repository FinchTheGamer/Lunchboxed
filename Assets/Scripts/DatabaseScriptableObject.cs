using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseScriptableObject", menuName = "Scriptable Objects/DatabaseScriptableObject")]
public class DatabaseScriptableObject : ScriptableObject
{
    public Vector2[] respawnLocations = {new Vector2(-10, -3), new Vector2(-10, 4), new Vector2(0,0) };
    public Sprite[] CollectibleObjectSpriteDatabase;
    private void OnEnable()
    {
        CollectibleObjectSpriteDatabase = Resources.LoadAll<Sprite>("Collectibles");
    }
}
