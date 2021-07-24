using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public bool test;

	public List<Vector3Int> path;

	SpriteRenderer SR;

	Transform jumper;

	TileLogic currentTile;

	const float MoveSpeed = 0.5f;
	const float JumpHigh = 0.5f;

	void Awake() {
		jumper = transform.Find("Jumper");

		SR = GetComponentInChildren<SpriteRenderer>();
	}

	void Update() {
		if (test) {
			test = false;

			StopAllCoroutines();

			StartCoroutine(Move());
		}
	}

	IEnumerator Move() {
		currentTile = Board.GetTile(path[0]);

		transform.position = currentTile.worldPos;

		for (int i = 1; i < path.Count; i++) {
			TileLogic to = Board.GetTile(path[i]);

			if (to == null) {
				continue;
			}

			currentTile.content = null;

			if (currentTile.floor != to.floor) {
				yield return StartCoroutine(Jump(to));
			} else {
				yield return StartCoroutine(Walk(to));
			}
		}
	}

	IEnumerator Walk(TileLogic to) {
		int id = LeanTween.move(transform.gameObject, to.worldPos, MoveSpeed).id;

		currentTile = to;

		yield return new WaitForSeconds(MoveSpeed * 0.5f);

		SR.sortingOrder = to.contentOrder;

		while(LeanTween.descr(id) != null) {
			yield return null;
		}

		to.content = this.gameObject;
	}

	IEnumerator Jump(TileLogic to) {
		int id1 = LeanTween.move(transform.gameObject, to.worldPos, MoveSpeed).id;

		LeanTween.moveLocalY(jumper.gameObject, JumpHigh, MoveSpeed * 0.5f)
			.setLoopPingPong(1)
			.setEase(LeanTweenType.easeInOutQuad);

		float timeOrderUpdate = MoveSpeed;

		if (currentTile.floor.tilemap.tileAnchor.y > to.floor.tilemap.tileAnchor.y) {
			timeOrderUpdate *= 0.85f;
		} else {
			timeOrderUpdate *= 0.2f;
		}

		yield return new WaitForSeconds(timeOrderUpdate);

		currentTile = to;

		SR.sortingOrder = to.contentOrder;

		while (LeanTween.descr(id1) != null) {
			yield return null;
		}

		to.content = this.gameObject;
	}
}
