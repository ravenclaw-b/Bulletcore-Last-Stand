using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 3f;

    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;
    }
}
