/*
* Future.cs is part of functional-dotnet project
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
    /// Represents a placeholder for a result of type <typeparamref name="T"/>. Once the computation 
    /// delivers a result, the associated future is replaced with the result value. 
    /// That value may itself be a future.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public abstract class Future<T> {
        /// <summary>
        /// Requests the result of the future. If the result isn't yet available, blocks the thread
        /// until it is obtained.
        /// </summary>
        /// <returns>The value of the result of this future.</returns>
        public T Await() {
            return Result.Value;
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public abstract Result<T> Result { get; }

        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public virtual Status Status {
            get {
                return IsCompleted
                           ? Result.Match(
                                 s => Status.Successful,
                                 f => Status.Failed)
                           : Status.Future;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a result; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsCompleted { get; }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>true</c> if this future is lazy; otherwise, <c>false</c>.</value>
        public abstract bool IsLazy { get; }

        /// <summary>
        /// Occurs when the result of a future is determined.
        /// </summary>
        public event EventHandler<FutureDeterminedArgs<T>> Determined;

        /// <summary>
        /// Called to raise <see cref="Determined"/>.
        /// </summary>
        protected void OnDetermined() {
            if (Determined != null)
                Determined(this, new FutureDeterminedArgs<T>(Result));
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        public override string ToString() {
            return ToStringHelper("Future");
        }

        protected internal string ToStringHelper(string futureType) {
            return string.Format("{0}({1})", futureType,
                                 (IsCompleted
                                      ? Result.Match(v => v.ToString(),
                                                     ex => ex.ToString())
                                      : "?"));
        }
    }

    /// <summary>
    /// Static convenience methods for working with <see cref="Future{T}"/>.
    /// </summary>
    public static class Future {
        /// <summary>
        /// Blocks until one of two futures produces a result. Wraps the result into an <see cref="Either{L,R}"/>
        /// and returns. If both futures have produced a result already, the first future is preferred.
        /// </summary>
        /// <typeparam name="T1">The type of the first future.</typeparam>
        /// <typeparam name="T2">The type of the second future.</typeparam>
        /// <param name="future1">The first future.</param>
        /// <param name="future2">The second future.</param>
        /// <returns></returns>
        /// <remarks>This method does not force lazy futures.</remarks>
        public static Either<Result<T1>, Result<T2>> AwaitEither<T1, T2>(Future<T1> future1,
                                                                         Future<T2> future2) {
            using (var resetEvent = new ManualResetEvent(false)) {
                EventHandler<FutureDeterminedArgs<T1>> endWait1 = delegate { resetEvent.Set(); };
                EventHandler<FutureDeterminedArgs<T2>> endWait2 = delegate { resetEvent.Set(); };
                future1.Determined += endWait1;
                future2.Determined += endWait2;
                if (!future1.IsCompleted && !future2.IsCompleted)
                    resetEvent.WaitOne();
                future1.Determined -= endWait1;
                future2.Determined -= endWait2;
            }
            if (future1.IsCompleted)
                return Either.Left<Result<T1>, Result<T2>>(future1.Result);
            else
                return Either.Right<Result<T1>, Result<T2>>(future2.Result);
        }

        /// <summary>
        /// Creates a future that has the result <see cref="Unit.Unit"/> after the specified timeout. 
        /// In conjunction with awaitEither, this can be used to program timeouts.
        /// </summary>
        /// <param name="millisecondTimeout">The timeout in milliseconds.</param>
        /// <returns></returns>
        public static Future<Unit> Alarm(int millisecondTimeout) {
            return Spawn(() => {
                             Thread.Sleep(millisecondTimeout);
                             return Unit.Unit;
                         });
        }

        /// <summary>
        /// Creates a future which calculates <paramref name="calculation"/> on a new thread and immediately
        /// returns it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="calculation">The calculation.</param>
        /// <returns></returns>
        public static Concurrent<T> Spawn<T>(Func<T> calculation) {
            return new Concurrent<T>(calculation);
        }

        /// <summary>
        /// Creates a future which calculates <paramref name="calculation"/> on the calling thread when its result 
        /// is requested and immediately returns it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="calculation">The calculation.</param>
        /// <returns></returns>
        public static Lazy<T> Lazy<T>(Func<T> calculation) {
            return new Lazy<T>(calculation);
        }

        public static Ready<T> Ready<T>(Result<T> result) {
            return new Ready<T>(result);
        }
    }
}