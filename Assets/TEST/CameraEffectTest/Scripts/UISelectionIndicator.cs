using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISelectionIndicator : MonoBehaviour {

	    public PixelEffect pixelEffectGameObject; //Container to be able to access the GameObject wit hattached PixelEffect.cs, in which the hitObject value can be accessed.

	  
	    void Update () {

        //If the hitObject has a value/gameObject set, the UI Image(red corners) and text get set active/visible
        //If the element has a Text component attached, the text gets changed to the name of the GameObject in order to display it.
		if(pixelEffectGameObject.hitObject != null) {
			for (int i = 0; i < this.transform.childCount; i++) {
				this.transform.GetChild(i).gameObject.SetActive(true);
                if(this.transform.GetChild(i).gameObject.GetComponent<Text>() != null)
                {
                    this.transform.GetChild(i).gameObject.GetComponent<Text>().text = pixelEffectGameObject.hitObject.name;
                }
			}
            //A 2D Rectangle defined by X and Y position, width and height.
            //The Renderer of child-Gameobject of the hitObject get transfered as paramter to the RendererBoundsInScreenSpace method.
            //The method returns a Rect with outermost/extreme render bound corners in screenspace and set it to visualRect
            Rect visualRect = RendererBoundsInScreenSpace(pixelEffectGameObject.hitObject.GetComponentInChildren<Renderer>());
            
            //Get the Rect attached to the UI Element GameObject
			RectTransform rt = GetComponent<RectTransform>();

            //Set the position and size of the Rect to the outermost/extreme render bound corners of the hitObject in screenspace to adjust the red corner image accordingly.
            rt.position = new Vector2( visualRect.xMin, visualRect.yMin );
			rt.sizeDelta = new Vector2( visualRect.width, visualRect.height ); 
		}
		else {
			for (int i = 0; i < this.transform.childCount; i++) {
				this.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	    }

	    static Vector3[] screenSpaceCorners;
	    static Rect RendererBoundsInScreenSpace(Renderer r) {
        // Bounds is used by Collider.bounds, Mesh.bounds and Renderer.bounds , representing a box fully enclosing some Gameobject.
        //In this case we store the Renderer.bounds of the hitObject in hitObjectBounds.
        Bounds hitObjectBounds = r.bounds;

		if(screenSpaceCorners == null)
			screenSpaceCorners = new Vector3[8];

		Camera theCamera = Camera.main;

		// For each of the 8 corners of our renderer's world space bounding box,
		// convert those corners into screen space using the camera (mainCamera in this case).
		screenSpaceCorners[0] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x + hitObjectBounds.extents.x, hitObjectBounds.center.y + hitObjectBounds.extents.y, hitObjectBounds.center.z + hitObjectBounds.extents.z ) );
		screenSpaceCorners[1] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x + hitObjectBounds.extents.x, hitObjectBounds.center.y + hitObjectBounds.extents.y, hitObjectBounds.center.z - hitObjectBounds.extents.z ) );
		screenSpaceCorners[2] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x + hitObjectBounds.extents.x, hitObjectBounds.center.y - hitObjectBounds.extents.y, hitObjectBounds.center.z + hitObjectBounds.extents.z ) );
		screenSpaceCorners[3] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x + hitObjectBounds.extents.x, hitObjectBounds.center.y - hitObjectBounds.extents.y, hitObjectBounds.center.z - hitObjectBounds.extents.z ) );
		screenSpaceCorners[4] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x - hitObjectBounds.extents.x, hitObjectBounds.center.y + hitObjectBounds.extents.y, hitObjectBounds.center.z + hitObjectBounds.extents.z ) );
		screenSpaceCorners[5] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x - hitObjectBounds.extents.x, hitObjectBounds.center.y + hitObjectBounds.extents.y, hitObjectBounds.center.z - hitObjectBounds.extents.z ) );
		screenSpaceCorners[6] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x - hitObjectBounds.extents.x, hitObjectBounds.center.y - hitObjectBounds.extents.y, hitObjectBounds.center.z + hitObjectBounds.extents.z ) );
		screenSpaceCorners[7] = theCamera.WorldToScreenPoint( new Vector3(hitObjectBounds.center.x - hitObjectBounds.extents.x, hitObjectBounds.center.y - hitObjectBounds.extents.y, hitObjectBounds.center.z - hitObjectBounds.extents.z ) );

		// Get the the min/max X & Y of these screen space corners to get the outermost/extreme render bound corners in screenspace.
		float min_x = screenSpaceCorners[0].x;
		float min_y = screenSpaceCorners[0].y;
		float max_x = screenSpaceCorners[0].x;
		float max_y = screenSpaceCorners[0].y;

		for (int i = 1; i < 8; i++) {
			if(screenSpaceCorners[i].x < min_x) {
				min_x = screenSpaceCorners[i].x;
			}
			if(screenSpaceCorners[i].y < min_y) {
				min_y = screenSpaceCorners[i].y;
			}
			if(screenSpaceCorners[i].x > max_x) {
				max_x = screenSpaceCorners[i].x;
			}
			if(screenSpaceCorners[i].y > max_y) {
				max_y = screenSpaceCorners[i].y;
			}
		}

        //returning a Rect with outermost/extreme render bound corners in screenspace
		return Rect.MinMaxRect( min_x, min_y, max_x, max_y );
	}
}
