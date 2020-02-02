using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool zoom;
    private float orthoZoomComponent;
    private Camera cam;
    private int zoomLevel;
    //public GameObject player;
     
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        zoomLevel = 2;
    }

    public void ZoomIn() {
        orthoZoomComponent = zoomLevel;
        zoom = true;
	}
    public void ZoomOut() {
        orthoZoomComponent = zoomLevel;
        zoom = false;
    }

    void Update() {
        //print(zoom);
        if (zoom) {
            Time.timeScale = 0.2f;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, -10), 0.1f);
            orthoZoomComponent = cam.transform.localPosition.y + (0.3f / 2.3f * zoomLevel);
            if (orthoZoomComponent < zoomLevel) {
                cam.orthographicSize = orthoZoomComponent * 2.3f * zoomLevel;
            }
            //print(zoom);
        } else if (!zoom) {
            Time.timeScale = 1;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0.35f, -10), 0.1f);
            orthoZoomComponent = cam.transform.localPosition.y + (0.3f / 2.3f * zoomLevel);
            if (orthoZoomComponent < zoomLevel) {
                cam.orthographicSize = orthoZoomComponent * 2.3f * zoomLevel;
            }
            //print(zoom);
        }
        //print(zoom);
    }
}
