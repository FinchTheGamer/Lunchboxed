using UnityEngine;

public class EnableCameraFollowPlayerFlagScript : MonoBehaviour
{

    //I'm doing the same things... for the disable version... but in reverse... this is a nightmare
    public delegate void EnableCameraFollowPlayerAction();
    public event EnableCameraFollowPlayerAction OnEnableCameraFollowPlayer;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("PlayerTag"))
        {
            OnEnableCameraFollowPlayer.Invoke();
        }
    }
}
