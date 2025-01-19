using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingPotion : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    float countdown;
    bool hasExploded;

    public GameObject destructionEffect;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (hasExploded) return;  // Avoid multiple explosions

        hasExploded = true;
        Explode();
    }
    void Explode()
    {
        Instantiate(destructionEffect, transform.position, transform.rotation);

        Collider[] objectsToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in objectsToDestroy)
        {
            Destructable dest = nearbyObject.GetComponent<Destructable>();
            if(dest != null)
            {
                dest.onDestruction();
            }
        }

        Collider[] objectsToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in objectsToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(500f, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
