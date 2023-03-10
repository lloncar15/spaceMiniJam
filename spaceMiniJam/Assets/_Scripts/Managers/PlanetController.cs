using System;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] public Planet planet;
    public SpriteRenderer hat;
    public ParticleSystem system;

    public bool hasHat = false;

    private void FixedUpdate()
    {
        if (!hasHat)
        {
            PushAway();
        }
    }

    private void PushAway()
    {
        // find all rigidbodies within the gravity radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, planet.gravityRadius);

        // apply gravitational force to each rigidbody
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag != "Player")
            {
                continue;
            }

            Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                Vector2 direction = (rigidbody.position - (Vector2)transform.position).normalized;
                rigidbody.AddForce(direction * planet.gravitationalForce * rigidbody.mass);
            }
        }
    }

    public void putHat()
    {
        hasHat = true;
        hat.enabled = true;
        Destroy(system);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, planet.gravityRadius);
    }
}
