using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sharp48.Core.PlayArea
{
    public class Column : ReadOnlyCollection<ISquare>, IColumn
    {
        public Column(IList<ISquare> list) : base(list)
        {
        }
    }
}