using Checkers.Engine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers.WinGame
{
    class FieldFactory
    {
        static readonly Image whitePiecePawn = Image.FromFile("whitePiecePawn.png");
        static readonly Image blackPiecePawn = Image.FromFile("blackPiecePawn.png");
        static readonly Image whiteDamePawn = Image.FromFile("whiteDamePawn.png");
        static readonly Image blackDamePawn = Image.FromFile("blackDamePawn.png");
        protected Size fieldSize;

        protected Point GetFieldLocation(Dot loc)
        {
            return new Point(loc.Y * fieldSize.Width, loc.X * fieldSize.Height);
        }

        protected bool IsWhiteField(Dot loc)
        {
            return ((loc.X % 2 == 0 && loc.Y % 2 == 0) || (loc.X % 2 == 1 && loc.Y % 2 == 1));
        }

        protected Image GetPawnPath(FieldInfo fieldInfo)
        {
            if(fieldInfo.Color == PawnColor.WHITE && fieldInfo.IsDame == false)
            {
                return whitePiecePawn;
            }
            else if(fieldInfo.Color == PawnColor.WHITE && fieldInfo.IsDame)
            {
                return whiteDamePawn;
            }
            else if(fieldInfo.Color == PawnColor.BLACK && fieldInfo.IsDame == false)
            {
                return blackPiecePawn;
            }
            else if(fieldInfo.Color == PawnColor.BLACK && fieldInfo.IsDame)
            {
                return blackDamePawn;
            }
            else
            {
                throw new ArgumentException();
            }         
        }

        protected Color GetFieldColor(FieldInfo fieldInfo)
        {
            if(fieldInfo.FieldAct == FieldAct.CHECKED_PAWN)
            {
                return Color.DarkGreen;
            }
            else if(fieldInfo.FieldAct == FieldAct.MOVE_PAWN)
            {
                return Color.LightGreen;
            }
            else if(fieldInfo.FieldAct == FieldAct.MOVE_FIELD)
            {
                return Color.Orange;
            }
            else if (IsWhiteField(fieldInfo.Loc))
            {
                return Color.White;
            }
            else
            {
                return Color.Brown;
            }
        }

        protected PictureBox CreateEmptyField(FieldInfo fieldInfo)
        {
            var result = new PictureBox();
            result.BackColor = GetFieldColor(fieldInfo);
            result.Size = fieldSize;
            result.Location = GetFieldLocation(fieldInfo.Loc);
            return result;
        }

        protected PictureBox CreateFilledField(FieldInfo fieldInfo)
        {
            var pawnPath = GetPawnPath(fieldInfo);
            var result = new PictureBox();
            result.Image = pawnPath;
            result.SizeMode = PictureBoxSizeMode.StretchImage;
            result.Size = fieldSize;
            result.Location = GetFieldLocation(fieldInfo.Loc);
            result.BackColor = GetFieldColor(fieldInfo);
            return result;
        }

        public FieldFactory(Size fieldSize)
        {
            this.fieldSize = fieldSize;
        }

        public PictureBox CreateField(FieldInfo fieldInfo)
        {
            if(fieldInfo.Color == PawnColor.WHITE || fieldInfo.Color == PawnColor.BLACK)
            {
                return CreateFilledField(fieldInfo);
            }
            else
            {
                return CreateEmptyField(fieldInfo);
            }
        }
    }
}
