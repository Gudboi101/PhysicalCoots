using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPulling : MonoBehaviour
{
    public int PullState;

    private bool AnimationHasPlayed = false;
    public bool ShouldPull;

    private Animator animator;

    [SerializeField] private GameObject CootsHead;
    [SerializeField] private GameObject[] HeadWaypoints;

    [SerializeField] private GameObject Tail;
    [SerializeField] private GameObject[] TailWaypoints;

    private Timer timerScript;
    private GameHandler GameHandlerScript;

    [SerializeField] private GameObject RedMeter;

    private GameObject BoatMovingSound;
    private GameObject YaySound;
    
    

    // Start is called before the first frame update
    void Start()
    {
        PullState = 0;

        animator = GetComponent<Animator>();
        GameHandlerScript = FindObjectOfType<GameHandler>();
        timerScript = FindObjectOfType<Timer>();

        CootsHead.transform.position = HeadWaypoints[0].transform.position;
        Tail.transform.position = TailWaypoints[0].transform.position;

        RedMeter.SetActive(true);

        BoatMovingSound = GameObject.FindGameObjectWithTag("Boat Moving");
        YaySound = GameObject.FindGameObjectWithTag("Yay Sound");
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandlerScript.GameHasEnded == true && GameHandlerScript.LostTheLevel == true)
        {
            RedMeter.SetActive(false);
        }

        if (timerScript.CurrentTime == 0)
        {
            GameHandlerScript.GameHasEnded = true;
            GameHandlerScript.LostTheLevel = true;
        }

        if (ShouldPull == true)
        {
            ShouldPull = false;
            BoatMovingSound.GetComponent<AudioSource>().Play();
            PullState += 1;
            AnimationHasPlayed = false;
            CootsHead.SetActive(false);
        }

        if (AnimationHasPlayed == false)
        {
            AnimationHasPlayed = true;
            StartCoroutine(MoveTail());
            switch (PullState)
            {
                case 1:
                    animator.Play("State 1");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[1].transform.position;
                    break;
                case 2:
                    animator.Play("State 2");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[2].transform.position;
                    break;
                case 3:
                    animator.Play("State 3");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[3].transform.position;
                    break;
                case 4:
                    animator.Play("State 4");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[4].transform.position;
                    break;
                case 5:
                    animator.Play("State 5");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[5].transform.position;
                    break;
                case 6:
                    animator.Play("State 6");
                    StartCoroutine(WaitForAnimationToFinish());
                    CootsHead.transform.position = HeadWaypoints[6].transform.position;
                    //Game Has Ended //Won The Level
                    GameHandlerScript.GameHasEnded = true;
                    GameHandlerScript.WonTheLevel = true;
                    
                    break;
            }
        }
    }

    private IEnumerator WaitForAnimationToFinish()
    {
        CootsHead.SetActive(false);
        //Make Time Reduction 0
        timerScript.TimeReduction = 0;
        yield return new WaitForSeconds(0.583f);
        //Make Time Reduction Whatever Number it was According to level
        if (PullState < 6)
        {
            CootsHead.SetActive(true);
            //If Level 1
            SettingDifficulty();
        }
        else if (PullState == 6)
        {
            //Turn off Red Meter
            RedMeter.SetActive(false);
            YaySound.GetComponent<AudioSource>().Play();
        }
    }

    private IEnumerator MoveTail()
    {
        switch (PullState)
        {
            case 1:
                Tail.transform.position = TailWaypoints[0].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[1].transform.position;
                break;
            case 2:
                Tail.transform.position = TailWaypoints[2].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[3].transform.position;
                break;
            case 3:
                Tail.transform.position = TailWaypoints[4].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[5].transform.position;
                break;
            case 4:
                Tail.transform.position = TailWaypoints[6].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[7].transform.position;
                break;
            case 5:
                Tail.transform.position = TailWaypoints[8].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[9].transform.position;
                break;
            case 6:
                Tail.transform.position = TailWaypoints[10].transform.position;
                yield return new WaitForSeconds(0.3333f);
                Tail.transform.position = TailWaypoints[11].transform.position;
                yield return new WaitForSeconds(0.3333f);
                //Off this tail and turn on the final tail
                Tail.SetActive(false);
                break;
        }
    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            timerScript.TimeReduction = 10;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            timerScript.TimeReduction = 11;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            timerScript.TimeReduction = 12f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            timerScript.TimeReduction = 13;
        }
    }
}
