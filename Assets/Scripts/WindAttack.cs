using UnityEngine;

public class WindAttack : MonoBehaviour
{
    [SerializeField] private BossHealth health;

    private bool hasHitBoss;

    private void OnEnable()
    {
        hasHitBoss = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryHitBoss(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryHitBoss(other);
    }

    private void TryHitBoss(Collider other)
    {
        if (hasHitBoss)
        {
            return;
        }

        if (!other.CompareTag("Boss"))
        {
            return;
        }

        if (health == null)
        {
            Debug.LogWarning("BossHealth is not assigned on WindAttack.");
            return;
        }

        hasHitBoss = true;

        health.BossHurt();
        Debug.Log("Boss hit");
    }
}