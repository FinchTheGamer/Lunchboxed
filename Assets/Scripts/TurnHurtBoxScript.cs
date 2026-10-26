using UnityEngine;
using UnityEngine.Events;
public class TurnHurtBoxScript : MonoBehaviour
{
    public UnityEvent EnemyTurnAroundEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            EnemyTurnAroundEvent.Invoke();
        }
        catch 
        {
            Debug.Log("something happened in the TurnHurtBox");
        }

    }

}
