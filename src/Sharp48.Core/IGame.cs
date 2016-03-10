using Sharp48.Core.PlayArea;

namespace Sharp48.Core
{
    public interface IGame
    {
        IGrid Grid { get; }
        bool Over { get; }
        long Score { get; }
    }
}