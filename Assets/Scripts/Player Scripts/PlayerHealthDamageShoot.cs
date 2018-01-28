using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealthDamageShoot : MonoBehaviour {

    [SerializeField]
    private Transform playerBullet;

	private float distanceBeforeNewPlatform = 120f;

	private LevelGenerator levelGenerator;
    private LevelGeneratorPooling levelGenerator_pooling;

    [HideInInspector]
    public bool canShoot;

    private Button shootBtn;

	void Awake () {
		levelGenerator = GameObject.Find (Tags.LevelGenerator_ObjTag).GetComponent<LevelGenerator> ();
        levelGenerator_pooling = GameObject.Find(Tags.LevelGenerator_ObjTag).GetComponent<LevelGeneratorPooling>();

        shootBtn = GameObject.Find(Tags.Shoot_Button_Obj).GetComponent<Button>();
        shootBtn.onClick.AddListener(() => Shoot());//Same as dragging and dropping that we use normally
    }

	void FixedUpdate () {
        Fire();
	}

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canShoot)
            {
                Vector3 bulletPos = transform.position;
                bulletPos.y += 1.5f;
                bulletPos.x += 1f;
                Transform newBullet = (Transform)(Instantiate(playerBullet, bulletPos, Quaternion.identity));
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
                newBullet.parent = transform;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x += 1f;
            Transform newBullet = (Transform)(Instantiate(playerBullet, bulletPos, Quaternion.identity));
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }
    }

	void OnTriggerEnter(Collider target){

        if (target.tag == Tags.MonsterBullet_Tag || target.tag==Tags.Bounds_Tag)
        {
            GameplayController.instance.TakeDamage();
            Destroy(gameObject);
        }

        if (target.tag == Tags.Health_Tag)
        {
            GameplayController.instance.IncrementHealth();
            target.gameObject.SetActive(false);
        }
		if (target.tag == Tags.MorePlatforms_Tag) {
			Vector3 temp = target.transform.position;
			temp.x += distanceBeforeNewPlatform;
			target.transform.position = temp;

            //levelGenerator.GenerateLevel (false);
            levelGenerator_pooling.PoolingPlatforms();

		}
	}

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.Monster_Tag)
        {
            GameplayController.instance.TakeDamage();
            Destroy(gameObject);
        }
    }
}





























