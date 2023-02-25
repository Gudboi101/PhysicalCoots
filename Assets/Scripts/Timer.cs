using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameHandler gameHandler;
    [SerializeField] private Slider slider;

    public float CurrentTime;
    public float TimeReduction;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();

        slider.maxValue = 100;
        CurrentTime = 100;
        slider.value = CurrentTime;

        //According to Level
        TimeReduction = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameHandler.GameHasStarted == true)
        {
            if (CurrentTime > 0)
            {
                CurrentTime -= TimeReduction * Time.deltaTime;
            }
            else if (CurrentTime <= 0)
            {
                CurrentTime = 0;
            }

            slider.value = CurrentTime;
        }
    }
}
