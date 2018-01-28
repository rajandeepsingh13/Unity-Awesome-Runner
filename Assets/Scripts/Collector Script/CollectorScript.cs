using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour {

	void OnTriggerEnter(Collider target){
        if (target.tag == Tags.Platform_Tag || target.tag==Tags.Health_Tag || target.tag==Tags.Monster_Tag || target.tag==Tags.MonsterBullet_Tag)
        {
            target.gameObject.SetActive(false);
        }
	}
}
