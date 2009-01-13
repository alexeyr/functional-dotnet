/*
* ReverseRope.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// A rope which is a substring of another rope.
    /// </summary>
    [Serializable]
    public sealed class ReverseRope<TSequence> : FlatRope where TSequence : IFlatCharSequence {
        private readonly CharSequenceRope<TSequence> _sourceRope;
        private readonly int _offsetFromEnd;
        private readonly int _length;

        private int FirstIndex { get { return _sourceRope.Count - _offsetFromEnd - _length; } }

        private int LastIndex { get { return _sourceRope.Count - _offsetFromEnd - 1; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseRope{TSequence}"/> class.
        /// </summary>
        /// <param name="sourceRope">The char sequence.</param>
        /// <param name="offsetFromEnd">The offset.</param>
        /// <param name="length">The length.</param>
        public ReverseRope(CharSequenceRope<TSequence> sourceRope, int offsetFromEnd, int length) {
            Requires.That.
                IsIndexAndCountInRange(sourceRope, sourceRope.Count - offsetFromEnd - length, length, "offsetFromEnd", "length").Check();

            _sourceRope = sourceRope;
            _offsetFromEnd = offsetFromEnd;
            _length = length;
        }

        public override IEnumerator<char> GetEnumerator(int startIndex) {
            Requires.That.IsIndexInRange(this, startIndex, "startIndex").Check();

            int lastIndex = LastIndex;
            for (int i = 0; i < _length; i++)
                yield return _sourceRope[lastIndex - i];
        }

        public override int Count {
            get { return _length; }
        }

        public override char this[int index] {
            get {
                Requires.That.IsIndexInRange(this, index, "startIndex").Check();

                return _sourceRope[LastIndex - index];
            }
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            Requires.That.IsIndexAndCountInRange(this, sourceIndex, count, "sourceIndex", "count").Check();

            // BUG: Possible off-by-one!
            int firstIndex = _sourceRope.Count - _offsetFromEnd - count;
            _sourceRope.CopyTo(firstIndex, destination, destinationIndex, count);
            Array.Reverse(destination, destinationIndex, count);
        }

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            // _sourceRope.WriteOut(writer, _offsetFromEnd + startIndex, count);
        }

        public override Rope SubStringInternal(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;

            if (count <= MAX_SHORT_SIZE) {
                // BUG: Possible off-by-one!
                int firstIndex = _sourceRope.Count - _offsetFromEnd - count;
                var array = _sourceRope.ToArray(firstIndex, count);
                Array.Reverse(array);
                return array.ToRope();
            }
            return new ReverseRope<TSequence>(_sourceRope, _offsetFromEnd + startIndex, count);
        }

        public override Rope Reverse() {
            return _sourceRope.SubString(_sourceRope.Count - _offsetFromEnd - _length, _length);
        }
    }
}