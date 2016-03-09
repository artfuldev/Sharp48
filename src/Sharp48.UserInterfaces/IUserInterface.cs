using System;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.UserInterfaces
{
    public interface IUserInterface : IDisposable
    {
        void Initialize();
        IGrid GetGrid(Move move = Move.None);
    }
}