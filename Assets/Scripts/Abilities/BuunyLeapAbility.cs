using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuunyLeapAbility : Ability
{
    private PlayerController controller;
    public float jumpHeightMultiplier;
    private float baseJumpHeight;
    public override void Activate(GameObject player)
    {
        FindObjectOfType<AudioManager>().Play("Rabbit_Hop_Acitve");
        controller = player.GetComponent<PlayerController>();
        baseJumpHeight = controller.jumpHeight;
        controller.jumpHeight = baseJumpHeight * jumpHeightMultiplier;
    }

    public override void Deactivate(GameObject player)
    {
        controller = player.GetComponent<PlayerController>();
        controller.jumpHeight = baseJumpHeight;
    }
}
