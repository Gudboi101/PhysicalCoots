using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OtherCatsPushUp : MonoBehaviour
{
    [SerializeField] private float PushUpSpeed;
    private float PushUpTimer = 1;
    private float GoUpPushUpTimer = 1;
    private GameHandler gameHandlerScript;
    private bool IsDoingPushUp;
    private SpriteRenderer sr;
    public int PushUpCount = 0;
    [SerializeField] private Sprite[] PushUpSprites;
    [SerializeField] private GameObject Tail;
    [SerializeField] private TextMeshPro ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameHandlerScript = FindObjectOfType<GameHandler>();
        IsDoingPushUp = false;

        ScoreText.text = PushUpCount.ToString();
        Tail.transform.localPosition = new Vector2 (0.0499f, -0.0015f);
        SettingDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasStarted == true && gameHandlerScript.GameHasEnded == false)
        {
            ScoreText.text = PushUpCount.ToString();

            if (IsDoingPushUp == false && PushUpTimer > 0)
            {
                PushUpTimer -= PushUpSpeed * Time.deltaTime;
                if (PushUpTimer <= 0)
                {
                    PushUpTimer = 0;
                    IsDoingPushUp = true;
                }
            }
            else if (IsDoingPushUp == true)
            {
                PushUpTimer = 0;
                DoPushUp();    
            }
        }
    }

    private void DoPushUp()
    {
        Tail.transform.localPosition = new Vector2(0.0499f, -0.01f);
        sr.sprite = PushUpSprites[1];
        if (GoUpPushUpTimer > 0)
        {
            GoUpPushUpTimer -= PushUpSpeed * Time.deltaTime;
        }
        else if (GoUpPushUpTimer <= 0)
        {
            GoUpPushUpTimer = 0;
        }

        if (GoUpPushUpTimer ==0)
        {
            if (gameHandlerScript.GameHasEnded == false)
            {
                PushUpTimer = 1;
                sr.sprite = PushUpSprites[0];
                Tail.transform.localPosition = new Vector2(0.0499f, -0.0015f);
                PushUpCount += 1;
                IsDoingPushUp = false;
                GoUpPushUpTimer = 1;
            }
        }

    }

    
    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            PushUpSpeed = Random.Range(4, 7);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            PushUpSpeed = Random.Range(6, 8);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            PushUpSpeed = Random.Range(7, 9);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            PushUpSpeed = Random.Range(9, 11);
        }
    }
}
