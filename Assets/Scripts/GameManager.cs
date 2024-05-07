using WordZuma.Common;
using System.Collections.Generic;
using UnityEngine;

namespace WordZuma.Core
{
    public class GameManager : MonoBehaviour
    {
        #region START_METHODS

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
                DontDestroyOnLoad(gameObject);
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            CurrentGrade = 1;
            CurrentLevel = 1;

            Levels = new Dictionary<string, LevelData>();

            foreach (var item in _allLevels.Levels)
            {
                Levels[item.LevelName] = item;
            }
        }


        #endregion

        #region GAME_VARIABLES

        [HideInInspector]
        public int CurrentGrade;

        [HideInInspector]
        public int CurrentLevel;

        [HideInInspector]
        public string GradeName;

        public bool IsLevelUnlocked(int level)
        {
            string levelName = "Level" + CurrentGrade.ToString() + level.ToString();

            if (level == 1)
            {
                PlayerPrefs.SetInt(levelName, 1);
                return true;
            }

            if (PlayerPrefs.HasKey(levelName))
            {
                return PlayerPrefs.GetInt(levelName) == 1;
            }

            PlayerPrefs.SetInt(levelName, 0);
            return false;
        }

        public void UnlockLevel()
        {
            CurrentLevel++;

            if (CurrentLevel == 6)
            {
                CurrentLevel = 1;
                CurrentGrade++;

                if (CurrentGrade == 3)
                {
                    CurrentGrade = 1;
                    GoToMainMenu();
                }
            }

            string levelName = "Level" + CurrentGrade.ToString() + CurrentLevel.ToString();
            PlayerPrefs.SetInt(levelName, 1);
        }

        #endregion

        #region LEVEL_DATA

        [SerializeField]
        private LevelData _defaultLevel;

        [SerializeField]
        private LevelList _allLevels;

        private Dictionary<string, LevelData> Levels;

        public LevelData GetLevel()
        {
            string levelName = "Level" + CurrentGrade.ToString() + CurrentLevel.ToString();

            if (Levels.ContainsKey(levelName))
            {
                return Levels[levelName];
            }

            return _defaultLevel;
        }

        public string getLevelSign()
        {
            return "Level" + CurrentGrade.ToString() + CurrentLevel.ToString();
        }

        public string getNextLevelSign()
        {
            int nextLevel = CurrentLevel + 1;
            if (nextLevel <= 6)
            {
                return "Level" + CurrentGrade.ToString() + nextLevel.ToString();
            }
            else
            {
                return "Level" + (CurrentGrade + 1).ToString() + "1";
            }

        }

        #endregion

        #region SCENE_LOAD

        private const string MainMenu = "StartPage";
        private const string Gameplay = "PreK-level-1";

        public void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu);
        }

        public void GoToGameplay()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(getLevelSign());
        }

        #endregion
    }
}
