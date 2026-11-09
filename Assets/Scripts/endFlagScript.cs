using playerCode;
using UnityEngine;
using UnityEngine.Events;
public class endFlagScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent nextLevel;
    float cosine;
    Vector3 SITTINGPOSITION;
    //I think this should move along a cosine. Expensive?
    private void Awake()
    {
        SITTINGPOSITION = transform.position; 
    }
    void Start()
    {
        //nextLevel.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().newLevel);
        nextLevel.AddListener(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>().newLevel);
        //Debug.Log(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<NewPlayerScript>().name);
        nextLevel.AddListener(GameObject.FindGameObjectWithTag("GAMETag").GetComponent<GameManagerScript>().endLevelandLoadNextOne);
        
    }
    // I need to automatically connect this to the newLevel method of the Player and the endLevelandLoadNextOne method of the GameManager.
    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerTag")) // we are the player tag right?
        {
            nextLevel.Invoke(); //how many times does this run? what if this is being... added twice?
            //fixed the problem here! The code was connecting to the flag twice!
        }
    }
    private void FixedUpdate()
    {
        cosine = Mathf.Cos(Time.time);
        transform.position = new Vector3(SITTINGPOSITION.x, (SITTINGPOSITION.y + (cosine * 0.1f)), SITTINGPOSITION.z);
    }


}
