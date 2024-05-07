using WordZuma.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WordZuma.Core
{
	public class GameplayManager : MonoBehaviour
	{
        #region START_METHODS

        #region START_VARIABLES
        public static GameplayManager Instance;

        [HideInInspector] public bool hasGameFinished;

        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private GameObject _winText;
        [SerializeField] private SpriteRenderer _clickHighlight;

        private void Awake()
        {
            Instance = this;

            hasGameFinished = false;
            _winText.SetActive(false);
            _titleText.gameObject.SetActive(true);
            _titleText.text = GameManager.Instance.GradeName + 
                " - " + GameManager.Instance.CurrentLevel.ToString();

            CurrentLevelData = GameManager.Instance.GetLevel();

            // SpawnBoard();
        }

        #endregion

        #region BOARD_SPAWN

        [SerializeField] private SpriteRenderer _boardPrefab, _bgCellPrefab;

        private void SpawnBoard()
        {
            
        }
        
        #endregion

        private LevelData CurrentLevelData;

        #endregion

        #region WIN_CONDITION

        private void CheckWin()
        {
            // bool IsWinning = true;

            // foreach (var item in _nodes)
            // {
            //     item.SolveHighlight();
            // }

            // foreach (var item in _nodes)
            // {
            //     IsWinning &= item.IsWin;
            //     if(!IsWinning)
            //     {
            //         return;
            //     }
            // }

            GameManager.Instance.UnlockLevel();

            _winText.gameObject.SetActive(true);
            _clickHighlight.gameObject.SetActive(false);

            hasGameFinished = true;
        }

        #endregion

        #region BUTTON_FUNCTIONS

        public void ClickedBack()
        {
            GameManager.Instance.GoToMainMenu();
        }

        public void ClickedRestart()
        {
            GameManager.Instance.GoToGameplay();
        }

        public void ClickedNextLevel()
        {
            if (!hasGameFinished) return;
            
            GameManager.Instance.GoToGameplay();
        }

        #endregion
    }
}
