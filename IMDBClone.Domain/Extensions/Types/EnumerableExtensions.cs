using System.Collections;
using System.Linq;

namespace IMDBClone.Domain.Extensions.Types
{
    public static class EnumerableExtensions
    {
        public static string ToEnumerableString(this IEnumerable enumerable)
        {
            return enumerable.Cast<object>().Aggregate("", (current, element) => current + (element + "\n"));
        }
    }
}