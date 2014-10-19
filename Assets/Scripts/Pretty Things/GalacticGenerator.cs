using UnityEngine;
using System.Collections;

public class GalacticGenerator : MonoBehaviour {

	public GameObject[] starPrefabs;
	public float probability;
	public int numToGenerate = 4;

	[SerializeField]
	private float nextSpawnWave = 0.0f;


	// Use this for initialization
	void Start () {
		nextSpawnWave = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		nextSpawnWave += Time.deltaTime;

		if ( nextSpawnWave >= 1.5f ) {

			GenerateStar(numToGenerate);
			numToGenerate = Random.Range(2,50);
			nextSpawnWave = 0;
		}

	}

	public void GenerateStar( int numStars ){
		while ( numStars > 0 ) {
			Debug.Log ("Generating");
			int starToGenerate = Random.Range(0 , starPrefabs.Length - 1);

			Vector3 placeToGenerate = new Vector3 (
				Random.Range (-200.0f,200.0f),
				Random.Range (-200.0f,200.0f),
				Random.Range (-20.0f,-50.0f)
			    );

			GameObject clone = Instantiate(starPrefabs[starToGenerate], placeToGenerate, Quaternion.identity) as GameObject;
			clone.transform.parent = transform;

			clone.GetComponent<LensFlare>().brightness = Random.Range(5f,60f);

			numStars--;

		}
	}

}
