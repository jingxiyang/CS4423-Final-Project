using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogNpc : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textHolder;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed = 0.03f;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textHolder.text = string.Empty;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (textHolder.text == lines[index])
        //     {
        //         NextLine();
        //     }
        //     else
        //     {
        //         StopAllCoroutines();
        //         textHolder.text = lines[index];
        //     }
        // }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textHolder.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void StartDialogue(string info1, string info2)
    {
        textHolder.text = string.Empty;
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLineWithFormat(info1, info2));
    }

    IEnumerator TypeLineWithFormat(string info1, string info2)
    {
        string lineText = string.Format(lines[index], info1, info2);
        foreach (char c in lineText.ToCharArray())
        {
            textHolder.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        gameObject.SetActive(false);
    }

    public void StartSummaryDialogue(params object[] args)
    {
        textHolder.text = string.Empty;
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLineWithFormatByParams(args));
    }

    IEnumerator TypeLineWithFormatByParams(params object[] args)
    {
        string lineText = string.Format(lines[index], args);
        foreach (char c in lineText.ToCharArray())
        {
            textHolder.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textHolder.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
