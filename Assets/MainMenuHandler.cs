using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    private int SceneLoadRandomizer;

    // Start is called before the first frame update
    void Start()
    {
        //if not web
        Screen.SetResolution(1920, 1080, true);

        SceneLoadRandomizer = Random.Range(0, 6);
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("Current Score", 0);
        PlayerPrefs.SetInt("Difficulty", 0);
        PlayerPrefs.SetInt("Lives Left", 3);
        PlayerPrefs.SetFloat("Physical Level", 0);
    }

    public void PlayButton()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        switch(SceneLoadRandomizer)
        {
            case 0:
                SceneManager.LoadScene("Climbing");
                break;
            case 1:
                SceneManager.LoadScene("Hanging");
                break;
            case 2:
                SceneManager.LoadScene("Pulling Boat");
                break;
            case 3:
                SceneManager.LoadScene("Push Up");
                break;
            case 4:
                SceneManager.LoadScene("Rock Carry");
                break;
            case 5:
                SceneManager.LoadScene("Running");
                break;
        }
    }
}
