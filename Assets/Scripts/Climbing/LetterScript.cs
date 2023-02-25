using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    public int LetterRandomizer;
    public bool ChangeLetter;
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] Letters;
    private GameHandler gameHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        LetterRandomizer = 27;
        gameHandlerScript = FindObjectOfType<GameHandler>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            if (ChangeLetter == true)
            {
                ChangeLetter = false;
                LetterRandomizer = Random.Range(0, 26);
            }
            if (LetterRandomizer != 27)
            {
                sr.sprite = Letters[LetterRandomizer];
            }
        }
    }
}
