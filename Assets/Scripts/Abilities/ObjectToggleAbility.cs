using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectToggleAbility : Ability
{
    public override void Activate(GameObject player)
    {
        foreach (ToggleableObject obj in FindObjectsOfType<ToggleableObject>())
        {
            obj.UpdateObject();
        }
    }

    public override void Deactivate(GameObject player)
    {
        foreach (ToggleableObject obj in FindObjectsOfType<ToggleableObject>())
        {
            obj.RevertObject();
        }
    }
}
