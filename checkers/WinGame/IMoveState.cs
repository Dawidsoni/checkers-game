using Checkers.Engine;

namespace Checkers.WinGame
{
    interface IMoveState
    {
        void InitState();
        void HandleEvent(Dot pos);
    }
}
