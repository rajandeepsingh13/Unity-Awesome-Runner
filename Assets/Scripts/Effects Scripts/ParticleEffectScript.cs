using UnityEngine;
using System.Collections;

public class ParticleEffectScript : MonoBehaviour {

	public float timer = 1.5f;

	void Start(){
		StartCoroutine (StopEffect());
	}

	IEnumerator StopEffect(){
		yield return new WaitForSeconds (timer);
		gameObject.SetActive (false);
	}
}































