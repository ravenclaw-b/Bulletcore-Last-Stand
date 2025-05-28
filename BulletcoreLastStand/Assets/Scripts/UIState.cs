using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    public GameState gameState;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isBetweenWaves)
        {
            text.text = "NEXT WAVE IN " + (int) gameState.timeUntilNextWave + " SECONDS";
        }
        else
        {
            int remaining = gameState.enemiesToSpawn - gameState.enemiesSpawned + gameState.enemiesAlive;
            if (remaining > 1) { text.text = remaining + " ENEMIES"; }
            else if (remaining == 1) { text.text = remaining + " ENEMY"; }
        }
    }
}
