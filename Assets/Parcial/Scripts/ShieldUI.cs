using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa la biblioteca UI de Unity

public class ShieldUI : MonoBehaviour
{
    [SerializeField] Image shieldImage; // Cambia de RectTransform a Image

    private void OnEnable()
    {
        EventManager.onTakeDamage += UpdateShieldDisplay;
    }

    private void OnDisable()
    {
        EventManager.onTakeDamage -= UpdateShieldDisplay;
    }

    void UpdateShieldDisplay(float percentage)
    {
        // Asegúrate de que el valor de percentage esté entre 0 y 1.
        shieldImage.fillAmount = Mathf.Clamp01(percentage);
    }
}
