using UnityEngine;

public class StirMechanic : MonoBehaviour
{
    [Header("Stir Settings")]
    public float requiredRotation = 720f; // degrees (2 full circles)
    public float minMoveThreshold = 5f;

    [Header("Bowl")]
    public Transform bowlCenter;
    public float bowlRadius = 1.2f;

    private float accumulatedRotation;
    private Vector2 lastDirection;
    private bool hasDirection;
    private bool completed;

    public bool IsCompleted => completed;

    public void ResetMechanic()
    {
        accumulatedRotation = 0f;
        hasDirection = false;
        completed = false;
    }

    public void ProcessInput(Vector2 screenDelta, Vector2 screenPosition)
    {
        if (completed) return;
        if (!IsInsideBowl(screenPosition)) return;
        if (screenDelta.magnitude < minMoveThreshold) return;

        Vector2 direction = screenDelta.normalized;

        if (hasDirection)
        {
            float angle = Vector2.SignedAngle(lastDirection, direction);
            accumulatedRotation += angle;
        }

        lastDirection = direction;
        hasDirection = true;

        if (Mathf.Abs(accumulatedRotation) >= requiredRotation)
        {
            completed = true;
            Debug.Log("✅ Stir completed!");
        }
    }

    private bool IsInsideBowl(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return Vector2.Distance(worldPos, bowlCenter.position) <= bowlRadius;
    }

    public float GetProgress01()
    {
        return Mathf.Clamp01(Mathf.Abs(accumulatedRotation) / requiredRotation);
    }
}
