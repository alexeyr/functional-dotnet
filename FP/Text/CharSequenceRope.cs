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

namespace FP.Text {
    /// <summary>
    /// A flat rope containing an <see cref="ICharSequence"/>.
    /// </summary>
    /// <typeparam name="TSequence">The type of the character sequence used by the rope.</typeparam>
    /// <seealso cref="StringRope"/>
    /// <seealso cref="ArrayRope"/>
    /// <seealso cref="SubstringRope{TSequence}"/>
    /// <remarks>If you plan to use this class with a specific type of character sequences, it may be convenient to 
    /// create a subclass.</remarks>
    [Serializable]
    public class CharSequenceRope<TSequence> : FlatRope
        where TSequence : ICharSequence {
        [CLSCompliant(false)] protected readonly TSequence _charSequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharSequenceRope{TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The character sequence.</param>
        public CharSequenceRope(TSequence charSequence) {
            _charSequence = charSequence;
        }

        public override sealed IEnumerator<char> GetEnumerator() {
            return _charSequence.GetEnumerator();
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public override sealed int Count {
            get { return _charSequence.Count; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// </summary>
        public override sealed char this[int index] {
            get {
                //bounds checking is responsibility of _charSequence
                return _charSequence[index];
            }
        }

        public override sealed void CopyTo(int sourceIndex, char[] destination,
                                           int destinationIndex, int count) {
            _charSequence.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public override Rope SubString(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;
            if (count <= MAX_SHORT_SIZE) {
                var array = new char[count];
                _charSequence.CopyTo(startIndex, array, 0, count);
                return
                    new ArrayRope(array);
            }
            return new SubstringRope<TSequence>(_charSequence, startIndex, count);
        }
        }
}