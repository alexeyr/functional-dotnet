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