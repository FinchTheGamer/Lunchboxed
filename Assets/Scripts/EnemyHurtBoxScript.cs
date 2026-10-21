using UnityEngine;

public class EnemyHurtBoxScript : MonoBehaviour
{
    public GameObject enemy;
    public BoxCollider2D enemyHurtBox;
    public EnemySpawnerScript enemySpawnerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemySpawnerScript = GameObject.FindGameObjectWithTag("EnemySpawnerTag").GetComponent<EnemySpawnerScript>();
    }

    // Update is called once per frame
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.CompareTag("PlayerTag")))
        {
            if (collision.otherCollider == enemyHurtBox) //for some strange reason, "otherCollider" is OUR collider
            {
                Debug.Log("Enemy dies 2!");
                Destroy(enemy);
                enemySpawnerScript.enemiesOnScreen--;
            }

        }


    }
}
