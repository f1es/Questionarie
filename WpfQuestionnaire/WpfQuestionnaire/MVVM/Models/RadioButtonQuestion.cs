using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfQuestionnaire.MVVM.ViewModel;

namespace WpfQuestionnaire.MVVM.Models
{
    public class RadioButtonQuestion : ObservableObject
    {
        private Question _question;
        private string _questionMessage = "";
        private List<RadioButton> _answers = new List<RadioButton>();

        public Question Question
        {
            get => _question;
        }
        public string QuestionMessage
        {
            get => _questionMessage;
            set
            {
                _questionMessage = value;
                OnPropertyChanged();
            }
        }
        public List<RadioButton> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                OnPropertyChanged();
            }
        }
        public RadioButtonQuestion(Question question)
        {
            question.InitializeScoreDictionary();
            InitizlizeQuestion(question);
        }
        public string GetCheckedAnswer()
        {
            foreach (var answer in _answers)
            {
                if (answer.IsChecked == true)
                {
                    var textBlock = answer.Content as TextBlock;
                    return textBlock.Text;
                }
            }

            return "";
        }
        public int GetCheckedAnswerIndex()
        {
            for (int i = 0; i < _answers.Count; i++)
            {
                if (_answers[i].IsChecked == true)
                    return i;
            }

            return -1;
        }
        public int GetScore()
        {
            int index = GetCheckedAnswerIndex();

            if (index == -1)
                return -1;
            else
                return _question.GetAnswersScore(GetCheckedAnswer());
            //return _question.AnswersScore[GetCheckedAnswer()];
        }
        public void InitizlizeQuestion(Question question)
        {
            _question = question;
            QuestionMessage = question.QuestionString;

            foreach (var answer in question.Answers)
            {
                var textBlock = new TextBlock();
                textBlock.Text = answer;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Width = double.NaN;
                textBlock.Height = double.NaN;

                RadioButton radioButton = new RadioButton();
                radioButton.Content = textBlock;
                radioButton.GroupName = question.QuestionString;

                var style = Application.Current.Resources["RadioButtonStyle"] as Style;
                radioButton.Style = style;

                _answers.Add(radioButton);
            }

            
        }
        public void ResetQuestion()
        {
            foreach (var answer in _answers)
            {
                if (answer.IsChecked == true)
                {
                    answer.IsChecked = false;
                    return;
                }
            }
        }
    }
}
