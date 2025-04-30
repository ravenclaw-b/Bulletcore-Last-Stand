using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    int health = 100;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int dmg)
    {
        if (health <= dmg)
        {
            health = 0;
            Die();
        }
        else
        {
            health -= dmg;
        }
    }

    public int GetPlayerHealth()
    {
        return health;
    }

    public bool IsPlayerAlive ()
    {
        return isAlive;
    }

    void Die()
    {
        isAlive = false;

        Debug.Log("Player died");
        SceneManager.LoadScene("Level");
    }
}
