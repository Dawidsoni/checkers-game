using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Engine
{
    abstract class Board
    {
        protected const int BOARD_SIZE = 8;
        protected const int PAWN_COUNT = 12;
        protected HashSet<Pawn> pawnList = new HashSet<Pawn>();

        protected void InitWhiteList()
        {
            int rowNum = 0;
            int colNum = 1;
            for (int i = 0; i < PAWN_COUNT; i++)
            {
                pawnList.Add(new PiecePawn(new Dot(rowNum, colNum), PawnColor.WHITE, 1));
                colNum += 2;
                if(colNum >= BOARD_SIZE)
                {
                    rowNum += 1;
                    colNum = (colNum - 1) % 2;
                }
            }
        }

        protected void InitBlackList()
        {
            int rowNum = BOARD_SIZE - 1;
            int colNum = 0;
            for(int i = 0; i < PAWN_COUNT; i++)
            {
                pawnList.Add(new PiecePawn(new Dot(rowNum, colNum), PawnColor.BLACK, -1));
                colNum += 2;
                if(colNum >= BOARD_SIZE)
                {
                    rowNum -= 1;
                    colNum = (colNum - 1) % 2;
                }
            }
        }

        protected List<PawnMove> FilterMaxCaptureList(List<PawnMove> moveList)
        {
            moveList = moveList.Where(it => it.GetPosList().Count > 0).ToList();
            if (moveList.Count() == 0)
            {
                return moveList;
            }
            var maxCap = moveList.Max((it) => it.GetCapturedList().Count());
            return moveList.Where((it) => it.GetCapturedList().Count() == maxCap).ToList();
        }

        protected List<PawnMove> GetMoveList(PawnColor pawnColor)
        {
            var result = new List<PawnMove>();
            var boardState = new BoardState(this);
            var colorList = GetPawnList(pawnColor);
            foreach (var pawn in colorList)
            {
                result.AddRange(pawn.GetMoveList(boardState));
            }
            return FilterMaxCaptureList(result);
        }

        protected void UpdateBoard(PawnMove pawnMove)
        {
            foreach (var pawn in pawnMove.GetCapturedList())
            {
                pawnList.Remove(pawn);
            }
            pawnList.Remove(pawnMove.MovedPawn);
            var movedPos = pawnMove.GetPosList().Last();
            if (pawnMove.TurnDame)
            {
                pawnList.Add(new DamePawn(movedPos, pawnMove.MovedPawn.GetPawnColor()));
            }
            else
            {
                pawnList.Add(pawnMove.MovedPawn.GetMovedToPos(movedPos));
            }
        }

        public Board()
        {
            InitWhiteList();
            InitBlackList();
        }

        public List<Pawn> GetWhiteList()
        {
            return pawnList.Where((it) => it.GetPawnColor() == PawnColor.WHITE).ToList();
        }

        public List<Pawn> GetBlackList()
        {
            return pawnList.Where((it) => it.GetPawnColor() == PawnColor.BLACK).ToList();
        }

        public List<Pawn> GetPawnList(PawnColor pawnColor)
        {
            if (pawnColor == PawnColor.WHITE)
            {
                return GetWhiteList();
            }
            else if (pawnColor == PawnColor.BLACK)
            {
                return GetBlackList();
            }
            else
            {
                throw new ArgumentException("Invalid argument");
            }
        }

        public int GetBoardSize()
        {
            return BOARD_SIZE;
        }

        public BoardState GetBoardState()
        {
            return new BoardState(this);
        }

        abstract public List<PawnMove> GetMoveList();
        abstract public void PerformMove(PawnMove pawnMove);
    }
}
