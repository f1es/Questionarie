using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQuestionnaire.MVVM.Models
{
    public class Question
    {
        private string _question;
        private string _backgroundSource;
        private List<string> _answers = new List<string>();
        private List<int> _answersScore = new List<int>();
        private Dictionary<string, int> _scoreDictionary = new Dictionary<string, int>();
        public string QuestionString
        {
            get => _question;
            set => _question = value;
        }
        public string BackgroundSource
        {
            get => _backgroundSource;
            set => _backgroundSource = value;
        }
        public List<string> Answers 
        {
            get => _answers;
            set => _answers = value; 
        }
        public List<int> AnswersScore
        {
            get => _answersScore;
            set => _answersScore = value;
        }
        public void InitializeScoreDictionary()
        {
            for (int i = 0; i < _answers.Count; i++)
            {
                _scoreDictionary.Add(_answers[i], _answersScore[i]);
            }
        }
        public int GetAnswersScore(string answer)
        {
            return _scoreDictionary[answer];
        }
    }
}
