using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WordController : MonoBehaviour
{

    [SerializeField] TextMeshPro holdText;

    [SerializeField] GameObject floatingTextPrefab;

    // [SerializeField] private float destroyTime = 2f;

    private MoveWords moveWordsScript;

    private bool matchedFlag;

    void Start()
    {
        moveWordsScript = GameObject.FindObjectOfType<MoveWords>();
        matchedFlag = false;
        Debug.Log("Instante a word=" + holdText.text);
    }
    void Update()
    {
        if (matchedFlag)
        {
            handleRightWord();
        }
    }

    private void handleRightWord()
    {
        // Destroy(gameObject, destroyTime);
    }

    public void setWord(string newText)
    {
        if (holdText != null)
        {
            holdText.SetText(newText);
        }
    }

    public string getWord()
    {
        string wordText = null;
        if (holdText != null)
        {
            wordText = holdText.text;
        }
        return wordText;
    }

    public void setMatchedFlag(bool flag)
    {
        matchedFlag = flag;
    }

    public bool getMatchedFlag()
    {
        return matchedFlag;
    }

    public void showScore(float scorePerWord)
    {
        if (floatingTextPrefab != null)
        {
            GameObject floatingTextParent = Instantiate(floatingTextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            floatingTextParent.GetComponentInChildren<TextMesh>().text = "+" + scorePerWord;
        }
    }
}
