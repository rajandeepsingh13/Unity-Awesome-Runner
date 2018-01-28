using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedPlayerDied;

    [HideInInspector]
    public float score, health, level;

    [HideInInspector]
    public bool canPlayMusic=true;

    void Awake () {
        MakeSingleton();
	}
	
	void MakeSingleton () {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
