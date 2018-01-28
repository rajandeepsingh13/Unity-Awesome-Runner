using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//To go from ne scene to the other
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite soundOff, soundOn;

    public void PlayGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        SceneManager.LoadScene(Tags.Gameplay_Scene);
    }

    public void ControlMusic()
    {
        if (GameManager.instance.canPlayMusic)
        {
            musicBtn.image.sprite = soundOff;
            GameManager.instance.canPlayMusic = false;
        }
        else
        {
            musicBtn.image.sprite = soundOn;
            GameManager.instance.canPlayMusic = true;
        }
    }
}
