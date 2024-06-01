using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfQuestionnaire.MVVM.Models.Interfaces;

namespace WpfQuestionnaire.MVVM.Models
{
    public class TeenResult : IResult
    {
        private string _resultMessage;
        private string _header;
        private string _backgroundSource;
        private int _minScore;
        private int _maxScore;
        public string ResultMessage 
        { 
            get => _resultMessage; 
            set => _resultMessage = value; 
        }
        public string Header
        {
            get => _header;
            set => _header = value;
        }
        public string BackgroundSource
        {
            get => _backgroundSource;
            set => _backgroundSource = value;
        }
        public int MinScore 
        { 
            get => _minScore; 
            set => _minScore = value; 
        }
        public int MaxScore 
        { 
            get => _maxScore; 
            set => _maxScore = value; 
        }
    }
}
