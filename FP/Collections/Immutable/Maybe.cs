/* (C) Alexey Romanov 2008 */

using System;
using System.Collections;
using System.Collections.Generic;
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
            if (maybe.HasValue)
                return maybe.Value;
            else
                return null;
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
        /// A version of <see cref="Enumerable3.Map{T,TR}"/> which can throw away elements.
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
        /// A version of <see cref="Enumerable3.Map{T,TR}"/> which can throw away elements.
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
    }

    /// <summary>
    /// This struct represents an optional value, like <see cref="Nullable{T}"/>, but
    /// works with reference types as well. Also known as <c>Option</c> in ML/F#/Scala.
    /// </summary>
    /// <typeparam name="T">The type of wrapped object.</typeparam>
    /// <remarks>
    /// While the class is declared to be <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>,
    /// this is only true if <typeparamref name="T"/> is <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>!
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
        /// <exception cref="ArgumentException">if this is <c>Nothing</c>.</exception>
        public T Value { get {
            if (_hasValue)
                return Value;
            else
                throw new ArgumentException();
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
        /// The wrapper for no value.
        /// </summary>
        public static readonly Maybe<T> Nothing = new Maybe<T>();

        internal Maybe(T value) {
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
        /// <remarks>Called <c>may</c> in OCaml</remarks>
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
                action(Value);
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
            return HasValue ? Value : @default;
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns the result.
        /// Otherwise returns <paramref name="default"/>.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="default">The default result.</param>
        public R MapOrElse<R>(Func<T, R> function, R @default) {
            return HasValue ? function(Value) : @default;
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns the result.
        /// Otherwise calculates <paramref name="default"/> and returns it. This is the deferred version of
        /// <see cref="MapOrElse(Func{T,R},R)"/>.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="default">The default result.</param>
        public R MapOrElse<R>(Func<T, R> function, Func<R> @default) {
            return HasValue ? function(Value) : @default();
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns <c>Just</c> the result.
        /// Otherwise returns <c>Nothing</c>.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public Maybe<R> Map<R>(Func<T, R> function) {
            return HasValue ? Maybe.Just(function(Value)) : Maybe<R>.Nothing;
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
                              ? Comparer<T>.Default.Compare(Value, other.Value)
                              : 1)
                       : (other.HasValue ? -1 : 0);
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
                       ? other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value)
                       : !other.HasValue;
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

        public static bool operator !=(Maybe<T> one, Maybe<T> other) {
            return !(one == other);
        }

        public override int GetHashCode() {
            return typeof(T).GetHashCode() ^ Value.GetHashCode();
        }

        public override bool Equals(object obj) {
            var @this = this;
            return Switch.ExprOn<object, bool>(obj)
                .Case<Maybe<T>>(m => @this == m)
                .Default(o => (object) @this == o);
        }
    }
}
// ReSharper restore RedundantIfElseBlock
