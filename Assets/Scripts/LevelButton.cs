using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordZuma.Core
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] TMP_Text _levelText;
        [SerializeField] private Color _inactiveColor;

        private bool isLevelUnlocked;
        private int currentLevel;

        private void Awake()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnEnable()
        {
            MainMenuManager.Instance.LevelOpened += LevelOpened;
        }

        private void OnDisable()
        {
            MainMenuManager.Instance.LevelOpened -= LevelOpened;
        }

        private void LevelOpened()
        {
            string gameObjectName = gameObject.name;
            string[] parts = gameObjectName.Split('_');
            _levelText.text = parts[parts.Length - 1];
            currentLevel = int.Parse(_levelText.text);
            isLevelUnlocked = GameManager.Instance.IsLevelUnlocked(currentLevel);
            _image.color = isLevelUnlocked ? MainMenuManager.Instance.CurrentColor : _inactiveColor;
        }

        private void Clicked()
        {
            if (!isLevelUnlocked)
                return;

            GameManager.Instance.CurrentLevel = currentLevel;
            Debug.Log("Will enter level=" + GameManager.Instance.getLevelSign());
            PlayerPrefs.SetString("currentGradeName", GameManager.Instance.GradeName);
            PlayerPrefs.SetString("currentGrade", GameManager.Instance.CurrentGrade.ToString());
            PlayerPrefs.SetString("currentLevel", GameManager.Instance.CurrentLevel.ToString());
            PlayerPrefs.SetString("currentColor", MainMenuManager.Instance.CurrentColor.ToString());
            PlayerPrefs.SetString("nextLevelSceneSign", GameManager.Instance.getNextLevelSign());
            GameManager.Instance.GoToGameplay();
        }
    }
}
