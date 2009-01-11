/*
* Rope.cs is part of functional-dotnet project
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

using System;
using System.Collections;
using System.Collections.Generic;
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// A rope (heavyweight string). Supports (amortized) 
    /// constant-time concatenation, logarithmic insertion, substrings, random access, 
    /// linear iteration.
    /// </summary>
    /// <remarks>
    /// <see cref="Rope"/> doesn't implement <see cref="IMeasured{V}"/> so that it doesn't
    /// get put into a <see cref="FingerTree{T,V}"/> by accident.
    /// </remarks>
    [Serializable]
    public abstract class Rope : IRope<Rope> {
        /// <summary>
        /// This is the maximum size for the flat rope to be considered "short".
        /// </summary>
        protected const int MAX_SHORT_SIZE = 25;

        /// <summary>
        /// The maximum depth a tree can reach before being rebalanced.
        /// </summary>
        protected const int MAX_ROPE_DEPTH = 45;

        protected static readonly int[] MinLength = new int[MAX_ROPE_DEPTH + 1] {
/* 0 */1, /* 1 */2, /* 2 */3, /* 3 */5, /* 4 */8, /* 5 */13, /* 6 */21,
/* 7 */34, /* 8 */55, /* 9 */89, /* 10 */144, /* 11 */233, /* 12 */377,
/* 13 */610, /* 14 */987, /* 15 */1597, /* 16 */2584, /* 17 */4181,
/* 18 */6765, /* 19 */10946, /* 20 */17711, /* 21 */28657, /* 22 */46368,
/* 23 */75025, /* 24 */121393, /* 25 */196418, /* 26 */317811,
/* 27 */514229, /* 28 */832040, /* 29 */1346269, /* 30 */2178309,
/* 31 */3524578, /* 32 */5702887, /* 33 */9227465, /* 34 */14930352,
/* 35 */24157817, /* 36 */39088169, /* 37 */63245986, /* 38 */102334155,
/* 39 */165580141, /* 40 */267914296, /* 41 */433494437, /* 42 */701408733,
/* 43 */1134903170, /* 44 */1836311903, /* 45 */ int.MaxValue
                                                                              };

        public abstract IEnumerator<char> GetEnumerator(int startIndex);

        public IEnumerator<char> GetEnumerator() {
            return GetEnumerator(0);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less
        /// than 0 or greater or equal to <see cref="Count"/>.</exception>
        public abstract char this[int index] { get; }

        /// <summary>
        /// Copies the part of the rope starting with <paramref name="sourceIndex"/> to 
        /// <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index from which copying starts.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of chars to copy.</param>
        public abstract void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

        /// <summary>
        /// Returns the substring.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The length.</param>
        public abstract Rope SubString(int startIndex, int count);

        public Rope Concat(Rope other) {
            if (other == null || other.IsEmpty)
                return this;
            if (IsEmpty)
                return other;
            var otherFlat = other as FlatRope;
            if (otherFlat != null && IsRightMostChildShort &&
                otherFlat.Count <= MAX_SHORT_SIZE) return ConcatShort(otherFlat);
            return ConcatAndReBalanceIfNeeded(other);
        }

        private Rope ConcatAndReBalanceIfNeeded(Rope other) {
            var result = new ConcatRope(this, other);
            byte depth = result.Depth;
            return depth > MAX_ROPE_DEPTH || (depth > 20 && result.Count < MinLength[3 * depth / 4])
                       ? result.ReBalance()
                       : result;
        }

        internal abstract Rope ConcatShort(FlatRope otherFlat);

        /// <summary>
        /// Gets the depth of the rope (0 for the flat ropes).
        /// </summary>
        /// <value>The depth.</value>
        protected internal abstract byte Depth { get; }

        protected internal abstract bool IsRightMostChildShort { get; }

        /// <summary>
        /// Gets a value indicating whether this rope is balanced.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is balanced; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsBalanced { get; }

        public bool IsEmpty {
            get { return Count == 0; }
        }

        /// <summary>
        /// Rebalances this instance.
        /// </summary>
        public abstract Rope ReBalance();
    }
}