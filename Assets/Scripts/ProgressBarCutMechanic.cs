using UnityEngine;
using UnityEngine.UI;

public class ProgressBarCutMechanic : MonoBehaviour
{
    [SerializeField] private CutMechanic _cutMechanic;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        fillImage.fillAmount = _cutMechanic.GetProgress01();
    }
}
