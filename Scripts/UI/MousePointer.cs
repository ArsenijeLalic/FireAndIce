using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MousePointer : MonoBehaviour
{
    [SerializeField] private float xBound;
    [SerializeField] private float yBound;
    private GameObject player;
    [SerializeField] private float sensitivity;

    GameManager gm;
    private void Awake()
    { 
        player = GameObject.Find("Player");
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (gm.gameIsOn)
        {
            Vector2 mousePointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FocusOn(mousePointer);
        }
        else
        {
            FocusOn(player.transform.position);
        }
    }

    private bool OutOfBounds(float x0, float x1, float bound)
    {
        if (Mathf.Abs(x0 - x1) > bound)
        {
            return true;
        }
        return false;
    }

    private void FocusOn(Vector2 focusPoint)
    {
        Vector2 cameraDirection = (focusPoint - (Vector2)transform.position).normalized;

        Vector2 currPosition = transform.position;
        currPosition = currPosition + cameraDirection * sensitivity * Time.fixedDeltaTime;
        if (OutOfBounds(currPosition.x, player.transform.position.x, xBound))
        {
            currPosition.x = transform.position.x;
        }
        if (OutOfBounds(currPosition.y, player.transform.position.y, yBound))
        {
            currPosition.y = transform.position.y;
        }
        transform.position = currPosition;
    }
}
