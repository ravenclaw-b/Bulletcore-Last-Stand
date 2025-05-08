using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int shotsPerSecond = 5;
    private double shotDelay;
    private long lastShot;

    public Transform playerCam;
    [SerializeField] private WeaponSway weaponSway;


    [Header("Recoil Settings")]
    [SerializeField] private float recoilX = 2f;
    [SerializeField] private float recoilY = 0.5f;
    [SerializeField] private float recoilZ = 0.2f;

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
                    Debug.Log(hit.collider.gameObject.name);
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
