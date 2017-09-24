using Checkers.Engine;

namespace Checkers.WinGame
{
    class FieldInfo
    {
        public PawnColor Color { get; private set; }
        public Dot Loc { get; private set; }
        public bool IsDame { get; private set; }
        public FieldAct FieldAct { get; private set; }

        public FieldInfo(PawnColor color, Dot loc, FieldAct fieldAct, bool isDame = false)
        {
            Color = color;
            Loc = loc;
            IsDame = isDame;
            FieldAct = fieldAct;
        }

        public FieldInfo(Pawn pawn, FieldAct fieldAct)
        {
            Color = pawn.GetPawnColor();
            Loc = pawn.GetPosition();
            IsDame = pawn.IsDame();
            FieldAct = fieldAct;
        }
    }
}
