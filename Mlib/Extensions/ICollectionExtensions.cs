using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlib.Extensions
{
    public static class ICollectionExtensions
    {
        public static void Swap<T>(this ICollection<T> collection, T oldValue, T newValue)
        {
            var collectionAsList = collection as IList<T>;
            if (collectionAsList != null)
            {
                var oldIndex = collectionAsList.IndexOf(oldValue);
                collectionAsList.RemoveAt(oldIndex);
                collectionAsList.Insert(oldIndex, newValue);
            }
            else
            {
                collection.Remove(oldValue);
                collection.Add(newValue);
            }

        }
    }
}
