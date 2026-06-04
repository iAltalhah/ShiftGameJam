using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Hearts UI")]
    [SerializeField] private List<Image> bossHearts = new List<Image>();

    [SerializeField] BoxCollider bossCollider;
    [SerializeField] Animator bossAnimator;
    [SerializeField] GameObject boss;

    [SerializeField] GameObject endingBox;
    [SerializeField] NavMeshAgent agent;


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

        StartCoroutine(BossDeath());

    }

    IEnumerator BossDeath()
    {
        bossCollider.enabled = false;
        agent.enabled = false;
        bossAnimator.Play("Death");
        yield return new WaitForSeconds(5f);

        boss.SetActive(false);
        endingBox.SetActive(true);

    }
}