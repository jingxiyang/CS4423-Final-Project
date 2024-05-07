using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCounterText;
    public static ScoreCounter singletonHolder;
    float tokensCollected = 0f;

    void Awake()
    {
        if (singletonHolder != null)
        {
            Destroy(this.gameObject);
        }
        singletonHolder = this;
    }
    void Start()
    {

    }

    public void RegisterScore(float score)
    {
        tokensCollected += score;
        scoreCounterText.text = tokensCollected.ToString();
    }
}
