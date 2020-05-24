using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {

    public PixelEffect pixelEffectGameObject;
    public float padding = 1.0f;

    void Update () {
		if(pixelEffectGameObject.hitObject != null) {
			GetComponentInChildren<Renderer>().enabled = true;

			Bounds bigBounds = pixelEffectGameObject.hitObject.GetComponentInChildren<Renderer>().bounds;

			this.transform.position = new Vector3(bigBounds.center.x, bigBounds.center.y, bigBounds.center.z);
			this.transform.localScale = new Vector3( bigBounds.size.x*padding, bigBounds.size.y*padding, bigBounds.size.z*padding );
		}
		else {
			GetComponentInChildren<Renderer>().enabled = false;
		}
	}
}
 