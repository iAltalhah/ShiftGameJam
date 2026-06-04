using AC;
using TMPro;
using UnityEngine;

public class CollectingManager : MonoBehaviour
{
    [SerializeField] private int counter;
    [SerializeField] private TextMeshProUGUI counterText;

    [Header("AC Component Variable")]
    [SerializeField] private Variables acVariables;
    [SerializeField] private int cubesCollectedVariableID;

    [SerializeField] AudioSource coin;

    public void UpdateCounter()
    {
        counter++;
        coin.Play();
        if (counterText != null)
        {
            counterText.text = counter.ToString();
        }

        if (acVariables == null)
        {
            Debug.LogWarning("AC Variables component is not assigned.");
            return;
        }

        GVar cubesVariable = acVariables.GetVariable(cubesCollectedVariableID);

        if (cubesVariable == null)
        {
            Debug.LogWarning("Could not find AC component variable with ID: " + cubesCollectedVariableID);
            return;
        }

        cubesVariable.IntegerValue = counter;

        Debug.Log("Counter is now: " + counter);
        Debug.Log("AC Component Variable is now: " + cubesVariable.IntegerValue);
    }
}