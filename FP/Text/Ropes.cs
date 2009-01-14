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

using System;
using System.Collections.Generic;
using System.Globalization;

namespace FP.Text {
    /// <summary>
    /// Utility class for Ropes.
    /// </summary>
    public static class Ropes {
        /// <summary>
        /// Converts a string to a rope.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>The rope holding <paramref name="s"/>.</returns>
        public static CharSequenceRope<StringCharSequence> ToRope(this string s) {
            return new CharSequenceRope<StringCharSequence>(new StringCharSequence(s));
        } // ToRope()

        /// <summary>
        /// Converts an array to a rope.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The rope holding <paramref name="array"/>.</returns>
        public static CharSequenceRope<ArrayCharSequence> ToRope(this char[] array) {
            return new CharSequenceRope<ArrayCharSequence>(new ArrayCharSequence(array));
        } // ToRope()

        /// <summary>
        /// Converts a char sequence to a rope.
        /// </summary>
        /// <param name="charSequence">The array.</param>
        /// <returns>The rope holding <paramref name="charSequence"/>.</returns>
        public static Rope ToRope<TCharSequence>(this TCharSequence charSequence) 
            where TCharSequence : IFlatCharSequence {
            var asRope = charSequence as Rope;
            if (asRope != null)
                return asRope;
            return new CharSequenceRope<TCharSequence>(charSequence);
        } // ToRope()

        public static TRope Append<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence) 
            where TRope : IRope<TRope> where TCharSequence : IFlatCharSequence {
            return rope.Append(charSequence, 0, charSequence.Count);
        }

        public static TRope Append<TRope>(this TRope rope, char ch) where TRope : IRope<TRope> {
            return rope.Append(new RepeatedCharSequence(ch, 1));
        }

        public static TRope Prepend<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence)
            where TRope : IRope<TRope>
            where TCharSequence : IFlatCharSequence {
            return rope.Prepend(charSequence, 0, charSequence.Count);
        }

        public static TRope Prepend<TRope>(this TRope rope, char ch) where TRope : IRope<TRope> {
            return rope.Prepend(new RepeatedCharSequence(ch, 1));
        }

        public static TRope Remove<TRope>(this TRope rope, int startIndex) where TRope : IRope<TRope> {
            return Remove(rope, startIndex, 1);
        }

        public static TRope Remove<TRope>(this TRope rope, int startIndex, int count) where TRope : IRope<TRope> {
            return rope.SubString(0, startIndex).Concat(
                rope.SubString(startIndex + count, rope.Count - startIndex - count));
        }

        public static TRope Insert<TRope, TCharSequence>(this TRope rope, int startIndex, TCharSequence charSequence) 
            where TRope : IRope<TRope> where TCharSequence : ICharSequence {
            throw new System.NotImplementedException();
        }

        public static TRope PadStart<TRope>(this TRope rope, int totalWidth, char paddingChar) where TRope : IRope<TRope> {
            int paddingWidth = rope.Count - totalWidth;
            if (paddingWidth <= 0)
                return rope;
            return rope.Prepend(new RepeatedCharSequence(paddingChar, paddingWidth));
        }

        public static TRope PadEnd<TRope>(this TRope rope, int totalWidth, char paddingChar) where TRope : IRope<TRope> {
            int paddingWidth = rope.Count - totalWidth;
            if (paddingWidth <= 0)
                return rope;
            return rope.Append(new RepeatedCharSequence(paddingChar, paddingWidth));
        }

        public static TRope StartsWithOrdinal<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence, bool ignoreCase)
            where TRope : IRope<TRope>
            where TCharSequence : ICharSequence {
            throw new System.NotImplementedException();
        }

        public static TRope EndsWithOrdinal<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence, bool ignoreCase)
            where TRope : IRope<TRope>
            where TCharSequence : ICharSequence {
            throw new System.NotImplementedException();
        }

        public static TRope IndexOfOrdinal<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence, bool ignoreCase)
            where TRope : IRope<TRope>
            where TCharSequence : ICharSequence {
            throw new System.NotImplementedException();
        }

        public static TRope EqualsOrdinal<TRope, TCharSequence>(this TRope rope, TCharSequence charSequence, bool ignoreCase)
            where TRope : IRope<TRope>
            where TCharSequence : ICharSequence {
            throw new System.NotImplementedException();
        }

        public static bool StartsWith<TRope>(this TRope rope, string prefix, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static bool EndsWith<TRope>(this TRope rope, string suffix, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int CompareTo<TRope>(this TRope rope, string str, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static bool Equals<TRope>(this TRope rope, string str, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int IndexOf<TRope>(this TRope rope, char ch, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int IndexOf<TRope>(this TRope rope, string str, int startIndex, int count, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int IndexOfAny<TRope>(this TRope rope, char[] anyOf, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int IndexOfAny<TRope>(this TRope rope, Func<char, bool> anyOf, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int LastIndexOf<TRope>(this TRope rope, char ch, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int LastIndexOf<TRope>(this TRope rope, string str, int startIndex, int count, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int LastIndexOfAny<TRope>(this TRope rope, char[] anyOf, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int LastIndexOfAny<TRope>(this TRope rope, Func<char, bool> anyOf, int startIndex, int count)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static IEnumerable<TRope> Split<TRope>(this TRope rope, char[] separators, StringSplitOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static IEnumerable<TRope> Split<TRope>(this TRope rope, string[] separators, StringSplitOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }
    } // class Ropes
} // namespace FP.Text