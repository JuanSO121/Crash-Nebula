using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Exposion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject blowUp;
    [SerializeField] Rigidbody body;
    [SerializeField] Shield shield;
    [SerializeField] float laserHitModifier = 10f;
    [SerializeField] bool isPlayer;

    void IveBeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);

        if (shield == null)
            return;

        Debug.Log("making Damage");
        shield.TakeDamage();
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
        EventManager.PlayerDeath();
        Instantiate(blowUp, transform.position, Quaternion.identity);

        if (isPlayer)
        {
            EventManager.PauseGame();
        }

        Destroy(gameObject);
    }
}
