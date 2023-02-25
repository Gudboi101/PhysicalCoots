using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhysicalMeter : MonoBehaviour
{
    [SerializeField] private float PhysicalLevel;
    private ScoreSystemHandler scoreSystemHandler;
    [SerializeField] private Slider slider;
    [SerializeField]private Image[] Meters;
    public float TargetValue;

    private Color Difficulty0Color;
    private Color Difficulty1Color;
    private Color Difficulty2Color;
    private Color Difficulty3Color;

    [SerializeField] private TextMeshProUGUI PhysicalDifficultyText;

    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#718b3e", out Difficulty0Color);
        ColorUtility.TryParseHtmlString("#e0c863", out Difficulty1Color);
        ColorUtility.TryParseHtmlString("#D28964", out Difficulty2Color);
        ColorUtility.TryParseHtmlString("#d25a5a", out Difficulty3Color);

        PhysicalLevel = PlayerPrefs.GetFloat("Physical Level", 0);
        scoreSystemHandler = FindObjectOfType<ScoreSystemHandler>();

        slider.maxValue = 100;
        slider.value = PhysicalLevel;

        PhysicalDifficultyText.text = "Physical Difficulty: " + ((int)PhysicalLevel).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhysicalLevel >= 98)
        {
            PhysicalDifficultyText.text = "Physical Difficulty: " + ((int)PhysicalLevel + 2).ToString();
        }
        else
        {
            PhysicalDifficultyText.text = "Physical Difficulty: " + ((int)PhysicalLevel).ToString();
        }


        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            TargetValue = 30;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            TargetValue = 50;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            TargetValue = 75;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            TargetValue = 98;
        }

        if (PhysicalLevel < TargetValue)
        {
            PhysicalLevel += 20 * Time.deltaTime;
            slider.value = PhysicalLevel;
        }
        else if (PhysicalLevel >= TargetValue)
        {
            PhysicalLevel = TargetValue;
            PlayerPrefs.SetFloat("Physical Level", PhysicalLevel);
            slider.value = PhysicalLevel;
        }

        if (PhysicalLevel >= 0 && PhysicalLevel <= 30)
        {
            Meters[0].color = Difficulty0Color;
            PhysicalDifficultyText.color = Difficulty0Color;
            //Meters[1].color = Difficulty0Color;
        }
        else if (PhysicalLevel > 30 && PhysicalLevel <= 50)
        {
            Meters[0].color = Difficulty1Color;
            PhysicalDifficultyText.color = Difficulty1Color;
            //Meters[1].color = Difficulty1Color;
        }
        else if (PhysicalLevel > 50 && PhysicalLevel <= 75)
        {
            Meters[0].color = Difficulty2Color;
            PhysicalDifficultyText.color = Difficulty2Color;
            //Meters[1].color = Difficulty2Color;
        }
        else if (PhysicalLevel > 75 && PhysicalLevel <= 100)
        {
            Meters[0].color = Difficulty3Color;
            PhysicalDifficultyText.color = Difficulty3Color;
            //Meters[1].color = Difficulty3Color;
        }
    }
}
