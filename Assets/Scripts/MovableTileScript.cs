using playerCode;
using UnityEngine;


public class MovableTileScript : MonoBehaviour
{
    [SerializeField] private int moveableTileSpeed = 100;
    [SerializeField ] private int changeInX = 0;
    [SerializeField] private int changeInY = 10;
    [SerializeField] private int changeInZ = 0;
    private bool isOpen = false;

    [SerializeField] private string requiredKeyForThisTile = "00000000";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //i could make a temporary variable here... but that would be expensive for the memory
        
        Debug.Log("Do we have the Key?"); //apparently, we never had the key at all. So the problem is that we never have a key
        Debug.Log(collision.gameObject.GetComponent<PlayerScript>().Backpack.Contains(requiredKeyForThisTile));

        if (collision.gameObject.CompareTag("PlayerTag") && !isOpen && collision.gameObject.GetComponent<PlayerScript>().Backpack.Contains(requiredKeyForThisTile)) //if the player touches this and has the key
        {
            Open();
            
            
        }

    }

    void Open()
    {
        Vector3 newPosition = new Vector3(transform.position.x + changeInX, transform.position.y + changeInY, transform.position.z + changeInZ);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveableTileSpeed * Time.deltaTime);
        isOpen = true;
    }

}
