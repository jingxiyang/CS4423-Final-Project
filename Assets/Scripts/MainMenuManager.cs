using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WordZuma.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance;

        [SerializeField] private GameObject _startPagePanel;
        [SerializeField] private GameObject _gradePanel;
        [SerializeField] private GameObject _levelPanel;
        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _gameTitle;


        private void Awake()
        {
            Instance = this;
            _startPagePanel.SetActive(true);
            _gradePanel.SetActive(false);
            _levelPanel.SetActive(false);
            _optionsPanel.SetActive(false);
        }

        // Start page's Play button
        public void ClickedPlay()
        {
            _startPagePanel.SetActive(false);
            _gradePanel.SetActive(true);
        }

        public void ClickedOptions()
        {
            _background.SetActive(false);
            _gameTitle.SetActive(false);
            _startPagePanel.SetActive(false);
            _optionsPanel.SetActive(true);
        }

        public void ClickedBackToStartPage()
        {
            _startPagePanel.SetActive(true);
            _gradePanel.SetActive(false);
        }

        public void ClickedBackToGradePanel()
        {
            _gradePanel.SetActive(true);
            _levelPanel.SetActive(false);
        }

        public void ClickedBackToStartPageFromOptions()
        {
            _background.SetActive(true);
            _gameTitle.SetActive(true);
            _startPagePanel.SetActive(true);
            _optionsPanel.SetActive(false);
        }

        public UnityAction LevelOpened;

        [HideInInspector]
        public Color CurrentColor;

        [SerializeField]
        private TMP_Text _levelTitleText;
        [SerializeField]
        private Image _levelTitleImage;

        public void ClickedGrade(string gradeName, Color gradeColor)
        {
            _gradePanel.SetActive(false);
            _levelPanel.SetActive(true);
            CurrentColor = gradeColor;
            _levelTitleText.text = gradeName;
            _levelTitleImage.color = CurrentColor;
            LevelOpened?.Invoke();
        }
    }
}

