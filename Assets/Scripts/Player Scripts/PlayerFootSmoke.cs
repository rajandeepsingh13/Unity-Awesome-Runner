using UnityEngine;
using System.Collections;

public class PlayerFootSmoke : MonoBehaviour {

	public GameObject smokeEffect;
	public GameObject smokePosition;

	void OnTriggerEnter(Collider target){
		if (target.tag == Tags.Platform_Tag) {
			if (smokePosition.activeInHierarchy) {
				Instantiate (smokeEffect, smokePosition.transform.position, Quaternion.identity);
			}
		}
	}


}




























