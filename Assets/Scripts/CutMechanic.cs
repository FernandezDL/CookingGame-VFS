using UnityEngine;

public class CutMechanic : MonoBehaviour
{
    [Header("Cut Settings")]
    public int requiredCuts = 5;
    public float minSwipeDistance = 80f;
    public float maxAngleFromDown = 30f;

    private Vector2 swipeStart;
    private bool swiping;
    private int currentCuts;
    private bool completed;

    public bool IsCompleted => completed;

    public void ResetMechanic()
    {
        currentCuts = 0;
        completed = false;
        swiping = false;
    }

    public void OnPressStarted(Vector2 screenPos)
    {
        if (completed) return;

        swipeStart = screenPos;
        swiping = true;
    }

    public void OnPressEnded(Vector2 screenPos)
    {
        if (!swiping || completed) return;

        Vector2 swipe = screenPos - swipeStart;

        if (IsValidCutSwipe(swipe))
        {
            currentCuts++;
            if (currentCuts >= requiredCuts)
            {
                completed = true;
            }
        }

        swiping = false;
    }

    private bool IsValidCutSwipe(Vector2 swipe)
    {
        if (swipe.magnitude < minSwipeDistance)
            return false;

        Vector2 dir = swipe.normalized;
        float angle = Vector2.Angle(Vector2.down, dir);

        return angle <= maxAngleFromDown;
    }

    public float GetProgress01()
    {
        return (float)currentCuts / requiredCuts;
    }
}
