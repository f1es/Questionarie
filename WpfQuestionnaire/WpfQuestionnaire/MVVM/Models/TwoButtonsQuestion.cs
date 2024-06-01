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
    public class TwoButtonsQuestion : ObservableObject
    {
        private Question _question;
        private string _questionMessage = "";
        private List<Button> _answers = new List<Button>();
        private int _answerScore = -1;
        
        public Question Question 
        { 
            get => _question; 
            set => _question = value; 
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
        public List<Button> ButtonAnswers 
        { 
            get => _answers; 
            set => _answers = value; 
        }
        public int AnswerIndex
        {
            get => _answerScore;
            set
            {
                _answerScore = value;
            }
        }

        public TwoButtonsQuestion(Question question)
        {
            question.InitializeScoreDictionary();
            InitizlizeQuestion(question);
        }

        public void InitizlizeQuestion(Question question)
        {
            _question = question;
            QuestionMessage = question.QuestionString;

            foreach (var answer in _question.Answers)
            {
                var textBlock = new TextBlock();
                textBlock.Text = answer;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Width = double.NaN;
                textBlock.Height = double.NaN;
                textBlock.MaxWidth = 800;
                textBlock.MinWidth = 800;

                Button button = new Button();
                button.Content = textBlock;

                var style = Application.Current.Resources["AnswerButtonStyle"] as Style;
                button.Style = style;

                ButtonAnswers.Add(button);
            }
        }
    }
}
