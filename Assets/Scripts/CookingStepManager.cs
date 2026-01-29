using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CookingStepManager : MonoBehaviour
{
    public enum CookingStep
    {
        Stir,
        Cut,
        Done
    }

    public CookingStep currentStep;

    [Header("Mechanics")]
    public StirMechanic stirMechanic;
    public CutMechanic cutMechanic;

    [Header("Input")]
    public CookingInputController input;
    [SerializeField] private GameObject endgameButton;

    [Header("UI")] 
    [SerializeField] private TMP_Text guideText;
    [SerializeField] private string stirGuideText;
    [SerializeField] private string cutGuideText;
    [SerializeField] private string winText;
    
    [Header("Progress Bars")]
    [SerializeField] private ProgressBarStirMechanic progressBarStir;
    [SerializeField] private ProgressBarCutMechanic progressBarCut;
    
    [Header("Feedback")]
    [SerializeField] private List<GameObject> ingredients = new List<GameObject>();
    [SerializeField] private GameObject result;

    private void Start()
    {
        StartStirStep();
    }

    private void Update()
    {
        if (!input.isPressing) return;

        switch (currentStep)
        {
            case CookingStep.Stir:
                stirMechanic.ProcessInput(
                    input.GetDelta(),
                    input.CurrentPosition
                );

                if (stirMechanic.IsCompleted)
                    StartCutStep();
                break;
        }
    }

    public void OnTouchPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentStep == CookingStep.Cut)
                cutMechanic.OnPressStarted(input.CurrentPosition);
        }
        else if (context.canceled)
        {
            if (currentStep == CookingStep.Cut)
            {
                cutMechanic.OnPressEnded(input.CurrentPosition);

                if (cutMechanic.IsCompleted)
                    FinishCooking();
            }
        }
    }

    private void StartStirStep()
    {
        currentStep = CookingStep.Stir;
        stirMechanic.ResetMechanic();
        
        // UI
        guideText.text = stirGuideText;
        
        // Feedback
        progressBarStir.gameObject.SetActive(true);
        foreach (var ingredient in ingredients)
        {
            ingredient.SetActive(true);
        }
    }

    private void StartCutStep()
    {
        currentStep = CookingStep.Cut;
        cutMechanic.ResetMechanic();
        
        // Ui
        guideText.text = cutGuideText;
        
        // Feedback
        progressBarStir.gameObject.SetActive(false);
        progressBarCut.gameObject.SetActive(true);
    }

    private void FinishCooking()
    {
        currentStep = CookingStep.Done;
        
        // UI
        guideText.text = winText;

        // Feedback
        progressBarCut.gameObject.SetActive(false);

        foreach (var ingredient in ingredients)
        {
            ingredient.SetActive(false);
        }
        result.SetActive(true);
        
        endgameButton.gameObject.SetActive(true);
    }
}
