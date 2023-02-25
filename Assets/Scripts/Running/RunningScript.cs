using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningScript : MonoBehaviour
{
    private GameHandler gameHandlerScript;

    [SerializeField] private int RunState;

    private float TimeBeforeStand;
    [SerializeField] private float TimeBeforeStandReduction = 5f;

    private SpriteRenderer sr;
    private Animator animator;
    [SerializeField] private Sprite[] RunningSprites;

    [SerializeField] private float LeapSpeed;

    [SerializeField] private GameObject Flag;

    //Sound
    private GameObject YaySound;
    private GameObject SadSound;
    private bool SadSoundHasPlayed = false;
    private bool YaySoundHasPlayed = false;
    private GameObject StepSound;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameHandlerScript = FindObjectOfType<GameHandler>();

        RunState = 0;

        animator.enabled = true;
        animator.Play("Idle");

        YaySound = GameObject.FindGameObjectWithTag("Yay Sound");
        SadSound = GameObject.FindGameObjectWithTag("Sad Sound");
        StepSound = GameObject.FindGameObjectWithTag("Step Sound"); //0, 3, 5, 7
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            TimeBeforeStand -= TimeBeforeStandReduction * Time.deltaTime;

            if (TimeBeforeStand > 0)
            {
                animator.enabled = false;
                sr.sprite = RunningSprites[RunState];
            }
            else if (TimeBeforeStand <= 0)
            {
                TimeBeforeStand = 0;
                animator.enabled = true;
                animator.Play("Idle");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TimeBeforeStand = 1f;
                transform.position = new Vector2(transform.position.x + LeapSpeed, transform.position.y);
                if (RunState >= RunningSprites.Length - 1)
                {
                    RunState = 0;
                }
                else if (RunState < RunningSprites.Length - 1)
                {
                    RunState += 1;
                    if (RunState == 0 || RunState == 4)
                    {
                        StepSound.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }

        //Won
        if (transform.position.x >= 6.93f)
        {
            transform.position = new Vector2(6.93f, transform.position.y);
            Flag.SetActive(false);
            animator.enabled = true;
            animator.Play("Win");
            gameHandlerScript.GameHasEnded = true;
            gameHandlerScript.WonTheLevel = true;
            if (YaySoundHasPlayed == false)
            {
                YaySound.GetComponent<AudioSource>().Play();
                YaySoundHasPlayed = true;
            }
           
        }

        //Lost
        if (gameHandlerScript.LostTheLevel == true)
        {
            animator.enabled = true;
            animator.Play("Lose");
            if (SadSoundHasPlayed == false)
            {
                SadSound.GetComponent<AudioSource>().Play();
                SadSoundHasPlayed = true;
            }
        }
    }
}
