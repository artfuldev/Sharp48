using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sharp48.Core.PlayArea
{
    public class Column : ReadOnlyCollection<ISquare>, IColumn
    {
        public Column(IList<ISquare> list) : base(list)
        {
        }

        public override string ToString()
        {
            return string.Join(",", this.Select(x => x.ToString()));
        }
    }
}