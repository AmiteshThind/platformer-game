using UnityEngine;
using UnityEngine.EventSystems;

public class nextLevelBtnScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public bool Pressed;
    SceneManagement sceneManagement;
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        print("pressed jump");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        print("rel jump");
        sceneManagement.nextLevel();
    }
}
