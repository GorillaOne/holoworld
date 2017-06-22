using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Holoworld.InputStates
{
	public enum InputStateType
	{
		MainMenu,
		GameScene,
	}

	public abstract class InputState
	{
		public abstract void Setup();
		public abstract void Update();
		public abstract void TearDown();
	}
}
