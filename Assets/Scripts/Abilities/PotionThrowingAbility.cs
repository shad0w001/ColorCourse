using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionThrowingAbility : Ability
{
    public float force = 40f;
    public GameObject potionPrefab;
    public override void Activate(GameObject player)
    {
        var camera = player.GetComponentInChildren<Camera>();

        GameObject potion = Instantiate(potionPrefab, camera.transform.position, camera.transform.rotation);
        Rigidbody rb = potion.GetComponent<Rigidbody>();
        rb.AddForce(camera.transform.forward * force, ForceMode.VelocityChange);
    }
}
