namespace FP.Future {
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
        /// Computation has succeded.
        /// </summary>
        Successful
    }
}