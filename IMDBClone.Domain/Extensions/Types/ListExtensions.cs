using System.Collections.Generic;
using System.Linq;

namespace IMDBClone.Domain.Extensions.Types
{
    public static class ListExtensions
    {
        public static string ToListString<T>(this List<T> list) where T : class
        {
            var content = list.Cast<object>().Aggregate("", (current, element) => current + (element));
            return content;
        }
    }
}