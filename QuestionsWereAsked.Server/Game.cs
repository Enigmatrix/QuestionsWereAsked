using System;
using System.Net.Sockets;
using System.Threading;
using QuestionsWereAsked.Common;
using QuestionsWereAsked.Common.Messages;

namespace QuestionsWereAsked.Server
{
    public class Game
    {
        public static readonly TimeSpan QuestionDuration = TimeSpan.FromSeconds(10);
        private bool GameEnded = false;

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            GameSendingThread = new Thread(GameMain);
            GameRecving1Thread = new Thread(GameRecv1);
            GameRecving2Thread = new Thread(GameRecv2);
        }

        public DateTime LastQuestionAskedTime { get; set; }
        public Question LastQuestionAsked { get; set; }

        public Player Player1 { get; }
        public Player Player2 { get; }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public Thread GameSendingThread { get; }
        public Thread GameRecving1Thread { get; }
        public Thread GameRecving2Thread { get; }

        public AnswerMessage Player1Answer { get; set; }

        public AnswerMessage Player2Answer { get; set; }

        public DateTime Player1AnswerTime { get; set; }
        public DateTime Player2AnswerTime { get; set; }

        public event Action<Game> End;

        public void SendPlayer1(GameState msg)
        {
            msg.OtherPlayer = new PlayerState
            {
                Nick = Player2.Nick,
                Score = Player2Score
            };
            msg.ThisPlayer = new PlayerState
            {
                Nick = Player1.Nick,
                Score = Player1Score
            };
            Player1.Socket.SendTripleDES(new GameStateMessage {NewState = msg}, Player1.Crypt);
        }

        public void SendPlayer2(GameState msg)
        {
            msg.OtherPlayer = new PlayerState
            {
                Nick = Player1.Nick,
                Score = Player1Score
            };
            msg.ThisPlayer = new PlayerState
            {
                Nick = Player2.Nick,
                Score = Player2Score
            };
            Player2.Socket.SendTripleDES(new GameStateMessage {NewState = msg}, Player2.Crypt);
        }

        private void GameRecv1()
        {
            while (!GameEnded)
                try
                {
                    Player1Answer = Player1.Socket.ReceiveTripleDES<AnswerMessage>(Player1.Crypt);
                    Player1AnswerTime = DateTime.Now;
                }
                catch (SocketException se)
                {
                    EndGame(Player1.Socket);
                }
        }

        private void GameRecv2()
        {
            while (!GameEnded)
                try
                {
                    Player2Answer = Player2.Socket.ReceiveTripleDES<AnswerMessage>(Player2.Crypt);
                    Player2AnswerTime = DateTime.Now;
                }
                catch (SocketException se)
                {
                    EndGame(Player2.Socket);
                }
        }

        public void GameMain()
        {
            SendPlayer1(new GameState {Tag = GameTag.Started});
            SendPlayer2(new GameState {Tag = GameTag.Started});

            Thread.Sleep(500);
            var qns = new QuestionRepo();

            for (var i = 0; i < 5; i++)
            {
                var question = qns.GetQuestion();
                var qnState = new QuestionState
                {
                    Answers = question.Answers,
                    Title = question.Title
                };
                LastQuestionAskedTime = DateTime.Now;

                SendPlayer1(new GameState
                {
                    Tag = GameTag.QuestionStart,
                    Question = qnState,
                    LastAnswer = LastQuestionAsked?.Answers[LastQuestionAsked.AnswerIndex]
                });
                SendPlayer2(new GameState
                {
                    Tag = GameTag.QuestionStart,
                    Question = qnState,
                    LastAnswer = LastQuestionAsked?.Answers[LastQuestionAsked.AnswerIndex]
                });

                Thread.Sleep(QuestionDuration);

                if (Player1Answer?.ChosenIndex == question.AnswerIndex) Player1Score += (int) Score(Player1AnswerTime);

                if (Player2Answer?.ChosenIndex == question.AnswerIndex) Player2Score += (int) Score(Player2AnswerTime);

                Player1Answer = null;
                Player2Answer = null;
                Player1AnswerTime = DateTime.MinValue;
                Player2AnswerTime = DateTime.MinValue;
                LastQuestionAsked = question;
            }
            EndGame();
        }


        private void EndGame(Socket s = null)
        {
            if (GameEnded) return;
            if (s != Player1.Socket)
                SendPlayer1(new GameState {Tag = GameTag.Ended});
            if (s != Player2.Socket)
                SendPlayer2(new GameState {Tag = GameTag.Ended});

            GameEnded = true;
            End?.Invoke(this);
        }

        public double Score(DateTime time)
        {
            return 200 * (1 - (time - LastQuestionAskedTime) / QuestionDuration);
        }

        public void Start()
        {
            GameSendingThread.Start();
            GameRecving1Thread.Start();
            GameRecving2Thread.Start();
        }
    }
}