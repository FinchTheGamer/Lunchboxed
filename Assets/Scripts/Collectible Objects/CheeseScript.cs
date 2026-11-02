using UnityEngine;

public class Cheese : CollectableObjectScript
{
    private float lengthThisPowerUpLasts = 10f;
    public delegate void CheeseDelegate(float powerUpLength);
    public event CheeseDelegate CheeseCollected;
    void Start()
    {
        CheeseDelegate returnCheeseLength = (lengthThisPowerUpLasts) => { };
    }
}
