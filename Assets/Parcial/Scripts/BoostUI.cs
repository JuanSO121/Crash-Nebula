using UnityEngine;
using UnityEngine.UI;

public class BoostUI : MonoBehaviour
{
    [SerializeField] private Image boostImage; // Cambia de RectTransform a Image

    private void OnEnable()
    {
        EventManager.onBoostChange += UpdateBoostDisplay;
    }

    private void OnDisable()
    {
        EventManager.onBoostChange -= UpdateBoostDisplay;
    }

    private void UpdateBoostDisplay(float percentage)
    {
        // Aseg�rate de que el valor de percentage est� entre 0 y 1.
        boostImage.fillAmount = Mathf.Clamp01(percentage);
    }
}
