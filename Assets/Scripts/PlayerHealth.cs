using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private List<Image> playerHearts = new List<Image>();

    int currentHealth;

    private void Start()
    {
        currentHealth = playerHearts.Count;

        for (int i = 0; i < playerHearts.Count; i++)
        {
            playerHearts[i].enabled = true;
        }
    }

    public void PlayerHurt(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            PlayerDied();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < playerHearts.Count; i++)
        {
            if (i < currentHealth)
            {
                playerHearts[i].enabled = true;
            }
            else
            {
                playerHearts[i].enabled = false;
            }
        }
    }


    public void PlayerHeal()
    {
        currentHealth = playerHearts.Count;
        UpdateHeartsUI();
    }

    private void PlayerDied()
    {
        Debug.Log("Player died");

        // Death logic here
        // Example:
        // disable movement
        // play death animation
        // reload scene
        // show game over UI
    }

}
