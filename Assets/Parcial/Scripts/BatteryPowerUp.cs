using UnityEngine;

public class BatteryPowerUp : MonoBehaviour
{
    [SerializeField] private float boostAmount = 20f;

    private void OnEnable()
    {
        EventManager.onEndGame += OnEndGame;
    }

    private void OnDisable()
    {
        EventManager.onEndGame -= OnEndGame;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Nave spaceship = other.GetComponent<Nave>();
            if (spaceship != null)
            {
                spaceship.AddBoost(boostAmount);
                Destroy(gameObject);
            }
        }
    }

    private void OnEndGame()
    {
        // Verificar si el GameObject aún existe antes de intentar destruirlo
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
