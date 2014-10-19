using UnityEngine;
using System.Collections;

public class ResourceTrigger : MonoBehaviour {
    GameObject inTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ResourcePackage"))
        {
            inTrigger = other.gameObject;
        }
        Debug.Log("On Trigger Enter Called");

    }

    void OnTriggerExit(Collider other)
    {
        if (other == inTrigger.collider)
        {
            inTrigger = null;
        }
        Debug.Log("On Trigger Exit Called");
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Debug.Log("In update : getButtonUp for trigger");
                if (inTrigger != null)
                {
                    Debug.Log("Destroying Object");
                    this.GetComponent<SocietyBar>().addResources(inTrigger.GetComponent<ResourcePackage>().resourceQuantity);
                    inTrigger.GetComponent<ResourcePackage>().resourceQuantity = 0;
                    this.GetComponent<SocietyBar>().conveyResources();

                    Destroy(inTrigger);
                }
                else
                {
                    Debug.Log("inTrigger is null");
                }
            }
        }
    }
}
