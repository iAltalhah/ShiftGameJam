using Unity.Cinemachine;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        cinemachineCamera.Priority = 20;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        cinemachineCamera.Priority = 10;
    }
}
