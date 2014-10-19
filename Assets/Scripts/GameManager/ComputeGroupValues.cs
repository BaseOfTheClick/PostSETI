using UnityEngine;
using System.Collections;

public class ComputeGroupValues : MonoBehaviour
{
    public GameObject scienceGroup;
    public GameObject econGroup;
    public GameObject polGroup;

    public float computeScienceAverage()
    {
        float sum = 0;
        const float groupSize = 3;

        foreach (Transform t in scienceGroup.GetComponentsInChildren<Transform>())
        {
            sum += t.gameObject.GetComponent<SocietyBar>().getResources();
        }

        return (sum / groupSize);
    }
    public float computeEconAverage()
    {
        float sum = 0;
        const float groupSize = 3;

        foreach (Transform t in econGroup.GetComponentsInChildren<Transform>())
        {
            sum += t.gameObject.GetComponent<SocietyBar>().getResources();
        }

        return (sum / groupSize);
    }
    public float computePolAverage()
    {
        float sum = 0;
        const float groupSize = 3;

        foreach (Transform t in polGroup.GetComponentsInChildren<Transform>())
        {
            sum += t.gameObject.GetComponent<SocietyBar>().getResources();
        }

        return (sum / groupSize);
    }
}
