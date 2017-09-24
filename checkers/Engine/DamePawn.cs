using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Engine
{
    class DamePawn : Pawn
    {
        public DamePawn(Dot pos, PawnColor pawnColor) : base(pos, pawnColor) { }

        protected Pawn GetCapturedPawn(Dot pos, BoardState boardState, Dot dir)
        {
            pos = pos.GetMoved(dir);
            while (boardState.IsOnBoard(pos) && boardState.GetColor(pos) == PawnColor.NONE)
            {
                pos = pos.GetMoved(dir);
            }
            if (boardState.IsOnBoard(pos) == false || IsOponentColor(boardState.GetColor(pos)) == false)
            {
                return null;
            }
            var dest = pos.GetMoved(dir);
            if(boardState.IsOnBoard(dest) == false || boardState.GetColor(dest) != PawnColor.NONE)
            {
                return null;
            }
            return boardState.GetPawn(pos);
        }

        protected override bool CanCapture(Dot pos, BoardState boardState, Dot dir)
        {
            return (GetCapturedPawn(pos, boardState, dir) != null);
        }

        protected List<PawnMove> GetNoneCaptureList(Dot pos, BoardState boardState, Dot dir)
        {
            var result = new List<PawnMove>();
            pos = pos.GetMoved(dir);
            while(boardState.IsOnBoard(pos) && boardState.GetColor(pos) == PawnColor.NONE)
            {
                result.Add(new PawnMove(pos, this, false));
                pos = pos.GetMoved(dir);
            }
            return result;
        }

        protected override List<PawnMove> GetNoneCaptureList(Dot pos, BoardState boardState)
        {
            var result = new List<PawnMove>();
            result.AddRange(GetNoneCaptureList(pos, boardState, new Dot(1, 1)));
            result.AddRange(GetNoneCaptureList(pos, boardState, new Dot(-1, -1)));
            result.AddRange(GetNoneCaptureList(pos, boardState, new Dot(-1, 1)));
            result.AddRange(GetNoneCaptureList(pos, boardState, new Dot(1, -1)));
            return result;
        }

        protected override List<PawnMove> GetCaptureList(Dot pos, BoardState bState, Dot dir, PawnMove pMove)
        {
            var capPawn = GetCapturedPawn(pos, bState, dir);
            if (capPawn == null)
            {
                return new List<PawnMove>();
            }
            pos = capPawn.GetPosition();
            if (pMove.GetCapturedList().Contains(capPawn))
            {
                return GetCaptureList(pos, bState, dir, pMove);
            }
            pos = pos.GetMoved(dir);
            var result = new List<PawnMove>();
            while(bState.IsOnBoard(pos) && bState.GetColor(pos) == PawnColor.NONE)
            {
                var destMove = pMove.CreateIncrMove(pos, capPawn, false);
                result.AddRange(GetCaptureList(pos, bState, destMove));
                pos = pos.GetMoved(dir);
            }
            return result;
        }

        public override Pawn GetMovedToPos(Dot pos)
        {
            return new DamePawn(pos, pawnColor);
        }

        public override bool IsDame()
        {
            return true;
        }

        public override Pawn GetPromoted()
        {
            return this;
        }
    }
}
