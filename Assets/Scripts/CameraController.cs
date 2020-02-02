using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool zoom;
    private float orthoZoomComponent;
    private Camera cam;
    //public GameObject player;
     
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    public void ZoomIn() {
        orthoZoomComponent = 1;
        zoom = true;
	}

    void Update() {
        if (zoom) {
            Time.timeScale = 0.3f;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, -10), 0.1f);
            orthoZoomComponent = cam.transform.localPosition.y + (0.3f / 2);
            if (orthoZoomComponent < 1) {
                cam.orthographicSize = orthoZoomComponent * 2.3f;
            }
        }
    }
}
