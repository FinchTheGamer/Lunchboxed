using UnityEngine;
using UnityEngine.Events;
public class PlayButtonColliderScript : MonoBehaviour
{
    public UnityEvent OnThisPhysicalButtonClicked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(gameObject.GetComponent<RectTransform>().position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerTag")) //the other gameObject that we hit
        {
            Debug.Log("Button Triggered");
            OnThisPhysicalButtonClicked?.Invoke();
        }
    }
    
}
