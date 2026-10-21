using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    public GameObject player;
    public Camera cameraComponent;
    public AudioListener audioListener;
    public int cameraSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        
    }
    void Awake() // Awake is called before any other Start() has been called. A constructor for GameObjects
    {
        cameraComponent = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        cameraComponent.enabled = true;
        audioListener.enabled = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
