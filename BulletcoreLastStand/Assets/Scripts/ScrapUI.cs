using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapUI : MonoBehaviour
{
    public PlayerInventory playerInv;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = playerInv.scraps.ToString();
    }
}
