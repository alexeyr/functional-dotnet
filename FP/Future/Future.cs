using System;

namespace FP.Future {
    /// <summary>
    /// Represents a placeholder for a result of type <typeparamref name="T"/>. Once the computation 
    /// delivers a result, the associated future is replaced with the result value. 
    /// That value may be a future on its own.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public abstract class Future<T> {
        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public abstract Future.Status Status { get; }
        /// <summary>
        /// Requests the result of the future. If the result isn't yet available, blocks the thread
        /// until it is obtained.
        /// </summary>
        /// <returns></returns>
        public abstract T Request();

        /// <summary>
        /// Gets a value indicating whether this future is failed.
        /// </summary>
        /// <value><c>true</c> if this instance is failed; otherwise, <c>false</c>.</value>
        public bool IsFailed { get { return Status == Future.Status.Failed; } }
        /// <summary>
        /// Gets a value indicating whether this future is determined.
        /// </summary>
        /// <value>
        /// <c>true</c> if this future is determined; otherwise, <c>false</c>.
        /// </value>
        public bool IsDetermined { get { return Status == Future.Status.Determined; } }
        /// <summary>
        /// Gets a value indicating whether this future is future.
        /// </summary>
        /// <value><c>true</c> if this future is future; otherwise, <c>false</c>.</value>
        public bool IsFuture { get { return Status == Future.Status.Future; } }
        /// <summary>
        /// Gets a value indicating whether this future is lazy.
        /// </summary>
        /// <value><c>true</c> if this future is lazy; otherwise, <c>false</c>.</value>
        public abstract bool IsLazy { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FP.Future.Future&lt;T&gt;"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="future">The future.</param>
        /// <returns>The result of the future. If the result isn't yet available, blocks the thread
        /// until it is obtained.</returns>
        static public explicit operator T(Future<T> future) {
            return future.Request();
        }
    }

    public static class Future {
        /// <summary>
        /// Possible statuses of <see cref="Future{T}"/>.
        /// </summary>
        public enum Status {
            /// <summary>
            /// Doesn't have a result yet.
            /// </summary>
            Future,
            /// <summary>
            /// Computation has failed.
            /// </summary>
            Failed,
            /// <summary>
            /// A result has been found.
            /// </summary>
            Determined
        }
    }

}
