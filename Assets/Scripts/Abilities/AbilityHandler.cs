using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    public List<Ability> abilities;
    private int currentAbilityIndex = 0;
    public Ability currentAbility => abilities[currentAbilityIndex];

    private float durationTime;
    private float cooldownTime;
    enum AbilityState
    {
        ready,
        active,
        onCooldown
    }
    private AbilityState abilityState = AbilityState.ready;

    public KeyCode abilityKey;
    public KeyCode swapKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(swapKey))
        {
            SwapAbility();
        }

        switch (abilityState)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(abilityKey))
                {
                    Debug.Log($"Activated ability: {currentAbility.name}");
                    currentAbility.Activate(gameObject);
                    abilityState = AbilityState.active;
                    durationTime = currentAbility.durationTime;
                }
                break;
            case AbilityState.active:
                if(durationTime > 0)
                {
                    durationTime -= Time.deltaTime;
                }
                else
                {
                    currentAbility.Deactivate(gameObject);
                    abilityState = AbilityState.onCooldown;
                    cooldownTime = currentAbility.cooldownTime;
                }
                break;
            case AbilityState.onCooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    abilityState = AbilityState.ready;
                }
                break;
        }
    }

    private void SwapAbility()
    {
        currentAbility.Deactivate(gameObject);
        currentAbilityIndex = (currentAbilityIndex + 1) % abilities.Count;
        abilityState = AbilityState.ready;
        Debug.Log($"Swapped to ability: {currentAbility.name}");
    }

    //these are for the ui
    public float GetCooldownTime()
    {
        return abilityState == AbilityState.onCooldown ? cooldownTime : 0;
    }

    public float GetDurationTime()
    {
        if(abilityState == AbilityState.active)
        {
            return durationTime;
        }
        else
        {
            return 0;
        }
    }
    public Sprite GetAbilityIcon()
    {
        return currentAbility.icon;
    }
}
