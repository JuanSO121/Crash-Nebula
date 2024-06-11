using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(TrailRenderer))]
public class Thruster : MonoBehaviour
{
    Light thrLight;

    void Awake()
    {
        thrLight = GetComponent<Light>();
    }

    void Start()
    {
        thrLight.intensity = 0;
    }

    public void Intencity(float inten)
    {
        thrLight.intensity = inten * 2f;
    }
}
