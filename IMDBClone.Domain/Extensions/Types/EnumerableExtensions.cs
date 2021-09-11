using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace IMDBClone.Domain.Extensions.Types
{
    public static class EnumerableExtensions
    {
        public static string ToEnumerableString(this IEnumerable<IdentityError> enumerable)
        {
            return enumerable.Cast<IdentityError>().Aggregate("", (current, element) => current + (element.Description + "\n"));
        }
    }
}