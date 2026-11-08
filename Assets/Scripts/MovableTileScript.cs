using System.Collections;
using System.Runtime.CompilerServices;
using playerCode;
using UnityEngine;


public class MovableTileScript : MonoBehaviour
{
    [SerializeField] private int moveableTileSpeed = 100;
    [SerializeField ] private int changeInX = 0;
    [SerializeField] private int changeInY = 10;
    [SerializeField] private int changeInZ = 0;
    private bool isOpen = false;
    private bool doesPlayerHaveKey;
    [SerializeField] private string requiredKeyForThisTile = "00000000";

    const float SNAPDISTANCE = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //temp
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //i could make a temporary variable here... but that would be expensive for the memory
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            doesPlayerHaveKey = collision.gameObject.GetComponent<PlayerScript>().Backpack.Contains(requiredKeyForThisTile);
            //probably very inefficient. Croissant would yell at me so bad
        }
        else
        {
            doesPlayerHaveKey = collision.gameObject.GetComponentInChildren<PlayerInventoryScript>();
        }
        if (collision.gameObject.CompareTag("PlayerTag") && !isOpen && doesPlayerHaveKey) //if the player touches this and has the key
        {
            StartCoroutine(ExperimentalOpen());
           
            
        }

    }

    void Open()
    {
        
        isOpen = true;
    }
    public IEnumerator ExperimentalOpen() //INEFFICIENT?
    {
        Vector3 STARTINGPOSITION = transform.position; //so this is the beginning
        Vector3 ENDINGPOSITION = new Vector3(transform.position.x + changeInX, transform.position.y + changeInY, transform.position.z + changeInZ);
        while (STARTINGPOSITION != ENDINGPOSITION) //what if it misses ohso slightly?s
        {
            if (Vector3.Distance(transform.position, ENDINGPOSITION) > SNAPDISTANCE) 
            {
                transform.position = Vector3.MoveTowards(transform.position, ENDINGPOSITION, moveableTileSpeed * Time.deltaTime); //well now they are flying... into the sun                                                                                                                  
                //should I put a heavy if statement?
                //yeah I want to
            }
            else
            {
                transform.position = ENDINGPOSITION;
                break;
            }
            //if (Vector3.Distance(transform.position, ENDINGPOSITION) < 1) { transform.position = ENDINGPOSITION; } // is it over or undershooting?
            
            yield return null; //now why is it going

        }
        isOpen = true;
        
    }
}
