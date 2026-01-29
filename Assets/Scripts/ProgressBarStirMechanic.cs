using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarStirMechanic : MonoBehaviour
{
    [SerializeField] private StirMechanic _stirMechanic;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        fillImage.fillAmount = _stirMechanic.GetProgress01();
    }
}
