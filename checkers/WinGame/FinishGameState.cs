using System;
using Checkers.Engine;

namespace Checkers.WinGame
{
    class FinishGameState : IMoveState
    {
        protected GameBoard gameBoard;
        protected PawnColor winnerColor;

        public FinishGameState(GameBoard gameBoard, PawnColor winnerColor)
        {
            this.gameBoard = gameBoard;
            this.winnerColor = winnerColor;
        }

        public void InitState()
        {
            gameBoard.BoardInterface.FinishGame(winnerColor);
        }

        public void HandleEvent(Dot pos)
        {
            gameBoard.ResetGame();
        }
    }
}
