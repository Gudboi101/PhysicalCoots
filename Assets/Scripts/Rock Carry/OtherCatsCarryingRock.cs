using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCatsCarryingRock : MonoBehaviour
{
    private GameHandler gameHandlerScript;
    private Timer timer;

    private bool DroppedRock = false;
    private bool AnimationHasPlayed = false;

    [SerializeField] private float TimeToFall;

    [SerializeField] private float BalanceNumber;
    [SerializeField] private float BalanceReduction;

    private bool FallingRight = false;
    private bool FallingLeft = false;

    [SerializeField] private int FallingSide;

    private Animator animator;
    private SpriteRenderer sr;

    [SerializeField] private Sprite[] FallingRightSprites;
    [SerializeField] private Sprite[] FallingLeftSprites;
    [SerializeField] private Sprite MiddleSprite;

    private GameObject RockDropSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gameHandlerScript = FindObjectOfType<GameHandler>();
        timer = FindObjectOfType<Timer>();
        animator.enabled = false;
        BalanceNumber = 0;
        FallingSide = Random.Range(0, 2);
        BalanceReduction = Random.Range(0, 1.5f);

        if (FallingSide == 0)
        {
            FallingLeft = false;
            FallingRight = true;
        }
        else if (FallingSide == 1)
        {
            FallingLeft = true;
            FallingRight = false;
        }

        RockDropSound = GameObject.FindGameObjectWithTag("Rock Drop Sound");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasStarted == true && DroppedRock == false)
        {
            if (FallingRight == true)
            {
                BalanceNumber += BalanceReduction * Time.deltaTime;
            }
            else if (FallingLeft == true)
            {
                BalanceNumber -= BalanceReduction * Time.deltaTime;
            }

            if (BalanceNumber >= 0.5)
            {
                //BalanceNumber -= BalanceReduction * Time.deltaTime;
                FallingRight = false;
            }
            if (BalanceNumber<= -0.5)
            {
                //BalanceNumber += BalanceReduction * Time.deltaTime;
                FallingLeft = false;
            }

            if(FallingRight == false && FallingLeft == false)
            {
                FallingSide = Random.Range(0, 2);
                BalanceReduction = Random.Range(0, 1.5f);
                if (FallingSide == 0)
                {
                    FallingLeft = false;
                    FallingRight = true;
                }
                else if (FallingSide == 1)
                {
                    FallingLeft = true;
                    FallingRight = false;
                }
            }

            if (BalanceNumber <= 0.1f && BalanceNumber >= -0.1f)
            {
                sr.sprite = MiddleSprite;
            }
            else if (BalanceNumber > 0.1f && BalanceNumber <= 0.25f)
            {
                sr.sprite = FallingRightSprites[0];
            }
            else if (BalanceNumber > 0.25f && BalanceNumber <= 0.4f)
            {
                sr.sprite = FallingRightSprites[1];
            }
            else if (BalanceNumber < -0.1f && BalanceNumber >= -0.25f)
            {
                sr.sprite = FallingLeftSprites[0];
            }
            else if (BalanceNumber < -0.25f && BalanceNumber >= -0.4f)
            {
                sr.sprite = FallingLeftSprites[1];
            }
        }

        if (timer.CurrentTime <= TimeToFall && AnimationHasPlayed == false)
        {
            AnimationHasPlayed = true;
            RockDropSound.GetComponent<AudioSource>().Play();
            DroppedRock = true;
            animator.enabled = true;
            if (FallingLeft == true)
            {
                animator.Play("Falling Left");
            }
            else if (FallingRight == true)
            {
                animator.Play("Falling Right");
            }
        }
    }
}
