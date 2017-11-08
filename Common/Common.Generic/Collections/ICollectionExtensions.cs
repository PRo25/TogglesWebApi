using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Generic.Collections
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            foreach (T item in itemsToAdd)
            {
                collection.Add(item);
            }
        }

        public static bool AddIfNotExists<T>(this ICollection<T> collection, T item,
            Func<T, bool> equalityPredicate)
        {
            bool wasAdded = false;
            if (!collection.Any(equalityPredicate))
            {
                collection.Add(item);
                wasAdded = true;
            }
            return wasAdded;
        }

        public static void AddOrReplace<T>(this ICollection<T> collection, T item,
            Func<T, bool> equalityPredicate)
        {
            T existingItem = collection.FirstOrDefault(equalityPredicate);
            if (existingItem != null)
            {
                collection.Remove(existingItem);
            }
            collection.Add(item);
        }

        public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            List<T> itemsToRemove = collection.Where(predicate).ToList();
            collection.RemoveRange(itemsToRemove);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, ICollection<T> itemsToRemove)
        {
            foreach (T itemToRemove in itemsToRemove)
            {
                collection.Remove(itemToRemove);
            }
        }
    }
}
