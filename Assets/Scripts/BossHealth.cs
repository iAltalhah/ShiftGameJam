using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Hearts UI")]
    [SerializeField] private List<Image> bossHearts = new List<Image>();


    private int currentHealth;
    private bool isDead;

    private void Start()
    {
        currentHealth = bossHearts.Count;

        for (int i = 0; i < bossHearts.Count; i++)
        {
            bossHearts[i].enabled = true;
        }
    }

    public void BossHurt()
    {
        if (isDead)
        {
            return;
        }

        currentHealth--;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            BossDied();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < bossHearts.Count; i++)
        {
            if (i < currentHealth)
            {
                bossHearts[i].enabled = true;
            }
            else
            {
                bossHearts[i].enabled = false;
            }
        }
    }

    private void BossDied()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        Debug.Log("Boss died");

        Destroy(gameObject);
    }
}