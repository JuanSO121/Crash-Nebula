using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class Laser : MonoBehaviour
{
    [SerializeField] float laserOffTime = 0.5f;
    [SerializeField] float maxDistance = 300f;
    [SerializeField] float fireDelay = 2f;
    [SerializeField] AudioClip laserSound;
    AudioSource audioSource;

    Light lsLight;

    LineRenderer lr;
    bool canFire;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lsLight = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        lr.enabled = false;
        lsLight.enabled = false;
        canFire = true;
    }

    //AnimatorUpdateMode()
    //{

    //}

    //void Update()
    //{
    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.blue);
    //}


    Vector3 CastRay()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward * maxDistance);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("Hit:" + hit.transform.name);

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
            // Check if the target is not the player before applying force and blowing up
            if (target.tag != "Player")
            {

                temp.BlowUp();
            }
        }
    }



    public void FireLaser()
    {
        Vector3 pos = CastRay();
        FireLaser(pos);

        //FireLaser(CastRay());
        if (audioSource && laserSound) // Verifica que tanto AudioSource como el AudioClip estén asignados
        {
            audioSource.PlayOneShot(laserSound); // Reproduce el sonido del láser
        }

    }

    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if (canFire)
        {
            if (target != null)
            {
                SpawnExplosion(targetPosition, target);
            }

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPosition);
            lr.enabled = true;
            lsLight.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
        }
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        lsLight.enabled = false;
    }

    public float Distance
    {
        get { return maxDistance; }
    }

    void CanFire()
    {
        canFire = true;
    }
}