using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {

	public Texture2D galacticMap;
	public GameObject gridItem;
	public int spawnOffsetX;
	public int spawnOffsetY;

	private int innerRadiusX = 0;
	private int outerRadiusX = 0;
	private int innerRadiusY = 0;
	private int outerRadiusY = 0;

	private Camera mainCam;
	private Transform camTrans;

	// Use this for initialization
	void Start () {

		mainCam = Camera.main;
		camTrans = mainCam.transform;

		CalculateViewExtents();

		UpdateGrid();

	} // End Start
	
	// Update is called once per frame
	void Update () {

		CalculateViewExtents();
		
		if ( innerRadiusX != outerRadiusX ) {

			UpdateGrid();

		}

	} // END Update


	// Calculate the range to cover with the grid
	public void CalculateViewExtents () {

		float camZPos = camTrans.position.z;
		// Get the middle left point of the screen at worldspace z = 0
		Vector3 xExtent = mainCam.ScreenToWorldPoint( new Vector3 (1f, 0.5f, camZPos) );
		outerRadiusX = Mathf.RoundToInt(xExtent.x) + 4;

		Debug.Log (outerRadiusX); // Log that number to console

		// Get the middle top point of the screen at worldspace z = 0
		Vector3 yExtent = mainCam.ScreenToWorldPoint( new Vector3 (0.5f, 1f, camZPos) );
		outerRadiusY = Mathf.RoundToInt(yExtent.y) + 4;

		Debug.Log (outerRadiusY); // Log that number to console

	} // END CalculateViewExtents()


	public void InstantiateGridItems(float x, float y) {

		GameObject clone = Instantiate(
			gridItem,
			new Vector3 ( x, y, 0.0f ),
			Quaternion.identity
			) as GameObject;
		
		clone.transform.parent = this.transform;

		if (clone.gameObject.GetComponent<LensFlare>() != null) {
			clone.gameObject.GetComponent<LensFlare>().brightness = Random.Range (1.0f, 30.0f);
		}

	} // END InstantiateGridItems()


	// Generate grid items
	public void PopulateGrid () {

		for ( int x = -outerRadiusX; x < -innerRadiusX || x >= innerRadiusX && x <= outerRadiusX; x++ ) {

//			Debug.Log ("x = " + x);

			for ( int y = -outerRadiusY; y < -innerRadiusY || y >= innerRadiusY && y <= outerRadiusY; y++ ) {

//				Debug.Log ("\ty = " + y);

				InstantiateGridItems(x,y);

			}

		}
					
			// Update Radii so we don't create overlapping objects:
			// old outer radius becomes new inner radius
			innerRadiusX = outerRadiusX;
			innerRadiusY = outerRadiusY;

	} // END PopulateGrid



	public void UpdateGrid () {

		for ( int x = -outerRadiusX; x <= outerRadiusX; x++ ) {

			if ( x < -innerRadiusX || x > innerRadiusX ) {

				for ( int y = -outerRadiusY; y <= outerRadiusY; y++ ) {

					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;
					
					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {
						
						InstantiateGridItems(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );

					}

				}

			} else {

				for ( int y = -outerRadiusY; y < -innerRadiusY; y++ ) {
					
					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;

					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {

						InstantiateGridItems(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );

					}

				}

				for ( int y = innerRadiusY + 1; y <= outerRadiusY; y++ ) {
					
					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;
					
					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {
						
						InstantiateGridItems(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );

						
					}

				}
				
			}
		}

		// Update Radii so we don't create overlapping objects:
		// old outer radius becomes new inner radius
		innerRadiusX = outerRadiusX;
		innerRadiusY = outerRadiusY;
		
	} // END UpdateGrid

}
