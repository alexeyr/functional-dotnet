/*
* Ropes.cs is part of functional-dotnet project
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

namespace FP.Text {
    /// <summary>
    /// Utility class for Ropes.
    /// </summary>
    public static class Ropes {
        public static StringRope ToRope(this string s) {
            return new StringRope(s);
        } // ToRope()

        public static ArrayRope<TChar> ToRope<TChar>(this TChar[] array) {
            return new ArrayRope<TChar>(array);
        } // ToRope(, array)
    } // class Ropes
} // namespace FP.Text