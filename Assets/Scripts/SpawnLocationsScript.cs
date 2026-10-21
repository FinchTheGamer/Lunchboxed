using UnityEngine;

[CreateAssetMenu(fileName = "SpawnLocationsScript", menuName = "Scriptable Objects/SpawnLocationsScript")]
public class SpawnLocationsScript : ScriptableObject
{
    //we should use an object pool! Instead of destroying a coin, simply deactivate it when collective, and send it to an off screen pool
    public Vector2[] coinSpawnLocations = {new Vector2(0,0) };
}
