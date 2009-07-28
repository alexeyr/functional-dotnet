/*
* BoyerMooreHorspool.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
    /// Represents a preprocessed pattern used for searching in character sequences
    /// </summary>
    [Serializable]
    public struct BoyerMooreHorspool {
        internal readonly int[] BadCharShift;

        public BoyerMooreHorspool(string s) : this(s, false) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="BoyerMooreHorspool"/> struct.
        /// </summary>
        /// <param name="pattern">The pattern we will be searching for.</param>
        /// <param name="ignoreCase">If set to <c>true</c>, case will be ignored .</param>
        public BoyerMooreHorspool(string pattern, bool ignoreCase) {
            BadCharShift = new int[256];
            int length = pattern.Length;
            for (int i = 0; i < 256; i++)
                BadCharShift[i] = length;
            if (ignoreCase) {
                for (int i = 0; i < length; i++) {
                    char c = pattern[i];
                    if (char.IsLetter(c)) {
                        BadCharShift[char.ToLowerInvariant(c) & 255] =
                            BadCharShift[char.ToUpperInvariant(c) & 255] = length - i - 1;
                    }
                    else
                        BadCharShift[c & 255] = length - i - 1;
                }
            }
            else {
                for (int i = 0; i < length; i++)
                    BadCharShift[pattern[i] & 255] = length - i - 1;                
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoyerMooreHorspool"/> struct.
        /// </summary>
        /// <param name="pattern">The pattern we will be searching for.</param>
        /// <param name="ignoreCase">If set to <c>true</c>, case will be ignored .</param>
        public BoyerMooreHorspool(IRandomAccess<char> pattern, bool ignoreCase) {
            BadCharShift = new int[256];
            int length = pattern.Count;
            for (int i = 0; i < 256; i++)
                BadCharShift[i] = length;
            if (ignoreCase) {
                int i = 0;
                foreach (char c in pattern) {
                    if (char.IsLetter(c)) {
                        BadCharShift[char.ToLowerInvariant(c) & 255] =
                            BadCharShift[char.ToUpperInvariant(c) & 255] = length - i - 1;
                    }
                    else
                        BadCharShift[c & 255] = length - i - 1;
                    i++;
                }
            }
            else {
                int i = 0;
                foreach (char c in pattern) {
                    BadCharShift[c & 255] = length - i - 1;
                    i++;
                }
            }
        }
    }
}