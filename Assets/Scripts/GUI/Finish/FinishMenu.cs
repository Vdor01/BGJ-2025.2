using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Finish
{
    [AddComponentMenu("BGJ 2025.2/GUI/Finish/Finish menu")]
    public class FinishMenu : Menu
    {
        // Fields
        private const string _DaysPlaceholder = "#DAYS#";
        private const string _DaysQuantifierPlaceholder = "#DAYS_QUANTIFIER#";
        private const string _DaysQuantifier = "day";
        private const string _CookiesPlaceholder = "#COOKIES#";
        private const string _CookiesQuantifierPlaceholder = "#COOKIES_QUANTIFIER#";
        private const string _CookiesQuantifier = "cookie";
        private const string _TasksPlaceholder = "#TASKS#";
        private const string _TasksQuantifierPlaceholder = "#TASKS_QUANTIFIER#";
        private const string _TasksQuantifier = "task";

        [SerializeField] private TextMeshProUGUI _summaryLabel;
        [SerializeField][TextArea(7, 10)] private string _baseText;


        // Methods
        public override void Open()
        {
            SetUp();

            base.Open();
        }

        public override void SetUp()
        {
            _summaryLabel.SetText(_baseText
                .Replace(_DaysPlaceholder, Player.Data.Days.ToString())
                .Replace(_DaysQuantifierPlaceholder, $"{_DaysQuantifier}{(Player.Data.Days != 1 ? "s" : "")}")
                .Replace(_CookiesPlaceholder, Player.Data.Cookies.ToString())
                .Replace(_CookiesQuantifierPlaceholder, $"{_CookiesQuantifier}{(Player.Data.Cookies != 1 ? "s" : "")}")
                .Replace(_TasksPlaceholder, Player.Data.Tasks.ToString())
                .Replace(_TasksQuantifierPlaceholder, $"{_TasksQuantifier}{(Player.Data.Tasks != 1 ? "s" : "")}"));
        }

        public override void Cancel()
        {

        }

        public void OpenMain()
        {
            _gui.TransitionPanel.TransitionInAndOut(() =>
            {
                Close();
                _gui.MainMenu.Open();
            });
        }
    }
}