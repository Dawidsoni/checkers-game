using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers.WinGame
{
    public partial class CheckersForm : Form
    {
        const int BOARD_SIZE = 760;
        const int MENU_SIZE = 25;
        ContainerControl boardContainer;
        GameBoard gameBoard;

        protected void InitUI()
        {
            boardContainer = new ContainerControl();
            boardContainer.Size = new Size(BOARD_SIZE, BOARD_SIZE);
            boardContainer.Location = new Point(0, MENU_SIZE);
            Controls.Add(boardContainer);
            gameBoard = new GameBoard(boardContainer);
        }

        public CheckersForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void newGameMenuItem_Click(object sender, EventArgs e)
        {
            gameBoard.ResetGame();
        }
    }
}
