using System.Collections.Generic;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based ordered (by keys) sequence.
    /// </summary>
    /// <typeparam name="K">The type of the keys.</typeparam>
    /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
    public class OrderedSequence<K, T> {
        /// <summary>
        /// The sentinel value for keys. Never add an element with this value!
        /// </summary>
        public K NoKey { get; private set;}

        private readonly IComparer<K> _comparer;
        private readonly Monoid<K> _monoid;
        private FingerTree<Element, K> _ft;

        private struct Element : IMeasured<K> {
            internal readonly K Key;
            internal readonly T Value;

            public Element(K key, T value) {
                Key = key;
                Value = value;
            }

            public K Measure {
                get { return Key; }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer used to check for equality to <paramref name="noKey"/>.</param>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(IComparer<K> comparer, K noKey) {
            NoKey = noKey;
            _monoid = new Monoid<K>(noKey, (x, y) => _comparer.Compare(y, noKey) == 0 ? x : y);
            _ft = FingerTree.Empty<Element, K>(_monoid);
            _comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(K noKey) : this(Comparer<K>.Default, noKey) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer used to check for equality to <paramref name="noKey"/>.</param>
        /// <param name="noKey">The sentinel value.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the queue initially.</param>
        public OrderedSequence(IEnumerable<Pair<K, T>> sequence, IComparer<K> comparer, K noKey) {
            NoKey = noKey;
            _monoid = new Monoid<K>(noKey, (x, y) => _comparer.Compare(y, noKey) == 0 ? x : y);
            _comparer = comparer;
            //TODO: _ft = FingerTree.FromEnumerable(sequence.Map(pair => new Element(pair.First, pair.Second)), _monoid);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="noKey">The sentinel value.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the queue initially.</param>
        public OrderedSequence(IEnumerable<Pair<K, T>> sequence, K noKey) : this(sequence, Comparer<K>.Default, noKey) { }

        //TODO: Factory methods for classes using null as NoKey
    }
}