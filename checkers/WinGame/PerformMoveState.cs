using Checkers.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.WinGame
{
    class PerformMoveState : IMoveState
    {
        protected GameBoard gameBoard;
        protected Pawn movedPawn;
        protected List<PawnMove> moveList;
        protected HashSet<Dot> fieldSet;
        protected int moveStage = 0;

        protected void UpdateMoveList()
        {
            fieldSet = new HashSet<Dot>();
            gameBoard.BoardInterface.UpdateField(new FieldInfo(movedPawn, FieldAct.CHECKED_PAWN));
            foreach (var move in moveList)
            {
                var pos = move.GetPosList().ElementAt(moveStage);
                fieldSet.Add(pos);
                var fieldInfo = new FieldInfo(PawnColor.NONE, pos, FieldAct.MOVE_FIELD);
                gameBoard.BoardInterface.UpdateField(fieldInfo);
            }
        }

        protected void CleanMoveList()
        {
            var mFieldInfo = new FieldInfo(PawnColor.NONE, movedPawn.GetPosition(), FieldAct.INACTIVE);
            gameBoard.BoardInterface.UpdateField(mFieldInfo);
            foreach (var move in moveList)
            {
                var pos = move.GetPosList().ElementAt(moveStage);
                fieldSet.Remove(pos);
                var fieldInfo = new FieldInfo(PawnColor.NONE, pos, FieldAct.INACTIVE);
                gameBoard.BoardInterface.UpdateField(fieldInfo);
            }
        }

        protected void CompleteMove(PawnMove pawnMove)
        {
            gameBoard.BoardInterface.UpdateField(new FieldInfo(movedPawn, FieldAct.INACTIVE));
            gameBoard.ChangeState(new FinishMoveStrategy(gameBoard, pawnMove).ResolveState());
        }

        protected void RemoveCapturedPawn(PawnMove pawnMove)
        {
            if(pawnMove.GetCapturedList().Count <= moveStage)
            {
                return;
            }
            var capPawn = pawnMove.GetCapturedList().ElementAt(moveStage);
            var fieldInfo = new FieldInfo(PawnColor.NONE, capPawn.GetPosition(), FieldAct.INACTIVE);
            gameBoard.BoardInterface.UpdateField(fieldInfo);
        }

        protected void UpdateStage(Dot pos)
        {
            movedPawn = movedPawn.GetMovedToPos(pos);
            moveList = moveList.Where(it => it.GetPosList().ElementAt(moveStage).Equals(pos)).ToList();
            RemoveCapturedPawn(moveList.First());
            moveStage++;
            if(moveList.Where(it => it.GetPosList().Count() > moveStage).Count() == 0)
            {
                CompleteMove(moveList.First());
                return;
            }
            UpdateMoveList();
        }

        public PerformMoveState(GameBoard gameBoard, Pawn movedPawn)
        {
            this.gameBoard = gameBoard;
            this.movedPawn = movedPawn;
        }

        public void InitState()
        {
            moveList = gameBoard.BoardEngine.GetMoveList();
            moveList = moveList.Where(it => it.MovedPawn == movedPawn).ToList();
            UpdateMoveList();
        }

        public void HandleEvent(Dot pos)
        {
            if(fieldSet.Contains(pos) == false)
            {
                return;
            }
            CleanMoveList();
            UpdateStage(pos);
        }

    }
}