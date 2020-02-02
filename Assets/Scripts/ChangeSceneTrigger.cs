using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{

    public string sceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            if(sceneName != ""){
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            } else {
                print("ERROR: No scene name on trigger at " + this.gameObject.transform.position.x + ", " + this.gameObject.transform.position.y);
            }
        }
    }
}
