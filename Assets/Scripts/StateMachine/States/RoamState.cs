using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State {
	public override void Enter() {
		base.Enter();

		InputController.instance.OnMove += OnMove;

		CheckNullPosition();
	}

	public override void Exit() {
		base.Exit();

		InputController.instance.OnMove -= OnMove;
	}

	void OnMove(object sender, object args) {
		Vector3Int input = (Vector3Int)args;

		TileLogic t = Board.GetTile(TileSelector.instance.position + input);

		if (t != null) {
			TileSelector.instance.position = t.pos;
			TileSelector.instance.tile = t;
			TileSelector.instance.spriteRenderer.sortingOrder = t.contentOrder;
			TileSelector.instance.transform.position = t.worldPos;
		}
	}

	void CheckNullPosition() {
		TileLogic t = Board.GetTile(new Vector3Int(-6, -2, 0));

		TileSelector.instance.position = t.pos;
		TileSelector.instance.tile = t;
		TileSelector.instance.spriteRenderer.sortingOrder = t.contentOrder;
		TileSelector.instance.transform.position = t.worldPos;
	}
}
