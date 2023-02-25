using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sushi : MonoBehaviour
{
    private Color black;
    private Color white;

    private GameHandler gameHandler;
    private CootsThoughts cootsThoughtsScript;

    private SpriteRenderer Sr;
    private Animator animator;

    [SerializeField] private int ThisSushiNumber;

    public bool MouseIsOver = false;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#BFBFBF", out black);
        ColorUtility.TryParseHtmlString("#FFFFFF", out white);

        Sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gameHandler = FindObjectOfType<GameHandler>();
        cootsThoughtsScript = FindObjectOfType<CootsThoughts>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandler.GameHasEnded == true)
        {
            Sr.color = white;
        }
        else if (MouseIsOver == true && gameHandler.GameHasEnded == false)
        {
            Sr.color = black;
        }
        else
        {
            Sr.color = white;
        }
    }

    private void OnMouseOver()
    {
        if (gameHandler.GameHasEnded == false && gameHandler.GameHasStarted == true)
        {
            MouseIsOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (gameHandler.GameHasEnded == false && gameHandler.GameHasStarted == true)
        {
            MouseIsOver = false;
        }
    }

    private void OnMouseDown()
    {
        if (gameHandler.GameHasEnded == false && gameHandler.GameHasStarted == true)
        {
            animator.Play("Chosen");
            cootsThoughtsScript.SushiChosenNumber = ThisSushiNumber;
            cootsThoughtsScript.HasChosenSushi = true;
        }
    }
}
