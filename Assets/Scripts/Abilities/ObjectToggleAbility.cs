using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectToggleAbility : Ability
{
    public override void Activate(GameObject player)
    {
        FindObjectOfType<AudioManager>().Play("Object_Toggle_Activate");
        foreach (ToggleableObject obj in FindObjectsOfType<ToggleableObject>())
        {
            obj.UpdateObject();
        }
    }

    public override void Deactivate(GameObject player)
    {
        FindObjectOfType<AudioManager>().Play("Object_Toggle_Deactivate");
        foreach (ToggleableObject obj in FindObjectsOfType<ToggleableObject>())
        {
            obj.RevertObject();
        }
    }
}
