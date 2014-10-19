using UnityEngine;
using System.Collections;

public class EconGuy : MonoBehaviour
{
    public GameObject econGroupObject;

    public bool isEconomist(GameObject obj)
    {
        foreach (Transform t in econGroupObject.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject == obj)
            {
                return true;
            }
        }

        return false;
    }
}
