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
	public GameObject referenceSphere;

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
		instructionText.text = "You tapped! OH SNAP!";
		referenceSphere.transform.position = headRay.origin;

		// Do a raycast into the world based on the user's
		// head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		Physics.Raycast(headPosition, gazeDirection, out hitInfo);

		Vector3 movement = gazeDirection * hitInfo.distance;
		var newPosition = headPosition + movement;
		PositionDiorama(newPosition);
		
	}

	private void PositionDiorama(Vector3 newPosition)
	{
		referenceSphere.transform.position = newPosition; 
	}
}
