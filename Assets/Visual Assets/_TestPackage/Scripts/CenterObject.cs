using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterObject : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Man")
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); //we collect around the object to thwo when collision happens
            foreach (Collider collider in colliders)
            {
             Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
    }
}
