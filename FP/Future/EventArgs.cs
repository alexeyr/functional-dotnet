/*
* EventArgs.cs is part of functional-dotnet project
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
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Arguments for <see cref="Promise{T}.Fulfilled"/>.
    /// </summary>
    public class PromiseFulfilledArgs<T> : EventArgs {
        /// <summary>
        /// Gets the result of the promise, if the promise has been fulfilled completely.
        /// </summary>
        /// <value>The result of the promise, if the promise has been fulfilled completely; <c>null</c> otherwise.</value>
        public Result<T> Result { get; private set; }
        /// <summary>
        /// Gets the future representing the result of the promise, if the promise has not been fulfilled completely.
        /// </summary>
        /// <value>The future representing the result of the promise, if the promise has not been fulfilled completely; <c>null</c> otherwise.</value>
        public Future<T> Future { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether the promise has been fulfilled completely.
        /// </summary>
        /// <value>
        /// <c>true</c> if the promise has been fulfilled completely; otherwise, <c>false</c>.
        /// </value>
        public bool IsComplete { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PromiseFulfilledArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="isComplete">is set to <c>true</c> if the promise has been fulfilled completely.</param>
        /// <param name="result">The <see cref="Result"/>.</param>
        /// <param name="future">The <see cref="Future"/>.</param>
        public PromiseFulfilledArgs(bool isComplete, Result<T> result, Future<T> future) {
            Future = future;
            IsComplete = isComplete;
            Result = result;
        }
    }

    /// <summary>
    /// Arguments for <see cref="Future{T}.Determined"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FutureDeterminedArgs<T> : EventArgs {
        /// <summary>
        /// Gets the result of the future.
        /// </summary>
        /// <value>The result.</value>
        public Result<T> Result { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FutureDeterminedArgs&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public FutureDeterminedArgs(Result<T> result) {
            Result = result;
        }
    }
}
