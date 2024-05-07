using UnityEngine;
using TMPro;
using System;

public class BallColliderOpt : MonoBehaviour
{

    [SerializeField] TextMeshPro fillChar;

    [SerializeField] private float charBallSpeed = 20f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask activeWordsBallLayer;

    private MoveWords moveWordsScript;
    private bool onceFlag;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        moveWordsScript = GameObject.FindObjectOfType<MoveWords>();
        onceFlag = true;

        rb = GetComponent<Rigidbody2D>();
        setMouseDirVelocity();
        destroyByTime();
    }

    private void destroyByTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void setMouseDirVelocity()
    {
        rb.velocity = transform.right * charBallSpeed;
    }

    public void setToMatchChar(string newChar)
    {
        if (fillChar != null)
        {
            fillChar.SetText(newChar);
        }
    }

    public string getToMatchChar()
    {
        string charText = null;
        if (fillChar != null)
        {
            charText = fillChar.text;
        }
        return charText;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((activeWordsBallLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("OnTriggerEnter2D to meet activeBalls layer");
            // spawn particles

            // play sound FX

            // Screen shake

            // Merge word ball

            // Destroy this char ball
            // Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ActiveBalls" && onceFlag)
        {
            onceFlag = false;

            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

            string toMatchChar = this.getToMatchChar();
            WordController wordController = other.gameObject.GetComponent<WordController>();
            string wordText = wordController.getWord();
            string toCheckWord = wordText.Replace("?", toMatchChar.ToLower());

            Debug.Log("Collision word ball=" + wordText + ", toCheckWord=" + toCheckWord);
            if (moveWordsScript.IsRightWord(toCheckWord))
            {
                wordController.setWord(toCheckWord);
                wordController.setMatchedFlag(true);
                moveWordsScript.addRightWord(toCheckWord);
                Debug.Log("MatchedRightWord=" + toCheckWord);

                float scorePerWord = moveWordsScript.getScorePerWord();
                moveWordsScript.addScore(scorePerWord);
                wordController.showScore(scorePerWord);
            }
            else
            {
                //Dialog to show the wrong information
                moveWordsScript.showGuideDialogInfo(wordText, toCheckWord);

                // wordController.setWord(toCheckWord);
                // wordController.setMatchedFlag(true);
                // moveWordsScript.addRightWord(toCheckWord);
                // Debug.Log("SetMatchedWord then get=" + wordController.getWord());

            }

            this.gameObject.GetComponent<BallColliderOpt>().enabled = false;
            Destroy(gameObject);
        }
    }
}
