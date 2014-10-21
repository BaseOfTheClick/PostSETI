using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour {
    public GameObject resourcePackage;

    public IntelligentPlanet planet;
    private Vector3[] planetVertices;

    public float timeInterval = 1f;
    private float timeAccumulate = 0;

    private Vector3 origin = Vector3.zero;

	// Use this for initialization
	void Start () {
        planet = this.GetComponent<IntelligentPlanet>();
        planetVertices = planet.GetComponent<MeshFilter>().mesh.vertices;
        timeInterval = this.GetComponent<IntelligentPlanet>().spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
        timeAccumulate += Time.deltaTime;

        if (timeAccumulate >= timeInterval)
        {
            Vector3 randomVertex = planetVertices[Random.Range(0, planetVertices.Length)];
            Vector3 vertexWithParentTransform = planet.gameObject.transform.TransformPoint(randomVertex);
            Vector3 finalPosition = (vertexWithParentTransform - origin) + ((vertexWithParentTransform - origin) * 0.25f);


            GameObject clone = Instantiate(resourcePackage, finalPosition, Quaternion.identity) as GameObject;
            clone.transform.parent = planet.gameObject.transform;

            timeAccumulate = 0;
        }
	}
}
