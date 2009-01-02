/*
* Concurrent.cs is part of functional-dotnet project
* 
* Copyright (c) 2008-2009 Alexey Romanov
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
using System.Runtime.Remoting.Messaging;
using System.Threading;
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Represents a future whose result is computed in a <see cref="ThreadPool"/>
    /// thread when one becomes available.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Concurrent<T> : Future<T> {
        private Result<T> _result;
        private AsyncResult _asyncResult;

        /// <summary>
        /// Initializes a new instance of a <see cref="Concurrent{T}"/> <see cref="Future{T}"/>.
        /// </summary>
        /// <param name="calculation">The calculation the new future does.</param>
        public Concurrent(Func<T> calculation) {
            Func<Result<T>> calculation1 = () => Core.Result.Try(calculation);
            _asyncResult = (AsyncResult) calculation1.BeginInvoke(
                                             asyncResult => {
                                                 _result = calculation1.EndInvoke(asyncResult);
                                                 _asyncResult = null;
                                                 OnDetermined();
                                             }, null);
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (!IsCompleted) {
                    _result = ((Func<Result<T>>) _asyncResult.AsyncDelegate).EndInvoke(_asyncResult);
                    _asyncResult = null;
                }
                return _result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a result; otherwise, <c>false</c>.
        /// </value>
        public override bool IsCompleted {
            get { return _result != null; }
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>false</c>.</value>
        public override bool IsLazy {
            get { return false; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ToStringHelper("Concurrent");
        }
    }
}