using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public float cooldownTime;
    public float durationTime;

    public virtual void Activate(GameObject player)
    {
        
    }
    public virtual void Deactivate(GameObject player)
    {

    }
}
