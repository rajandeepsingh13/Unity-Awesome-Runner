using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollower : MonoBehaviour {
	private Transform playerTarget;
	public float offsetZ=-15f;
	public float offsetX=-5f;
	public float constantY=5f;
	public float cameraLerpTime=0.05f;

	// Use this for initialization
	void Awake () {
		playerTarget = GameObject.FindGameObjectWithTag (Tags.Player_Tag).transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playerTarget) {
			Vector3 TargetPosition = new Vector3 (playerTarget.position.x + offsetX, constantY, playerTarget.position.z + offsetZ);
			transform.position = Vector3.Lerp (transform.position, TargetPosition, cameraLerpTime);
			//Lerp basically goes from a to b in time t
		}
	}
}













