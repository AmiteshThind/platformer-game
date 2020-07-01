using UnityEngine;
using UnityEngine.EventSystems;

public class JumpJoyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler 
{
    [HideInInspector]
    public bool Pressed;

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void FixedUpdate()
    {

    }

    // Event Handlers for Button Press
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        print("pressed jump");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        print("rel jump");
    } 

}
