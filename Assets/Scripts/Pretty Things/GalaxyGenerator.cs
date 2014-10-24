using UnityEngine;
using System.Collections;

public class GalaxyGenerator : MonoBehaviour {

	// The galactic map!
	public Texture2D galacticMap;
	
	// Offset for where the planet spawns in the galactic map
	public int spawnOffsetX; 
	public int spawnOffsetY; 

	// variables for celestial object instantiation
	public GameObject brightBody;
	public GameObject occludingBody;
	public Texture2D[] brightBodyTextures;
	public Texture2D[] occludingBodyTextures;

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
		outerRadiusX = Mathf.RoundToInt(xExtent.x) + 35;

//		Debug.Log (outerRadiusX); // Log that number to console

		// Get the middle top point of the screen at worldspace z = 0
		Vector3 yExtent = mainCam.ScreenToWorldPoint( new Vector3 (0.5f, 1f, camZPos) );
		outerRadiusY = Mathf.RoundToInt(yExtent.y) + 35;

//		Debug.Log (outerRadiusY); // Log that number to console

	} // END CalculateViewExtents()


	public void InstantiateBrightBody( float x, float y ) {

		// Instantiate bright body prefab as gameobject
		// At coordinates provided
		// And at random z-depth (on the other side of the z-axis from the camera)
		// And with a random z-rotation
		GameObject clone = Instantiate(
			brightBody,
			new Vector3 ( x, y, Random.Range (0, 30) ),
			Quaternion.Euler( new Vector3( 0, 0, Random.Range( 0, 360 ) ) )
			) as GameObject;
		
		clone.transform.parent = this.transform;
		/*
		//set material variables for this instance:
		Material mat = clone.renderer.material;
		//Pick random sprite
		mat.mainTexture = brightBodyTextures[Random.Range(0, brightBodyTextures.Length - 1 )];
		//Pick random color for sprite
		Color matColor = new Color( Random.Range(0.65f, 0.75f), Random.Range(0.65f, 0.75f), Random.Range(0.75f, 1f));
		mat.SetColor ("_Color", matColor);
		//Pick random emmissivity for sprite.
		float emmisVal = Random.Range (0.0f, 0.1f);
		Color emmisColor = new Color (emmisVal,emmisVal,emmisVal);
		mat.SetColor ("_EmisColor", emmisColor);*/

	} // END InstantiateGridItems()

	public void InstantiateOccludingBody (float x, float y) {

		// Instantiate occluding body prefab as gameobject
		// At coordinates provided
		// And at random z-depth (on the other side of the z-axis from the camera)
		// And with a random z-rotation
		GameObject clone = Instantiate(
			occludingBody,
			new Vector3 ( x, y, Random.Range (2, 27) ),
			Quaternion.Euler( new Vector3( 0, 0, Random.Range( 0, 360 ) ) )
			) as GameObject;
		//Set Parent for this instance to keep scene clean
		clone.transform.parent = this.transform;
		clone.renderer.material.mainTexture = brightBodyTextures[Random.Range(0, brightBodyTextures.Length - 1 )];

	}
	

	public void UpdateGrid () {

		for ( int x = -outerRadiusX; x <= outerRadiusX; x++ ) {

			if ( x < -innerRadiusX || x > innerRadiusX ) {

				for ( int y = -outerRadiusY; y <= outerRadiusY; y++ ) {

					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;
					
					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {
						
						InstantiateBrightBody(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );

					}
//					if ( probability < Random.Range (0.1f, 0.2f) ) {
//
//						InstantiateOccludingBody ( x, y );
//
//					}

				}

			} else {

				for ( int y = -outerRadiusY; y < -innerRadiusY; y++ ) {
					
					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;

					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {

						InstantiateBrightBody(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );

					}
//					if ( probability < Random.Range (0.1f, 0.2f) ) {
//						
//						InstantiateOccludingBody ( x, y );
//						
//					}

				}

				for ( int y = innerRadiusY + 1; y <= outerRadiusY; y++ ) {
					
					float probability = galacticMap.GetPixel( x + spawnOffsetX, y + spawnOffsetY ).grayscale;
					
					if ( probability > Random.Range ( 0.2f, 1.0f ) ) {
						
						InstantiateBrightBody(x + Random.Range(-0.95f, 0.95f),
						                     y + Random.Range(-0.95f, 0.95f)
						                     );
						
					}
//					if ( probability < Random.Range (0.1f, 0.2f) ) {
//						
//						InstantiateOccludingBody ( x, y );
//						
//					}

				}
				
			}
		}

		// Update Radii so we don't create overlapping objects:
		// old outer radius becomes new inner radius
		innerRadiusX = outerRadiusX;
		innerRadiusY = outerRadiusY;
		
	} // END UpdateGrid

}
