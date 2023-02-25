using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCatsHanging : MonoBehaviour
{
    private GameHandler gameHandler;

    public bool TimeToFall;
    private bool ChangeAnimationState;

    public int AnimationStateToPlay;
    [SerializeField] private float speed = 5f;

    private Animator animator;

    [SerializeField] private GameObject Waypoint;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();
        animator = GetComponent<Animator>();

        AnimationStateToPlay = Random.Range(0, 3);
        ChangeAnimationState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.GameHasStarted == true && TimeToFall == false)
        { 
            //Changing Animations
            StartCoroutine(AnimationStateChanging());
            AnimationStateDecider();

        }
        else if (TimeToFall == true)
        {
            if (Vector2.Distance(transform.position, Waypoint.transform.position) > 0)
            {
                animator.Play("Fall");
                transform.position = Vector2.MoveTowards(transform.position, Waypoint.transform.position, speed * Time.deltaTime);
            }
        }
    }

    void AnimationStateDecider()
    {
        switch (AnimationStateToPlay)
        {
            case 0:
                animator.Play("State 1");
                break;
            case 1:
                animator.Play("State 2");
                break;
            case 2:
                animator.Play("State 3");
                break;

        }
    }

    private IEnumerator AnimationStateChanging()
    {
        if (ChangeAnimationState == true)
        {
            ChangeAnimationState = false;
            yield return new WaitForSeconds(0.5f);
            AnimationStateToPlay = Random.Range(0, 3);
            if (TimeToFall == false)
            {
                ChangeAnimationState = true;
            }
        }
    }
}
