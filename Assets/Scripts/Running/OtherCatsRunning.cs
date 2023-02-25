using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCatsRunning : MonoBehaviour
{
    private float LeapSpeed = 0.2f;
    //Depending on difficulty
    [SerializeField] private float PressingSpeed;

    private float PressingNumber;

    private int RunningNumber = 0;

    private Animator animator;

    private SpriteRenderer sr;
    [SerializeField] private Sprite[] RunningSprites;

    private GameHandler gameHandlerScript;

    private bool isRunning = false;

    [SerializeField] private GameObject Flag;

    // Start is called before the first frame update
    void Start()
    {
        gameHandlerScript = FindObjectOfType<GameHandler>();

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.Play("Idle");

        SettingDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            animator.enabled = false;

            if (isRunning == false && PressingNumber > 0)
            {
                PressingNumber -= PressingSpeed * Time.deltaTime;
            }
            else if (PressingNumber <= 0)
            {
                PressingNumber = 0;
                isRunning = true;
            }

            if (isRunning == true)
            {
                PressingNumber = 1;
                isRunning = false;
                if (RunningNumber < RunningSprites.Length - 1)
                {
                    RunningNumber += 1;
                }
                else
                {
                    RunningNumber = 0;
                }
                sr.sprite = RunningSprites[RunningNumber];
                transform.position = new Vector2(transform.position.x + LeapSpeed, transform.position.y);
            }

            //Coots Lost
            if (transform.position.x >= 6.93f)
            {
                animator.enabled = true;
                gameHandlerScript.GameHasEnded = true;
                gameHandlerScript.LostTheLevel = true;
                animator.Play("Win");
                Flag.SetActive(false);
            }
        }

        if (gameHandlerScript.WonTheLevel == true || gameHandlerScript.GameHasEnded == true && transform.position.x < 6.93f)
        {
            animator.enabled = true;
            animator.Play("Lose");
        }
    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            PressingSpeed = Random.Range(4, 7);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            PressingSpeed = Random.Range(6, 8);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            PressingSpeed = Random.Range(7, 9);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            PressingSpeed = Random.Range(9, 12);
        }
    }
}
