#region License
/*
* StringRope.cs is part of functional-dotnet project
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
#endregion

using System;

namespace FP.Text {
    /// <summary>
    /// A rope representing a string.
    /// </summary>
    [Serializable]
    public sealed class ArrayRope<TChar> : CharSequenceRope<TChar, ArrayCharSequence<TChar>> {
        public ArrayRope(TChar[] array) : base(new ArrayCharSequence<TChar>(array)) { }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="FP.Text.StringRope"/>.
        /// </summary>
        /// <param name="array">The string.</param>
        /// <returns>The rope containing the string.</returns>
        public static implicit operator ArrayRope<TChar>(TChar[] array) {
            return new ArrayRope<TChar>(array);
        }
    } // class StringRope
} // namespace FP.Text