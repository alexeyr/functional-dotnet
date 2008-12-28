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
        /// <param name="t">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotNull<T>(this Validation validation, T t,
                                              string paramName) {
            if (t != null) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentNullException(paramName));
        } // IsNotNull(, validation, t)

        /// <summary>
        /// Validate that the specified parameter is not null. A more specific overload for
        /// <see cref="Nullable{T}"/> only.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="t">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsNotNull<T>(this Validation validation, T? t,
                                      string paramName) where T : struct {
            if (t.HasValue) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentNullException(paramName));
        } // IsNotNull(, validation, t)

        /// <summary>
        /// Validate that the specified parameter is positive.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsPositive(this Validation validation, long value, string paramName) {
            if (value >= 0) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentOutOfRangeException(paramName,
                                                    "must be positive, but was " + value));
        } // IsPositive(validation, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is positive.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsPositive(this Validation validation, int value, string paramName) {
            if (value >= 0) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentOutOfRangeException(paramName,
                                                    "must be positive, but was " + value));
        } // IsPositive(validation, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is positive.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsPositive(this Validation validation, double value, string paramName) {
            if (value >= 0) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentOutOfRangeException(paramName,
                                                    "must be positive, but was " + value));
        } // IsPositive(validation, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is positive.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsPositive(this Validation validation, decimal value, string paramName) {
            if (value >= 0) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentOutOfRangeException(paramName,
                                                    "must be positive, but was " + value));
        } // IsPositive(validation, value, paramName)

        /// <summary>
        /// Validate that the specified parameter is positive.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public static Validation IsPositive(this Validation validation, float value, string paramName) {
            if (value >= 0) return validation;
            return
                (validation ?? new Validation()).AddException(
                    new ArgumentOutOfRangeException(paramName,
                                                    "must be positive, but was " + value));
        } // IsPositive(validation, value, paramName)

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
    } // class ValidationExtensions
} // namespace FP.Validation