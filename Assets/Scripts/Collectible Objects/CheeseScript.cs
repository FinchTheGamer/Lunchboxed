using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class Cheese : CollectableObjectScript
{
    
    public UnityEvent CollectThisCheese;
    //public delegate void CheeseDelegate(float powerUpLength);
    //public event CheeseDelegate CheeseCollected;
    void Start()
    {
        thisPowerupsBenefits = new PowerupBenefits(0, 10, 0, 0);
        lengthThisPowerUpLasts = 10f;
        //CheeseDelegate returnCheeseLength = (lengthThisPowerUpLasts) => { };
        //we need to make this... do what?
        GameObject Player = GameObject.FindGameObjectWithTag("PlayerTag");
        PlayerScript playerScript  = Player.GetComponent<PlayerScript>();
        //CollectThisCheese.AddListener(playerScript.CollectPowerUp); //what?
        //how can I return this?
        Type = "Cheese";
        typeIndex = 3;
    }
    protected override void collectThisItem()
    {
        // I think this just calls the normal collectThisItem
        base.CollectThisPowerup();

        
    }
    
}
