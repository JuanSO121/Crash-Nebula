using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nave : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 50f;
    [SerializeField] private float turnSpeed = 60f;
    [SerializeField] private Thruster[] thruster;
    [SerializeField] private float boostMultiplier = 2f;
    [SerializeField] private float maxBoost = 100f;
    [SerializeField] private float boostDrainRate = 20f;
    [SerializeField] private float boostRecoveryRate = 10f;

    private float currentBoost;
    private bool isBoosting;

    private Transform myT;

    void Awake()
    {
        myT = transform;
        currentBoost = maxBoost;
        UpdateBoostUI();
    }

    void Update()
    {
        Turn();
        Thrust();
        RecoverBoost();
    }

    private void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");

        myT.Rotate(-pitch, yaw, -roll);
    }

    private void Thrust()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float speed = verticalInput * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && currentBoost > 0)
        {
            speed *= boostMultiplier;
            isBoosting = true;
            currentBoost -= boostDrainRate * Time.deltaTime;
            UpdateBoostUI();
        }
        else
        {
            isBoosting = false;
        }

        currentBoost = Mathf.Max(currentBoost, 0);

        if (verticalInput > 0)
        {
            myT.position += myT.forward * speed;
            SetThrustersIntensity(verticalInput);
        }
        else
        {
            SetThrustersIntensity(0);
        }

        if (verticalInput < 0)
        {
            myT.position += myT.forward * (speed / 2);
        }
    }

    private void SetThrustersIntensity(float intensity)
    {
        foreach (Thruster t in thruster)
        {
            t.Intencity(intensity);
        }
    }

    private void RecoverBoost()
    {
        if (!isBoosting && currentBoost < maxBoost)
        {
            currentBoost += boostRecoveryRate * Time.deltaTime;
            currentBoost = Mathf.Min(currentBoost, maxBoost);
            UpdateBoostUI();
        }
    }

    public void AddBoost(float amount)
    {
        currentBoost += amount;
        currentBoost = Mathf.Min(currentBoost, maxBoost);
        UpdateBoostUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public float GetCurrentBoost()
    {
        return currentBoost;
    }

    public float GetMaxBoost()
    {
        return maxBoost;
    }

    private void UpdateBoostUI()
    {
        float boostPercentage = currentBoost / maxBoost;
        EventManager.BoostChange(boostPercentage);
    }
}
