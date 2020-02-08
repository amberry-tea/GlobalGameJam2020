using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	public PlayerController player;
	private bool tripped = false;
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject == player.gameObject)
		{
			MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
			musicPlayer.FadeOutMusic(1.5f);
		} else {
			print("NOT PLAYER");
		}
	}
}