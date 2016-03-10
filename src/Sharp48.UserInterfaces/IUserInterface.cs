using System;
using Sharp48.Core;
using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.UserInterfaces
{
    public interface IUserInterface : IDisposable
    {
        void Initialize();
        IGame Game { get; }
        IGame MakeMove(Move move);
    }
}