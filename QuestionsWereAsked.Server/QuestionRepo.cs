using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionsWereAsked.Server
{
    public class QuestionRepo
    {
        private Question[] _questions = {
            new Question
            {
                Title = "1+1",
                Answers = new []{"Window", "2", "22", "11"},
                AnswerIndex = 1
            },
            new Question
            {
                Title = "[] == ![]",
                Answers = new []{"1", "true", "0", "false"},
                AnswerIndex = 1
            },
            new Question
            {
                Title = "!!'false'",
                Answers = new []{"1", "true", "false", "0"},
                AnswerIndex = 1
            },
            new Question
            {
                Title = "'foo' + + 'bar'",
                Answers = new []{"fooNaN", "foobar", "foo1", "!Compiler Error"},
                AnswerIndex = 0
            },
            new Question
            {
                Title = "[1, 2, 3] + [4, 5, 6]",
                Answers = new []{"'1,2,34,5,6'", "[1,2,3,4,5,6]", "[1,2,3,1]", "2"},
                AnswerIndex = 0
            },
            new Question
            {
                Title = "[,,,].length",
                Answers = new []{"0", "1", "3", "4"},
                AnswerIndex = 2
            },
            new Question
            {
                Title = "Number(undefined)",
                Answers = new []{"undefined", "1", "0", "NaN"},
                AnswerIndex = 3
            },
            new Question
            {
                Title = "parseInt('f*ck', 16)",
                Answers = new []{"NaN", "0", "15", "undefined"},
                AnswerIndex = 2
            },

        };

        public QuestionRepo()
        {
            rnd = new Random();
            used = new List<int>();
        }

        private Random rnd { get; set; }
        private List<int> used;
        public Question GetQuestion()
        {
            var r = -1;
            do
            {
                r = rnd.Next(0, _questions.Length);
            } while (used.Contains(r));
            used.Add(r);
            return _questions[r];
        }
    }
}
