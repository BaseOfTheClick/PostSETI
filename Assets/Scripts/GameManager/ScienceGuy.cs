using UnityEngine;
using System.Collections;

public class ScienceGuy : MonoBehaviour {
    public GameObject scienceGroupObject;

    //returns true if this game object has children in the science category
    public bool isScienceGuy(GameObject obj)
    {
        foreach (Transform t in scienceGroupObject.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject == obj)
            {
                return true;
            }
        }
        return false;
    }
}
