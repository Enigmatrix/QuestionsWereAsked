using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionsWereAsked.Common.Messages
{
    public class GameStateMessage : MessageBase
    {
        public GameState NewState { get; set; }
    }
}
