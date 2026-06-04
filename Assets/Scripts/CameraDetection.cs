using Unity.Cinemachine;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;


    private void OnDisable()
    {

        cinemachineCamera.Priority = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        cinemachineCamera.Priority = 20;
    }

    private void OnTriggerStay(Collider other)
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
