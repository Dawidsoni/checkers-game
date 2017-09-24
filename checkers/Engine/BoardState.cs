using System.Collections.Generic;

namespace Checkers.Engine
{
    class BoardState
    {
        protected readonly int boardSize;
        protected PawnColor[,] pawnColorList;
        protected Pawn[,] pawnList;

        protected void InitAsEmpty()
        {
            for(int i = 0; i < boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    pawnColorList[i, j] = PawnColor.NONE;
                }
            }
        }

        protected void AddPawnList(IReadOnlyCollection<Pawn> rList, PawnColor pawnColor)
        {
            foreach(var pawn in rList)
            {
                Dot pawnPos = pawn.GetPosition();
                pawnList[pawnPos.X, pawnPos.Y] = pawn;
                pawnColorList[pawnPos.X, pawnPos.Y] = pawnColor;
            }
        }

        protected void InitFromBoard(Board board)
        {
            InitAsEmpty();
            AddPawnList(board.GetWhiteList(), PawnColor.WHITE);
            AddPawnList(board.GetBlackList(), PawnColor.BLACK);            
        }

        public BoardState(Board board)
        {
            this.boardSize = board.GetBoardSize();
            pawnColorList = new PawnColor[boardSize, boardSize];
            pawnList = new Pawn[boardSize, boardSize];
            InitFromBoard(board);
        }

        public Pawn GetPawn(Dot location)
        {
            return pawnList[location.X, location.Y];
        }

        public PawnColor GetColor(Dot location)
        {
            return pawnColorList[location.X, location.Y];
        }

        public int GetBoardSize()
        {
            return boardSize;
        }

        public bool IsOnBoard(Dot loc)
        {
            return (loc.X >= 0 && loc.Y >= 0 && loc.X < GetBoardSize() && loc.Y < GetBoardSize());
        }
    }
}
