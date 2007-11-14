﻿using System;
using System.Collections.Generic;
using System.Text;

namespace de.unika.ipd.grGen.lgsp
{
    // TODO: Implement as heap to improve performance
    public class PriorityQueue<T> : ICollection<T>
    {
        protected List<T> items = new List<T>();

        public T DequeueFirst()
        {
            T elem = items[0];
            items.RemoveAt(0);
            return elem;
        }

        public void Add(T item)
        {
            int index = items.BinarySearch(item);
            if(index < 0)
                items.Insert(~index, item);
            else
                items.Insert(index, item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
