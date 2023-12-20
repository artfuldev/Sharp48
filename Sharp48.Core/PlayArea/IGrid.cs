namespace Sharp48.Core.PlayArea
{
    public interface IGrid
    {
        IReadOnlyCollection<ISquare> Squares { get; }
        IReadOnlyCollection<IRow> Rows { get; }
        IReadOnlyCollection<IColumn> Columns { get; }
    }
}