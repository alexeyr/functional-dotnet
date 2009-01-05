using System;
using System.Diagnostics;

namespace FP.Util {
    internal static class ArrayUtil {
        public static bool ContentEquals<T>(T[] array1, T[] array2) {
            if (ReferenceEquals(array1, array2)) return true;
            if (array1.Length != array2.Length) return false;
            for (int i = 0; i < array1.Length; i++) {
                if (!Equals(array1[i], array2[i])) return false;
            }
            return true;
        }

        internal static T[] CopyNoChecks<T>(this T[] array) {
            return array.CopyNoChecks(0, array.Length - 1);
        }

        internal static T[] CopyNoChecks<T>(this T[] array, int startIndex) {
            return array.CopyNoChecks(startIndex, array.Length - startIndex - 1);
        }

        internal static T[] CopyNoChecks<T>(this T[] array, int startIndex, int length) {
            Debug.Assert(startIndex >= 0);
            Debug.Assert(length >= 0);
            Debug.Assert(startIndex + length <= array.Length);
            var result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }

        /// <summary>
        /// Specialized version of FoldLeft for arrays.
        /// </summary>
        public static A FoldLeft<T, A>(this T[] array, Func<A, T, A> binOp, A initial) {
            A result = initial;
            for (int i = array.Length - 1; i >= 0; i--)
                result = binOp(result, array[i]);
            return result;
        }

        /// <summary>
        /// Specialized version of FoldRight for arrays.
        /// </summary>
        public static A FoldRight<T, A>(this T[] array, Func<T, A, A> binOp, A initial) {
            A result = initial;
            for (int i = array.Length - 1; i >= 0; i--)
                result = binOp(array[i], result);
            return result;
        }

        public static T[] EmptyArray<T>() {
            return EmptyArrayCache<T>.Instance;
        }

        private static class EmptyArrayCache<T> {
            private static T[] _instance;

            public static T[] Instance {
                get {
                    if (_instance == null)
                        _instance = new T[0];
                    return _instance;
                }
            }
        }
    }
}