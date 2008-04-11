using System.Collections;
using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A base class for immutable lists. It provides only an implementations of <see cref="IEnumerable{T}"/>,
    /// so it's not necessary for other implementations to inherit from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ImmutableListBase<T> : IImmutableList<T> {
        public abstract T Head { get; }
        public abstract IImmutableList<T> Tail { get; }
        public abstract bool IsEmpty { get; }
        public abstract IImmutableList<T> Prepend(T newHead);

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() {
            IImmutableList<T> list = this;
            while (list != null && !list.IsEmpty) {
                yield return list.Head;
                list = list.Tail;
            }
        }
    }
}