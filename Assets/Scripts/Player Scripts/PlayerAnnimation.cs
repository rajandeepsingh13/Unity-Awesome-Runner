using UnityEngine;
using System.Collections;

public class PlayerAnnimation : MonoBehaviour {

	private Animation anim;

	void Awake () {
		anim = GetComponent<Animation> ();
	}

	public void didJump(){
		anim.Play (Tags.animation_Jump);
		anim.PlayQueued (Tags.animation_Jump_Fall);
	}

	public void DidLand(){
		anim.Stop (Tags.animation_Jump_Fall);
		anim.Stop (Tags.animation_Jump_Land);
		anim.Blend (Tags.animation_Jump_Land, 0);
		anim.CrossFade (Tags.animation_Run);
	}

	public void PlayerRun(){
		anim.Play (Tags.animation_Run);
	}
}


























