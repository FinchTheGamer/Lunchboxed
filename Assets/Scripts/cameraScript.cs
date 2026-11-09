using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class cameraScript : MonoBehaviour
{
    public GameObject player;
    public Camera cameraComponent;
    public AudioListener audioListener;
    public int cameraSpeed = 5;
    private bool shouldFollowPlayer = true;
    // do I need better names? do I need to private all these? i've learned so much from this project
    private DisableCameraFollowPlayerFlagScript disableCameraFollowPlayerFlag;
    private EnableCameraFollowPlayerFlagScript enableCameraFollowPlayerFlag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

            player = GameObject.FindGameObjectWithTag("PlayerTag");
        try
        {
            disableCameraFollowPlayerFlag = GameObject.FindGameObjectWithTag("DisableCameraFollowPlayerFlagTag").GetComponent<DisableCameraFollowPlayerFlagScript>();
            enableCameraFollowPlayerFlag = GameObject.FindGameObjectWithTag("EnableCameraFollowPlayerFlagTag").GetComponent<EnableCameraFollowPlayerFlagScript>();
            disableCameraFollowPlayerFlag.OnDisableCameraFollowPlayer += DisableCameraFollowPlayer;
            enableCameraFollowPlayerFlag.OnEnableCameraFollowPlayer += EnableCameraFollowPlayer;
        }
        catch (Exception e) 
        {
            Debug.Log(e);
        }
        

            
        
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
        if (shouldFollowPlayer)
        { 
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z); // so this is the player's current position being set to our position
        }
        //I'm so sorry this is so expensive
       
    }
    private void DisableCameraFollowPlayer()
    {
        shouldFollowPlayer = false;
    }
    private void EnableCameraFollowPlayer()
    {
        shouldFollowPlayer = true;
    }
    private void OnDestroy()
    {
        try
        {
            disableCameraFollowPlayerFlag.OnDisableCameraFollowPlayer -= DisableCameraFollowPlayer; //when this is destroyed, it'll disconnect
            enableCameraFollowPlayerFlag.OnEnableCameraFollowPlayer -= EnableCameraFollowPlayer;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
    
}
