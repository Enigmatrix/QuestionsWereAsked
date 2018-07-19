using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionsWereAsked.Server
{
    public class Question
    {
        public string Title { get; set; }
        public int AnswerIndex { get; set; }
        public string[] Answers { get; set; }
    }
}
