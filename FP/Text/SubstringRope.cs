/*
* SubstringRope.cs is part of functional-dotnet project
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

namespace FP.Text {
    /// <summary>
    /// A rope which is a substring of another rope.
    /// </summary>
    /// <typeparam name="TSequence">The type of the character sequence.</typeparam>
    [Serializable]
    public sealed class SubstringRope<TSequence> :
        CharSequenceRope<CharSubsequence<TSequence>>
        where TSequence : ICharSequence {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubstringRope{TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The char sequence.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public SubstringRope(TSequence charSequence, int offset, int length)
            : base(new CharSubsequence<TSequence>(charSequence, offset, length)) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstringRope{TSequence}"/> class.
        /// </summary>
        /// <param name="subsequence">The subsequence.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public SubstringRope(CharSubsequence<TSequence> subsequence, int offset, int length)
            : base(new CharSubsequence<TSequence>(subsequence, offset, length)) {}

        public override Rope SubString(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;
            if (count <= MAX_SHORT_SIZE) {
                char[] array = new char[count];
                _charSequence.CopyTo(startIndex, array, 0, count);
                return
                    new ArrayRope(array);
            }
            return new SubstringRope<TSequence>(_charSequence, startIndex, count);
        }
        }
}