using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

	private float collisionSoundEffect = 1f;

	public float audioFootVolume = 1f;
	public float soundEffectPitchRandomness=0.05f;

	private AudioSource audioSource;
	public AudioClip genericFootSound;
	public AudioClip metalFootSound;

	void Awake () {
		audioSource = GetComponent<AudioSource> ();
	}
	

	void FootSound () {
		audioSource.volume = collisionSoundEffect * audioFootVolume;
		audioSource.pitch = Random.Range (1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

		if (Random.Range (0, 2) > 0)
			audioSource.clip = genericFootSound;
		else
			audioSource.clip = metalFootSound;
		audioSource.Play ();
	}
}
