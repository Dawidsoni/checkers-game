using Checkers.Engine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers.WinGame
{
    class BoardInterface
    {
        protected PictureBox[,] fieldList;
        protected ContainerControl bContainer;
        protected FieldFactory fieldFactory;
        protected Action<Dot> cHandler;

        protected void SetWinnerLabel(String winnerName)
        {
            var winnerLabel = new Label();
            winnerLabel.Text = "The winner is " + winnerName + "!";
            winnerLabel.Location = new Point(120, 320);
            winnerLabel.Font = new Font("Arial", 30, FontStyle.Bold);
            winnerLabel.AutoSize = true;
            MouseEventHandler mHandler = ((x, y) => cHandler(new Dot(0, 0)));
            winnerLabel.MouseDown += mHandler;
            bContainer.Controls.Add(winnerLabel);
        }

        public BoardInterface(ContainerControl bContainer, int fSize, Action<Dot> cHandler)
        {
            this.bContainer = bContainer;
            this.cHandler = cHandler;
            fieldList = new PictureBox[fSize, fSize];
            fieldFactory = new FieldFactory(new Size(fSize, fSize));
        }

        public void UpdateField(FieldInfo fieldInfo)
        {
            int pX = fieldInfo.Loc.X;
            int pY = fieldInfo.Loc.Y;
            bContainer.Controls.Remove(fieldList[pX, pY]);
            fieldList[pX, pY] = fieldFactory.CreateField(fieldInfo);
            MouseEventHandler mHandler = ((x, y) => cHandler(new Dot(pX, pY)));
            fieldList[pX, pY].MouseDown += mHandler;
            bContainer.Controls.Add(fieldList[pX, pY]);
        }

        public void ResetGame()
        {
            bContainer.Controls.Clear();
        }

        public void FinishGame(PawnColor winnerColor)
        {
            String winnerName = "";
            if(winnerColor == PawnColor.WHITE)
            {
                winnerName = "white player";
            }
            else if(winnerColor == PawnColor.BLACK)
            {
                winnerName = "black player";
            }
            else
            {
                throw new ArgumentException();
            }
            bContainer.Controls.Clear();
            SetWinnerLabel(winnerName);
        }
    }
}
