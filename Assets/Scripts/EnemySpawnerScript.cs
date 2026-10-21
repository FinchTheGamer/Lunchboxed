using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public int enemiesOnScreen = 0;
    [SerializeField] private float enemySpawnTimer = 5;
    [SerializeField] private bool isThereSomethingInMySpawnZone = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("EnemyTag").Length;

    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnTimer -= Time.deltaTime;
        if (enemiesOnScreen < 3)
        {
            if (enemySpawnTimer <= 0)
            {
                if (!isThereSomethingInMySpawnZone)
                {
                    transform.position = new Vector2(3, -3.5f);
                    spawnEnemy(3, -3.5f);
                    enemiesOnScreen++;
                    enemySpawnTimer = 5;
                }
                else
                {
                    enemySpawnTimer = 5;
                }
            }
        }
    }
    [ContextMenu("spawnEnemy")] private void spawnEnemy(float x = 0, float y = 0)
    {
        //GameObject clone =
        Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.MoveGameObjectToScene(clone, activeScene);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTag") || collision.CompareTag("EnemyTag"))
        {
            isThereSomethingInMySpawnZone = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerTag") || collision.CompareTag("EnemyTag"))
        {
            isThereSomethingInMySpawnZone = false;
        }
    }
}
