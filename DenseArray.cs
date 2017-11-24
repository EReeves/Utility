using System;
using System.Collections.Generic;

namespace Game.Shared.Utility
{
    public class DenseArray<T> : IEnumerable<T>
    {
        private readonly T[] _dataarray;
        private int[] yIndexes;

        public int SizeX { get; }
        public int SizeY { get; }

        /// <summary>
        /// Calculates the Y indexes
        /// </summary>
        private void CalculateYIndexes()
        {
            yIndexes = new int[SizeX];
            for (var i = 0; i < SizeX; i++)
            {
                yIndexes[i] = i * SizeY;
            }
        }

        /// <summary>
        /// Creates a new instance of DenseArray
        /// </summary>
        public DenseArray(int xsize, int ysize)
        {
            _dataarray = new T[xsize * ysize];
            SizeX = xsize;
            SizeY = ysize;
            //Speeds up indexing a whole lot
            CalculateYIndexes();
        }

        /// <summary>
        /// Creates a new instance of DenseArray
        /// </summary>
        /// <param name="source">Source DenseArray</param>
        public DenseArray(DenseArray<T> source)
        {
            _dataarray = new T[source.SizeX * source.SizeY];
            SizeX = source.SizeX;
            SizeY = source.SizeY;
            Array.Copy(source._dataarray, this._dataarray, source._dataarray.LongLength);
            yIndexes = new int[SizeY];
            Array.Copy(source.yIndexes,
                this.yIndexes,
                source.yIndexes.LongLength);
        }

        /// <summary>
        /// Gets or sets an element of the array
        /// </summary>
        /// <returns>Value at x,y index</returns>
        public T this[int x, int y]
        {
            get => _dataarray[yIndexes[y] + x];
            set => _dataarray[yIndexes[y] + x] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// IEnumerable implementation.
        /// </summary>
        /// <returns>internal array enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>) _dataarray.GetEnumerator();
        }

        /// <inheritdoc />
        /// <summary>
        /// IEnumerable Implementation
        /// </summary>
        /// <returns>internal array enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dataarray.GetEnumerator();
        }
    }
}