/* (C) Alexey Romanov 2008
 * Code adapted from http://blogs.msdn.com/wesdyer/archive/2007/02/12/why-all-of-the-love-for-lists.aspx
 */

using System.Collections.Generic;

namespace FP.Collections.Immutable {
    public class LazyList<T> : ImmutableListBase<T> {
        IImmutableList<T> _tail;
        readonly T _head;
        IEnumerator<T> _enumerator;

        LazyList(T head, IEnumerator<T> enumerator) {
            _head = head;
            _enumerator = enumerator;
        }

        LazyList(T head, IImmutableList<T> tail) {
            _head = head;
            _tail = tail;
        }

        public static IImmutableList<T> Create(IEnumerable<T> enumerable) {
            return Create(enumerable.GetEnumerator());
        }

        public static IImmutableList<T> Create(IEnumerator<T> enumerator) {
            return enumerator.MoveNext()
                       ? new LazyList<T>(enumerator.Current, enumerator)
                       : null;
        }

        public override IImmutableList<T> Tail {
            get {
                if (_enumerator != null) {
                    _tail = Create(_enumerator);
                    _enumerator = null;
                }
                return _tail;
            }
        }

        public override bool IsEmpty {
            get { return false; }
        }

        public override T Head { get { return _head; } }

        public override IImmutableList<T> Prepend(T newHead) {
            return new LazyList<T>(newHead, this);
        }
    }
}