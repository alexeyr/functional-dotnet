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

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseRope{TSequence}"/> class.
        /// </summary>
        /// <param name="sourceRope">The original rope we want to reverse.</param>
        public ReverseRope(CharSequenceRope<TSequence> sourceRope) {
            _sourceRope = sourceRope;
        }

        public override IEnumerable<char> IteratorFrom(int startIndex) {
            Requires.That.IsIndexInRange(this, startIndex, "startIndex").Check();

            for (int i = _sourceRope.Count - 1 - startIndex; i >= 0; i--)
                yield return _sourceRope[i];
        }

        public override IEnumerable<char> ReverseIterator() {
            return _sourceRope;
        }

        public override int Count {
            get { return _sourceRope.Count; }
        }

        public override char this[int index] {
            get {
                return _sourceRope[_sourceRope.Count - 1 - index];
            }
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            Requires.That.IsIndexAndCountInRange(this, sourceIndex, count, "sourceIndex", "count").Check();

            // BUG: Possible off-by-one!
            _sourceRope.CopyTo(_sourceRope.Count - count, destination, destinationIndex, count);
            Array.Reverse(destination, destinationIndex, count);
        }

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            writer.Write(this.ToArray(startIndex, count));
        }

        internal override Rope SubStringInternal(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;

            if (count <= MAX_SHORT_SIZE) {
                // BUG: Possible off-by-one!
                var array = _sourceRope.ToArray(_sourceRope.Count - count, count);
                Array.Reverse(array);
                return array.ToRope();
            }
            var charSequence = _sourceRope.CharSequence;
            int firstIndex = _sourceRope.StartIndex + _sourceRope.Count - startIndex - count;
            return new ReverseRope<TSequence>(
                new CharSequenceRope<TSequence>(charSequence, firstIndex, count));
        }

        public override Rope Reverse() {
            return _sourceRope;
        }
    }
}