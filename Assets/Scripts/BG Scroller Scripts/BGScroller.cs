using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public float offsetSpeed=-0.0006f;
	private Renderer myRenderer;

	[HideInInspector]
	public bool canScroll;

	void Awake () {
		myRenderer = GetComponent<MeshRenderer> ();
	}

	void Update () {
		if (canScroll) {
			myRenderer.material.mainTextureOffset -= new Vector2 (offsetSpeed, 0);
		}
	}
}
