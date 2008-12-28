/*
* FPValidationExtensions.cs is part of functional-dotnet project
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
using FP.Collections;

namespace FP.Validation {
    /// <summary>
    /// Extension methods on Validation for the types in FP.Core and FP.Collections.
    /// </summary>
    public static class FPValidationExtensions {
        /// <summary>
        /// Validate that the specified parameter can be used as an index for 
        /// <paramref name="seq"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of sequence.</typeparam>
        /// <typeparam name="TSeq">The type of the sequence.</typeparam>
        /// <param name="validation">The validation.</param>
        /// <param name="seq">The sequence.</param>
        /// <param name="index">The value of the parameter.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public static Validation IsIndexInRange<T, TSeq>(
            this Validation validation, IRandomAccessSequence<T, TSeq> seq, int index, string paramName) where TSeq : IRandomAccessSequence<T, TSeq> {
            if (index >= 0 && index < seq.Count)
                return validation;
            return validation.AddException(
                new ArgumentOutOfRangeException(
                    paramName,
                    "the collection has " + seq.Count +
                    " elements, but tried to access element with index" + index));
        }
    } // class FPValidationExtensions
} // namespace FP.Validation
