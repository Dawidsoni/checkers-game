using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Engine
{
    class PiecePawn : Pawn
    {
        protected readonly int moveDir;

        protected override bool CanCapture(Dot pos, BoardState boardState, Dot dir)
        {
            var cap = pos.GetMoved(dir);
            var dest = pos.GetMoved(dir.X * 2, dir.Y * 2);
            int boardSize = boardState.GetBoardSize();
            if(boardState.IsOnBoard(dest) == false)
            {
                return false;
            }
            if(IsOponentColor(boardState.GetColor(cap)) == false)
            {
                return false;
            }
            return (boardState.GetColor(dest) == PawnColor.NONE);
        }

        protected bool IsDameLanePos(Dot pos, BoardState boardState)
        {
            if (moveDir < 0)
            {
                return (pos.X == 0);
            }
            else
            {
                return (pos.X == boardState.GetBoardSize() - 1);
            }
        }

        protected PawnMove GetNoneCaptureMove(Dot pos, BoardState boardState)
        {
            return new PawnMove(pos, this, IsDameLanePos(pos, boardState));
        }

        protected override List<PawnMove> GetNoneCaptureList(Dot pos, BoardState boardState)
        {
            Func<Dot, bool> emptyFunc = ((x) => boardState.GetColor(x) == PawnColor.NONE);
            var result = new List<PawnMove>();
            var move1 = pos.GetMoved(moveDir, 1);
            var move2 = pos.GetMoved(moveDir, -1);
            if(boardState.IsOnBoard(move1) && emptyFunc(move1))
            {
                result.Add(GetNoneCaptureMove(move1, boardState));
            }
            if(boardState.IsOnBoard(move2) && emptyFunc(move2))
            {
                result.Add(GetNoneCaptureMove(move2, boardState));
            }
            return result;
        }

        protected override List<PawnMove> GetCaptureList(Dot pos, BoardState bState, Dot dir, PawnMove pMove)
        {
            if(CanCapture(pos, bState, dir) == false)
            {
                return new List<PawnMove>();
            }
            var capPawn = bState.GetPawn(pos.GetMoved(dir));
            if(pMove.GetCapturedList().Contains(capPawn))
            {
                return new List<PawnMove>();
            }
            var destPos = pos.GetMoved(dir.X * 2, dir.Y * 2);
            bool isDamePos = IsDameLanePos(destPos, bState);
            var destMove = pMove.CreateIncrMove(destPos, capPawn, isDamePos);
            return GetCaptureList(destPos, bState, destMove);
        }

        public PiecePawn(Dot pos, PawnColor pawnColor, int moveDir) : base(pos, pawnColor)
        {
            this.moveDir = moveDir;
        }

        public override Pawn GetMovedToPos(Dot pos)
        {
            return new PiecePawn(pos, pawnColor, moveDir);
        }

        public override bool IsDame()
        {
            return false;
        }

        public override Pawn GetPromoted()
        {
            return new DamePawn(pawnPos, pawnColor);
        }
    }
}
