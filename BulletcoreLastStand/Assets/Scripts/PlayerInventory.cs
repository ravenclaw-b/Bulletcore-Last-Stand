using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int scraps = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with " + other.gameObject.name);
        if (other.gameObject.layer == 11)
        {
            ScrapValue pickupValue = other.gameObject.GetComponent<ScrapValue>();
            if (pickupValue != null)
            {
                scraps += pickupValue.value;
                Destroy(other.gameObject);
            }
        }
    }
}