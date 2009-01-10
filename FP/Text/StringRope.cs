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

using System;

namespace FP.Text {
    /// <summary>
    /// A rope representing a string.
    /// </summary>
    [Serializable]
    public sealed class StringRope : CharSequenceRope<StringCharSequence> {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringRope"/> class.
        /// </summary>
        /// <param name="s">The string.</param>
        public StringRope(string s) : base(new StringCharSequence(s)) {}
    } // class StringRope
} // namespace FP.Text