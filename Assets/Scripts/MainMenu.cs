using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordZuma.Core
{
    public class MainMenu : MonoBehaviour
    {
        public void SetGameOptions()
        {
            // TODO: 
            // SceneManager.LoadScene("OptionsPage");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
