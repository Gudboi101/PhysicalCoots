using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PawIcon : MonoBehaviour
{
    public bool AtTheBottomOfPushUp = false;
    public bool AtTheTopOfPushUp = false;
    [SerializeField] private SpriteRenderer CootsPushUpSpriteRenderer;
    [SerializeField] private Sprite[] CootsPushUpSprites;
    public int PushUpCount = 0;
    [SerializeField]private Animator animator;
    private SpriteRenderer MySpriteRenderer;
    private Color ClickedColor;
    private bool MouseIsPressed = false;
    private GameHandler gameHandlerScript;
    [SerializeField] private TextMeshPro ScoreText;
    [SerializeField] private GameObject Tail;
    [SerializeField] private Sprite LosingSprite;
    [SerializeField] private Sprite WinningSprite;
    private Timer timerScript;
    [SerializeField] private OtherCatsPushUp[] otherCatsPushUpScript;
    private Color normalTextColor;
    private Color loseTextColor;
    private Color winTextColor;

    //Sound
    private GameObject YaySound;
    private GameObject SadSound;
    private GameObject BlowSound;
    private bool YaySoundHasPlayed = false;
    private bool SadSoundHasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        CootsPushUpSpriteRenderer.sprite = CootsPushUpSprites[0];
        transform.position = new Vector3(7.23f, 3.547652f, 0);
        animator = GetComponent<Animator>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#D1D1D1", out ClickedColor);
        ColorUtility.TryParseHtmlString("#A57847", out normalTextColor);
        ColorUtility.TryParseHtmlString("#718b3e", out winTextColor);
        ColorUtility.TryParseHtmlString("#b93838", out loseTextColor);
        gameHandlerScript = FindObjectOfType<GameHandler>();
        timerScript = FindObjectOfType<Timer>();

        ScoreText.text = PushUpCount.ToString();
        ScoreText.color = normalTextColor;

        YaySound = GameObject.FindGameObjectWithTag("Yay Sound");
        SadSound = GameObject.FindGameObjectWithTag("Sad Sound");
        BlowSound = GameObject.FindGameObjectWithTag("Blow Sound");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            if (AtTheTopOfPushUp == true && AtTheBottomOfPushUp == true)
            {
                AtTheBottomOfPushUp = false;
                AtTheTopOfPushUp = false;
                PushUpCount += 1;
                BlowSound.GetComponent<AudioSource>().Play();
            }
        }

        if (timerScript.CurrentTime <= 0)
        {
            gameHandlerScript.GameHasEnded = true;

            if(PushUpCount > otherCatsPushUpScript[0].PushUpCount &&
                PushUpCount > otherCatsPushUpScript[1].PushUpCount)
            {
                gameHandlerScript.WonTheLevel = true;
                CootsPushUpSpriteRenderer.sprite = WinningSprite;
                ScoreText.color = winTextColor;
                if (YaySoundHasPlayed == false)
                {
                    YaySoundHasPlayed = true;
                    YaySound.GetComponent<AudioSource>().Play();
                }
            }
            else if (PushUpCount <= otherCatsPushUpScript[0].PushUpCount)
            {
                gameHandlerScript.LostTheLevel = true;
                CootsPushUpSpriteRenderer.sprite = LosingSprite;
                ScoreText.color = loseTextColor;
                if (SadSoundHasPlayed == false)
                {
                    SadSoundHasPlayed = true;
                    SadSound.GetComponent<AudioSource>().Play();
                }
            }
            else if (PushUpCount <= otherCatsPushUpScript[1].PushUpCount)
            {
                gameHandlerScript.LostTheLevel = true;
                CootsPushUpSpriteRenderer.sprite = LosingSprite;
                ScoreText.color = loseTextColor;
                if (SadSoundHasPlayed == false)
                {
                    SadSoundHasPlayed = true;
                    SadSound.GetComponent<AudioSource>().Play();
                }
            }
        }

        ScoreText.text = PushUpCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            if (collision.gameObject.tag == "Push Up Up")
            {
                if (AtTheBottomOfPushUp == true)
                {
                    AtTheTopOfPushUp = true;
                    CootsPushUpSpriteRenderer.sprite = CootsPushUpSprites[0];
                    Tail.transform.localPosition = new Vector2(0.0798f, 0.0075f);
                    collision.gameObject.GetComponent<Animator>().Play("Expand");
                }
            }
            else if (collision.gameObject.tag == "Push Up Down")
            {
                AtTheBottomOfPushUp = true;
                CootsPushUpSpriteRenderer.sprite = CootsPushUpSprites[1];
                Tail.transform.localPosition = new Vector2(0.0894f, -0.0108f);
                collision.gameObject.GetComponent<Animator>().Play("Expand");
            }
        }
    }

    private void OnMouseDrag()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (MousePosition.y > 3.547652f)
            {
                gameObject.transform.position = new Vector2(7.23f, 3.547652f);
            }
            else if (MousePosition.y < -0.73f)
            {
                gameObject.transform.position = new Vector2(7.23f, -0.73f);
            }
            else
            {
                gameObject.transform.position = new Vector2(7.23f, MousePosition.y);
            }

        }
        
    }

    private void OnMouseDown()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            animator.Play("Clicked");
            MySpriteRenderer.color = Color.white;
            MouseIsPressed = true;
        }
    }

    private void OnMouseOver()
    {
        if (MouseIsPressed == false)
        {
            MySpriteRenderer.color = ClickedColor;
        }
    }

    private void OnMouseExit()
    {
        MySpriteRenderer.color = Color.white;
    }

    private void OnMouseUp()
    {
        MouseIsPressed = false;
    }
}
