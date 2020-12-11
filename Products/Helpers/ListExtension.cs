using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.Helpers
{
    public static class ListExtensions
    {
      
        public static void RemoveWhere<T>(this List<T> input, Predicate<T> predicate)
        {
            input.RemoveAll(predicate);
        }

    }
}