using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float lifeTime = 3f;
    private float startY;

    void Awake()
    {
        startY = transform.position.y;
        StartCoroutine(TurnOffBullet ());
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    IEnumerator TurnOffBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.Monster_Tag || target.tag == Tags.Player_Tag || target.tag == Tags.PlayerBullet_Tag || target.tag == Tags.MonsterBullet_Tag)
        {
            gameObject.SetActive(false);

        }
    }
}
