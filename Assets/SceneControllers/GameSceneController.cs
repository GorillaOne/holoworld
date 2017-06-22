using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;

using UnityEngine;
using Holoworld.InputStates;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;
using System;

public class GameSceneController : MonoBehaviour
{

	public Text instructionText;
	public GameObject dioramaFramingBox;
	public List<AudioSource> myAudioSources;
	private AudioSource currentSongPlaying;

	bool playingEffects = false;  

	// Use this for initialization
	void Start()
	{
		var startingState = new DioramaSetupInputState(this);
		InputStateManager.Instance.LoadUserInteractionState(startingState);
	}

	// Update is called once per frame
	void Update()
	{
		InputStateManager.Instance.Update();
		if (playingEffects == false)
		{
			playingEffects = true;
			StartCoroutine(PlayRandomAudio()); 
		}
	}

	public void DioramaSetupInputState_OnTap(InteractionSourceKind source, int tapCount, UnityEngine.Ray headRay)
	{
		instructionText.text = "You tapped! OH SNAP!";

		// Do a raycast into the world based on the user's
		// head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		Physics.Raycast(headPosition, gazeDirection, out hitInfo);

		Vector3 movement = gazeDirection * hitInfo.distance;
		var newPosition = headPosition + movement;
		
		PositionDiorama(newPosition, hitInfo.normal);	
	}

	private void PositionDiorama(Vector3 newPosition, Vector3 surfaceNormal)
	{
		dioramaFramingBox.SetActive(true); 
		dioramaFramingBox.transform.position = newPosition;
		dioramaFramingBox.transform.forward = surfaceNormal; 
	}

	private IEnumerator PlayRandomAudio()
	{
		System.Random randomClassInstance = new System.Random(); //This instantiates a new instance of the Random object so we can get random numbers. 
		int randomSecondsToWait = randomClassInstance.Next(3, 6); 

		yield return new WaitForSeconds(randomSecondsToWait); 

		int numberOfSongs = myAudioSources.Count; //Gets the number of songs we have in the list. 
		int songIndex = randomClassInstance.Next(numberOfSongs); //This returns in integer between 0 and the maximum number of songs we have. 
		currentSongPlaying = myAudioSources[songIndex];		
		currentSongPlaying.Play(); //This gets the song using that index and plays it. 

		yield return currentSongPlaying.isPlaying == true;
		playingEffects = false; 
	}
}
