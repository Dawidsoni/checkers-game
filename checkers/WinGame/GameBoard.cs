using System.Windows.Forms;
using Checkers.Engine;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace Checkers.WinGame
{
    class GameBoard
    {
        protected IMoveState moveState;
        public BoardEngine BoardEngine { get; private set; }
        public BoardInterface BoardInterface { get; private set; }

        protected void InitField(BoardState boardState, int pX, int pY)
        {
            var color = boardState.GetColor(new Dot(pX, pY));
            var fieldInfo = new FieldInfo(color, new Dot(pX, pY), FieldAct.INACTIVE, false);
            BoardInterface.UpdateField(fieldInfo);
        }

        protected void EventHandler(Dot pos)
        {
            moveState.HandleEvent(pos);
        }

        protected void InitBoard()
        {
            var boardState = BoardEngine.GetBoardState();
            for(int i = 0; i < boardState.GetBoardSize(); i++)
            {
                for(int j = 0; j < boardState.GetBoardSize(); j++)
                {
                    InitField(boardState, i, j);
                }
            }
            ChangeState(new ChooseMoveState(this));
        }

        public GameBoard(ContainerControl boardContainer)
        {
            BoardEngine = new BoardEngine();
            int fieldSize = boardContainer.Width / BoardEngine.GetBoardSize();
            BoardInterface = new BoardInterface(boardContainer, fieldSize, EventHandler);
            InitBoard();
        }

        public void ResetGame()
        {
            BoardEngine = new BoardEngine();
            BoardInterface.ResetGame();
            InitBoard();
        }

        public void ChangeState(IMoveState moveState)
        {
            this.moveState = moveState;
            this.moveState.InitState();
        }
    }
}
