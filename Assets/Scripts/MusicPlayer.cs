using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/MusicPlayer")]
public class MusicPlayer : MonoBehaviour
{
	[Tooltip("Name of the song to start with.")]
	public string music = "DefaultMusic";
	[Tooltip("Start playing at runtime.")]
	public bool autoStart = false;
	[Tooltip("Path to the music folder, relative to the Resources folder.")]
	public string musicPath = "Audio";
	[Tooltip("AudioSources that will play music.")]
	public AudioSource[] musicSources;
	[Tooltip("How quickly to fade when starting a new song, in seconds.")]
	public float newSongFade = 0.4f;
	
	private bool isPlaying = false;
	private bool waitingToRestart = false;
	private string currentMusicName;
	private string restartMusicName;
	private AudioClip currentMusic;
	private AudioClip currentMusicLoop;
	
	// Start is called before the first frame update
	void Start()
	{
		// Remove this if this won't be a Singleton
		DontDestroyOnLoad(gameObject);
		
		if(autoStart)
		{
			PlayMusic(music);
		}
		
		if(musicSources.Length < 0)
		{
			musicSources = GetComponents<AudioSource>();
		}
	}
		
	/**
	 *	This command plays music.
	 *	musicName = Filename of the music you want to play, without extensions.
	 */
	public void PlayMusic(string musicName)
	{
		// If music is currently playing, fade it out quickly, then restart the PlayMusic command.
		if(isPlaying && currentMusicName != musicName)
		{
			StartCoroutine(ChangeVolume(newSongFade, 0.0f));;
			waitingToRestart = true;
			restartMusicName = musicName;
		}
		else if(isPlaying == false)
		{
			// Load the desired audio files.
			currentMusicName = musicName;
			currentMusic = Resources.Load(musicPath + "/" + currentMusicName) as AudioClip;
			currentMusicLoop = Resources.Load(musicPath + "/" + currentMusicName + "Loop") as AudioClip;

			if(musicSources.Length == 0)
				print("Music Player Error: No Audio Sources available! Please add them to 'Music Sources' in the inspector.");
	
			if(currentMusic != null)
			{
				// Load the audio files into Audio Sources.
				musicSources[0].clip = currentMusic;
				musicSources[0].volume = 1.0f;
				
				if(currentMusicLoop != null)
				{
					musicSources[1].clip = currentMusicLoop;
					musicSources[1].volume = 1.0f;
					musicSources[1].loop = true;
				}
	
				// Tell the Audio Sources to play.
				isPlaying = true;
				musicSources[0].Play();
				
				if(currentMusicLoop != null)
					musicSources[1].PlayDelayed(currentMusic.length);
			}
			else
				print("Music Player Error: No Audio Clip loaded! This file may not exist, or the song you requested has a typo.");
		}
	}
	
	/**
	 *	This command stops music completely.
	 */
	public void StopMusic()
	{
		isPlaying = false;
		foreach(AudioSource source in musicSources)
		{
			source.Stop();
		}
	}
	
	/**
	 *	This command fades the music to nothing, then stops music.
	 *	fadeLength = Length of time to fade to zero, in seconds.
	 */
	public void FadeOutMusic(float fadeLength)
	{
		StartCoroutine(ChangeVolume(fadeLength, 0.0f));
	}
	
	/**
	 *	Sets the music volume to a specific value, fading over time.
	 *	targetVolume = The volume you want the music to change to.
	 *	fadeLength = The amount of time to fade to targetVolume. Use 0.0 to make it immediate.
	 */
	public void MusicVolume(float targetVolume, float fadeLength)
	{
		StartCoroutine(ChangeVolume(fadeLength, targetVolume));
	}
	
	public void DeathPitch()
	{
		StartCoroutine(ChangeVolume(0.75f, 0.0f));
		StartCoroutine(ChangePitch(0.5f, 0.1f));
	}
	
	// Volume fade coroutine.
	public IEnumerator ChangeVolume(float fadeLength, float newVolume)
	{
		if(isPlaying == true)
		{
			float startVolume;
			
			if(musicSources[0].isPlaying == true)
			{
				// If the intro is still playing, get the volume of the intro's Audio Source.
				startVolume = musicSources[0].volume;
				isPlaying = false;
				
				// Fade the volume to the target volume. Fade both Audio Sources, in case the intro finishes before we fade out.
				while(musicSources[0].volume > newVolume)
				{
					musicSources[0].volume -= startVolume * Time.deltaTime / fadeLength;
					musicSources[1].volume -= startVolume * Time.deltaTime / fadeLength;
					yield return null;
				}
				
				// If the target volume is 0, stop the music entirely.
				if(newVolume == 0.0f)
				{
					// Stop the Audio Sources, and restore their volumes to the original value.
					musicSources[0].Stop();
					musicSources[1].Stop();
					musicSources[0].volume = startVolume;
					musicSources[1].volume = startVolume;
					print("All music stopped.");
				
					// If this fadeout was called when switching to a new song, call the PlayMusic function again.
					if(waitingToRestart)
					{
						PlayMusic(restartMusicName);
						waitingToRestart = false;
					}
				}
			}
			else if(musicSources[1].isPlaying == true)
			{
				// If the loop playing, get the volume of the loop's Audio Source.
				startVolume = musicSources[1].volume;
				isPlaying = false;
				
				// Fade the volume to the target volume.
				while(musicSources[1].volume > newVolume)
				{
					musicSources[1].volume -= startVolume * Time.deltaTime / fadeLength;
					yield return null;
				}
				
				// If the target volume is 0, stop the music entirely.
				if(newVolume == 0.0f)
				{
					// Stop the Audio Source, and restore its volume to the original value.
					musicSources[1].Stop();
					musicSources[1].volume = startVolume;
					print("All music stopped.");
					
					// If this fadeout was called when switching to a new song, call the PlayMusic function again.
					if(waitingToRestart)
					{
						PlayMusic(restartMusicName);
						waitingToRestart = false;
					}
				}
			}
			else
			{
				// If neither are playing, assume we shouldn't be doing anything.
				print("Music Player Warning: Something's weird, and isPlaying is set while no audio is actually playing.");
			}
		}
		else
			print("Music Player Message: There is no music to fade out");
	}
	
	public IEnumerator ChangePitch(float pitchLength, float newPitch)
	{
		if(isPlaying == true)
		{
			float startPitch = 1;
			
			if(musicSources[0].isPlaying == true)
			{
				// If the intro is still playing, get the volume of the intro's Audio Source.
				startPitch = musicSources[0].pitch;
				
				// Fade the volume to the target volume. Fade both Audio Sources, in case the intro finishes before we fade out.
				while(musicSources[0].pitch > newPitch)
				{
					musicSources[0].pitch -= startPitch * Time.deltaTime / pitchLength;
					musicSources[1].pitch -= startPitch * Time.deltaTime / pitchLength;
					yield return null;
				}
				
			}
			else if(musicSources[1].isPlaying == true)
			{
				// If the loop playing, get the volume of the loop's Audio Source.
				startPitch = musicSources[1].pitch;
				
				// Fade the volume to the target volume.
				while(musicSources[1].pitch > newPitch)
				{
					musicSources[1].pitch -= startPitch * Time.deltaTime / pitchLength;
					yield return null;
				}
			}
		}
		else
			print("Music Player Message: There is no music to change pitch.");
	}
}
