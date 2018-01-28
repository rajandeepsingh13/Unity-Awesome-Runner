using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour {

    [SerializeField]
    private Transform platform, platform_parent;

    [SerializeField]
    private Transform monster, monster_parent;

    [SerializeField]
    private Transform health_Collectable, healthCollectable_parent;

    [SerializeField]
    private int levelLength = 100;

    [SerializeField]
    private float distance_between_platforms = 15f;

    [SerializeField]
    private float MIN_position_Y = 0f, MAX_position_Y = 7f;

    [SerializeField]
    private float chanceForMonsterExistence = 0.25f, chanceForHealthCollectableExistence=0.1f;

    [SerializeField]
    private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;

    private float platformLastPositionX;
    private Transform[] platform_Array;

    void Start () {
        CreatePlatforms();
	}

    void CreatePlatforms()
    {
        platform_Array = new Transform[levelLength];

        for (int i=0;i<platform_Array.Length;i++)
        {
            Transform newPlaform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
            platform_Array[i] = newPlaform;
        }

        for (int i = 0; i < platform_Array.Length; i++)
        {
            float platformPositionY = Random.Range(MIN_position_Y, MAX_position_Y);

            Vector3 platformPosition;

            if (i < 2)
            {
                platformPositionY = 0f;
            }

            platformPosition = new Vector3(distance_between_platforms * i, platformPositionY, 0);

            platformLastPositionX = platformPosition.x;

            platform_Array[i].position = platformPosition;
            platform_Array[i].parent = platform_parent;

            //spawn monsters and health
            SpawnHealthAndMonster(platformPosition, i, true);
        }

    }

    public void PoolingPlatforms()
    {
        for (int i = 0; i < platform_Array.Length; i++)
        {
            if (!platform_Array[i].gameObject.activeInHierarchy)
            {
                platform_Array[i].gameObject.SetActive(true);
                float platformPositionY = Random.Range(MIN_position_Y, MAX_position_Y);
                Vector3 platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, platformPositionY, 0);
                platform_Array[i].position = platformPosition;
                platformLastPositionX = platformPosition.x;

                // spawn health and monsters
                SpawnHealthAndMonster(platformPosition, i, false);
            }
        }
    }

    void SpawnHealthAndMonster(Vector3 platformPosition, int i, bool gameStarted)
    {
        if (i > 2)
        {
            if (Random.Range(0f, 1f) < chanceForMonsterExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_platforms * i, platformPosition.y + 0.1f, 0f);
                }
                else
                {
                    platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, platformPosition.y + 0.1f, 0f);
                }
                Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0));
                createMonster.parent = monster_parent;
            }

            if (Random.Range(0f, 1f) < chanceForHealthCollectableExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_platforms * i, platformPosition.y + Random.Range(healthCollectable_MinY,healthCollectable_MaxY), 0f);
                }
                else
                {
                    platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, platformPosition.y + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0f);
                }
                Transform createHealthCollectable = (Transform)Instantiate(health_Collectable, platformPosition, Quaternion.identity);
                createHealthCollectable.parent = healthCollectable_parent;
            }
        }
    }
	
	
}
