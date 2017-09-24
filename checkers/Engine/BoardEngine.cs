using System;
using System.Collections.Generic;

namespace Checkers.Engine
{
    class BoardEngine : Board
    {
        protected int moveCount = 0;
        protected int moveListCount = -1;
        protected PawnColor pawnColor = PawnColor.WHITE;
        protected List<PawnMove> moveList;

        protected void SwapPawnColor()
        {
            if(pawnColor == PawnColor.WHITE)
            {
                pawnColor = PawnColor.BLACK;
            }
            else if(pawnColor == PawnColor.BLACK)
            {
                pawnColor = PawnColor.WHITE;
            } 
        }

        public PawnColor GetWinnerColor()
        {
            if(GetWhiteList().Count == 0)
            {
                return PawnColor.BLACK;
            }
            else if(GetMoveList().Count == 0 && pawnColor == PawnColor.WHITE)
            {
                return PawnColor.BLACK;
            }
            else if(GetBlackList().Count == 0)
            {
                return PawnColor.WHITE;
            }
            else if(GetMoveList().Count == 0 && pawnColor == PawnColor.BLACK)
            {
                return PawnColor.WHITE;
            }
            else
            {
                return PawnColor.NONE;
            }
        }

        public override List<PawnMove> GetMoveList()
        {
            if(moveListCount < moveCount)
            {
                moveList = GetMoveList(pawnColor);
                moveListCount = moveCount;
            }
            return moveList;
        }

        public override void PerformMove(PawnMove pawnMove)
        {
            if(moveList.Contains(pawnMove) == false)
            {
                throw new ArgumentException("Invalid move");
            }
            UpdateBoard(pawnMove);
            moveCount++;
            SwapPawnColor();
        }
    }
}
