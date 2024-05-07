using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsUp : MonoBehaviour
{
    public string playerTag = "PlayerBoddy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            AxeSpawner.playerIsUp = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            AxeSpawner.playerIsUp = false;
    }
}
