using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfQuestionnaire.MVVM.Models;

namespace WpfQuestionnaire.MVVM.ViewModel
{
    public static class FileLoader
    {
        public static string VideoPath
        {
            get
            {
                string directory = Directory.GetCurrentDirectory() + "\\Video";
                string videoPath = Directory.GetFiles(directory).First();
                return videoPath;
            }
        }
        public static List<RadioButtonQuestion> LoadTeenQuestions()
        {
            string directory = Directory.GetCurrentDirectory() + "\\TeenQuestions";
            string[] questionsPaths = Directory.GetFiles(directory);
            List<RadioButtonQuestion> questions = new List<RadioButtonQuestion>();

            foreach (string questionsPath in questionsPaths)
            {
                string questionJson = File.ReadAllText(questionsPath);

                try
                {
                    Question question = JsonSerializer.Deserialize<Question>(questionJson);
                    questions.Add(new RadioButtonQuestion(question));
                }
                catch
                {

                }
            }

            return questions;
        }
        public static List<TwoButtonsQuestion> LoadChildQuestions()
        {
            string directory = Directory.GetCurrentDirectory() + "\\ChildQuestions";
            string[] questionsPaths = Directory.GetFiles(directory);
            List<TwoButtonsQuestion> questions = new List<TwoButtonsQuestion>();

            foreach (string questionsPath in questionsPaths)
            {
                string questionJson = File.ReadAllText(questionsPath);

                try
                {
                    Question question = JsonSerializer.Deserialize<Question>(questionJson);
                    questions.Add(new TwoButtonsQuestion(question));
                }
                catch
                {

                }
            }
            
            return questions;
        }
        public static List<TeenResult> LoadTeenResults()
        {
            string directory = Directory.GetCurrentDirectory() + "\\TeenResults";
            string[] resultsPaths = Directory.GetFiles(directory);

            List<TeenResult> results = new List<TeenResult>();
            foreach (string questionsPath in resultsPaths)
            {
                string resultJson = File.ReadAllText(questionsPath);

                try
                {
                    TeenResult result = JsonSerializer.Deserialize<TeenResult>(resultJson);
                    results.Add(result);
                }
                catch
                {

                }
            }

            return results;
        }
        public static List<ChildResult> LoadChildResults()
        {
            string directory = Directory.GetCurrentDirectory() + "\\ChildResults";
            string[] resultsPaths = Directory.GetFiles(directory);

            List<ChildResult> results = new List<ChildResult>();
            foreach (string questionsPath in resultsPaths)
            {
                string resultJson = File.ReadAllText(questionsPath);

                try
                {
                    ChildResult result = JsonSerializer.Deserialize<ChildResult>(resultJson);
                    results.Add(result);
                }
                catch
                {

                }
            }

            return results;
        }
    }
}
