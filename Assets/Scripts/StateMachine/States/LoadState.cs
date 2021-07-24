using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : State {
	public override void Enter() {
	 	StartCoroutine(LoadSequence());
	}

	IEnumerator LoadSequence() {
		yield return StartCoroutine(Board.instance.InitSequence(this));

		// Do Some Things

		yield return null;

		// StateMachineController.instance.ChangeTo<>();
	}
}
