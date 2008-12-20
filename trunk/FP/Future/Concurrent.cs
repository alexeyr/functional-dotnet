/*
* Concurrent.cs is part of functional-dotnet project
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
using System.Threading;
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Represents a future whose result is computed in a separate thread.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Concurrent<T> : Future<T> {
        private Result<T> _result;
        private readonly Thread _thread;

        /// <summary>
        /// Initializes a new instance of a <see cref="Concurrent{T}"/> <see cref="Future{T}"/>.
        /// </summary>
        /// <param name="calculation">The calculation the new future does.</param>
        public Concurrent(Func<T> calculation) {
            _thread = new Thread(() => {
                                     _result = Core.Result.Try(calculation);
                                     OnDetermined(_result);
                                 });
            _thread.Start();
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (!_thread.IsAlive)
                    _thread.Join();
                return _result;
            }
        }

        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public override Future.Status Status {
            get {
                return _thread.IsAlive
                           ? Future.Status.Future
                           : _result.Match(
                                 s => Future.Status.Successful,
                                 f => Future.Status.Failed);
            }
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