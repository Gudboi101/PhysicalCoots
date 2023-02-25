using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreenButtons : MonoBehaviour
{
    private int PreviousLevel;
    private int Randomizer;
    private PhysicalMeter physicalMeter;

    private void Start()
    {
        PreviousLevel = PlayerPrefs.GetInt("Previous Level", 1);
        Randomizer = Random.Range(1, 4);

        physicalMeter = FindObjectOfType<PhysicalMeter>();
    }


    public void OnPressNextButton()
    {
        if (PlayerPrefs.GetInt("Lives Left", 3) != 0)
        {
            //Still Alive
            LoadNextSceneRandomizer();
            PlayerPrefs.SetFloat("Physical Level", physicalMeter.TargetValue);
        }
        else
        {
            SceneManager.LoadScene("Main Menu"); 
           
        }
    }

    public void OnPressHomeButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void LoadNextSceneRandomizer()
    {
        switch(PreviousLevel)
        {
            case 1:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Push Up");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Rock Carry");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Running");
                }
                break;
            case 2:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Push Up");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Rock Carry");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Running");
                }
                break;
            case 3:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Push Up");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Rock Carry");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Running");
                }
                break;
            case 4:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Climbing");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Hanging");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Pulling Boat");
                }
                break;
            case 5:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Climbing");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Hanging");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Pulling Boat");
                }
                break;
            case 6:
                if (Randomizer == 1)
                {
                    SceneManager.LoadScene("Climbing");
                }
                else if (Randomizer == 2)
                {
                    SceneManager.LoadScene("Hanging");
                }
                else if (Randomizer == 3)
                {
                    SceneManager.LoadScene("Pulling Boat");
                }
                break;
        }
    }
}
