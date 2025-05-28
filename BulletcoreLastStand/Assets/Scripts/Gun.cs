using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int shotsPerSecond = 10;
    private double shotDelay;
    private long lastShot;

    public Transform playerCam;
    [SerializeField] private WeaponSway weaponSway;


    [Header("Recoil Settings")]
    [SerializeField] private float recoilX = 2f;
    [SerializeField] private float recoilY = 0.5f;
    [SerializeField] private float recoilZ = 0.2f;

    float forceMagnitude = 1000f;

    private void Start()
    {
        shotDelay = 1000f / shotsPerSecond;
        lastShot = (long)(Time.time * 1000);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        long currentTime = (long)(Time.time * 1000);

        if (lastShot + shotDelay <= currentTime)
        {
            if (Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    Health hitHealth = hit.collider.gameObject.GetComponentInParent<Health>();
                    if (hitHealth != null)
                    {
                        string tag = hit.collider.gameObject.tag;
                        float multi = tag == "Head" ? 2 : 1;

                        hitHealth.Damage(14f * multi);
                        // Debug.Log("hit " + hit.collider.gameObject.name + " with tag: " + tag + ". Health is now at " + hitHealth.GetHealth() + ".");

                        if (!hitHealth.IsAlive() && hit.rigidbody != null && !hit.rigidbody.isKinematic)
                        {
                            Vector3 forceDirection = playerCam.forward;
                            hit.rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, hit.point);
                        }
                    }
                    else
                    {
                        // Debug.Log("hit " + hit.collider.gameObject.name + " but no health found.");
                    }
                }
            }

            // Apply recoil
            if (weaponSway != null)
            {
                weaponSway.AddRecoil(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
            }

            lastShot = currentTime;
        }
    }
}
