using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableObject : MonoBehaviour
{
    Renderer renderer;
    Collider collider;
    public Material defaultMaterial;
    public Material overrideMaterial;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        renderer.enabled = true;
        renderer.material = defaultMaterial;
        collider.enabled = false;
    }
    public void UpdateObject()
    {
        renderer.material = overrideMaterial;
        collider.enabled = true;
    }
    public void RevertObject()
    {
        renderer.material = defaultMaterial;
        collider.enabled = false;
    }
}
