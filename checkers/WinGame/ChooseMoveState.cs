using System.Linq;
using Checkers.Engine;
using System.Collections.Generic;

namespace Checkers.WinGame
{
    class ChooseMoveState : IMoveState
    {
        protected GameBoard gameBoard;
        protected Dictionary<Dot, Pawn> movePawnMap;

        protected void CleanState()
        {
            foreach(var pawn in movePawnMap.Values)
            {
                gameBoard.BoardInterface.UpdateField(new FieldInfo(pawn, FieldAct.INACTIVE));
            }
        }

        public ChooseMoveState(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public void HandleEvent(Dot pos)
        {
            if(movePawnMap.ContainsKey(pos) == false)
            {
                return;
            }
            CleanState();
            var movedPawn = movePawnMap[pos];
            gameBoard.ChangeState(new PerformMoveState(gameBoard, movedPawn));
        }

        public void InitState()
        {
            var moveList = gameBoard.BoardEngine.GetMoveList();
            if (gameBoard.BoardEngine.GetWinnerColor() != PawnColor.NONE)
            {
                var winnerColor = gameBoard.BoardEngine.GetWinnerColor();
                gameBoard.ChangeState(new FinishGameState(gameBoard, winnerColor));
            }
            var movePawnList = moveList.Select((it) => it.MovedPawn).Distinct().ToList();
            movePawnMap = movePawnList.ToDictionary((it) => it.GetPosition(), (it) => it);
            foreach (var pawn in movePawnMap.Values)
            {
                gameBoard.BoardInterface.UpdateField(new FieldInfo(pawn, FieldAct.MOVE_PAWN));
            }
        }
    }
}
