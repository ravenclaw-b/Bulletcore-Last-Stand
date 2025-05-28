using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public float totalHealth = 100f;

    bool isAlive = true;

    float timeDied;
    float secondsToDelete = 2f;

    Rigidbody rb;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject scrap;
    [SerializeField] public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        if (!isAlive && (currentTime - timeDied > secondsToDelete) && CompareTag("Enemy"))
        {
            Destroy(head);
            Destroy(body);
            Destroy(gameObject);
        }
    }

    public void Damage(float dmg)
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

    public float GetHealth()
    {
        return health;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    void Die()
    {
        isAlive = false;
        timeDied = Time.time;

        Debug.Log(transform.gameObject.name + " died");

        if (CompareTag("Enemy"))
        {
            GameObject spawnedScrap = Instantiate(scrap, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody spawnedRb = spawnedScrap.GetComponent<Rigidbody>();
            ScrapValue scrapValue = spawnedScrap.GetComponent<ScrapValue>();

            if(scrapValue != null && gameState != null)
            {
                scrapValue.value = gameState.wave;
            }

            if (spawnedRb != null)
            {
                spawnedRb.velocity = rb.velocity;
            }

            if (rb != null)
            {
                rb.freezeRotation = false;
            }

            // Detach and apply physics to head
            if (head != null)
            {
                head.transform.parent = null;
                if (head.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody headRb = head.AddComponent<Rigidbody>();
                    headRb.velocity = rb.velocity;
                }
            }

            // Detach and apply physics to body
            if (body != null)
            {
                body.transform.parent = null;
                if (body.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody bodyRb = body.AddComponent<Rigidbody>();
                    bodyRb.velocity = rb.velocity;
                    bodyRb.mass = 1.5f;
                }
            }


        }
        else if (CompareTag("Player"))
        {
            SceneManager.LoadScene("Level");
        }
    }

}
