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

using System.Collections.Generic;
using System.Globalization;
using FP.Core;

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
        public static FlatRope ToRope(this string s) {
            return new CharSequenceRope<StringCharSequence>(new StringCharSequence(s));
        } // ToRope()

        /// <summary>
        /// Converts an array to a rope.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The rope holding <paramref name="array"/>.</returns>
        public static FlatRope ToRope(this char[] array) {
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
            where TRope : IRope<TRope> where TCharSequence : IFlatCharSequence {
            if (startIndex == 0)
                return rope.Prepend(charSequence);
            if (startIndex == rope.Count)
                return rope.Append(charSequence);
            return rope.SubString(0, startIndex).Append(charSequence).
                Concat(rope.SubString(startIndex, rope.Count - startIndex));
        }

        public static TRope Insert<TRope>(this TRope rope, int startIndex, TRope rope2)
            where TRope : IRope<TRope> {
            if (startIndex == 0)
                return rope2.Concat(rope2);
            if (startIndex == rope.Count)
                return rope.Concat(rope2);
            return rope.SubString(0, startIndex).Concat(rope2).
                Concat(rope.SubString(startIndex, rope.Count - startIndex));
        }

        public static TRope PadStart<TRope>(this TRope rope, int totalWidth, char paddingChar) where TRope : IRope<TRope> {
            int paddingWidth = totalWidth - rope.Count;
            if (paddingWidth <= 0)
                return rope;
            return rope.Prepend(new RepeatedCharSequence(paddingChar, paddingWidth));
        }

        public static TRope PadEnd<TRope>(this TRope rope, int totalWidth, char paddingChar) where TRope : IRope<TRope> {
            int paddingWidth = totalWidth - rope.Count;
            if (paddingWidth <= 0)
                return rope;
            return rope.Append(new RepeatedCharSequence(paddingChar, paddingWidth));
        }

        public static bool StartsWith<TRope>(
            this TRope rope, string prefix, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static bool EndsWith<TRope>(
            this TRope rope, string suffix, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int CompareTo<TRope>(
            this TRope rope, string str, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static bool Equals<TRope>(this TRope rope, string str, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int IndexOf<TRope>(this TRope rope, string str, int startIndex, int count, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static int LastIndexOf<TRope>(this TRope rope, string str, int startIndex, int count, CultureInfo culture, CompareOptions options)
            where TRope : IRope<TRope> {
            throw new System.NotImplementedException();
        }

        public static Rope Concat(this IEnumerable<Rope> ropes) {
            var rope = Rope.EmptyInstance;
            foreach (var rope1 in ropes)
                rope = rope.Concat(rope1);
            return rope;
        }

        public static Rope Join(this IEnumerable<Rope> ropes, Rope separator) {
            if (separator.IsNullOrEmpty())
                return ropes.Concat();
            return ropes.Intersperse(separator).Concat();
        }

        public static Rope Concat<TSequence>(this IEnumerable<TSequence> ropes) 
            where TSequence : IFlatCharSequence {
            var rope = Rope.EmptyInstance;
            foreach (var sequence in ropes)
                rope = rope.Append(sequence);
            return rope;
        }

        public static Rope Join<TSequence>(this IEnumerable<TSequence> ropes, TSequence separator) 
            where TSequence : IFlatCharSequence {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (separator.IsNullOrEmpty())
                return ropes.Concat();
            // ReSharper restore CompareNonConstrainedGenericWithNull
            return ropes.Intersperse(separator).Concat();
        }

        public static IRope<TRope> Trim<TRope>(this IRope<TRope> rope, params char[] trimChars) 
            where TRope : IRope<TRope> {
            return rope.TrimStart(trimChars).TrimEnd(trimChars);
        }
    } // class Ropes
} // namespace FP.Text