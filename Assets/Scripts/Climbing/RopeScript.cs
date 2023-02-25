using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    GameHandler gameHandlerScript;

    public Transform TargetPosition;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameHandlerScript = FindObjectOfType<GameHandler>();
        SettingDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHandlerScript.GameHasEnded == false && gameHandlerScript.GameHasStarted == true)
        {
            TargetPosition.position = new Vector2(transform.position.x, transform.position.y - 1f);
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition.position, speed * Time.deltaTime);
        }
    }

    private void SettingDifficulty()
    {
        if (PlayerPrefs.GetInt("Difficulty", 0) == 0)
        {
            speed = 1;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 1)
        {
            speed = 1.1f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 2)
        {
            speed = 1.3f;
        }
        else if (PlayerPrefs.GetInt("Difficulty", 0) == 3)
        {
            speed = 1.5f;
        }
    }
}
