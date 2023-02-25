using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CootsClimbing : MonoBehaviour
{
    private Animator animator;
    private bool GotLetterCorrect;
    private LetterScript letterScript;
    private GameHandler gameHandlerScript;
    private Timer timer;
    [SerializeField] private int NumberOfTimesToPressLetter;
    [SerializeField] private GameObject Tail;

    [SerializeField] private GameObject LoseWaypoint;
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameObject SpeechBubble;

    //Sounds
    [SerializeField] private GameObject ClimbingSfx;
    [SerializeField] private GameObject FallingSound;
    private GameObject YaySound;
    private bool YaySoundHasPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        letterScript = FindObjectOfType<LetterScript>();
        gameHandlerScript = FindObjectOfType<GameHandler>();
        timer = FindObjectOfType<Timer>();
        Tail.transform.localPosition = new Vector2(-0.0013f, -0.01010001f);

        ClimbingSfx = GameObject.FindGameObjectWithTag("Rope Climb");
        FallingSound = GameObject.FindGameObjectWithTag("Falling Sound");
        YaySound = GameObject.FindGameObjectWithTag("Yay Sound");

        NumberOfTimesToPressLetter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.CurrentTime <= 0)
        {
            gameHandlerScript.GameHasEnded = true;
            gameHandlerScript.WonTheLevel = true;
        }

        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            if (letterScript.LetterRandomizer == 27)
            {
                letterScript.ChangeLetter = true;
            }

            if (NumberOfTimesToPressLetter == 0)
            {
                NumberOfTimesToPressLetter = 1;
                StartCoroutine(ClimbingCoroutine());
            }

            if (GotLetterCorrect == true)
            {
                GotLetterCorrect = false;
                letterScript.ChangeLetter = true;
                NumberOfTimesToPressLetter -= 1;
            }

            if (letterScript.LetterRandomizer != 27 && UnityEngine.Input.anyKeyDown)
            {
                LetterInputLogic();
            }
        }

        if (gameHandlerScript.LostTheLevel == true)
        {
            SpeechBubble.SetActive(false);
            transform.position = Vector2.MoveTowards(transform.position, LoseWaypoint.transform.position, speed * Time.deltaTime);
        }
        else if (gameHandlerScript.WonTheLevel == true)
        {
            Tail.transform.localPosition = new Vector2(0.0702f, -0.01010001f);
            SpeechBubble.SetActive(false);
            if (YaySoundHasPlayed == false)
            {
                YaySoundHasPlayed = true;
                YaySound.GetComponent<AudioSource>().Play();
            }
            animator.Play("Win");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Red Line" && gameHandlerScript.GameHasEnded == false)
        {
            gameHandlerScript.GameHasEnded = true;
            gameHandlerScript.LostTheLevel = true;
            animator.Play("Lose");
            FallingSound.GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator ClimbingCoroutine()
    {
        animator.Play("Climb");
        ClimbingSfx.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.083f);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.13f);
    }

    private void LetterInputLogic()
    {
        switch(letterScript.LetterRandomizer)
        {
            case 0:
                LetterShortcut("A");
                break;
            case 1:
                LetterShortcut("B");
                break;
            case 2:
                LetterShortcut("C");
                break;
            case 3:
                LetterShortcut("D");
                break;
            case 4:
                LetterShortcut("E");
                break;
            case 5:
                LetterShortcut("F");
                break;
            case 6:
                LetterShortcut("G");
                break;
            case 7:
                LetterShortcut("H");
                break;
            case 8:
                LetterShortcut("I");
                break;
            case 9:
                LetterShortcut("J");
                break;
            case 10:
                LetterShortcut("K");
                break;
            case 11:
                LetterShortcut("L");
                break;
            case 12:
                LetterShortcut("M");
                break;
            case 13:
                LetterShortcut("N");
                break;
            case 14:
                LetterShortcut("O");
                break;
            case 15:
                LetterShortcut("P");
                break;
            case 16:
                LetterShortcut("Q");
                break;
            case 17:
                LetterShortcut("R");
                break;
            case 18:
                LetterShortcut("S");
                break;
            case 19:
                LetterShortcut("T");
                break;
            case 20:
                LetterShortcut("U");
                break;
            case 21:
                LetterShortcut("V");
                break;
            case 22:
                LetterShortcut("W");
                break;
            case 23:
                LetterShortcut("X");
                break;
            case 24:
                LetterShortcut("Y");
                break;
            case 25:
                LetterShortcut("Z");
                break;

        }
    }

    private void LetterShortcut(string s)
    {
        KeyCode kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), s);
        if (Input.GetKeyDown(kc))
        {
            GotLetterCorrect = true;
        }
        else
        {
            //Lose
            gameHandlerScript.GameHasEnded = true;
            gameHandlerScript.LostTheLevel = true;
            animator.Play("Lose");
            FallingSound.GetComponent<AudioSource>().Play();
        }
    }
}
