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
	private AudioClip currentSongPlaying;

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
	}

	public void DioramaSetupInputState_OnTap(InteractionSourceKind source, int tapCount, UnityEngine.Ray headRay)
	{
		instructionText.text = "Say 'Just Right' when the diorama is positioned how you want it.";

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

	public void DioramaPositionFinalized()
	{
		instructionText.gameObject.SetActive(false); 
		InputStateManager.Instance.LoadUserInteractionState(new ViewerInputState());
	}


}
