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
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// The concatenation of two ropes.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Depth = {Depth}, Count = {Count}, Child1({_child1.Depth}, {_child1.Count}), Child2({_child2.Depth}, {_child2.Count})")]
    public sealed class ConcatRope : Rope {
        private readonly Rope _child1;
        private readonly Rope _child2;
        private readonly int _count;
        private readonly byte _depth;
        private readonly bool _isBalanced;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcatRope"/> class.
        /// </summary>
        /// <param name="child1">The first rope.</param>
        /// <param name="child2">The second rope.</param>
        internal ConcatRope(Rope child1, Rope child2) {
            _child1 = child1;
            _child2 = child2;
            _count = _child1.Count + _child2.Count;
            if (_count < 0) {
                throw new ArgumentException(
                    string.Format(
                        "Attempted to concatenate ropes with lengths {0} and {1}. " + 
                        "The total length of resulting rope would be {2}. The maximum length permitted is {3}.",
                        _child1.Count, _child2.Count, (long) _child1.Count + _child2.Count, int.MaxValue));
            }
            _depth = (byte)(Math.Max(_child1.Depth, _child2.Depth) + 1);
            _isBalanced = (_count >= MinCount[_depth]);
        }

        private ConcatRope(Rope child1, Rope child2, int count, byte depth, bool isBalanced) {
            _child1 = child1;
            _child2 = child2;
            _count = count;
            _depth = depth;
            _isBalanced = isBalanced;
        }

        public override IEnumerator<char> GetEnumerator(int startIndex) {
            if (_child1.Count <= startIndex)
                return _child2.GetEnumerator(startIndex - _child1.Count);
            return MergedEnumerator(startIndex);
        }

        private IEnumerator<char> MergedEnumerator(int startIndex) {
            using (var child1enum = _child1.GetEnumerator(startIndex)) {
                while (child1enum.MoveNext()) {
                    yield return child1enum.Current;
                }
            }
            using (var child2enum = _child2.GetEnumerator(0)) {
                while (child2enum.MoveNext()) {
                    yield return child2enum.Current;
                }
            }
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public override int Count {
            get { return _count; }
        }

        /// <summary>
        /// Gets the index at which the second child begins.
        /// </summary>
        private int SplitIndex {
            get { return _child1.Count; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or
        /// greater or equal to <see cref="Count"/>.</exception>
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

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            if (startIndex < SplitIndex) {
                int count1 = SplitIndex - startIndex;
                if (count <= count1) {
                    _child1.WriteOut(writer, startIndex, count);
                    return;
                }
                _child1.WriteOut(writer, startIndex, count1);
                _child2.WriteOut(writer, 0, count - count1);
                return;
            }
            _child2.WriteOut(writer, startIndex - SplitIndex, count);
        }

        /// <summary>
        /// Returns the substring.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The length.</param>
        public override Rope SubStringInternal(int startIndex, int count) {
            if (startIndex == 0 && count == _count)
                return this;
            if (startIndex + count <= SplitIndex)
                return _child1.SubStringInternal(startIndex, count);
            if (startIndex >= SplitIndex)
                return _child2.SubStringInternal(startIndex - SplitIndex, count);
            int length1 = SplitIndex - startIndex;
            return
                _child1.SubStringInternal(startIndex, length1).Concat(_child2.SubStringInternal(0, count - length1));
        }

        protected internal override Rope AppendShort<TCharSequence>(TCharSequence sequence, int startIndex, int count) {
            return _child1.Concat(_child2.AppendShort(sequence, startIndex, count));
        }

        protected internal override Rope PrependShort<TCharSequence>(TCharSequence sequence, int startIndex, int count) {
            return _child1.PrependShort(sequence, startIndex, count).Concat(_child2);
        }

        protected override IFlatCharSequence Flatten() {
            return new ArrayCharSequence(this.ToArray());
        }

        protected override int RightChildCount {
            get { return _child2.Count; }
        }

        protected override int LeftChildCount {
            get { return _child1.Count; }
        }

        public override bool IsBalanced {
            get { return _isBalanced; }
        }

        public override byte Depth {
            get { return _depth; }
        }

        public override Rope ReBalance() {
            if (IsBalanced)
                return this;
            var forest = new Rope[MAX_ROPE_DEPTH + 1];
            AddToForest(this, forest);
            Rope result = null;
            foreach (var rope in forest) {
                if (rope != null)
                    result = rope.Concat(result);
            }
            Debug.Assert(result != null);
            if (result.Depth > MAX_ROPE_DEPTH) throw new ArgumentException("The rope is too long.");
            return result;
        }

        private static void AddToForest(Rope rope, Rope[] forest) {
            if (!rope.IsBalanced) {
                // only ConcatRope can be unbalanced
                var concat = rope as ConcatRope;
                Debug.Assert(concat != null);
                AddToForest(concat._child1, forest);
                AddToForest(concat._child2, forest);
                return;
            }

            // Adding balanced rope
            // First find where we should add it
            int i = 0;
            Rope tempRope = null;
            while (rope.Count >= MinCount[i + 1]) {
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
                if (i == MAX_ROPE_DEPTH || tempRope.Count < MinCount[i + 1]) {
                    forest[i] = tempRope;
                    return;
                }
                i++;
            }
        }

        public override Rope Reverse() {
            return new ConcatRope(_child2.Reverse(), _child1.Reverse(), _count, _depth, _isBalanced);
        }

        internal override Rope TrimStartInternal(char[] trimChars) {
            var trimmedChild1 = _child1.TrimStart(trimChars);
            if (trimmedChild1 == _child1)
                return this;
            if (trimmedChild1.IsEmpty)
                return _child2.TrimStart(trimChars);
            return trimmedChild1.Concat(_child2);
        }

        internal override Rope TrimEndInternal(char[] trimChars) {
            var trimmedChild2 = _child2.TrimEnd(trimChars);
            if (trimmedChild2 == _child2)
                return this;
            if (trimmedChild2.IsEmpty)
                return _child1.TrimEnd(trimChars);
            return _child1.Concat(trimmedChild2);
        }
    }
}