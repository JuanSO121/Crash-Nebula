using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class LaserPlayer : MonoBehaviour
{
    [SerializeField] float maxDistance = 300f;
    [SerializeField] float fireDelay = 0.1f; // Tiempo entre disparos consecutivos
    [SerializeField] AudioClip laserSound;
    [SerializeField] ParticleSystem laserParticles; // Sistema de partículas para el láser
    AudioSource audioSource;
    bool canFire = true;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (laserParticles != null)
        {
            laserParticles.Stop();
        }
    }

    void Update()
    {
        // Para simular disparo continuo, actualizar la posición del raycast
        if (laserParticles.isPlaying)
        {
            CastRay();
        }
    }

    Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward * maxDistance);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("Hit: " + hit.transform.name);
            SpawnExplosion(hit.point, hit.transform);
            return hit.point;
        }

        Debug.Log("Miss");
        return transform.position + (transform.forward * maxDistance);
    }

    void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        Exposion temp = target.GetComponent<Exposion>();
        if (temp != null)
        {
            temp.AddForce(hitPosition, transform);
            if (target.tag != "Player")
            {
                temp.BlowUp();
            }
        }
    }

    public void FireLaser()
    {
        if (canFire)
        {
            // Iniciar el sistema de partículas y el sonido
            if (laserParticles != null)
            {
                laserParticles.Play();
            }
            if (audioSource && laserSound)
            {
                audioSource.PlayOneShot(laserSound);
            }
            canFire = false;
            Invoke("ResetFire", fireDelay);
        }
    }

    public void StopLaser()
    {
        // Detener el sistema de partículas
        if (laserParticles != null)
        {
            laserParticles.Stop();
        }
    }

    void ResetFire()
    {
        canFire = true;
    }
}
