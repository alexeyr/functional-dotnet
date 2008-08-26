#region License
/*
* Vector.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
* All rights reserved.
*
* This source file is available under The New BSD License.
* See license.txt file for more information.
* 
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
* INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
*/
#endregion

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A vector (nearly array-like sequence).
    /// </summary>
    [Serializable]
    public sealed class Vector<T> : IRandomAccessSequence<T, Vector<T>> {
        private const int BRANCHING = 32;
        private static readonly Vector<T>[] _emptyArray = new Vector<T>[0];
        public static readonly Vector<T> Empty = new Vector<T>(default(T), 0, _emptyArray);
        private static readonly Vector<T> _default = new Vector<T>(default(T), 1, _emptyArray);

        private readonly T _data;
        private readonly int _count;
        private readonly Vector<T>[] _branches;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="count">The count.</param>
        /// <param name="branches">The branches.</param>
        public Vector(T data, int count, Vector<T>[] branches) {
            _data = data;
            _count = count;
            _branches = branches;
        }

        /// <summary>
        /// Updates the element at <paramref name="index"/> using <paramref name="function"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="function">The function to apply to the element currently at <paramref name="index"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        /// <remarks>
        /// Equivalent to <code>SetAt(index, function(this[index])), but faster.</code>
        /// </remarks>
        public Vector<T> UpdateAt(int index, Func<T, T> function) {
            return UpdateAtPath(Digits(index), function);
        }

        private Vector<T> UpdateAtPath(IEnumerable<int> path, Func<T, T> function) {
            if (path.IsEmpty())
                return new Vector<T>(function(_data), Math.Max(_count, 1), _branches);
            int head = path.First();
            IEnumerable<int> tail = path.Tail();
            int newBranchesLength = Math.Max(_branches.Length, head + 1);
            var newBranches = new Vector<T>[newBranchesLength];
            Vector<T> vector = head >= _branches.Length ? _default : (_branches[head] ?? _default);
            Array.Copy(_branches, newBranches, _branches.Length);
            for (int i = _branches.Length; i < newBranchesLength; i++)
                newBranches[i] = Empty;
            newBranches[head] = vector.UpdateAtPath(tail, function);
            return new Vector<T>(_data, Math.Max(_count, Number(path) + 1), newBranches);
        }

        public Vector<T> Append(T newValue) {
            return UpdateAt(_count + 1, _ => newValue);
        }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() {
            if (_count == 0)
                yield break;
            yield return _data;

        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the number of elements in the sequence.
        /// </summary>
        /// <value>The number of elements in the sequence.</value>
        public int Count {
            get { return _count; }
        }

        /// <summary>
        /// Gets a value indicating whether this collection is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        public bool IsEmpty {
            get { return _count == 0; }
        }

        public Vector<T> Subsequence(int startIndex, int count) {
            throw new System.NotImplementedException();
        }

        public T this[int index] {
            get {
                if (index < 0)
                    throw new ArgumentOutOfRangeException("index");
                var vector = this;
                foreach (int i in Digits(index)) {
                    if (vector._branches.Length <= i)
                        return default(T);
                    vector = vector._branches[i];
                }
                return vector._data;
            }
        }

        private static IEnumerable<int> Digits(int number) {
            return Digits(number, BRANCHING);
        }

        private static IEnumerable<int> Digits(int number, int @base) {
            var list = new List<int>(5);
            if (number == 0) {
                return new[] {0};
            }
            while (number > 0) {
                list.Add(number % @base);
                number /= @base;
            }
            list.Reverse();
            return list;
        }

        private static int Number(IEnumerable<int> digits) {
            return Number(digits, BRANCHING);
        }

        private static int Number(IEnumerable<int> digits, int @base) {
            int number = 0;
            foreach (int digit in digits) {
                number *= @base;
                number += digit;
            }
            return number;
        }
    }

    public static class Vector {
        public static Vector<T> New<T>(IEnumerable<T> ts) {
            var vector = Vector<T>.Empty;
            int i = 0;
            foreach (T t in ts)
                vector = vector.SetAt(i++, t);
            return vector;
        }

        public static Vector<T> New<T>(params T[] ts) {
            return New(ts.AsEnumerable());
        }
    }
}