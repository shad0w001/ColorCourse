using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("pls work");

        if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var playerController))
        {
            Debug.Log("pls work again");
            playerController.RespawnPlayer();
        }
    }
}
