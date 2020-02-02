using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	private float timer = 0.0f;
	private int currentEvent = 0;
	private bool hasExecuted = false;
	private float lerpAlpha = 0.0f;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// If the timer hasn't reached 0, decrement it.
        if(timer > 0)
		{
			timer -= 1.0f * Time.deltaTime;
		}
		// If the timer HAS reached 0 and we haven't executed any events,
		// increment the currentEvent counter and execute its command.
		else if(hasExecuted == false)
		{
			hasExecuted = true;
			currentEvent += 1;
			ExecuteCommand();
		}
    }
	
	// Reset the timer to its new length and allow event triggering.
	void TimerUpdate(float timerLength)
	{
		timer = timerLength;
		hasExecuted = false;
	}
	
	void ExecuteCommand()
	{
		switch (currentEvent)
		{
			// Event 1: Stop player input and center the camera on Monolith.
			case 1:
				PausePlayer();
				break;
			// Event 2: Monolith wakes up.
			case 2:
				
				break;
			// Event 3: Play spooky music. Monolith's eyes turn red.
			case 3:
				
				break;
				
			// Event 4: Laser charge.
			case 4:
				
				break;
			// Event 5: Laser fires and sweeps across screen.
			case 5:
				
				break;
			// Event 6: Laser 2.
			case 6:
				
				break;
			// Event 7: Fade to credits.
			case 7:
				
				break;
			default:
				break;
		}
	}
	
	void PausePlayer()
	{
		// Disable player control.
		CharacterController controller = FindObjectOfType<PlayerController>().GetComponent(typeof(CharacterController)) as CharacterController;
		controller.enabled = false;
		
		// Start camera movement coroutine.
		StartCoroutine(CameraMove());
	}
	
	// Camera movement coroutine.
	private IEnumerator CameraMove()
	{
		// Get player camera.
		Camera camera = FindObjectOfType<PlayerController>().GetComponentsInChildren<Camera>()[0] as Camera;
		float cameraMoveTime = 5.0f; // Camera move time in seconds
		
		Vector3 startPos = camera.transform.position;
		Vector3 endPos = new Vector3(0,0,0); //Final Position Goes Here
		
		MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
		musicPlayer.FadeOutMusic(1.5f);
		
		while(lerpAlpha < cameraMoveTime)
		{
			camera.transform.position = Vector3.Lerp(startPos, endPos, (lerpAlpha / cameraMoveTime));
			lerpAlpha += Time.deltaTime;
			yield return null;
		}
		
		TimerUpdate(2.0f);
		
	}
}
