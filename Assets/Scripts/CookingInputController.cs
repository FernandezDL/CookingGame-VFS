using UnityEngine;
using UnityEngine.InputSystem;

public class CookingInputController : MonoBehaviour
{
    public bool isPressing;
    public float holdTime;

    private Vector2 currentPosition;
    private Vector2 lastPosition;
    
    public Vector2 CurrentPosition => currentPosition;

    private void Update()
    {
        if (isPressing)
        {
            holdTime += Time.deltaTime;
        }
    }

    public void OnTouchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressing = true;
            holdTime = 0f;
        }
        else if (context.canceled)
        {
            isPressing = false;
            holdTime = 0f;
        }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        lastPosition = currentPosition;
        currentPosition = context.ReadValue<Vector2>();
    }

    public Vector2 GetDelta()
    {
        return currentPosition - lastPosition;
    }
}
