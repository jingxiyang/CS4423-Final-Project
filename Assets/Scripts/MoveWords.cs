using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;
using UnityEngine.UI;

public struct ActiveBallList
{
    List<GameObject> ballList;
    bool isMoving;
    bool isInTransition;
}

public enum BallColor
{
    red,
    green,
    blue,
    yellow
}

public class MoveWords : MonoBehaviour
{
    [SerializeField] SplineContainer spline;
    [SerializeField] float speed = 1f;
    [SerializeField] int ballCount;

    [SerializeField] GameObject redBall;
    [SerializeField] GameObject greenBall;
    [SerializeField] GameObject blueBall;
    [SerializeField] GameObject yellowBall;

    [SerializeField] Transform wordSpawnPosition;

    [SerializeField] ParticleSystem winParticleSystem;

    [SerializeField] DialogNpc dialogNpc;

    [SerializeField] DialogNpc winSummaryDialogNpc;

    [SerializeField] GameObject toStartPageHolder;

    [SerializeField] GameObject toNextLevelHolder;

    private List<GameObject> ballList;

    // Private
    private GameObject ballsContainerGO;
    private GameObject removedBallsContainer;
    float splineLength;
    private SplinePath<Spline> splinePath;

    private float distance = 0;
    private int headballIndex;

    private int addBallIndex;
    private int touchedBallIndex;
    private float ballRadius;

    private string[] toMatchWords;
    private string[] toFillChars;
    private string[] rightWords;
    private string[] toMatchWordsByLevel;
    private char[] toFillCharsByLevel;

    private float scorePerWord;

    private List<string> matchedWordList;

    private bool onceFlag;

    private string currentGradeName;
    private string currentGrade;
    private string currentLevel;
    private string nextLevelSceneSign;
    private string currentColor;

    void Start()
    {
        headballIndex = 0;
        addBallIndex = -1;

        spline = GetComponent<SplineContainer>();
        if (spline != null)
        {
            splinePath = new SplinePath<Spline>(spline.Splines);
            splineLength = splinePath != null ? splinePath.GetLength() : 0f;
        }

        LoadData();
        ballList = new List<GameObject>(ballCount);

        ballsContainerGO = new GameObject();
        ballsContainerGO.name = "Balls Container";
        removedBallsContainer = new GameObject();
        removedBallsContainer.name = "Removed Balls Container";

        for (int i = 0; i < ballCount; i++)
            CreateNewBall(i);

        ballRadius = ballList[0].GetComponent<CircleCollider2D>().radius;

        DOTween.SetTweensCapacity(3150, 50);

        scorePerWord = (float)System.Math.Round((double)100 / ballCount, 2);

        matchedWordList = new List<string>(ballCount);

        onceFlag = true;

        string currentGradeName = PlayerPrefs.GetString("currentGradeName");
        string currentGrade = PlayerPrefs.GetString("currentGrade");
        string currentLevel = PlayerPrefs.GetString("currentLevel");
        string nextLevelSceneSign = PlayerPrefs.GetString("currentColor");
        string currentColor = PlayerPrefs.GetString("currentGradeName");

        toStartPageHolder.SetActive(false);
        toNextLevelHolder.SetActive(false);
    }

    private void Awake()
    {
        winParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            GameObject item = ballList[i];
            if (item.GetComponent<WordController>() != null)
            {
                if (item.GetComponent<WordController>().getMatchedFlag())
                {
                    ballList.Remove(item);
                    Destroy(item);
                }
            }
        }

        if (ballList.Count > 0)
            MoveAllBallsAlongPath();

        if (ballList == null || ballList.Count == 0)
        {
            if (onceFlag)
            {
                // Winning effect
                if (winParticleSystem.isStopped)
                {
                    winParticleSystem.Play();
                }

                // Show the summary info
                showWinSummaryDialogInfo();
                toStartPageHolder.SetActive(true);
                toNextLevelHolder.SetActive(true);

                // UnityEngine.SceneManagement.SceneManager.LoadScene("StartPage");
                onceFlag = false;
            }
        }
    }

    private void LoadData()
    {
        TextAsset toMatchTextFile = Resources.Load("to_match_words") as TextAsset;
        toMatchWords = toMatchTextFile.text.Split("\n");

        TextAsset toFillCharTextFile = Resources.Load("to_match_char") as TextAsset;
        toFillChars = toFillCharTextFile.text.Split("\n");

        TextAsset rightWordsTextFile = Resources.Load("right_words_all") as TextAsset;
        rightWords = rightWordsTextFile.text.Split("\n");

        toMatchWordsByLevel = new string[ballCount];
        toFillCharsByLevel = new char[ballCount];
        string allWordsOutput = "";
        string allCharsOutput = "";
        HashSet<string> wordSet = new HashSet<string>(ballCount);
        for (int i = 0; i < ballCount; i++)
        {
            int index = Random.Range(0, 4);
            string aWord = rightWords[Random.Range(0, rightWords.Length)];
            while (wordSet.Contains(aWord))
            {
                aWord = rightWords[Random.Range(0, rightWords.Length)];
            }
            wordSet.Add(aWord);
            char toFillChar = aWord.ToCharArray()[index];
            toMatchWordsByLevel[i] = aWord.Replace(toFillChar, '?');
            toFillCharsByLevel[i] = toFillChar;

            allWordsOutput = allWordsOutput + aWord + ", ";
            allCharsOutput = allCharsOutput + toFillChar + ", ";
        }
        Debug.Log("toMatchWordsByLevel=" + allWordsOutput + ", toFillCharsByLevel=" + allCharsOutput);
    }

    public string getRandomWord()
    {
        // string word = toMatchWords[Random.Range(0, toMatchWords.Length)];
        string word = toMatchWordsByLevel[Random.Range(0, toMatchWordsByLevel.Length)];
        return word.ToLower().Trim();
    }

    public string getToFillChar()
    {
        // string fillChar = toFillChars[Random.Range(0, toFillChars.Length)];
        string fillChar = toFillCharsByLevel[Random.Range(0, toFillCharsByLevel.Length)].ToString();
        return fillChar.ToLower().Trim();
    }

    public bool IsRightWord(string word)
    {
        for (int i = 0; i < rightWords.Length; i++)
        {
            if (rightWords[i] == word)
            {
                return true;
            }
        }
        return false;
    }

    private void CreateNewBall(int index)
    {
        switch (GetRandomBallColor())
        {
            case BallColor.red:
                InstatiateBall(redBall, index);
                break;

            case BallColor.green:
                InstatiateBall(greenBall, index);
                break;

            case BallColor.blue:
                InstatiateBall(blueBall, index);
                break;

            case BallColor.yellow:
                InstatiateBall(yellowBall, index);
                break;
        }
    }

    public static BallColor GetRandomBallColor()
    {
        int rInt = Random.Range(0, 3);
        return (BallColor)rInt;
    }

    private void InstatiateBall(GameObject ballGameObject, int index)
    {
        GameObject go = Instantiate(ballGameObject, wordSpawnPosition.position, Quaternion.identity, ballsContainerGO.transform);
        go.SetActive(false);
        if (go.GetComponent<WordController>() != null)
        {
            string wordText = toMatchWordsByLevel[index];
            // string wordText = getRandomWord();
            go.GetComponent<WordController>().setWord(wordText);
            Debug.Log("Generated a random word=" + wordText);
        }
        ballList.Add(go.gameObject);
    }

    // Move the active section of balls along the path
    private void MoveAllBallsAlongPath()
    {
        int movingBallCount = 1;
        distance = distance + speed * Time.deltaTime;
        if (distance <= splineLength)
        {
            var t = Mathf.Clamp01(distance / splineLength);
            Vector3 position = spline.EvaluatePosition(splinePath, t);
            Quaternion rotation = Quaternion.identity;

            // Correct forward and up vectors based on axis remapping parameters
            var remappedForward = ballList[headballIndex].transform.right;
            var remappedUp = ballList[headballIndex].transform.forward;
            var axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

            // use a head index value which leads the balls on the path
            // This value will be changed when balls are deleted from the path
            Transform headTransform = ballList[headballIndex].transform;
            if (headTransform != null)
                headTransform.DOMove(position, 1);

            Vector3 forward = Vector3.Normalize(spline.EvaluateTangent(splinePath, t));
            Vector3 up = spline.EvaluateUpVector(splinePath, t);
            // ballList[headballIndex].transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;

            if (!ballList[headballIndex].activeSelf)
                ballList[headballIndex].SetActive(true);

            for (int i = headballIndex + 1; i < ballList.Count; i++)
            {
                float currentBallDist = distance - movingBallCount * ballRadius * 2;
                if (currentBallDist <= 0)
                {
                    break;
                }

                t = Mathf.Clamp01(currentBallDist / splineLength);
                Vector3 trailPos = spline.EvaluatePosition(splinePath, t);

                remappedForward = ballList[i].transform.right;
                remappedUp = ballList[i].transform.forward;
                axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

                Transform transform = ballList[i].transform;
                if (transform != null)
                {
                    if (i == addBallIndex && addBallIndex != -1)
                        transform.DOMove(trailPos, 0.5f).SetEase(Ease.Linear);
                    else
                        transform.transform.DOMove(trailPos, 1);
                }

                forward = Vector3.Normalize(spline.EvaluateTangent(splinePath, t));
                up = spline.EvaluateUpVector(splinePath, t);

                if (!ballList[i].activeSelf)
                    ballList[i].SetActive(true);

                movingBallCount++;
            }
        } // if
    }

    public float getScorePerWord()
    {
        return scorePerWord;
    }

    public void addScore(float score)
    {
        ScoreCounter.singletonHolder.RegisterScore(score);
    }

    public void showGuideDialogInfo(string wordText, string toCheckWord)
    {
        dialogNpc.StartDialogue(wordText, toCheckWord);
    }

    public void showWinSummaryDialogInfo()
    {
        string wordSeq = "";
        for (int i = 0; i < matchedWordList.Count; i++)
        {
            wordSeq += matchedWordList[i] + ", ";
        }

        object[] args = new object[] { currentGradeName, currentLevel, ballCount, wordSeq };
        winSummaryDialogNpc.StartSummaryDialogue(args);
    }

    public void addRightWord(string rightWord)
    {
        matchedWordList.Add(rightWord);
    }

    public bool isGamePlaying()
    {
        return onceFlag;
    }
}
