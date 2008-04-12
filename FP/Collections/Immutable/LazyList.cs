/* (C) Alexey Romanov 2008
 * Code adapted from http://blogs.msdn.com/wesdyer/archive/2007/02/12/why-all-of-the-love-for-lists.aspx
 */

using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A lazy singly linked list which allows saving the state of enumerators.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyList<T> : ImmutableListBase<T> {
        IImmutableList<T> _tail;
        readonly T _head;
        IEnumerator<T> _enumerator;
        /// <summary>
        /// The empty list.
        /// </summary>
        public static readonly EmptyList Empty = new EmptyList();

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="head">The head.</param>
        /// <param name="enumerator">The enumerator used to create following elements.
        /// Do _not_ hold on to any other references to it!</param>
        private LazyList(T head, IEnumerator<T> enumerator) {
            _head = head;
            _enumerator = enumerator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="head">The head.</param>
        /// <param name="tail">The tail.</param>
        private LazyList(T head, IImmutableList<T> tail) {
            _head = head;
            _tail = tail;
        }

        /// <summary>
        /// Creates the list based on the specified sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public static LazyList<T> Create(IEnumerable<T> sequence) {
            return Create(sequence.GetEnumerator());
        }

        /// <summary>
        /// Creates the list based on the specified enumerator.
        /// </summary>
        /// <param name="enumerator">The enumerator.
        /// Do _not_ hold on to any other references to it!</param>
        /// <returns></returns>
        public static LazyList<T> Create(IEnumerator<T> enumerator) {
            return enumerator.MoveNext()
                       ? new LazyList<T>(enumerator.Current, enumerator)
                       : Empty;
        }

        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <value>The head of the list.</value>
        /// <exception cref="EmptySequenceException">is the current list <see cref="IsEmpty"/>.</exception>
        public override T Head {
            get { return _head; }
        }

        /// <summary>
        /// Gets the "tail" (all elements but the first) of the list.
        /// </summary>
        /// <value>The tail of the list.</value>
        /// <exception cref="EmptySequenceException">is the current list <see cref="IsEmpty"/>.</exception>
        public override IImmutableList<T> Tail {
            get {
                if (_enumerator != null) {
                    _tail = Create(_enumerator);
                    _enumerator = null;
                }
                return _tail;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public override bool IsEmpty {
            get { return false; }
        }

        /// <summary>
        /// Prepends a new head.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>
        /// The list with <paramref name="newHead"/> as <see cref="Head"/>
        /// and the current list as <see cref="Tail"/>.
        /// </returns>
        public override IImmutableList<T> Prepend(T newHead) {
            return new LazyList<T>(newHead, this);
        }

        /// <summary>
        /// The empty list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class EmptyList : LazyList<T> {
            internal EmptyList() : base(default(T), (IImmutableList<T>)null) { }

            /// <summary>
            /// Gets the "head" (first element) of the list.
            /// </summary>
            /// <exception cref="EmptySequenceException"></exception>
            public override T Head {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is empty.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool IsEmpty {
                get { return true; }
            }

            /// <summary>
            /// Gets the "tail" (all elements but the first) of the list.
            /// </summary>
            /// <exception cref="EmptySequenceException">is the current list <see cref="IsEmpty"/>.</exception>
            public override IImmutableList<T> Tail {
                get { throw new EmptySequenceException(); }
            }
        }
    }
}