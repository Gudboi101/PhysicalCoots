using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CootsThoughts : MonoBehaviour
{
    [SerializeField] private Sprite[] SushiSprites;
    [SerializeField] private Sprite[] CootsSprites;

    public int SushiChosenNumber = 0;
    private int SushiWantedNumber;
    [SerializeField] private int NumberOfSushiToBeChosen;
    private float speed = 10;

    public bool IsThinkingOfSushi;
    public bool HasChosenSushi;
    private bool SushiCoverHasOpened = false;

    [SerializeField] private OtherCatsHanging[] OtherCatsHangingScript;
    private GameHandler gameHandler;
    private Timer timer;

    [SerializeField] private SpriteRenderer CootsSpriteRenderer;

    [SerializeField] private SpriteRenderer SushiThoughtsSr;
    [SerializeField] private GameObject SushiThoughtGameObject;
    [SerializeField] private GameObject ThoughtGameObject;
    [SerializeField] private Animator SushiThoughtAnimator;

    [SerializeField] private GameObject Waypoint;
    [SerializeField] private GameObject SpeechBubble;

    [SerializeField] private Animator SushiCoverAnimator;

    //Sounds
    private GameObject FallingSound;
    private GameObject CorrectSound;
    private bool SoundHasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        //Depends on Level
        NumberOfSushiToBeChosen = 5;

        SushiWantedNumber = Random.Range(1, 7);
        SushiThoughtsSr.sprite = SushiSprites[SushiWantedNumber - 1];

        OtherCatsHangingScript = FindObjectsOfType<OtherCatsHanging>();
        gameHandler = FindObjectOfType<GameHandler>();
        timer = FindObjectOfType<Timer>();

        SushiThoughtGameObject.SetActive(false);
        ThoughtGameObject.SetActive(false);
        CootsSpriteRenderer.sprite = CootsSprites[0];

        FallingSound = GameObject.FindGameObjectWithTag("Falling Sound");
        CorrectSound = GameObject.FindGameObjectWithTag("Correct Sound");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.GameHasStarted == true && gameHandler.GameHasEnded == false)
        {
            ThoughtGameObject.SetActive(true);
            SushiThoughtGameObject.SetActive(true);
            OpenSushiCover();
            SettingDifficulty();
        }

        if (HasChosenSushi == true)
        {
            HasChosenSushi = false;
            if (SushiWantedNumber == SushiChosenNumber)
            {
                NumberOfSushiToBeChosen -= 1;
            }

            if(NumberOfSushiToBeChosen > 0 && SushiChosenNumber == SushiWantedNumber)
            {
                StartCoroutine(SushiChosenCorrectlyCoroutine());
            }
            else if (SushiChosenNumber != SushiWantedNumber)
            {
                //Lose the Level
                gameHandler.GameHasEnded = true;
                gameHandler.LostTheLevel = true;
            }
        }

        if (timer.CurrentTime <= 0)
        {
            gameHandler.GameHasEnded = true;
            gameHandler.LostTheLevel = true;
        }

        if (gameHandler.LostTheLevel == true)
        {
            if (SoundHasPlayed == false)
            {
                SoundHasPlayed = true;
                FallingSound.GetComponent<AudioSource>().Play();
            }
            CootsSpriteRenderer.sprite = CootsSprites[2];
            SpeechBubble.SetActive(false);
            if (Vector2.Distance(transform.position, Waypoint.transform.position) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Waypoint.transform.position, speed * Time.deltaTime);
            }
        }

        if (NumberOfSushiToBeChosen <= 0)
        {
            //Won the Level
            gameHandler.GameHasEnded = true;
            gameHandler.WonTheLevel = true;
            CootsSpriteRenderer.sprite = CootsSprites[1];

            SushiThoughtGameObject.SetActive(false);
            SpeechBubble.SetActive(false);
            OtherCatsHangingScript[1].TimeToFall = true;
        }
        else if (NumberOfSushiToBeChosen == 3)
        {
            OtherCatsHangingScript[0].TimeToFall = true;
        }
    }

    private IEnumerator SushiChosenCorrectlyCoroutine()
    {
        SushiThoughtAnimator.Play("Chosen");
        CorrectSound.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.4f);
        SushiWantedNumber = Random.Range(1, 7);
        SushiThoughtsSr.sprite = SushiSprites[SushiWantedNumber - 1];
        SushiThoughtAnimator.Play("Idle");

    }

    private void OpenSushiCover()
    {
        if (SushiCoverHasOpened == false)
        {
            SushiCoverHasOpened = true;
            SushiCoverAnimator.Play("Open");
        }
    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            timer.TimeReduction = 10;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            timer.TimeReduction = 12;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            timer.TimeReduction = 14;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            timer.TimeReduction = 16;
        }
    }

}
