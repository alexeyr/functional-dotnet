/* (C) Alexey Romanov 2008 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Net;
using FP.HaskellNames;

// ReSharper disable RedundantIfElseBlock
namespace FP.Collections.Immutable {

    /// <summary>
    /// A convenience static class to provide static methods for <see cref="Maybe{T}"/> and
    /// <see cref="Nullable{T}"/>.
    /// </summary>
    public static class Maybe {
        /// <summary>
        /// Return a wrapper around the specified non-null object.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="t">The object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if <paramref name="t"/> is <c>null</c>.</exception>
        public static Maybe<T> Just<T>(T t) {
            if (t == null)
                throw new ArgumentNullException();
            else
                return new Maybe<T>(t);
        }

        /// <summary>
        /// Returns <see cref="Maybe{T}.Nothing"/>.
        /// </summary>
        public static Maybe<T> Nothing<T>() {
            return Maybe<T>.Nothing;
        }

        /// <summary>
        /// Converts a sequence to maybe.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns><c>Nothing</c> if <paramref name="sequence"/> is empty;
        /// <c>Just(sequence.First())</c> otherwise.</returns>
        public static Maybe<T> ToMaybe<T>(this IEnumerable<T> sequence) {
            using (var enumerator = sequence.GetEnumerator()) {
                if (enumerator.MoveNext())
                    return Just(enumerator.Current);
                else
                    return Nothing<T>();
            }
        }

        /// <summary>
        /// Converts <see cref="Nullable{T}"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="nullable">The nullable.</param>
        /// <returns><c>Nothing</c> if <paramref name="nullable"/> doesn't have a value;
        /// <c>Just(nullable.Value)</c> otherwise.</returns>
        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct {
            return nullable.HasValue ? Just(nullable.Value) : Nothing<T>();
        }

        /// <summary>
        /// Converts <see cref="Maybe{T}"/> to <see cref="Nullable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe">The maybe.</param>
        /// <returns><c>null</c> if <paramref name="maybe"/> is <c>Nothing</c>;
        /// <c>maybe.Value</c> otherwise.</returns>
        public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct {
            return maybe.MapOrElse(x => x, null);
        }

        /// <summary>
        /// An explicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}"/>.
        /// Works the same as the implicit cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        public static Maybe<T> FromValue<T>(T value) {
            return value;
        }

        /// <summary>
        /// An explicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}"/>.
        /// Works the same as the implicit cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        public static Maybe<T> Wrap<T>(T value) {
            return value;
        }

        /// <summary>
        /// Flattens the specified maybe.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe">The maybe.</param>
        /// <returns></returns>
        public static Maybe<T> Flatten<T>(this Maybe<Maybe<T>> maybe) {
            return maybe.Map(m => m.Value);
        }

        /// <summary>
        /// Selects the values of elements which have them.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Maybe<T>> sequence) {
            foreach (var maybe in sequence)
                if (maybe.HasValue)
                    yield return maybe.Value;
        }

        /// <summary>
        /// Selects the values of elements which have them.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<T?> sequence) where T : struct {
            return sequence.Map(x => x.ToMaybe()).SelectValues();
        }

        /// <summary>
        /// A version of <see cref="Enumerables2.Map{T,TR}"/> which can throw away elements.
        /// In particular, if <c>function(x)</c> doesn't have a value for an element <c>x</c> of 
        /// the <paramref name="sequence"/>, no element is included in the result; if it has value <c>y</c>,
        /// <c>y</c> is included in the list.
        /// </summary>
        /// <typeparam name="T">Type of elements of the sequence.</typeparam>
        /// <typeparam name="R">Type of elements of the resulting sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IEnumerable<R> MapMaybe<T, R>(this IEnumerable<T> sequence, Func<T, Maybe<R>> function) {
            return sequence.Map(function).SelectValues();
        }

        /// <summary>
        /// A version of <see cref="Enumerables2.Map{T,TR}"/> which can throw away elements.
        /// In particular, if <c>function(x)</c> doesn't have a value for an element <c>x</c> of 
        /// the <paramref name="sequence"/>, no element is included in the result; if it has value <c>y</c>,
        /// <c>y</c> is included in the list.
        /// </summary>
        /// <typeparam name="T">Type of elements of the sequence.</typeparam>
        /// <typeparam name="R">Type of elements of the resulting sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IEnumerable<R> MapMaybe<T, R>(this IEnumerable<T> sequence, Func<T, R?> function) where R : struct {
            return sequence.Map(function).SelectValues();
        }

        /// <summary>
        /// Tries the specified function.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns><see cref="Maybe{T}.Nothing"/> if there were exceptions or the function returns <c>null</c>;
        /// <c>Just(function())</c> otherwise.</returns>
        public static Maybe<T> Try<T>(this Func<T> function) {
            return function.Try<T, Exception>();
        }

        /// <summary>
        /// Tries the specified function, catching only exceptions of type <typeparam name="E"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <typeparam name="E">The type of exceptions to catch.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns><see cref="Maybe{T}.Nothing"/> if there were exceptions or the function returns <c>null</c>;
        /// <c>Just(function())</c> otherwise.</returns>
        public static Maybe<T> Try<T, E>(this Func<T> function) where E : Exception {
            try {
                return new Maybe<T>(function());
            }
            catch (E) {
                return new Maybe<T>();
            }
        }

#region Tries -- The versions of "Try..." methods from the framework
        /// <summary>
        /// Creates a new Uri using the specified String instance and a <see cref="UriKind"/>.
        /// </summary>
        public static Maybe<Uri> CreateUri(string uriString, UriKind uriKind) {
            Uri uri;
            return Uri.TryCreate(uriString, uriKind, out uri) ? uri : Nothing<Uri>();
        }

        /// <summary>
        /// Creates a new Uri using the specified base and relative String instances.
        /// </summary>
        public static Maybe<Uri> CreateUri(Uri baseUri, string relativeUri) {
            Uri uri;
            return Uri.TryCreate(baseUri, relativeUri, out uri) ? uri : Nothing<Uri>();
        }

        /// <summary>
        /// Creates a new Uri using the specified base and relative Uri instances.
        /// </summary>
        public static Maybe<Uri> CreateUri(Uri baseUri, Uri relativeUri) {
            Uri uri;
            return Uri.TryCreate(baseUri, relativeUri, out uri) ? uri : Nothing<Uri>();
        }

        /// <summary>
        /// Retrieves the value corresponding to the specified key.
        /// </summary>
        /// <remarks>This method doesn't distinguish between cases when the value is <c>null</c>
        /// and when the dictionary doesn't contain the key. Both cases return <c>Nothing</c>.</remarks>
        public static Maybe<TValue> GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : Nothing<TValue>();
        }

        /// <summary>
        /// Retrieves a value corresponding to the supplied key from this <see cref="DbConnectionStringBuilder"/>.
        /// </summary>
        public static Maybe<object> GetValue(this DbConnectionStringBuilder connectionStringBuilder, string keyword) {
            object value;
            return connectionStringBuilder.TryGetValue(keyword, out value) ? FromValue(value) : Nothing<object>();
        }

        ///<summary>
        ///Contains static methods for parsing strings which return <see cref="Maybe{T}"/>.
        ///</summary>
        public static class Parse {
            /// <summary>
            /// Parses a string which represents an <see cref="int"/>.
            /// </summary>
            public static Maybe<int> Int(string s) {
                int i;
                return int.TryParse(s, out i) ? i : Nothing<int>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="int"/>.
            /// </summary>
            public static Maybe<int> Int(string s, NumberStyles styles, IFormatProvider formatProvider) {
                int i;
                return int.TryParse(s, styles, formatProvider, out i) ? i : Nothing<int>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTime"/>.
            /// </summary>
            public static Maybe<DateTime> DateTime(string s) {
                DateTime dateTime;
                return System.DateTime.TryParse(s, out dateTime) ? dateTime : Nothing<DateTime>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTime"/>.
            /// </summary>
            public static Maybe<DateTime> DateTime(string s, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTime dateTime;
                return System.DateTime.TryParse(s, formatProvider, styles, out dateTime) ? dateTime : Nothing<DateTime>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTimeOffset"/>.
            /// </summary>
            public static Maybe<DateTimeOffset> DateTimeOffset(string s) {
                DateTimeOffset offset;
                return System.DateTimeOffset.TryParse(s, out offset) ? offset : Nothing<DateTimeOffset>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTimeOffset"/>.
            /// </summary>
            public static Maybe<DateTimeOffset> DateTimeOffset(string s, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTimeOffset offset;
                return System.DateTimeOffset.TryParse(s, formatProvider, styles, out offset) ? offset : Nothing<DateTimeOffset>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTime"/>.
            /// </summary>
            public static Maybe<DateTime> ExactDateTime(string s, string format, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTime dateTime;
                return System.DateTime.TryParseExact(s, format, formatProvider, styles, out dateTime) ? dateTime : Nothing<DateTime>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTime"/>.
            /// </summary>
            public static Maybe<DateTime> ExactDateTime(string s, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTime dateTime;
                return System.DateTime.TryParseExact(s, formats, formatProvider, styles, out dateTime) ? dateTime : Nothing<DateTime>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTimeOffset"/>.
            /// </summary>
            public static Maybe<DateTimeOffset> ExactDateTimeOffset(string s, string format, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTimeOffset offset;
                return System.DateTimeOffset.TryParseExact(s, format, formatProvider, styles, out offset) ? offset : Nothing<DateTimeOffset>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.DateTimeOffset"/>.
            /// </summary>
            public static Maybe<DateTimeOffset> ExactDateTimeOffset(string s, string[] formats, IFormatProvider formatProvider, DateTimeStyles styles) {
                DateTimeOffset offset;
                return System.DateTimeOffset.TryParseExact(s, formats, formatProvider, styles, out offset) ? offset : Nothing<DateTimeOffset>();
            }

            /// <summary>
            /// Parses a string which represents a <see cref="T:System.TimeSpan"/>.
            /// </summary>
            public static Maybe<TimeSpan> TimeSpan(string s) {
                TimeSpan offset;
                return System.TimeSpan.TryParse(s, out offset) ? offset : Nothing<TimeSpan>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="int"/>.
            /// </summary>
            public static Maybe<bool> Bool(string s) {
                bool b;
                return bool.TryParse(s, out b) ? b : Nothing<bool>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="int"/>.
            /// </summary>
            public static Maybe<char> Char(string s) {
                char c;
                return char.TryParse(s, out c) ? c : Nothing<char>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="byte"/>.
            /// </summary>
            public static Maybe<byte> Byte(string s) {
                byte b;
                return byte.TryParse(s, out b) ? b : Nothing<byte>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="byte"/>.
            /// </summary>
            public static Maybe<byte> Byte(string s, NumberStyles styles, IFormatProvider formatProvider) {
                byte b;
                return byte.TryParse(s, styles, formatProvider, out b) ? b : Nothing<byte>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="decimal"/>.
            /// </summary>
            public static Maybe<decimal> Decimal(string s) {
                decimal d;
                return decimal.TryParse(s, out d) ? d : Nothing<decimal>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="decimal"/>.
            /// </summary>
            public static Maybe<decimal> Decimal(string s, NumberStyles styles, IFormatProvider formatProvider) {
                decimal d;
                return decimal.TryParse(s, styles, formatProvider, out d) ? d : Nothing<decimal>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="double"/>.
            /// </summary>
            public static Maybe<double> Double(string s) {
                double d;
                return double.TryParse(s, out d) ? d : Nothing<double>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="double"/>.
            /// </summary>
            public static Maybe<double> Double(string s, NumberStyles styles, IFormatProvider formatProvider) {
                double d;
                return double.TryParse(s, styles, formatProvider, out d) ? d : Nothing<double>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="short"/>.
            /// </summary>
            public static Maybe<short> Short(string s) {
                short s1;
                return short.TryParse(s, out s1) ? s1 : Nothing<short>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="short"/>.
            /// </summary>
            public static Maybe<short> Short(string s, NumberStyles styles, IFormatProvider formatProvider) {
                short s1;
                return short.TryParse(s, styles, formatProvider, out s1) ? s1 : Nothing<short>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="long"/>.
            /// </summary>
            public static Maybe<long> Long(string s) {
                long l;
                return long.TryParse(s, out l) ? l : Nothing<long>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="long"/>.
            /// </summary>
            public static Maybe<long> Long(string s, NumberStyles styles, IFormatProvider formatProvider) {
                long l;
                return long.TryParse(s, styles, formatProvider, out l) ? l : Nothing<long>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="sbyte"/>.
            /// </summary>
            public static Maybe<sbyte> Sbyte(string s) {
                sbyte s1;
                return sbyte.TryParse(s, out s1) ? s1 : Nothing<sbyte>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="sbyte"/>.
            /// </summary>
            public static Maybe<sbyte> Sbyte(string s, NumberStyles styles, IFormatProvider formatProvider) {
                sbyte s1;
                return sbyte.TryParse(s, styles, formatProvider, out s1) ? s1 : Nothing<sbyte>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="float"/>.
            /// </summary>
            public static Maybe<float> Float(string s) {
                float f;
                return float.TryParse(s, out f) ? f : Nothing<float>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="float"/>.
            /// </summary>
            public static Maybe<float> Float(string s, NumberStyles styles, IFormatProvider formatProvider) {
                float f;
                return float.TryParse(s, styles, formatProvider, out f) ? f : Nothing<float>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="ushort"/>.
            /// </summary>
            public static Maybe<ushort> Ushort(string s) {
                ushort u;
                return ushort.TryParse(s, out u) ? u : Nothing<ushort>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="ushort"/>.
            /// </summary>
            public static Maybe<ushort> Ushort(string s, NumberStyles styles, IFormatProvider formatProvider) {
                ushort u;
                return ushort.TryParse(s, styles, formatProvider, out u) ? u : Nothing<ushort>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="uint"/>.
            /// </summary>
            public static Maybe<uint> Uint(string s) {
                uint u;
                return uint.TryParse(s, out u) ? u : Nothing<uint>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="uint"/>.
            /// </summary>
            public static Maybe<uint> Uint(string s, NumberStyles styles, IFormatProvider formatProvider) {
                uint u;
                return uint.TryParse(s, styles, formatProvider, out u) ? u : Nothing<uint>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="ulong"/>.
            /// </summary>
            public static Maybe<ulong> Ulong(string s) {
                ulong u;
                return ulong.TryParse(s, out u) ? u : Nothing<ulong>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="ulong"/>.
            /// </summary>
            public static Maybe<ulong> Ulong(string s, NumberStyles styles, IFormatProvider formatProvider) {
                ulong u;
                return ulong.TryParse(s, styles, formatProvider, out u) ? u : Nothing<ulong>();
            }

            /// <summary>
            /// Parses a string which represents an <see cref="ulong"/>.
            /// </summary>
            public static Maybe<IPAddress> IPAddress(string s) {
                IPAddress address;
                return System.Net.IPAddress.TryParse(s, out address) ? address : Nothing<IPAddress>();
            }            
        }



#endregion
    }

    /// <summary>
    /// This struct represents an optional value, like <see cref="Nullable{T}"/>, but
    /// works with reference types as well. Also known as <c>Option</c> in ML/F#/Scala.
    /// </summary>
    /// <typeparam name="T">The type of wrapped object.</typeparam>
    /// <remarks>
    /// While the class is declared to be <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>,
    /// this is only true if <typeparamref name="T"/> is <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>
    /// respectively!
    /// </remarks>
    /// <seealso cref="Nullable{T}"/>
    [Serializable]
    public struct Maybe<T> : IEnumerable<T>, IComparable<Maybe<T>>, IEquatable<Maybe<T>> {
        private readonly bool _hasValue;
        private readonly T _value;

        /// <summary>
        /// Gets the value, if it exists.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="InvalidOperationException">if this doesn't have a value.</exception>
        public T Value { get {
            if (_hasValue)
                return _value;
            else
                throw new InvalidOperationException("Maybe<T> doesn't have a value.");
        } }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        public bool HasValue { get { return _hasValue; } }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() {
            if (_hasValue)
                yield return _value;
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        /// <summary>
        /// Represents absence of value.
        /// </summary>
        public static readonly Maybe<T> Nothing = new Maybe<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe&lt;T&gt;"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Maybe(T value) {
            if (value != null) {
                _hasValue = true;
                _value = value;
            }
            else {
                _hasValue = false;
                _value = default(T);
            }
        }

        /// <summary>
        /// If the current instance has a value, do <paramref name="action"/> with it.
        /// </summary>
        /// <param name="action">The action to try.</param>
        public void Do(Action<T> action) {
            DoOrElse(action, Functions.DoNothing);
        }

        /// <summary>
        /// If the current instance has a value, do <paramref name="action"/> with it.
        /// Otherwise do <paramref name="defaultAction"/>.
        /// </summary>
        /// <param name="action">The action to try.</param>
        /// <param name="defaultAction">The default action.</param>
        public void DoOrElse(Action<T> action, Action defaultAction) {
            if (HasValue)
                action(_value);
            else
                defaultAction();
        }

        /// <summary>
        /// If the current instance has a value, return it.
        /// Otherwise return <paramref name="default"/>.
        /// </summary>
        /// <param name="default">The default value.</param>
        /// <returns></returns>
        public T ValueOrElse(T @default) {
            return HasValue ? _value : @default;
        }
        
        ///// <summary>
        ///// Similar to the <c>??<\c> operator.
        ///// </summary>
        ///// <seealso cref="ValueOrElse"/>
        ///// <example><c>Nothing | 5 == 5.</c></example>
        //public static T operator |(Maybe<T> maybe, T @default) {
        //    return maybe._hasValue ? maybe.Value : @default;
        //}

        /// <summary>
        /// Similar to the <c>??<\c> operator.
        /// </summary>
        /// <seealso cref="ValueOrElse"/>
        /// <example><c>Nothing || Just(3) || Just(5) == Just(3).</c></example>
        public static Maybe<T> operator |(Maybe<T> maybe, Maybe<T> @default) {
            return maybe._hasValue ? maybe : @default;
        }

        /// <summary>
        /// Implements the operator true.
        /// </summary>
        /// <param name="maybe">The maybe.</param>
        /// <returns><c>true</c> if <paramref name="maybe"/> has a value.</returns>
        public static bool operator true(Maybe<T> maybe) {
            return maybe._hasValue;
        }

        /// <summary>
        /// Implements the operator false.
        /// </summary>
        /// <param name="maybe">The maybe.</param>
        /// <returns><c>true</c> if <paramref name="maybe"/> doesn't have a value.</returns>
        public static bool operator false(Maybe<T> maybe) {
            return !maybe._hasValue;
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns the result.
        /// Otherwise returns <paramref name="default"/>.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="default">The default result.</param>
        public R MapOrElse<R>(Func<T, R> function, R @default) {
            return HasValue ? function(_value) : @default;
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns the result.
        /// Otherwise calculates <paramref name="default"/> and returns it. This is the deferred version of
        /// <see cref="MapOrElse(Func{T,R},R)"/>.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="default">The default result.</param>
        public R MapOrElse<R>(Func<T, R> function, Func<R> @default) {
            return HasValue ? function(_value) : @default();
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns <c>Just</c> the result.
        /// Otherwise returns <c>Nothing</c>.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public Maybe<R> Map<R>(Func<T, R> function) {
            return HasValue ? Maybe.Just(function(_value)) : Maybe<R>.Nothing;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="FP.Collections.Immutable.Maybe{T}"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>Just(value)</c> if value is not null; <c>Nothing</c> otherwise.</returns>
        /// <remarks>It is implicit by parallel with <see cref="Nullable{T}"/>.</remarks>
        public static implicit operator Maybe<T>(T value) {
            return new Maybe<T>(value);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="FP.Collections.Immutable.Maybe{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="maybe">The maybe.</param>
        /// <returns><see cref="Value"/> if it exists; <c>default(T)</c> otherwise.</returns>
        public static explicit operator T(Maybe<T> maybe) {
            return maybe.ValueOrElse(default(T));
        }
        
        ///<summary>
        ///Compares the current object with another object of the same type. <c>Nothing</c> is considered
        ///to be less than all <c>Just(value)</c>; if both objects have values, they are compared.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. 
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        /// <remarks>Requires that <typeparamref name="T"/> is <see cref="IComparable{T}"/>. Null is considered to be less than <c>Nothing</c>.</remarks>
        public int CompareTo(Maybe<T> other) {
            return HasValue
                       ? (other.HasValue
                              ? Comparer<T>.Default.Compare(_value, other._value)
                              : 1)
                       : (other.HasValue ? -1 : 0);
        }

        /// <summary>
        /// Implements the equality operator. Calls <see cref="Equals"/>.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="other">The other.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator==(Maybe<T> one, Maybe<T> other) {
            return one.Equals(other);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="one">The one.</param>
        /// <param name="other">The other.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Maybe<T> one, Maybe<T> other) {
            return !(one == other);
        }

        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        /// <remarks>Requires that <typeparamref name="T"/> is <see cref="IEquatable{T}"/>.</remarks>
        public bool Equals(Maybe<T> other) {
            return HasValue
                       ? other.HasValue && _value.Equals(other._value)
                       : !other.HasValue;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override bool Equals(object obj) {
            if (!(obj is Maybe<T>)) return false;
            return Equals((Maybe<T>) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode() {
            return 29*typeof(T).GetHashCode() + MapOrElse(v => v.GetHashCode(), 0);
        }
    }
}
// ReSharper restore RedundantIfElseBlock
