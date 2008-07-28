using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A monoid structure on the type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the elements of the monoid.</typeparam>
    public class Monoid<T> {
        /// <summary>
        /// The zero of the monoid. It should always be the case that <code>forall x. Zero.Add(x) == x</code>
        /// and <code>x.Add(Zero) == x</code>.
        /// </summary>
        public readonly T Zero;

        /// <summary>
        /// The addition operation. It should always be associative, that is, <code>forall x y z. x.Add(y).Add(z) == x.Add(y.Add(z))</code>.
        /// </summary>
        public readonly Func<T, T, T> Plus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monoid&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="zero">The zero.</param>
        /// <param name="add">The add.</param>
        public Monoid(T zero, Func<T, T, T> add) {
            Zero = zero;
            Plus = add;
        }
    }

    internal static class Monoids {
        internal static readonly Monoid<int> Size = new Monoid<int>(0, (x, y) => x + y);
    }
}