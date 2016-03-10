using Sharp48.Core.Moves;
using Sharp48.Core.PlayArea;

namespace Sharp48.Solvers
{
    public interface ISolver
    {
        Move GetBestMove(IGrid grid);
    }
}