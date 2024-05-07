using UnityEngine;
using UnityEngine.UI;

namespace WordZuma.Core
{
    public class GradeButton : MonoBehaviour
    {
        [SerializeField] private string _gradeName;
        [SerializeField] private Color _gradeColor;
        [SerializeField] private int _gradeNumber;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(ClickedButton);
        }

        private void ClickedButton()
        {
            GameManager.Instance.CurrentGrade = _gradeNumber;
            GameManager.Instance.GradeName = _gradeName;
            MainMenuManager.Instance.ClickedGrade(_gradeName, _gradeColor);
        }

    }
}
