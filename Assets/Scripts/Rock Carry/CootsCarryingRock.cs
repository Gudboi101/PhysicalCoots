using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CootsCarryingRock : MonoBehaviour
{
    [SerializeField] private bool FallingRight;
    [SerializeField] private bool FallingLeft;

    private int FallRandomizer;

    [SerializeField] private float BalanceNumber;
    [SerializeField] private float BalanceReduction;
    [SerializeField] private float BalanceAdder;

    private GameHandler gameHandlerScript;
    private Timer timer;

    //Cat Visuals Related
    [SerializeField] private Sprite[] CootsFallingLeftSprites;
    [SerializeField] private Sprite[] CootsFallingRightSprites;
    [SerializeField] private Sprite CootsCenterSprite;
    private SpriteRenderer sr;
    private Animator animator;
    private bool AnimationHasPlayed = false;

    //Meter Related
    [SerializeField] private GameObject MeterStick;

    //Sound
    private GameObject YaySound;
    private GameObject SadSound;
    private GameObject RockDropSound;
    //private GameObject StruggleCarrySound;
    private bool SadSoundPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        SetFallingDirectionAtTheStartOfTheLevel();
        gameHandlerScript = FindObjectOfType<GameHandler>();
        timer = FindObjectOfType<Timer>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        sr.sprite = CootsCenterSprite;
        SettingDifficulty();

        //StruggleCarrySound = GameObject.FindGameObjectWithTag("Struggle Carry Sound");
        RockDropSound = GameObject.FindGameObjectWithTag("Rock Drop Sound");
        YaySound = GameObject.FindGameObjectWithTag("Yay Sound");
        SadSound = GameObject.FindGameObjectWithTag("Sad Sound");
    }

    // Update is called once per frame
    void Update()
    {
        //To Change Sprite of Cat
        VisualCatFallingLogic();

        //All if game has started and game has not ended
        if (gameHandlerScript.GameHasStarted == true && gameHandlerScript.GameHasEnded == false)
        {
            //Called when passes the neutral position again
            ResetingTheDirectionToFallTo();

            //If the balance number didnt get to losing number yet
            if (FallingRight == true)
            {
                BalanceNumber += BalanceReduction * Time.deltaTime;
            }
            else if (FallingLeft == true)
            {
                BalanceNumber -= BalanceReduction * Time.deltaTime;
            }

            //Setting the ends of balance number to lose game
            if (BalanceNumber >= 0.4)
            {
                BalanceNumber = 0.4f;
                gameHandlerScript.GameHasEnded = true;
                gameHandlerScript.LostTheLevel = true;
                if (SadSoundPlayed == false)
                {
                    SadSoundPlayed = true;
                    SadSound.GetComponent<AudioSource>().Play();
                    RockDropSound.GetComponent<AudioSource>().Play();
                }
            }
            if (BalanceNumber <= -0.4)
            {
                BalanceNumber = -0.4f;
                gameHandlerScript.GameHasEnded = true;
                gameHandlerScript.LostTheLevel = true;
                if (SadSoundPlayed == false)
                {
                    SadSoundPlayed = true;
                    SadSound.GetComponent<AudioSource>().Play();
                    RockDropSound.GetComponent<AudioSource>().Play();
                }
            }

            //Setting the Winning Condition
            if (timer.CurrentTime == 0)
            {
                gameHandlerScript.GameHasEnded = true;
                gameHandlerScript.WonTheLevel = true;
                if (AnimationHasPlayed == false)
                {
                    AnimationHasPlayed = true;
                    StartCoroutine(WinningCoroutine());
                }
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow) && BalanceNumber < 0.4f && BalanceNumber >-0.4f)
            {
                BalanceNumber -= BalanceAdder;
                //StruggleCarrySound.GetComponent<AudioSource>().Play();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && BalanceNumber < 0.4f && BalanceNumber > -0.4f)
            {
                BalanceNumber += BalanceAdder;
                //StruggleCarrySound.GetComponent<AudioSource>().Play();
            }

            //Balance Meter Stick
            MeterStick.transform.localPosition = new Vector2(BalanceNumber, MeterStick.transform.localPosition.y);
        }
       
    }

    private IEnumerator WinningCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        animator.enabled = true;
        animator.Play("Win");
        YaySound.GetComponent<AudioSource>().Play();
        RockDropSound.GetComponent<AudioSource>().Play();
    }

    private void SetFallingDirectionAtTheStartOfTheLevel()
    {
        FallRandomizer = Random.Range(0, 2);
        if (FallRandomizer == 0)
        {
            FallingRight = true;
            FallingLeft = false;
        }
        else if (FallRandomizer == 1)
        {
            FallingRight = false;
            FallingLeft = true;
        }
        BalanceNumber = 0;
    }

    private void ResetingTheDirectionToFallTo()
    {
        if (BalanceNumber >= 0 && FallingLeft == true)
        {
            FallingRight = false;
            FallingLeft = false;
            BalanceNumber = 0;
            FallRandomizer = Random.Range(0, 2);
            if (FallRandomizer == 0)
            {
                FallingRight = true;
                FallingLeft = false;
            }
            else if (FallRandomizer == 1)
            {
                FallingRight = false;
                FallingLeft = true;
            }
        }
        else if (BalanceNumber <= 0 && FallingRight == true)
        {
            FallingRight = false;
            FallingLeft = false;
            BalanceNumber = 0;
            FallRandomizer = Random.Range(0, 2);
            if (FallRandomizer == 0)
            {
                FallingRight = true;
                FallingLeft = false;
            }
            else if (FallRandomizer == 1)
            {
                FallingRight = false;
                FallingLeft = true;
            }
        }
    }

    private void VisualCatFallingLogic()
    {
        if (BalanceNumber >= -0.05 && BalanceNumber <= 0.05)
        {
            sr.sprite = CootsCenterSprite;
        }
        //Falling Right
        else if (BalanceNumber > 0.05 && BalanceNumber <= 0.1)
        {
            sr.sprite = CootsFallingRightSprites[0];
        }
        else if (BalanceNumber > 0.1 && BalanceNumber <= 0.2)
        {
            sr.sprite = CootsFallingRightSprites[1];
        }
        else if (BalanceNumber > 0.2 && BalanceNumber <= 0.3)
        {
            sr.sprite = CootsFallingRightSprites[2];
        }
        else if (BalanceNumber > 0.3 && BalanceNumber < 0.4)
        {
            sr.sprite = CootsFallingRightSprites[3];
        }
        else if (BalanceNumber >= 0.4)
        {
            if (AnimationHasPlayed == false)
            {
                animator.enabled = true;
                AnimationHasPlayed = true;
                animator.Play("Fall Right");
            }
        }
        //Falling Left
        else if (BalanceNumber < -0.05 && BalanceNumber >= -0.1)
        {
            sr.sprite = CootsFallingLeftSprites[0];
        }
        else if (BalanceNumber < -0.1 && BalanceNumber >=  -0.2)
        {
            sr.sprite = CootsFallingLeftSprites[1];
        }
        else if (BalanceNumber < -0.2 && BalanceNumber >= -0.3)
        {
            sr.sprite = CootsFallingLeftSprites[2];
        }
        else if (BalanceNumber < -0.3 && BalanceNumber > -0.4)
        {
            sr.sprite = CootsFallingLeftSprites[3];
        }
        else if (BalanceNumber <= -0.4)
        {
            if (AnimationHasPlayed == false)
            {
                animator.enabled = true;
                AnimationHasPlayed = true;
                animator.Play("Fall Left");
            }
        }

    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            BalanceAdder = 0.05f;
            BalanceReduction = 0.1f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            BalanceAdder = 0.07f;
            BalanceReduction = 0.3f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            BalanceAdder = 0.08f;
            BalanceReduction = 0.5f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            BalanceAdder = 0.1f;
            BalanceReduction = 0.7f;
        }
    }
}
