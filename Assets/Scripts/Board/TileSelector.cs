using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour {
	public static TileSelector instance;

	public Vector3Int position;

	public TileLogic tile;

	[HideInInspector]
	public SpriteRenderer spriteRenderer;

	void Awake() {
		instance = this;

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}
}
