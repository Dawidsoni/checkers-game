using Checkers.Engine;
using System.Linq;

namespace Checkers.WinGame
{
    class FinishMoveStrategy
    {
        protected GameBoard gameBoard;
        protected PawnMove pawnMove;

        protected void TurnDame()
        {
            var pos = pawnMove.GetPosList().Last();
            var pawn = pawnMove.MovedPawn.GetPromoted().GetMovedToPos(pos);
            var fieldInfo = new FieldInfo(pawn, FieldAct.INACTIVE);
            gameBoard.BoardInterface.UpdateField(fieldInfo);
        }

        public FinishMoveStrategy(GameBoard gameBoard, PawnMove pawnMove)
        {
            this.gameBoard = gameBoard;
            this.pawnMove = pawnMove;
        }

        public IMoveState ResolveState()
        {
            gameBoard.BoardEngine.PerformMove(pawnMove);
            var winnerColor = gameBoard.BoardEngine.GetWinnerColor();
            if (winnerColor != PawnColor.NONE)
            {
                return new FinishGameState(gameBoard, winnerColor);
            }
            else if (pawnMove.TurnDame)
            {
                TurnDame();
            }
            return new ChooseMoveState(gameBoard);
        }
    }
}