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
    [Serializable]
    public sealed class SubstringRope<TChar, TSequence> : CharSequenceRope<TChar, CharSubsequence<TChar, TSequence>> where TSequence : ICharSequence<TChar> {
        public SubstringRope(TSequence charSequence, int offset, int length) : base(new CharSubsequence<TChar, TSequence>(charSequence, offset, length)) {}
        public SubstringRope(CharSubsequence<TChar, TSequence> subsequence, int offset, int length) : base(new CharSubsequence<TChar, TSequence>(subsequence, offset, length)) { }

        public override Rope<TChar> SubString(int startIndex, int length) {
            if (startIndex == 0 && length == Length)
                return this;
            if (length <= MaxShortSize) {
                var array = new TChar[length];
                _charSequence.CopyTo(startIndex, array, 0, length);
                return
                    new ArrayRope<TChar>(array);
            }
            return new SubstringRope<TChar, TSequence>(_charSequence, startIndex, length);
        }
    }
}