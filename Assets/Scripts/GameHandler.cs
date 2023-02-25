using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public bool GameHasStarted;
    public bool GameHasEnded;
    public bool WonTheLevel;
    public bool LostTheLevel;

    private int LivesLeft;

    private bool LoadedScoreScreen = false;
    private bool TookOutOneLife = false;

    private Timer timerScript;

    [SerializeField] private Animator InstructionsTextAnimator;

    // Start is called before the first frame update
    void Start()
    {

        KnowTheCurrentSceneNumber();
        
        LivesLeft =  PlayerPrefs.GetInt("Lives Left", 3);
        SetBoolsAtTheStart();
        Time.timeScale = 0;

        timerScript = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameStartMechanism();
    }

    void GameStartMechanism()
    {
        if (GameHasStarted == false && GameHasEnded == false)
        {
            if (UnityEngine.Input.anyKeyDown)
            {
                GameHasStarted = true;
                //Depends on Level
                Time.timeScale = 1;
                InstructionsTextAnimator.Play("Exit");
            }
        }

        if (GameHasEnded == true)
        {
            timerScript.TimeReduction = 0;

            if (WonTheLevel == true)
            {
                //Add points depends dapat on the scene
                PlayerPrefs.SetFloat("Points Added", Random.Range(10, 20));
                PlayerPrefs.SetInt("Win or Lose", 0);
            }
            else if (LostTheLevel == true && TookOutOneLife == false)
            {
                TookOutOneLife = true;
                //Take Out One life
                LivesLeft -= 1;
                PlayerPrefs.SetInt("Lives Left", LivesLeft);
                PlayerPrefs.SetInt("Win or Lose", 1);
            }

            if (LoadedScoreScreen == false)
            {
                LoadedScoreScreen = true;
                StartCoroutine(LoadScoreScreen());
            }
        }
    }

    void SetBoolsAtTheStart()
    {
        GameHasStarted = false;
        GameHasEnded = false;
        WonTheLevel = false;
        LostTheLevel = false;
    }

    private IEnumerator LoadScoreScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Score Screen");
    }

    private void KnowTheCurrentSceneNumber()
    {
        if (SceneManager.GetActiveScene().name == "Climbing")
        {
            PlayerPrefs.SetInt("Previous Level", 1);
        }
        else if (SceneManager.GetActiveScene().name == "Hanging")
        {
            PlayerPrefs.SetInt("Previous Level", 2);
        }
        else if (SceneManager.GetActiveScene().name == "Pulling Boat")
        {
            PlayerPrefs.SetInt("Previous Level", 3);
        }
        else if (SceneManager.GetActiveScene().name == "Push Up")
        {
            PlayerPrefs.SetInt("Previous Level", 4);
        }
        else if (SceneManager.GetActiveScene().name == "Rock Carry")
        {
            PlayerPrefs.SetInt("Previous Level", 5);
        }
        else if (SceneManager.GetActiveScene().name == "Running")
        {
            PlayerPrefs.SetInt("Previous Level", 6);
        }
    }
}
