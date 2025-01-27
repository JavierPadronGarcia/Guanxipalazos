using System.Collections;
using UnityEngine;
using UnityEngine.UI; // For managing UI elements

public class HPBar : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public float damageInterval = 5f; // Time in seconds between damage instances
    public int damageAmount = 10;

    public Image hpBarForeground; // The main HP bar image (must have Fill type set to Horizontal)

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
        StartCoroutine(DamageOverTime());
    }

    private IEnumerator DamageOverTime()
    {
        while (currentHP > 0)
        {
            TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }

        Debug.Log("Player has been defeated!");
    }

    private void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();

        // Log current HP to the console
        Debug.Log("Current HP: " + currentHP);
    }

    private void UpdateHPBar()
    {
        float hpPercentage = (float)currentHP / maxHP;

        // Update the fill amount of the Image component
        hpBarForeground.fillAmount = hpPercentage;
    }
}
