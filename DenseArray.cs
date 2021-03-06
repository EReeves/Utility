﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Shared.Utility
{
    /// <inheritdoc />
    /// <summary>
    ///     An efficient 2D array.
    ///     Faster than T[,]  Almost as fast as T[][]
    ///     Smallest memory footprint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DenseArray<T> : IEnumerable<T>
    {
        private readonly T[] dataarray;
        private int[] yIndexes;

        /// <summary>
        ///     Creates a new instance of DenseArray
        /// </summary>
        public DenseArray(int xsize, int ysize)
        {
            dataarray = new T[xsize * ysize];
            SizeX = xsize;
            SizeY = ysize;
            //Speeds up indexing a whole lot
            CalculateYIndexes();
        }

        /// <summary>
        ///     Creates a new instance of DenseArray
        /// </summary>
        /// <param name="source">Source DenseArray</param>
        public DenseArray(DenseArray<T> source)
        {
            dataarray = new T[source.SizeX * source.SizeY];
            SizeX = source.SizeX;
            SizeY = source.SizeY;
            Array.Copy(source.dataarray, dataarray, source.dataarray.LongLength);
            yIndexes = new int[SizeY];
            Array.Copy(source.yIndexes,
                yIndexes,
                source.yIndexes.LongLength);
        }

        public int SizeX { get; }
        public int SizeY { get; }

        /// <summary>
        ///     Gets or sets an element of the array
        /// </summary>
        /// <returns>Value at x,y index</returns>
        public T this[int x, int y]
        {
            get => dataarray[yIndexes[y] + x];
            set => dataarray[yIndexes[y] + x] = value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     IEnumerable implementation.
        /// </summary>
        /// <returns>internal array enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) dataarray).GetEnumerator();
        }

        /// <inheritdoc />
        /// <summary>
        ///     IEnumerable Implementation
        /// </summary>
        /// <returns>internal array enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dataarray.GetEnumerator();
        }

        /// <summary>
        ///     Calculates the Y indexes
        /// </summary>
        private void CalculateYIndexes()
        {
            yIndexes = new int[SizeY];
            for (var i = 0; i < SizeY; i++)
                yIndexes[i] = i * SizeY;
        }
    }
}