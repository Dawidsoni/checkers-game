using System.Collections.Generic;

namespace Checkers.Engine
{
    class PawnMove
    {
        protected List<Dot> posList = new List<Dot>();
        protected List<Pawn> capturedList = new List<Pawn>();

        public PawnMove(Pawn movedPawn)
        {
            posList = new List<Dot>();
            capturedList = new List<Pawn>();
            MovedPawn = movedPawn;
            TurnDame = false;
        }

        public PawnMove(Dot pos, Pawn movedPawn, bool turnDame)
        {
            posList = new List<Dot>();
            posList.Add(pos);
            capturedList = new List<Pawn>();
            MovedPawn = movedPawn;
            TurnDame = turnDame;
        }

        public PawnMove(List<Dot> posList, List<Pawn> capturedList, Pawn movedPawn, bool turnDame)
        {
            this.posList = new List<Dot>(posList);
            this.capturedList = new List<Pawn>(capturedList);
            MovedPawn = movedPawn;
            TurnDame = turnDame;
        }

        public Pawn MovedPawn { get; private set; }
        public bool TurnDame { get; private set; }

        public IReadOnlyCollection<Dot> GetPosList()
        {
            return posList.AsReadOnly();
        }

        public IReadOnlyCollection<Pawn> GetCapturedList()
        {
            return capturedList.AsReadOnly();
        }

        public PawnMove CreateIncrMove(Dot pos, Pawn capturedPawn, bool turnDame)
        {
            var incrPosList = new List<Dot>(posList);
            var incrCapturedList = new List<Pawn>(capturedList);
            incrPosList.Add(pos);
            incrCapturedList.Add(capturedPawn);
            return new PawnMove(incrPosList, incrCapturedList, MovedPawn, turnDame);
        }
    }
}
