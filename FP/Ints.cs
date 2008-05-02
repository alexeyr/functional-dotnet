/* (C) Alexey Romanov 2008 */

using System;
using System.Collections.Generic;

namespace FP {
    /// <summary>
    /// Static methods for generating sequences of <see cref="int"/>s and other convenience methods.
    /// </summary>
    public static class Ints {
        /// <summary>Generates a sequence of integral numbers starting with a specified number.</summary>
        /// <returns>An <see cref="IEnumerable{int}"/> that contains a range of sequential 
        /// integral numbers up to <see cref="int.MaxValue"/>.</returns>
        /// <param name="start">The value of the first integer in the sequence.</param>
        public static IEnumerable<int> From(int start) {
            for (int i = start; i < int.MaxValue; i++)
                yield return i;

            //[start..]
        }

        /// <summary>Generates a sequence of integral numbers starting with a specified number
        /// with a specified step.</summary>
        /// <returns>An <see cref="IEnumerable{int}"/> that contains a range of 
        /// integral numbers up to <see cref="int.MaxValue"/> or down to
        /// <see cref="int.MinValue"/>.</returns>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="step">The difference between two consequent elements of the
        /// sequence.</param>
        /// <exception cref="ArgumentException">if <paramref name="step"/> equals 0.</exception>
        public static IEnumerable<int> From(int start, int step) {
            if (step == 0)
                throw new ArgumentException();
            if (step > 0) {
                for (int i = start; i < int.MaxValue; i += step)
                    yield return i;
                yield break;
            }
            for (int i = start; i > int.MinValue; i += step)
                yield return i;

            //[start, start + step..]
        }

        /// <summary>Generates a sequence of integral numbers starting and ending with a specified number.</summary>
        /// <returns>An <see cref="IEnumerable{int}"/> that contains a range of sequential 
        /// integral numbers.</returns>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="end">The value of the last integer in the sequence.</param>
        public static IEnumerable<int> Range(int start, int end) {
            for (int i = start; i <= end; i++)
                yield return i;

            //[start..end]
        }

        /// <summary>Generates a sequence of integral numbers starting and ending with a specified number
        /// with a specified step.</summary>
        /// <returns>An <see cref="IEnumerable{int}"/> that contains a range of 
        /// sequential integral numbers.</returns>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="end">The value of the last integer in the sequence.</param>
        /// <param name="step">The difference between two consequent elements of the
        /// sequence.</param>
        /// <exception cref="ArgumentException">if <paramref name="step"/> equals 0
        /// or has a different sign from <c><paramref name="end"/> - <paramref name="start"/></c>.</exception>
        public static IEnumerable<int> Range(int start, int end,
                                             int step) {
            if (step == 0 || step * (end - start) < 0)
                throw new ArgumentException();
            if (step > 0) {
                for (int i = start; i <= end; i += step)
                    yield return i;
                yield break;
            }
            for (int i = start; i >= end; i += step)
                yield return i;

            //[start, start + step..end]
        }

        /// <summary>
        /// Performs an action several times.
        /// </summary>
        /// <param name="self">The number of times.</param>
        /// <param name="action">The action.</param>
        public static void Times (this int self, Action action) {
            for (int i = 0; i < self; i++) {
                action();
            }
        }
    }
}
