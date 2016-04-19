using System;
using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.UserInterfaces
{
    /// <summary>
    ///     Represents a disposable user interface in which to play the game.
    /// </summary>
    public interface IUserInterface : IDisposable
    {
        /// <summary>
        ///     The game as displayed on the UI.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        ///     A method to initialize the UI.
        /// </summary>
        void Initialize();

        /// <summary>
        ///     Makes a move on the game held by the UI and returns the current game representation from the UI.
        /// </summary>
        /// <param name="move">The move to make on the UI.</param>
        /// <returns>The current game represented by the UI.</returns>
        IGame MakeMove(Move move);
    }
}