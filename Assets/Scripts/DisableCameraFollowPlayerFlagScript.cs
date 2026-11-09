using UnityEngine;

public class DisableCameraFollowPlayerFlagScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void DisableCameraFollowPlayerAction();
    public event DisableCameraFollowPlayerAction OnDisableCameraFollowPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerTag"))
        {
            OnDisableCameraFollowPlayer.Invoke();
        }
    }
}
