/*
* FlatRope.cs is part of functional-dotnet project
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
using System.Diagnostics;
using FP.Collections;
using FP.Util;

namespace FP.Text {
    /// <summary>
    /// A leaf node of a <see cref="Rope"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Rope"/> doesn't implement <see cref="IMeasured{V}"/> so that there 
    /// </remarks>
    [Serializable]
    [DebuggerDisplay("Count = {Count}: {this.AsString()}")]
    public abstract class FlatRope : Rope, IFlatCharSequence, IMeasured<int> {
        public sealed override byte Depth {
            get { return 0; }
        }

        protected sealed override int RightChildCount {
            get { return Count; }
        }

        protected sealed override int LeftChildCount {
            get { return Count; }
        }

        public sealed override bool IsBalanced {
            get { return true; }
        }

        public sealed override Rope Rebalance() {
            return this;
        }

        protected override IFlatCharSequence Flatten() {
            return this;
        }

        /// <summary>
        /// Gets the measure of the object.
        /// </summary>
        /// <value>The measure.</value>
        public int Measure {
            get { return Count; }
        }

        protected internal sealed override Rope AppendShort<TCharSequence>(TCharSequence sequence, int startIndex, int count) {
            int thisCount = Count;
            var array = new char[thisCount + count];
            this.CopyTo(array, 0);
            sequence.CopyTo(startIndex, array, thisCount, count);
            return array.ToRope();
        }

        protected internal sealed override Rope PrependShort<TCharSequence>(TCharSequence sequence, int startIndex, int count) {
            var array = new char[count + Count];
            sequence.CopyTo(startIndex, array, 0, count);
            this.CopyTo(array, count);
            return array.ToRope();
        }

        internal override Rope TrimStartInternal(char[] trimChars) {
            int i = 0;
            // Use iterator since it may be substantially cheaper for StreamCharSequence
            // and shouldn't cost too much in other cases
            foreach (char c in this) {
                if (!trimChars.Contains(c)) break;
                i++;
            }
            if (Count - i == 0)
                return EmptyInstance;
            return SubStringInternal(i, Count - i);
        }

        internal override Rope TrimEndInternal(char[] trimChars) {
            int i;
            for (i = Count - 1; i >= 0; i--) {
                if (!trimChars.Contains(this[i])) break;
            }
            if (i == -1)
                return EmptyInstance;
            return SubStringInternal(0, i + 1);
        }
    }
}