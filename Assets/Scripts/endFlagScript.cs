using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class endFlagScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent nextLevel;
    void Start()
    {
        nextLevel.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().newLevel);
        nextLevel.AddListener(GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>().endLevelandLoadNextOne);
        
    }
    // I need to automatically connect this to the newLevel method of the Player and the endLevelandLoadNextOne method of the GameManager.
    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerTag"))
        {
            nextLevel.Invoke();
        }
    }
   
}
