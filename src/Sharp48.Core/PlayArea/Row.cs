using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sharp48.Core.PlayArea
{
    public class Row : ReadOnlyCollection<ISquare>, IRow
    {
        public Row(IList<ISquare> list) : base(list)
        {
        }

        public override string ToString()
        {
            return string.Join(",", this.Select(x => x.ToString()));
        }
    }
}