using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sharp48.Core.PlayArea
{
    public class Row : ReadOnlyCollection<ISquare>, IRow
    {
        public Row(IList<ISquare> list) : base(list)
        {
        }
    }
}