using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMeter : MonoBehaviour
{
    private bool GoRight;
    private bool GoLeft;
    private bool StopMoving;
    [SerializeField]private bool CollidingWithGreenMeter;

    [SerializeField] private GameObject RightWaypoint;
    [SerializeField] private GameObject LeftWaypoint;

    [SerializeField] private BoatPulling BoatPullingScript;
    [SerializeField] private GameHandler GameHandlerScript;

    [SerializeField] private Sprite[] Icons;
    private SpriteRenderer sr;

    [SerializeField] private Animator CootsHeadAnimator;

    //Level Dependent
    [SerializeField] private float Speed;
    private int GreenMeterLevel;
    [SerializeField] private GameObject[] GreenMeterGameObjects;
    [SerializeField] private GreenMeter[] GreenMetersScripts;

    //Sound
    private GameObject SadSound;
    private GameObject StrugglePullingSound;
    private bool SadSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        GoLeft = true;
        sr = GetComponent<SpriteRenderer>();

        //If level 1
        Speed = 15;
        //Change According To Level
        ReplaceGreenMeter();

        SadSound = GameObject.FindGameObjectWithTag("Sad Sound");
        StrugglePullingSound = GameObject.FindGameObjectWithTag("Struggle Pulling");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandlerScript.GameHasStarted == true && BoatPullingScript.ShouldPull == false && GameHandlerScript.GameHasEnded == false)
        {
            GoRightAndLeft();

            //Make it any button?
            if (Input.GetKeyDown(KeyCode.Space) && StopMoving == false)
            {
                StopMoving = true;
                StartCoroutine(IconIsPressedCoroutine());
                if (CollidingWithGreenMeter == true)
                {
                    BoatPullingScript.ShouldPull = true;
                    //Play Success Animation
                    StartCoroutine(IconIsOnGreenMeterPressed());

                    //Depending On Level
                    GreenMeterLevel = Random.Range(0, 2);
                }
                else if (CollidingWithGreenMeter == false)
                {
                    //Play Fail Animation
                    StartCoroutine(IconIsNotOnGreenMeterPressed());
                    CootsHeadAnimator.Play("Coots Head Fail");
                    StrugglePullingSound.GetComponent<AudioSource>().Play();
                }
            }
        }
        else if (GameHandlerScript.LostTheLevel == true)
        {
            CootsHeadAnimator.Play("Coots Head Lose");
            if (SadSoundPlayed == false)
            {
                SadSoundPlayed = true;
                SadSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Green Meter") 
        {
            CollidingWithGreenMeter = true;       
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Green Meter")
        {
            CollidingWithGreenMeter = false;
        }
    }

    private IEnumerator IconIsPressedCoroutine()
    {
        yield return new WaitForSeconds(0.583f);
        StopMoving = false;
    }

    private IEnumerator IconIsOnGreenMeterPressed()
    {
        sr.sprite = Icons[1];
        transform.localPosition = new Vector2(transform.localPosition.x, 0);
        yield return new WaitForSeconds(0.583f);
        sr.sprite = Icons[0];
        transform.localPosition = new Vector2(transform.localPosition.x, 0.0777f);
        ReplaceGreenMeter();
    }

    private IEnumerator IconIsNotOnGreenMeterPressed()
    {
        sr.sprite = Icons[0];
        transform.localPosition = new Vector2(transform.localPosition.x, 0.062f);
        yield return new WaitForSeconds(0.2915f);
        sr.sprite = Icons[0];
        transform.localPosition = new Vector2(transform.localPosition.x, 0.0777f);
    }

    private void ReplaceGreenMeter()
    {
        //Change According To Level
        SettingDifficulty();

        switch (GreenMeterLevel)
        {
            case 0:
                GreenMeterGameObjects[0].SetActive(true);
                GreenMeterGameObjects[1].SetActive(false);
                GreenMeterGameObjects[2].SetActive(false);
                GreenMeterGameObjects[3].SetActive(false);
                GreenMetersScripts[0].Shuffle();
                break;
            case 1:
                GreenMeterGameObjects[0].SetActive(false);
                GreenMeterGameObjects[1].SetActive(true);
                GreenMeterGameObjects[2].SetActive(false);
                GreenMeterGameObjects[3].SetActive(false);
                GreenMetersScripts[1].Shuffle();
                break;
            case 2:
                GreenMeterGameObjects[0].SetActive(false);
                GreenMeterGameObjects[1].SetActive(false);
                GreenMeterGameObjects[2].SetActive(true);
                GreenMeterGameObjects[3].SetActive(false);
                GreenMetersScripts[2].Shuffle();
                break;
            case 3:
                GreenMeterGameObjects[0].SetActive(false);
                GreenMeterGameObjects[1].SetActive(false);
                GreenMeterGameObjects[2].SetActive(false);
                GreenMeterGameObjects[3].SetActive(true);
                GreenMetersScripts[3].Shuffle();
                break;
        }
    }

    private void GoRightAndLeft()
    {
        if (GoLeft == true && StopMoving == false)
        {
            if (Vector2.Distance(transform.position, LeftWaypoint.transform.position) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, LeftWaypoint.transform.position, Speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, LeftWaypoint.transform.position) <= 0)
            {
                GoLeft = false;
                GoRight = true;
            }
        }
        else if (GoRight == true && StopMoving == false)
        {
            if (Vector2.Distance(transform.position, RightWaypoint.transform.position) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, RightWaypoint.transform.position, Speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, RightWaypoint.transform.position) <= 0)
            {
                GoLeft = true;
                GoRight = false;
            }
        }
    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            GreenMeterLevel = Random.Range(0, 2);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            GreenMeterLevel = Random.Range(1, 3);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            GreenMeterLevel = Random.Range(2, 4);
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            GreenMeterLevel = Random.Range(3, 4);
        }
    }
}
