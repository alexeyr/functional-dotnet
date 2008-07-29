using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Any structure which can be folded into a summary value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFoldable<T> {
        /// <summary>
        /// Reduces the finger tree from the right.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        A FoldRight<A>(Func<T, A, A> binOp, A initial);

        /// <summary>
        /// Reduces the finger tree from the left.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        A FoldLeft<A>(Func<A, T, A> binOp, A initial);
    }
}