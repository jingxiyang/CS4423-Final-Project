using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class RotateHandler : MonoBehaviour
{
    [SerializeField] GameObject initBall;

    [SerializeField] GameObject frog;

    [SerializeField] Transform ballSpawnPosition;
    [SerializeField] TextMeshPro nextCharTMP;

    private GameObject instanceBall;

    private Vector3 lookPos;

    private MoveWords moveWordsScript;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    private string nextChar;

    // Start is called before the first frame update
    void Start()
    {
        moveWordsScript = GameObject.FindObjectOfType<MoveWords>();
        nextChar = moveWordsScript.getToFillChar();
        nextCharTMP.SetText(nextChar);
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerAlongMousePosition();
        HandleCharBallShooting();
    }

    private void FixedUpdate()
    {

    }

    // Rotate the launcher along the mouse position
    private void RotatePlayerAlongMousePosition()
    {
        // Rotate the frog towards Mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)frog.transform.position).normalized;
        frog.transform.right = direction;

        // Debug.Log(frog.transform.rotation.z);

        // Flip the frog if it reaches a 90 degree threshod
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = new Vector3(1f, 1f, 1f);
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }
        frog.transform.localScale = localScale;
    }

    // Set balls postions and forward too the launcher
    private void HandleCharBallShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && moveWordsScript.isGamePlaying())
        {
            // Spawn char ball
            instanceBall = Instantiate(initBall, ballSpawnPosition.position, frog.transform.rotation);
            instanceBall.SetActive(true);

            instanceBall.tag = "NewBall";
            instanceBall.gameObject.layer = LayerMask.NameToLayer("Default");

            SetBallColor(instanceBall);
            if (instanceBall.GetComponent<BallColliderOpt>() != null)
            {
                instanceBall.GetComponent<BallColliderOpt>().setToMatchChar(nextChar);
                nextChar = moveWordsScript.getToFillChar();
                nextCharTMP.SetText(nextChar);
            }

            GetComponent<AudioSource>().Play();

            Debug.Log("HandleCharBallShooting char=" + instanceBall.GetComponent<BallColliderOpt>().getToMatchChar());
        }
    }

    private void SetBallColor(GameObject go)
    {
        BallColor randColor = MoveWords.GetRandomBallColor();
        switch (randColor)
        {
            case BallColor.red:
                go.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case BallColor.green:
                go.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case BallColor.blue:
                go.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case BallColor.yellow:
                go.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
        }
    }

    private void SetRandomColor(GameObject go)
    {
        Color color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        go.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    private void CreateBall()
    {
        instanceBall = Instantiate(initBall, frog.transform.position, Quaternion.identity);
        instanceBall.SetActive(true);

        instanceBall.tag = "NewBall";
        instanceBall.gameObject.layer = LayerMask.NameToLayer("Default");

        SetBallColor(instanceBall);
        // SetBallFillChar(instanceBall);
        if (instanceBall.GetComponent<BallColliderOpt>() != null)
        {
            instanceBall.GetComponent<BallColliderOpt>().setToMatchChar(moveWordsScript.getToFillChar());
        }

        Debug.Log("New ball with a fill char=" + instanceBall.GetComponent<BallColliderOpt>().getToMatchChar());
    }

    private void SetBallFillChar(GameObject go)
    {
        go.GetComponent<BallColliderOpt>().setToMatchChar("new");
        Debug.Log("a new ball with fill char=" + "new");
    }


}
