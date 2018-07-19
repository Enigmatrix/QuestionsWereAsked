using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionsWereAsked.Common
{
    public class GameState
    {
        public PlayerState ThisPlayer { get; set; }
        public PlayerState OtherPlayer { get; set; }

        public DateTime TaggedTime { get; set; }

        public string LastAnswer { get;set; }

        public QuestionState Question { get; set; }
        public GameTag Tag { get; set; }
    }

    public enum GameTag
    {
        Started, //give time till question starts
        QuestionStart, //give time when question time is up
        AnswerCorrect, //give time till next question
        Ended,
    }
}
