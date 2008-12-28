/*
* ValidationExtensions.cs is part of functional-dotnet project
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
using System.Linq;

namespace FP.Validation {
    /// <summary>
    /// Validation extensions
    /// </summary>
    public static class ValidationExtensions {
        /// <summary>
        /// Validate that the specified parameter is not null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotNull<T>(this Validation validation, T value,
                                              string paramName) {
            if (value != null) return validation;
            return
                validation.AddException(
                    new ArgumentNullException(paramName));
        } // IsNotNull(, validation, t)

        /// <summary>
        /// Validate that the specified parameter is not null. A more specific overload for
        /// <see cref="Nullable{T}"/> only.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotNull<T>(this Validation validation, T? value,
                                      string paramName) where T : struct {
            if (value.HasValue) return validation;
            return
                validation.AddException(
                    new ArgumentNullException(paramName));
        } // IsNotNull(, validation, t)

        /// <summary>
        /// Validate that the specified parameter is in range between two bounds (both
        /// bounds are valid).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (inclusive).</param>
        /// <param name="upperBound">The upper bound (inclusive).</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInRange<T>(this Validation validation, T lowerBound, T upperBound, T value, string paramName)
        where T : IComparable<T> {
            if (value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, value, paramName)

        /// <summary>
        /// Validate that the specified parameter has a value and is in range between two
        /// bounds (both bounds are valid).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (inclusive).</param>
        /// <param name="upperBound">The upper bound (inclusive).</param>
        /// <param name="nullableValue">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInRange<T>(this Validation validation, T lowerBound, T upperBound, T? nullableValue, string paramName)
        where T : struct, IComparable<T> {
            if (!nullableValue.HasValue) {
                return validation.AddException(
                    new ArgumentNullException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was null"));
            }
            T value = nullableValue.Value;
            if (value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, nullableValue, paramName)

        /// <summary>
        /// Validate that the specified parameter is in range between two bounds (lower
        /// bound is valid, but upper bound is not).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (inclusive).</param>
        /// <param name="upperBound">The upper bound (exclusive).</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInUpperExclusiveRange<T>(this Validation validation, T lowerBound, T upperBound, T value, string paramName)
        where T : IComparable<T> {
            if (value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) < 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is in range between two bounds (lower
        /// bound is valid, but upper bound is not).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (inclusive).</param>
        /// <param name="upperBound">The upper bound (exclusive).</param>
        /// <param name="nullableValue">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInUpperExclusiveRange<T>(this Validation validation, T lowerBound, T upperBound, T? nullableValue, string paramName)
        where T : struct, IComparable<T> {
            if (!nullableValue.HasValue) {
                return validation.AddException(
                    new ArgumentNullException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was null"));
            }
            T value = nullableValue.Value;
            if (value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) < 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, nullableValue, paramName)

        /// <summary>
        /// Validate that the specified parameter is in range between two bounds (neither
        /// bound is valid).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (exclusive).</param>
        /// <param name="upperBound">The upper bound (exclusive).</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInExclusiveRange<T>(this Validation validation, T lowerBound, T upperBound, T value, string paramName)
        where T : IComparable<T> {
            if (value.CompareTo(lowerBound) > 0 && value.CompareTo(upperBound) < 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is in range between two bounds (neither
        /// bound is valid).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="lowerBound">The lower bound (exclusive).</param>
        /// <param name="upperBound">The upper bound (exclusive).</param>
        /// <param name="nullableValue">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <returns></returns>
        public static Validation IsInExclusiveRange<T>(this Validation validation, T lowerBound, T upperBound, T? nullableValue, string paramName)
        where T : struct, IComparable<T> {
            if (!nullableValue.HasValue) {
                return validation.AddException(
                    new ArgumentNullException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was null"));
            }
            T value = nullableValue.Value;
            if (value.CompareTo(lowerBound) > 0 && value.CompareTo(upperBound) < 0)
                return validation;
            return
                validation.AddException(
                    new ArgumentOutOfRangeException(
                        paramName,
                        "must be between" + lowerBound + " and " + upperBound + ", but was " + value));
        } // IsInRange(validation, lowerBound, upperBound, nullableValue, paramName)

        /// <summary>
        /// Check whether there were any exceptions. Do not forget to call this after
        /// setting conditions!
        /// </summary>
        /// <param name="validation">The validation.</param>
        public static Validation Check(this Validation validation) {
            if (validation != null)
                validation.Throw();
            return null;
        } // Throw(validation)

        /// <summary>
        /// Validate that the specified sequence is not empty. Null sequences are considered empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="sequence"/>.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="sequence">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotEmpty<T>(this Validation validation, IEnumerable<T> sequence,
                                              string paramName) {
            if (sequence == null) return validation.AddException(
                    new ArgumentNullException(paramName));
            if (sequence.Any())
                return validation;
            return
                validation.AddException(
                    new EmptyEnumerableException(paramName));
        } // IsNotEmpty(validation, sequence, paramName)

        /// <summary>
        /// Validate that the specified collection is not empty. Null collections are considered empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="collection"/>.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="collection">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotEmpty<T>(this Validation validation, ICollection<T> collection,
                                              string paramName) {
            if (collection == null) return validation.AddException(
                    new ArgumentNullException(paramName));
            if (collection.Count == 0)
                return validation;
            return
                validation.AddException(
                    new EmptyEnumerableException(paramName));
        } // IsNotEmpty(validation, sequence, paramName)

        /// <summary>
        /// Allocates a new <see cref="Validation"/> if <paramref name="validation"/> is 
        /// <c>null</c> and adds <paramref name="exception"/> to the list of exceptions.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="exception">The exception.</param>
        /// <remarks>Use this method for adding new validations. Here is an example implementation
        /// of IsPositive:
        /// <example>
        /// <![CDATA[
        /// public static Validation IsPositive(this Validation validation, decimal value, 
        ///     string paramName) {
        ///     if (value > 0) return validation;
        ///     return
        ///         validation.AddException(
        ///             new ArgumentOutOfRangeException(paramName,
        ///                                             "must be positive, but was " + value));
        /// ]]></example>
        /// </remarks>
        public static Validation AddException(this Validation validation, Exception exception) {
            return (validation ?? new Validation()).AddExceptionInternal(exception);
        }
    } // class ValidationExtensions
} // namespace FP.Validation