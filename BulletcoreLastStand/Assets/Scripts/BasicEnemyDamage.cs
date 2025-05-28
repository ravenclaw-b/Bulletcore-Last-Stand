using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDamage : MonoBehaviour
{
    private float lastHit;
    public float damage;

    private void Start()
    {
        lastHit = Time.time;
    }

    public void OnCollisionStay(Collision collision)
    {
        float current = Time.time;
        Health playerHealth = collision.gameObject.GetComponentInParent<Health>();
        if (playerHealth == null)
        {
            return;
        }

        if (current > lastHit+0.6f)
        {
            lastHit = current;
            playerHealth.Damage(damage);

            // Apply knockback
            Rigidbody playerRb = collision.gameObject.GetComponentInParent<Rigidbody>();
            Rigidbody enemyRb = GetComponent<Rigidbody>();

            if (playerRb != null && enemyRb != null)
            {
                // Direction from enemy to player
                Vector3 direction = (collision.transform.position - transform.position).normalized;

                // Adjust these values as needed
                float knockbackForce = 15f;

                playerRb.AddForce(direction * knockbackForce, ForceMode.Impulse);
                enemyRb.AddForce(-direction * knockbackForce, ForceMode.Impulse);

                Debug.Log("KB applied");
            }
        }
    }
}
