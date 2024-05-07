using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController : MonoBehaviour
{

    private const string MainMenu = "StartPage";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu);
    }

    public void ToGameplayByLevel(string levelSceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelSceneName);
    }
}
