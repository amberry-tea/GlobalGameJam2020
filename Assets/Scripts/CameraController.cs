using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float zoom;
    //public GameObject player;
     
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - player.transform.position;
    }

    public void ZoomIn(){
    	StartCoroutine(ZoomInCoroutine());
	}

	IEnumerator ZoomInCoroutine(){
		while(zoom > .5){
            zoom -= 0.05F;
            yield return new WaitForEndOfFrame();
        }
	}

}
