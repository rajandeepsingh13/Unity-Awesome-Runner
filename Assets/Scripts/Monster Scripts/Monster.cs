using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public GameObject monsterDiedEffect;
    public Transform bullet;
    public float distanceFromPlayerToStartMove = 20f;
    public float movementSpeed_Min = 1f;
    public float movementSpeed_Max = 2f;

    private bool moveRight;
    private float movementSpeed;
    private bool isPlayerInRegion;
    private string FUNCTION_TO_INVOKE = "StartShooting";

    private Transform playerTransform;

    public bool canShoot;

	void Start () {
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }

        movementSpeed = Random.Range(movementSpeed_Min, movementSpeed_Max);

        playerTransform = GameObject.FindGameObjectWithTag(Tags.Player_Tag).transform;
	}

	void Update () {
        if (playerTransform)
        {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
            if (distanceFromPlayer < distanceFromPlayerToStartMove)
            {
                if (moveRight)
                {
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);
                }
                if (!isPlayerInRegion)
                {
                    if (canShoot)
                    {
                        InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1.5f);
                    }
                    isPlayerInRegion = true;
                }
            }
            else
            {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }
        }
	}

    void StartShooting()
    {
        if (playerTransform)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x -= 1f;
            Transform newBullet = (Transform)Instantiate(bullet, bulletPos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;

        }
    }

    void MonsterDied()
    {
        Vector3 effectPos = transform.position;
        effectPos.y += 2f;
        Instantiate(monsterDiedEffect, effectPos, Quaternion.identity);
        gameObject.SetActive(false);
        //Destroy(gameObject) can also be used. Destroy completely removes the game onject from the hierarchy
        //But it is a more expensive operation so not a good idea for mombile games
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.PlayerBullet_Tag)
        {
            GameplayController.instance.IncrementScore(200);
            MonsterDied();
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.Player_Tag)
        {
            MonsterDied();
        }
    }
}
