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
        Type = "Cheese"; //what is the use of cheese?
        typeIndex = 3;
        //I guess I should record my thoughts...
        //the plan was for everyone to inherit from collectible object script only
    }
    protected override void collectThisItem()
    {
        // I think this just calls the normal collectThisItem
        base.CollectThisPowerup(); //base?
        //so base should be a reference to our parent class right?
        //my fingers hurt a lot
        //the only reason I was doing this was because it was causing issues
        //I suppose that's a bit obvious
        //I'm captain obvious aren't I
    }
    
}
