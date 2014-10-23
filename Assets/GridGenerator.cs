using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {

	public GameObject gridItem;

	public int xViewRadius = 10;
	public int yViewRadius = 10;

	// Use this for initialization
	void Start () {

		// generate cubes for +x +y and 0,0
		for ( int x = -xViewRadius; x <= xViewRadius; x++ ) {
			for ( int y = -yViewRadius; y <= yViewRadius; y++ ) {
				GameObject clone = Instantiate(
					gridItem,
					new Vector3 ( x, y, 0.0f ),
					Quaternion.identity
				) as GameObject;

				clone.transform.parent = this.transform;

			}
		}

	}// End Start
	
	// Update is called once per frame
	void Update () {
	
		

	}

}
