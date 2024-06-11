using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemigoMuerte : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject blowUp;
    [SerializeField] Rigidbody body;
    [SerializeField] Shield shield;
    [SerializeField] float laserHitModifier = 10f;

    void IveBeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);
        if (shield == null)
            return;

        Debug.Log("making Damage");
        shield.TakeDamage();

        // Aquí desactivamos el GameObject del enemigo
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            IveBeenHit(contact.point);
        }
    }

    public void AddForce(Vector3 hitPosition, Transform hitSource)
    {
        IveBeenHit(hitPosition);

        if (body == null)
            return;

        Vector3 forceVector = (hitSource.position - hitPosition).normalized;
        body.AddForceAtPosition(forceVector * laserHitModifier, hitPosition, ForceMode.Impulse);

    }

    public void BlowUp()
    {
        Instantiate(blowUp, transform.position, Quaternion.identity);
        // No destruimos el GameObject directamente, lo desactivamos
        gameObject.SetActive(false);
    }
}