using System;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.UserInterfaces
{
    public interface IUserInterface : IDisposable
    {
        void Initialize();
        IGrid Grid { get; }
        IGrid MakeMove(Move move);
    }
}