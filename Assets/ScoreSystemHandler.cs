using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreSystemHandler : MonoBehaviour
{
    [SerializeField] private Button NextButton;
    [SerializeField] private Button HomeButton;
    //Score Related
    [SerializeField] private float PointsAdded;
    [SerializeField] public float PointsOnPreviousLevel;
    [SerializeField]private float HighScorePoints;

    [SerializeField] private TextMeshPro HighScoreText;
    [SerializeField] private TextMeshPro Scoretext;

    //Life Related
    [SerializeField] private bool WonThePreviousLevel;
    [SerializeField] private int LivesLeft;
    [SerializeField] private GameObject[] Alive;
    [SerializeField] private GameObject[] JustDied;
    [SerializeField] private GameObject[] BeenDead;

    //Difficulty Related
    private int DifficultyLevel;

    //Sound
    private bool SoundHasPlayed = false;
    [SerializeField] private GameObject BustBreakingSound;
    [SerializeField] private GameObject AddingPointsSound;

    // Start is called before the first frame update
    void Start()
    {
        BustBreakingSound = GameObject.FindGameObjectWithTag("Bust Breaking");
        AddingPointsSound = GameObject.FindGameObjectWithTag("Adding Points Sound");

        NextButton.interactable = false;
        HomeButton.interactable = false;

        ScoreRelatedStart();
        LifeRelatedStart();
        DifficultyLevelStart();

        

    }

    // Update is called once per frame
    void Update()
    {
        //Score Related
        Scoretext.text = "Score: " + ((int)PointsOnPreviousLevel).ToString();
        HighScoreText.text = "High Score: " + ((int)HighScorePoints).ToString();

        SettingDifficultyLevel();

        if (WonThePreviousLevel == true)
        {
            
            //Add Points
            if (PointsAdded > 0)
            {
                if (AddingPointsSound.GetComponent<AudioSource>().isPlaying == false)
                {
                    AddingPointsSound.GetComponent<AudioSource>().Play();
                }
                //Adding one to current score every second
                PointsOnPreviousLevel += (20 * Time.deltaTime);
                
                //Removing one from points added
                PointsAdded -= (20 * Time.deltaTime);

                if (PointsOnPreviousLevel >= HighScorePoints) 
                {
                    HighScorePoints = PointsOnPreviousLevel;
                }

                //Can't Press Buttons
            }
            else if (PointsAdded <= 0)
            {
                PointsAdded = 0;
                NextButton.interactable = true;
                HomeButton.interactable = true;
                PlayerPrefs.SetFloat("High Score", HighScorePoints);
                PlayerPrefs.SetFloat("Current Score", PointsOnPreviousLevel);
                PlayerPrefs.SetFloat("Points Added", PointsAdded);
            } 
        }
    }

    private void ScoreRelatedStart()
    {
        //Points Before the addition of the winnings from previous level
        PointsOnPreviousLevel = PlayerPrefs.GetFloat("Current Score", 0);
        PointsAdded = PlayerPrefs.GetFloat("Points Added", 0);
        HighScorePoints = PlayerPrefs.GetFloat("High Score", 0);

        Scoretext.text = "Score: " + ((int)PointsOnPreviousLevel).ToString();
        HighScoreText.text = "High Score: " + ((int)HighScorePoints).ToString();

    }

    private void LifeRelatedStart()
    {
        TurnOfAllCats();

        LivesLeft = PlayerPrefs.GetInt("Lives Left", 3);
        if (PlayerPrefs.GetInt("Win or Lose", 0) == 0)
        {
            //Won the previous level
            WonThePreviousLevel = true;
            IfWonTheLevel();
        }
        else
        {
            WonThePreviousLevel = false;
            IfLostTheLevel();
            NextButton.interactable = true;
            HomeButton.interactable = true;
        }
    }

    private void TurnOfAllCats()
    {
        Alive[0].SetActive(false);
        Alive[1].SetActive(false);
        Alive[2].SetActive(false);
        JustDied[0].SetActive(false);
        JustDied[1].SetActive(false);
        JustDied[2].SetActive(false);
        BeenDead[0].SetActive(false);
        BeenDead[1].SetActive(false);
        BeenDead[2].SetActive(false);
    }

    private void IfWonTheLevel()
    {
        switch(LivesLeft)
        {
            case 1:
                BeenDead[0].SetActive(true);
                BeenDead[1].SetActive(true);
                Alive[2].SetActive(true);
                break;
            case 2:
                BeenDead[0].SetActive(true);
                Alive[1].SetActive(true);
                Alive[2].SetActive(true);
                break;
            case 3:
                Alive[0].SetActive(true);
                Alive[1].SetActive(true);
                Alive[2].SetActive(true);
                break;
        }
    }

    private void IfLostTheLevel()
    {
        if (SoundHasPlayed == false)
        {
            SoundHasPlayed = true;
            BustBreakingSound.GetComponent<AudioSource>().Play();
        }

        switch (LivesLeft)
        {
            case 0:
                BeenDead[0].SetActive(true);
                BeenDead[1].SetActive(true);
                JustDied[2].SetActive(true);
                //You've lost
                break;
            case 1:
                BeenDead[0].SetActive(true);
                JustDied[1].SetActive(true);
                Alive[2].SetActive(true);
                break;
            case 2:
                JustDied[0].SetActive(true);
                Alive[1].SetActive(true);
                Alive[2].SetActive(true);
                break;
        }
    }

    private void DifficultyLevelStart()
    {
        //0, 1, 2, 3
        DifficultyLevel = PlayerPrefs.GetInt("Difficulty", 0);
    }

    private void SettingDifficultyLevel()
    {
        if (PlayerPrefs.GetFloat("Current Score", 0) <= 30f)
        {
            DifficultyLevel = 0;
        }
        else if (PlayerPrefs.GetFloat("Current Score", 0) > 30f && PlayerPrefs.GetFloat("Current Score", 0) <= 60)
        {
            DifficultyLevel = 1;
        }
        else if (PlayerPrefs.GetFloat("Current Score", 0) > 60f && PlayerPrefs.GetFloat("Current Score", 0) <= 90)
        {
            DifficultyLevel = 2;
        }
        else if (PlayerPrefs.GetFloat("Current Score", 0) > 90)
        {
            DifficultyLevel = 3;
        }

        PlayerPrefs.SetInt("Difficulty", DifficultyLevel);

    }
}
