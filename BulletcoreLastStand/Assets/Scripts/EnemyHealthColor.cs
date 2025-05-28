using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthColor : MonoBehaviour
{
    private Renderer[] childRenderers;

    private Health health;

    public Material startEnemyMat;

    void Start()
    {
        // Get all renderers in children
        childRenderers = GetComponentsInChildren<Renderer>();

        // Make sure each child has a unique material instance
        foreach (Renderer rend in childRenderers)
        {
            rend.material = new Material(startEnemyMat);
        }

        health = GetComponent<Health>();
    }

    void Update()
    {
        float healthAmount = Mathf.Clamp01(health.GetHealth() / health.totalHealth);

        // Lerp between red and green
        Color color = Color.Lerp(Color.red, Color.green, healthAmount);

        // Apply to all child renderers
        foreach (Renderer rend in childRenderers)
        {
            rend.material.color = color;
        }
    }
}
