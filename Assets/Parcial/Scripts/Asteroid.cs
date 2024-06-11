using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float minScale = .8f;
    [SerializeField] private float maxScale = 1.8f;
    [SerializeField] private float rotationOffSet = 50f;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private float dropProbability = 0.5f;

    private Transform myT;
    private Vector3 randomRotation;

    void Awake()
    {
        myT = transform;
    }

    void Start()
    {
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);

        myT.localScale = scale;

        randomRotation.x = Random.Range(-rotationOffSet, rotationOffSet);
        randomRotation.y = Random.Range(-rotationOffSet, rotationOffSet);
        randomRotation.z = Random.Range(-rotationOffSet, rotationOffSet);
    }

    void Update()
    {
        myT.Rotate(randomRotation * Time.deltaTime);
    }

    private void OnDestroy()
    {
        TryDropBattery();
    }

    private void TryDropBattery()
    {
        if (Random.Range(0f, 1f) <= dropProbability)
        {
            Instantiate(batteryPrefab, transform.position, Quaternion.identity);
        }
    }
}
