/*
* ConcatRope.cs is part of functional-dotnet project
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
using System.Collections.Generic;
using System.Diagnostics;

namespace FP.Text {
    /// <summary>
    /// The concatenation of two ropes.
    /// </summary>
    [Serializable]
    public sealed class ConcatRope : Rope {
        private readonly Rope _child1;
        private readonly Rope _child2;
        private readonly int _length;
        private readonly byte _depth;
        private readonly bool _isBalanced;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatRope"/> class.
        /// </summary>
        /// <param name="rope1">The first rope.</param>
        /// <param name="rope2">The second rope.</param>
        public ConcatRope(Rope rope1, Rope rope2) {
            _child1 = rope1;
            _child2 = rope2;
            _length = _child1.Length + _child2.Length;
            _depth = (byte) (Math.Max(_child1.Depth, _child2.Depth) + 1);
            _isBalanced = (_length >= MinLength[_depth]);
        }

        public override IEnumerator<char> GetEnumerator() {
            foreach (char c in _child1)
                yield return c;
            foreach (char c in _child2)
                yield return c;
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public override int Length {
            get { return _length; }
        }

        /// <summary>
        /// The index at which the second child begins.
        /// </summary>
        private int SplitIndex {
            get { return _child1.Length; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or
        /// greater or equal to <see cref="Rope.Length"/>.</exception>
        public override char this[int index] {
            get { return index < SplitIndex ? _child1[index] : _child2[index - SplitIndex]; }
        }

        /// <summary>
        /// Copies the rope to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The starting index to copy.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex,
                                    int count) {
            if (sourceIndex < SplitIndex) {
                int count1 = SplitIndex - sourceIndex;
                if (count <= count1) {
                    _child1.CopyTo(sourceIndex, destination, destinationIndex, count);
                    return;
                }
                _child1.CopyTo(sourceIndex, destination, destinationIndex, count1);
                _child2.CopyTo(0, destination, destinationIndex + count1, count - count1);
                return;
            }
            _child2.CopyTo(sourceIndex - SplitIndex, destination, destinationIndex, count);
        }

        /// <summary>
        /// Returns the substring.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        public override Rope SubString(int startIndex, int length) {
            if (startIndex < 0 || startIndex >= length)
                throw new ArgumentOutOfRangeException("startIndex");
            if (startIndex + length > _length)
                throw new ArgumentOutOfRangeException("length");
            if (startIndex == 0 && length == _length)
                return this;
            if (startIndex + length <= SplitIndex)
                return _child1.SubString(startIndex, length);
            if (startIndex >= SplitIndex)
                return _child2.SubString(startIndex - SplitIndex, length);
            int length1 = SplitIndex - startIndex;
            return
                _child1.SubString(startIndex, length1).Concat(_child2.SubString(0, length - length1));
        }

        internal override Rope ConcatShort(FlatRope otherFlat) {
            var child2flat = (FlatRope) _child2;
            return new ConcatRope(_child1, child2flat.ConcatShort(otherFlat));
        }

        protected internal override bool IsRightMostChildFlatAndShort {
            get { return _child2.Length < MAX_SHORT_SIZE && _child2 is FlatRope; }
        }

        protected internal override bool IsBalanced {
            get {
//                if (_isBalanced)
//                    return true;
//                if (_length >= MinLength[Depth]) {
//                    _isBalanced = true;
//                    return true;
//                }
                return _isBalanced;
            }
        }

        protected internal override byte Depth {
            get { return _depth; }
        }

        public override Rope Rebalance() {
            if (IsBalanced)
                return this;
            var forest = new Rope[MAX_ROPE_DEPTH + 1];
            AddToForest(this, forest);
            Rope result = null;
            foreach (var rope in forest)
                result = rope.Concat(result);
            //TODO: check that result can't be null
            if (result.Depth > MAX_ROPE_DEPTH) throw new ArgumentException("The rope is too long.");
            return result;
        }

        private static void AddToForest(Rope rope, Rope[] forest) {
            if (!rope.IsBalanced) {
                var concat = rope as ConcatRope;
                Debug.Assert(concat != null);
                AddToForest(concat._child1, forest);
                AddToForest(concat._child2, forest);
            }
            //Adding balanced rope
            //First find where we should add it
            int i = 0;
            Rope tempRope = null;
            while (rope.Length >= MinLength[i + 1]) {
                if (forest[i] != null) {
                    tempRope = forest[i].Concat(tempRope);
                    forest[i] = null;
                }
                i++;
            }
            tempRope = tempRope == null ? rope : tempRope.Concat(rope);
            while (true) {
                if (forest[i] != null) {
                    tempRope = forest[i].Concat(tempRope);
                    forest[i] = null;
                }
                if (i == MAX_ROPE_DEPTH || tempRope.Length < MinLength[i + 1]) {
                    forest[i] = tempRope;
                    return;
                }
                i++;
            }
        }
    }
}