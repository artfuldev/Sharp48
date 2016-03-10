using Sharp48.Core;
using Sharp48.Core.Moves;

namespace Sharp48.Solvers
{
    public interface ISolver
    {
        Move GetBestMove(IGame game);
    }
}