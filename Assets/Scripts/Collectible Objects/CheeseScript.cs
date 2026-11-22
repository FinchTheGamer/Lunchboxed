using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class Cheese : CollectableObjectScript
{
    private float lengthThisPowerUpLasts = 10f;
    public UnityEvent CollectThisCheese;
    //public delegate void CheeseDelegate(float powerUpLength);
    //public event CheeseDelegate CheeseCollected;
    void Start()
    {
        //CheeseDelegate returnCheeseLength = (lengthThisPowerUpLasts) => { };
        //we need to make this... do what?
        GameObject Player = GameObject.FindGameObjectWithTag("PlayerTag");
        PlayerScript playerScript  = Player.GetComponent<PlayerScript>();
        //CollectThisCheese.AddListener(playerScript.CollectPowerUp); //what?
        //how can I return this?
    }
    protected override void collectThisItem()
    {
        // I think this just calls the normal collectThisItem
        base.collectThisItem(); //what does base mean? I wish I understood anything;
    }
}
