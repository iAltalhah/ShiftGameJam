using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [Header("Objects that should be turned OFF")]
    [SerializeField] private List<GameObject> objectsToDeactivate = new List<GameObject>();

    public void ActivateObjectAndDeactivateList(GameObject objectToActivate)
    {
        // First, deactivate all objects in the list
        for (int i = 0; i < objectsToDeactivate.Count; i++)
        {
            if (objectsToDeactivate[i] != null)
            {
                objectsToDeactivate[i].SetActive(false);
            }
        }

        // Then activate the object chosen from AC
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No object was assigned from AC.");
        }
    }

    public void DeactivateAllObjects()
    {
        for (int i = 0; i < objectsToDeactivate.Count; i++)
        {
            if (objectsToDeactivate[i] != null)
            {
                objectsToDeactivate[i].SetActive(false);
            }
        }
    }
}