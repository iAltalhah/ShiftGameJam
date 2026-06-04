using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] PlayerHealth playerHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TryToFindHim();
    }

    private void OnEnable()
    {
        TryToFindHim();

    }

    private void Update()
    {
        TryToFindHim();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;

        playerHp.PlayerHurt(1);
    }

    void TryToFindHim()
    {
        agent.SetDestination(player.position);

    }
}
