using System;
using UnityEngine;
using UnityEngine.Events;
public class EnemyHurtBoxScript : MonoBehaviour
{
    public GameObject enemy;
    public BoxCollider2D enemyHurtBox;
    public EnemySpawnerScript enemySpawnerScript;
    public delegate void ThisEnemyDiesAction();
    public ThisEnemyDiesAction OnThisEnemyDies;
    public delegate void DamageThisEnemyAction(int damageToBeDealt);
    public DamageThisEnemyAction OnDamageThisEnemy;
    private bool isEnemySpawnerScriptNull = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            enemySpawnerScript = GameObject.FindGameObjectWithTag("EnemySpawnerTag").GetComponent<EnemySpawnerScript>(); // this isn't returning anything, but that doesn't matter;
        }
        catch(NullReferenceException e) // I don't need to save e, but its fun
        {
            Debug.Log($"Error! Something went wrong! What exactly went wrong? Its a bit of a long story, but essentially {e}");
            isEnemySpawnerScriptNull = true;
        }
        enemy = gameObject.transform.parent.gameObject; //let's just get our parent
    }

    // Update is called once per frame
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.CompareTag("PlayerTag")))
        {
            if (collision.otherCollider == enemyHurtBox) //for some strange reason, "otherCollider" is OUR collider
            {
                CallDamageThisEnemy(collision.gameObject.GetComponent<PlayerScript>().Damage); 
                if (enemy.GetComponent<EnemyScript>().EnemyHealth <= 0)
                {
                    // I think I want a method
                    ThisEnemyDies(); //WARNING! Enemy only tries to die the moment it hits 0
                }
                
                if (!isEnemySpawnerScriptNull)
                {
                    enemySpawnerScript.enemiesOnScreen--; //let's make this an event to get rid of that dumb event;
                }
            }

        }


    }
    private void CallDamageThisEnemy(int damageToBeDealt)
    {
        OnDamageThisEnemy.Invoke(damageToBeDealt); //destroyed but I keep tryna access it?
        Debug.Log(damageToBeDealt);
    }
    private void ThisEnemyDies()
    {
        //call for the player to destroy itself
        OnThisEnemyDies.Invoke();
        //Debug.Log("This enemy died! Bleh!");
    }
}
