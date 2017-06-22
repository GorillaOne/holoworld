using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PeriodicSoundEffect : MonoBehaviour {

	public List<AudioClip> audioClips;
	public int minSecondsToWait = 3;
	public int maxSecondsToWait = 10; 

	AudioSource source;
	bool playingEffects = false; 

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playingEffects == false)
		{
			StartCoroutine(PlayRandomAudio()); 
		}
	}

	private IEnumerator PlayRandomAudio()
	{
		playingEffects = true;
		System.Random randomClassInstance = new System.Random(); //This instantiates a new instance of the Random object so we can get random numbers. 
		int randomSecondsToWait = randomClassInstance.Next(minSecondsToWait, maxSecondsToWait);

		yield return new WaitForSeconds(randomSecondsToWait);

		int numberOfSongs = audioClips.Count; //Gets the number of songs we have in the list. 
		int songIndex = randomClassInstance.Next(numberOfSongs); //This returns in integer between 0 and the maximum number of songs we have. 
																					//This gets the song using that index and plays it. 
		source.clip = audioClips[songIndex];
		source.Play();

		yield return new WaitForEndOfFrame();
		yield return new WaitUntil(() => source.isPlaying == false); 

		playingEffects = false;
	}
}
