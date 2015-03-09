using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TusClasificados.Site.Helpers
{
    public static class LinqExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int partitionSize)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / partitionSize)
                        .Select(g => g.Select(x => x.item));
        }
    }
}