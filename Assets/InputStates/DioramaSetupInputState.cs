using HoloToolkit.Unity.InputModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

namespace Holoworld.InputStates
{
	public class DioramaSetupInputState : InputState
	{
		const string justRightKey = "Just Right"; 

		GestureRecognizer gestureRecognizer;
		GameSceneController controller;
		KeywordRecognizer keywordRecognizer; 


		public DioramaSetupInputState(GameSceneController controller)
		{
			this.controller = controller;
		}

		public override void Setup()
		{
			gestureRecognizer = new GestureRecognizer();
			gestureRecognizer.TappedEvent += controller.DioramaSetupInputState_OnTap;
			gestureRecognizer.StartCapturingGestures();

			keywordRecognizer = new KeywordRecognizer(new string[] { justRightKey });
			keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
			keywordRecognizer.Start(); 
		}

		public override void Update()
		{
			
		}
		public override void TearDown()
		{
			gestureRecognizer.StopCapturingGestures();
			keywordRecognizer.Stop(); 
		}

		private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
		{
			if (args.text == justRightKey)
			{
				controller.DioramaPositionFinalized(); 
			}
		}
	}
}
