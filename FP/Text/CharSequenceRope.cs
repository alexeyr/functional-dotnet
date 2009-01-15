/*
* CharSequenceRope.cs is part of functional-dotnet project
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
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// A rope backed by (possibly part of) a character sequence.
    /// </summary>
    /// <typeparam name="TSequence">The type of the sequence.</typeparam>
    [Serializable]
    public sealed class CharSequenceRope<TSequence> : FlatRope where TSequence : IFlatCharSequence {
        /// <summary>
        /// Gets the character sequence backing this rope.
        /// </summary>
        /// <value>The char sequence.</value>
        public TSequence CharSequence { get; private set; }

        /// <summary>
        /// Gets the starting index of part of the character sequence contained by the rope.
        /// </summary>
        /// <value>The offset.</value>
        public int StartIndex { get; private set; }
        
        private readonly int _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharSequenceRope{TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The char sequence.</param>
        public CharSequenceRope(TSequence charSequence) : this(charSequence, 0, charSequence.Count) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharSequenceRope{TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The char sequence.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="count">The length.</param>
        public CharSequenceRope(TSequence charSequence, int startIndex, int count) {
            Requires.That.
                IsIndexAndCountInRange(charSequence, startIndex, count, "startIndex", "count").Check();

            CharSequence = charSequence;
            StartIndex = startIndex;
            _count = count;
        }

        public override IEnumerator<char> GetEnumerator(int startIndex) {
            Requires.That.IsIndexInRange(this, startIndex, "startIndex").Check();

            using (var en = CharSequence.GetEnumerator(StartIndex + startIndex)) {
                int i = 0;
                while (i < _count && en.MoveNext()) {
                    yield return en.Current;
                    i++;
                }
            }
        }

        public override IEnumerable<char> ReverseIterator() {
            for (int i = StartIndex + _count - 1; i >= StartIndex; i--)
                yield return CharSequence[i];
        }

        public override int Count {
            get { return _count; }
        }

        public override char this[int index] {
            get {
                Requires.That.IsIndexInRange(this, index, "startIndex").Check();

                return CharSequence[StartIndex + index];
            }
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            Requires.That.IsIndexAndCountInRange(this, sourceIndex, count, "sourceIndex", "count").Check();

            CharSequence.CopyTo(StartIndex + sourceIndex, destination, destinationIndex, count);
        }

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            CharSequence.WriteOut(writer, StartIndex + startIndex, count);
        }

        internal override Rope SubStringInternal(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;
            if (count <= MAX_SHORT_SIZE) {
                var array = CharSequence.ToArray(StartIndex + startIndex, count);
                return array.ToRope();
            }

            return new CharSequenceRope<TSequence>(CharSequence, StartIndex + startIndex, count);
        }

        public override Rope Reverse() {
            return new ReverseRope<TSequence>(this);
        }
    }
}