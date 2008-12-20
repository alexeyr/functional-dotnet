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
    /// <typeparam name="TChar">The type of the char.</typeparam>
    /// <typeparam name="TChar">The type of chars used, normally either
    /// <see cref="char"/> or <see cref="byte"/>.</typeparam>
    [Serializable]
    public sealed class ConcatRope<TChar> : Rope<TChar> {
        private readonly Rope<TChar> _child1;
        private readonly Rope<TChar> _child2;
        private readonly int _length;
        private readonly byte _depth;
        private readonly bool _isBalanced;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatRope&lt;TChar&gt;"/> class.
        /// </summary>
        /// <param name="rope1">The first rope.</param>
        /// <param name="rope2">The second rope.</param>
        public ConcatRope(Rope<TChar> rope1, Rope<TChar> rope2) {
            _child1 = rope1;
            _child2 = rope2;
            _length = _child1.Length + _child2.Length;
            _depth = (byte)(Math.Max(_child1.Depth, _child2.Depth) + 1);
            _isBalanced = (_length >= MinLength[_depth]);
        }

        public override IEnumerator<TChar> GetEnumerator() {
            foreach (TChar c in _child1)
                yield return c;
            foreach (TChar c in _child2)
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
        /// Gets the <paramref name="index"/>-th <see cref="TChar"/> in the sequence.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or
        /// greater or equal to <see cref="Rope{TChar}.Length"/>.</exception>
        public override TChar this[int index] {
            get {
                return index < SplitIndex ? _child1[index] : _child2[index - SplitIndex];
            }
        }

        /// <summary>
        /// Copies the rope to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        public override void CopyTo(int sourceIndex, TChar[] destination, int destinationIndex, int count) {
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
        public override Rope<TChar> SubString(int startIndex, int length) {
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

        internal override Rope<TChar> ConcatShort(FlatRope<TChar> otherFlat) {
            var child2flat = (FlatRope<TChar>) _child2;
            return new ConcatRope<TChar>(_child1, child2flat.ConcatShort(otherFlat));
        }

        protected internal override bool IsRightMostChildFlatAndShort {
            get { return _child2.Length < MaxShortSize && _child2 is FlatRope<TChar>; }
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

        public override Rope<TChar> Rebalance() {
            if (IsBalanced)
                return this;
            var forest = new Rope<TChar>[MaxRopeDepth + 1];
            AddToForest(this, forest);
            Rope<TChar> result = null;
            foreach (var rope in forest)
                result = rope.Concat(result);
            if (result.Depth > MaxRopeDepth) {
                throw new ArgumentException("The rope is too long.");
            }
            return result;
        }

        private static void AddToForest(Rope<TChar> rope, Rope<TChar>[] forest) {
            if (!rope.IsBalanced) {
                var concat = rope as ConcatRope<TChar>;
                Debug.Assert(concat != null);
                AddToForest(concat._child1, forest);
                AddToForest(concat._child2, forest);
            }
            //Adding balanced rope
            //First find where we should add it
            int i = 0;
            Rope<TChar> tempRope = null;
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
                if (i == MaxRopeDepth || tempRope.Length < MinLength[i + 1]) {
                    forest[i] = tempRope;
                    return;
                }
                i++;
            }
        }
    }
}