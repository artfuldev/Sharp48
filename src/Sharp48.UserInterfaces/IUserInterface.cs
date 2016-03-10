using System;
using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.UserInterfaces
{
    public interface IUserInterface : IDisposable
    {
        IGame Game { get; }
        void Initialize();
        IGame MakeMove(Move move);
    }
}