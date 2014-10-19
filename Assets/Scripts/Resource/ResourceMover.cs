using UnityEngine;
using System.Collections;

public class ResourceMover : MonoBehaviour {
    [System.NonSerialized]
    public GameObject resourcePackage;
    [System.NonSerialized]
    public Camera mainCam;

    public float touchScaleFactor = 0.25f;

    private bool camOrbitEnabled = false;
    private bool pickupEnabled = false;

	private GameObject planet;

    private RaycastHit camHit;
    
    // Use this for initialization
	void Start () {
        mainCam = Camera.main;
		planet = GameObject.Find("Planet");
	}

	// Update is called once per frame
	void Update () 
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetButtonDown("Fire1"))
        {
            pollInput();
        }
        if (pickupEnabled)
        {
            pickupResource(camHit);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            resetChecks();
            if (camHit.transform != null)
            {
                if (camHit.transform.gameObject.CompareTag("ResourcePackage"))
                {
                    Destroy(camHit.transform.gameObject);
                }
            }
        }
        }

	}
    void LateUpdate()
    {
        if (camOrbitEnabled)
        {
            
            //planet.GetComponent<CameraController>().DoCamOrbit( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                planet.GetComponent<CameraController>().DoCamOrbit(touch.deltaPosition.x * touchScaleFactor, touch.deltaPosition.y * touchScaleFactor);
            }
        }
    }

    void pollInput()
    {
        Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out camHit))
        {
            if (camHit.transform.gameObject.CompareTag("ResourcePackage"))
            {
                pickupEnabled = true;
            }
            else
            {
                Debug.Log("Didn't Hit the Resource");
                camOrbitEnabled = true;
            }
        }
        else
        {
            Debug.Log("Hit Nothing");
            camOrbitEnabled = true;
            camHit = new RaycastHit();
        }
    }

    void resetChecks()
    {
        pickupEnabled = false;
        camOrbitEnabled = false;
    }

    void pickupResource(RaycastHit camHit)
    {
        Debug.Log("Hit Resource");
        GameObject rPack = camHit.transform.gameObject;
        rPack.GetComponent<ResourcePackage>().MoveMeWithMouse();
    }
}
