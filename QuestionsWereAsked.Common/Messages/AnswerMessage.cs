using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionsWereAsked.Common.Messages
{
    public class AnswerMessage : MessageBase
    {
        public int ChosenIndex { get; set; }
    }
}
