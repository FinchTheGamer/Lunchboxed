using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    public int enemiesOnScreen = 0;
    [SerializeField] private float enemySpawnTimer = 5;
    [SerializeField] private bool isThereSomethingInMySpawnZone = false;

    [SerializeField] bool overrideAutoTeleport = false;

    [SerializeField] Vector2 spawnLocation = new Vector2(3, -3.5f);
    [SerializeField] EnemyScript.EnemyModifierType kindOfEnemyWeSpawn =  EnemyScript.EnemyModifierType.Normal; // I really didn't expect that to exist

    // why is this failing?
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
                    transform.position = spawnLocation;
                    spawnEnemy(spawnLocation.x, spawnLocation.y);
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
    private void spawnEnemy(float x = 0, float y = 0)
    {
        //GameObject clone =
        GameObject clone = Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        clone.GetComponent<EnemyScript>().enemyModifierType = kindOfEnemyWeSpawn;
        //Scene activeScene = SceneManager.GetActiveScene();
        //SceneManager.MoveGameObjectToScene(clone, activeScene);

    }

    [ContextMenu("spawnEnemy")] private void DebugSpawnEnemy() //does it need to be public or private or protected?
    {
        spawnEnemy(transform.position.x, transform.position.y);
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
    //below I think we'll have some procedural generation methods:
    [ContextMenu("SpawnEnemiesInLine")] public void ProcedureGenerateEnemiesLine(Vector3 beginning, Vector3 end, float step)
    {
        //this is going to generate enemies on a line
        //first, calculate the distance between the two
        Vector3 distance = end - beginning; //that'll probably work..
        float distanceFloat = Vector3.Distance(beginning, end);
        float stepLength = distanceFloat / step; //why do we divide? what does a modulo do?
        bool shouldEnd = false;
        Vector3 counter = beginning;
        while (shouldEnd == false)
        {
            spawnEnemy(counter.x, counter.y);
            counter = new Vector3(counter.x + step, counter.y + step, counter.z + step);
            Debug.Log($"Step equals {step}");
            Debug.Log($"Counter equals {counter}");
            if (Vector3.Distance(end, counter) < stepLength)
            {
                shouldEnd = true;
            }
        }

        spawnEnemy(beginning.x, beginning.y);
    }
    //ugh I don't get how to spawn these sooooo
    private void ProcedureGenerateEnemiesCircle(Vector3 beginning, float r, float step)
    {
        //do we grab the circle???
        
        //we can nonot do math in here like i'm used to so I don't even know what to do
    }
    //I cannot figure out the circle code
    //how will we iterate on the circle
}
