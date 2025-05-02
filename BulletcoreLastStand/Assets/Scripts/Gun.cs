using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int shotsPerSecond = 5;
    private double shotDelay;
    private long lastShot;

    public Transform playerCam;

    private void Start()
    {
        shotDelay = 1000f / shotsPerSecond;
        lastShot = (long) (Time.time * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        long currentTime = (long) (Time.time * 1000);

        if (lastShot+shotDelay <= currentTime)
        {
            Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit hit);
            
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }

            lastShot = currentTime;
        }
    }
}
