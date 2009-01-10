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
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// A leaf node of a <see cref="Rope"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="Rope"/> doesn't implement <see cref="IMeasured{V}"/> so that there 
    /// </remarks>
    [Serializable]
    public abstract class FlatRope : Rope, IMeasured<int> {
        //TODO: Try to pull CharSequenceRope up into this class and make it sealed
        internal override Rope ConcatShort(FlatRope otherFlat) {
            return new StringRope(this.ToString() + otherFlat.ToString());
        } // ConcatShort(otherFlat)

        protected internal override sealed byte Depth {
            get { return 0; }
        }

        protected internal override sealed bool IsRightMostChildFlatAndShort {
            get { return Count < MAX_SHORT_SIZE; }
        }

        protected internal override sealed bool IsBalanced {
            get { return true; }
        }

        public override sealed Rope ReBalance() {
            return this;
        }

        /// <summary>
        /// Gets the measure of the object.
        /// </summary>
        /// <value>The measure.</value>
        public int Measure {
            get { return Count; }
        }
    }
}