namespace Checkers.Engine
{
    struct Dot
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Dot(int posX, int posY)
        {
            X = posX;
            Y = posY;
        }

        public Dot GetMoved(int dirX, int dirY)
        {
            return new Dot(X + dirX, Y + dirY);
        }

        public Dot GetMoved(Dot dir)
        {
            return new Dot(X + dir.X, Y + dir.Y);
        }
    }
}
