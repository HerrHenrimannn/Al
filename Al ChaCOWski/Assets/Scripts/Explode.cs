using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private float totalLifetime = 3f;
    private float lifetime = 0f;
    private bool hasExploded = false;
    private float radius = 5f;
    private float damage = 50f;
    private float explosionForce = 700f;
    public GameObject explosionEffect;
    private void Start()
    {
        lifetime = totalLifetime;
    }
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f && !hasExploded)
        {
            hasExploded = true;
            Boom();
        }
    }
    private void Boom()
    {
        GameObject explo = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }

            Target targ = collider.GetComponent<Target>();
            if(targ != null)
            {
                targ.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
