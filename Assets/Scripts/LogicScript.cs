using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text cooldownText;
    public Text durationText;
    public Image icon;
    private AbilityHandler abilityHandler;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            abilityHandler = playerObject.GetComponent<AbilityHandler>();
        }

        if (abilityHandler == null)
        {
            Debug.LogError("AbilityHandler not found on the playerObject!");
        }
    }
    private void Update()
    {
        UpdateCooldown();
        UpdateDuration();
        UpdateAbilityIcon();
    }
    public void UpdateCooldown()
    {
        var cooldown = abilityHandler.GetCooldownTime();

        if (abilityHandler == null || abilityHandler.currentAbility == null || abilityHandler.currentAbility.cooldownTime == 0)
        {
            cooldownText.text = string.Empty;
            return;
        }

        if (cooldown <= 0)
        {
            cooldownText.text = string.Empty;
            return;
        }

        string cooldownToString = $"{cooldown.ToString(".0#")}";
        cooldownText.text = cooldownToString;
    }

    public void UpdateDuration()
    {
        var duration = abilityHandler.GetDurationTime();

        if (abilityHandler == null || abilityHandler.currentAbility == null || abilityHandler.currentAbility.durationTime == 0)
        {
            durationText.text = string.Empty;
            return;
        }

        if (duration <= 0)
        {
            durationText.text = string.Empty;
            return;
        }

        string durationToString = $"{duration.ToString(".0#")}";
        durationText.text = durationToString;
    }

    public void UpdateAbilityIcon()
    {
        icon.overrideSprite = abilityHandler.GetAbilityIcon();
    }
}
