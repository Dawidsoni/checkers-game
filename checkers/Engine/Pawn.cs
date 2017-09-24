using System;
using System.Collections.Generic;

namespace Checkers.Engine
{
    abstract class Pawn
    {
        protected Dot pawnPos;
        protected readonly PawnColor pawnColor;

        protected bool CanCapture(Dot pos, BoardState boardState)
        {
            Func<int, int, bool> capFunc = ((x, y) => CanCapture(pos, boardState, new Dot(x, y)));
            return (capFunc(1, 1) || capFunc(-1, -1) || capFunc(1, -1) || capFunc(-1, 1));
        }

        protected List<PawnMove> GetCaptureList(Dot pos, BoardState boardState, PawnMove pawnMove)
        {
            var result = new List<PawnMove>();
            result.Add(pawnMove);
            result.AddRange(GetCaptureList(pos, boardState, new Dot(1, 1), pawnMove));
            result.AddRange(GetCaptureList(pos, boardState, new Dot(-1, -1), pawnMove));
            result.AddRange(GetCaptureList(pos, boardState, new Dot(-1, 1), pawnMove));
            result.AddRange(GetCaptureList(pos, boardState, new Dot(1, -1), pawnMove));
            return result;
        }

        public Pawn(Dot pawnPos, PawnColor pawnColor)
        {
            this.pawnPos = pawnPos;
            this.pawnColor = pawnColor;
        }

        public Dot GetPosition()
        {
            return pawnPos;
        }

        public PawnColor GetPawnColor()
        {
            return pawnColor;
        }

        public bool IsOponentColor(PawnColor oppColor)
        {
            return (pawnColor != oppColor && oppColor != PawnColor.NONE);
        }

        public List<PawnMove> GetMoveList(BoardState boardState)
        {
            if (CanCapture(pawnPos, boardState) == false)
            {
                return GetNoneCaptureList(pawnPos, boardState);
            }
            return GetCaptureList(pawnPos, boardState, new PawnMove(this));
        }

        protected abstract bool CanCapture(Dot pawnPos, BoardState boardState, Dot dir);
        protected abstract List<PawnMove> GetNoneCaptureList(Dot pawnPos, BoardState boardState);
        protected abstract List<PawnMove> GetCaptureList(Dot pos, BoardState bState, Dot dir, PawnMove pMove);
        public abstract Pawn GetMovedToPos(Dot pos);
        public abstract bool IsDame();
        public abstract Pawn GetPromoted();
    }
}
