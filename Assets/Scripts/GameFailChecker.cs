using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordZuma.Core;

public class GameFailChecker : MonoBehaviour
{

    private bool onceFlag;

    // Start is called before the first frame update
    void Start()
    {
        onceFlag = true;

    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ActiveBalls" && onceFlag)
        {
            onceFlag = false;

            WordController wordController = other.gameObject.GetComponent<WordController>();
            string wordText = wordController.getWord();
            Debug.Log(wordText + " active word met the end checker, so game failed");
            if (GameManager.Instance != null)
            {
                Debug.Log("Failed to pass the grade: " + GameManager.Instance.GradeName + " , level: " + GameManager.Instance.CurrentLevel);
            }
            // TODO: Show the fail information

            UnityEngine.SceneManagement.SceneManager.LoadScene("StartPage");

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D to GameFailChecker");
    }
}
