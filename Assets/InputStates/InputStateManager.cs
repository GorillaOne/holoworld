using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Holoworld.InputStates
{
	public class InputStateManager
	{
		public static InputStateManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new InputStateManager(); 
				}
				return instance; 
			}
		}
		static InputStateManager instance; 

		public InputState CurrentUserInteractionState { get; private set; }

		public void Setup()
		{

		}
		public void Update()
		{
			if (CurrentUserInteractionState != null)
				CurrentUserInteractionState.Update();
		}
		public void Teardown()
		{
			CurrentUserInteractionState.TearDown();
			CurrentUserInteractionState = null; 
		}

		public void LoadUserInteractionState(InputState newState, params object[] optionalParams)
		{
			if (CurrentUserInteractionState != null)
				CurrentUserInteractionState.TearDown();

			CurrentUserInteractionState = newState;
			CurrentUserInteractionState.Setup();
		}
		public void ClearUserInteractionState()
		{

		}
	}
}
