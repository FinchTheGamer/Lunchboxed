using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseScriptableObject", menuName = "Scriptable Objects/DatabaseScriptableObject")]
public class DatabaseScriptableObject : ScriptableObject
{
    public Vector2[] respawnLocations = {new Vector2(-10, -3), new Vector2(-10, 4), new Vector2(-12,-7), new Vector2(-4, -3), new Vector2(-58, -15), new Vector2(-1, -6), new Vector2(0, -6) }; //but if this is wrong, how is it loading the correct spawn data?
    public Sprite[] CollectibleObjectSpriteDatabase; //how many levels should we have?
    

    private void OnEnable()
    {
        CollectibleObjectSpriteDatabase = Resources.LoadAll<Sprite>("Collectibles");
    }
    private void OnDisable()
    {
        CollectibleObjectSpriteDatabase = null;
        Resources.UnloadUnusedAssets();
    }
    public GameObject projectile;
    private const string MEMORIAL = "I love you Lolo. I miss you. I'm sorry that we didn't talk more. You loved me so much. But I could never say it back.";
    //ping Lemion with "what" in the slack

}
