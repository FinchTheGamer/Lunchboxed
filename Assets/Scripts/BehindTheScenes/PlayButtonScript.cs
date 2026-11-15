using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayButtonScript : MonoBehaviour
{
    Transform PlayButtonTransform;
    GameObject PlayButtonCollider;
    BoxCollider2D PlayButtonColliderBoxCollider2D;
    RectTransform PlayButtonRectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayButtonTransform = gameObject.transform;

        PlayButtonCollider = GameObject.FindGameObjectWithTag("PlayButtonCollider");
        PlayButtonColliderBoxCollider2D = PlayButtonCollider.GetComponent<BoxCollider2D>();

        PlayButtonRectTransform = gameObject.GetComponent<RectTransform>();
        PlayButtonColliderBoxCollider2D.offset = Vector2.zero;
        PlayButtonColliderBoxCollider2D.size = PlayButtonRectTransform.sizeDelta;
        PlayButtonCollider.GetComponent<RectTransform>().position = PlayButtonRectTransform.position; 
        //could I set it directly? yeah...
            
        //woah
    }
    public void ClickThisButton()
    {
        gameObject.GetComponent<Button>().onClick.Invoke();
    }

    
}
