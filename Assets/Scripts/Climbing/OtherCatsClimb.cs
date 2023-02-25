using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCatsClimb : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float TimeToLose;
    private bool ClimbNow;
    private bool Lose;
    private float ClimbCountdown = 1;
    [SerializeField] private float ClimbReduction;
    private GameHandler gameHandlerScript;
    private Timer timer;
    [SerializeField] private GameObject TargetWaypointLose;

    // Start is called before the first frame update
    void Start()
    {
        gameHandlerScript = FindObjectOfType<GameHandler>();
        timer = FindObjectOfType<Timer>();
        animator = GetComponent<Animator>();

        ClimbReduction = Random.Range(0.5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true && Lose == false)
        {
            if (ClimbCountdown <= 0 && ClimbNow == false)
            {
                ClimbNow = true;
            }

            if (ClimbNow == true)
            {
                ClimbNow = false;
                ClimbCountdown = 1;
                ClimbReduction = Random.Range(0.5f, 2);
                StartCoroutine(ClimbingCoroutine());
            }
            else if (ClimbNow == false && ClimbCountdown > 0 && transform.position.y < 3.1)
            {
                ClimbCountdown -= ClimbReduction * Time.deltaTime;
            }

            if (transform.position.y <= -2.5f && ClimbNow == false)
            {
                //ClimbNow = false;
                animator.Play("Climb");
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.13f);
            }

            if (transform.position.y > 3.1)
            {
                ClimbNow = false;
            }


        }

        if (timer.CurrentTime <= TimeToLose)
        {
            Lose = true;
            animator.Play("Fall");
            transform.position = Vector2.MoveTowards(transform.position, TargetWaypointLose.transform.position, 8f * Time.deltaTime);
        }
        
    }

    private IEnumerator ClimbingCoroutine()
    {
        animator.Play("Climb");
        yield return new WaitForSeconds(0.083f);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.13f);
    }
}
