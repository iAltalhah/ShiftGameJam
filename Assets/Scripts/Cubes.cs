using UnityEngine;

public class Cubes : MonoBehaviour
{
    [SerializeField] CollectingManager manager;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        manager.UpdateCounter();

        Destroy(gameObject);
    }
}
