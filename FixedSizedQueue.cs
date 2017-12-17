using System.Collections;
using System.Collections.Generic;

namespace Game.Shared.Utility
{
    /// <inheritdoc />
    /// <summary>
    ///     Fixed size queue of type T.
    ///     Items can only be added through Enqueue(), but can be indexed[] or enumerated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedSizedQueue<T> : IEnumerable<T>
    {
        private readonly T[] circularArray;
        private readonly object syncRoot = new object();
        private int readIndex;
        private int writeIndex;

        public FixedSizedQueue(int _size)
        {
            circularArray = new T[_size];
        }

        public int Count { get; private set; }

        public T this[int index] => circularArray[(index + readIndex) % circularArray.Length];

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = readIndex; i < Count + readIndex; i++)
                yield return circularArray[i % circularArray.Length];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enqueue(T item)
        {
            lock (syncRoot)
            {
                circularArray[writeIndex] = item;
                writeIndex = (writeIndex + 1) % circularArray.Length;
                Count++;

                if (Count > circularArray.Length)
                    Dequeue();
            }
        }

        public T Dequeue()
        {
            lock (syncRoot)
            {
                var item = circularArray[readIndex];
                circularArray[readIndex] = default;
                readIndex = (readIndex + 1) % circularArray.Length;
                return item;
            }
        }
    }
}