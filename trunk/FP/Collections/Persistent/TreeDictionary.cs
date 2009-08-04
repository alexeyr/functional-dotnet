/*
* TreeDictionary.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FP.Core;

namespace FP.Collections.Persistent {
    /// <summary>
    /// A dictionary based on weight-balanced trees.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TComparer">The type of the comparer.</typeparam>
    [Serializable]
    public class TreeDictionary<TKey, TValue, TComparer> :
        ISortedDictionary<TKey, TValue, TComparer, TreeDictionary<TKey, TValue, TComparer>>,
        ICombinableDictionary<TKey, TValue, TreeDictionary<TKey, TValue, TComparer>>,
        IEquatable<TreeDictionary<TKey, TValue, TComparer>>
        where TComparer : IComparer<TKey>, new() {
        private static readonly TComparer _comparer = new TComparer();
        public static readonly TreeDictionary<TKey, TValue, TComparer> Empty = 
            new TreeDictionary<TKey, TValue, TComparer>(0, default(TKey), default(TValue), null, null);
        
        private readonly int _count;
        private readonly TKey _key;
        private readonly TValue _value;
        private readonly TreeDictionary<TKey, TValue, TComparer> _left;
        private readonly TreeDictionary<TKey, TValue, TComparer> _right;

        internal TreeDictionary(
            int count, TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            _count = count;
            _right = right;
            _left = left;
            _value = value;
            _key = key;
            AssertInvariant();
        }

        private TreeDictionary(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) :
            this(1 + left.Count() + right.Count(), key, value, left, right) { }

        [Conditional("DEBUG")]
        private void AssertInvariant() {
            int leftCount = _left.Count();
            int rightCount = _right.Count();
            bool searchTree = true;
            // ReSharper disable PossibleNullReferenceException
            if (leftCount > 0)
                searchTree = _comparer.Compare(_left._key, _key) < 0;
            if (rightCount > 0)
                searchTree = searchTree && _comparer.Compare(_key, _right._key) < 0;
            // ReSharper restore PossibleNullReferenceException
            Debug.Assert(_count == 0 || _count == 1 + leftCount + rightCount);
            Debug.Assert(searchTree);
            Debug.Assert(_count == 0 || (_left != null && _right != null));
            Debug.Assert(_count <= 2 || (leftCount <= DELTA * rightCount && rightCount <= DELTA * leftCount));
        }

        /// <summary>
        /// Returns an iterator which yields all elements of the sequence in the reverse order.
        /// </summary>
        /// <remarks>This should always be equivalent to, but faster than, 
        /// <code>
        /// AsEnumerable().Reverse();
        /// </code></remarks>
        public IEnumerable<Tuple<TKey, TValue>> ReverseIterator() {
            if (_count == 0) yield break;
            if (_right.Count() != 0) {
                foreach (var pair in _right.ReverseIterator())
                    yield return pair;
            }
            yield return Tuple.New(_key, _value);
            if (_left.Count() != 0) {
                foreach (var pair in _left.ReverseIterator())
                    yield return pair;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<Tuple<TKey, TValue>> GetEnumerator() {
            if (_count == 0) yield break;
            if (_left.Count() != 0) {
                foreach (var pair in _left)
                    yield return pair;
            }
            yield return Tuple.New(_key, _value);
            if (_right.Count() != 0) {
                foreach (var pair in _right)
                    yield return pair;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets a value indicating whether this collection is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        public bool IsEmpty {
            get { return _count != 0; }
        }

        /// <summary>
        /// Gets the number of elements in the dictionary.
        /// </summary>
        /// <value>The count.</value>
        public int Count {
            get { return _count; }
        }

        /// <summary>
        /// Looks up the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <value><see cref="Optional.Some{T}"/><c>(value)</c> if the
        /// dictionary contains the specified key and associates <c>value</c>
        /// to it and <see cref="Optional.None{T}"/> otherwise.</value>
        public Optional<TValue> this[TKey key] {
            get {
                var dict = this;
                while (dict.Count() != 0) {
                    int comparison = _comparer.Compare(key, dict._key);
                    if (comparison == 0) return dict._value;
                    dict = comparison < 0 ? dict._left : dict._right;
                }
                return Optional.None<TValue>();
            }
        }

        public TreeDictionary<TKey, TValue, TComparer> Add(
            TKey key, TValue value, Func<TValue, TValue> combiner) {
            // Max height is under 3*log(Count), so recursive methods shouldn't cause a stack overflow
            bool needRebalance = false;
            return AddRecursive(key, value, combiner, ref needRebalance);
            // return AddStack(key, value, combiner);
            // return AddRecursiveShortcut(key, value, combiner);
        }

        private TreeDictionary<TKey, TValue, TComparer> AddStack(TKey key, TValue value, Func<TValue, TValue> combiner) {
            if (_count == 0)
                return TreeDictionary.Single<TKey, TValue, TComparer>(key, value);
            int comparison = _comparer.Compare(key, _key);
            if (comparison == 0)
                return ReplaceValue(combiner(_value));
            var dict = this;
            var stack = new Stack<Tuple<TreeDictionary<TKey, TValue, TComparer>, bool>>();
            do {
                stack.Push(Tuple.New(dict, comparison < 0));
                dict = comparison < 0
                           ? dict._left
                           : dict._right;
                comparison = _comparer.Compare(key, dict._key);
            }
            while (dict._count != 0 && comparison != 0);
            if (dict._count == 0) {
                dict = TreeDictionary.Single<TKey, TValue, TComparer>(key, value);
                while (stack.Count > 0) {
                    var pair = stack.Pop();
                    var dict1 = pair.Item1;
                    bool dictGoesLeftOfDict1 = pair.Item2;
                    Debug.Assert(
                        _comparer.Compare(dict._key, dict1._key) != 0 &&
                        dictGoesLeftOfDict1 == _comparer.Compare(dict._key, dict1._key) < 0);
                    dict = dictGoesLeftOfDict1
                               ? dict1.Balance(dict, dict1._right)
                               : dict1.Balance(dict1._left, dict);
                }
                return dict;
            }
            else {
                Debug.Assert(comparison == 0);
                TValue newValue = combiner(dict._value);
                if (EqualityComparer<TValue>.Default.Equals(_value, newValue))
                    return this; // It turns out we don't need to replace anything!
                dict = dict.ReplaceValueDontCheckEquality(newValue);
                while (stack.Count > 0) {
                    var pair = stack.Pop();
                    var dict1 = pair.Item1;
                    bool dictGoesLeftOfDict1 = pair.Item2;
                    Debug.Assert(
                        _comparer.Compare(dict._key, dict1._key) != 0 &&
                        dictGoesLeftOfDict1 == _comparer.Compare(dict._key, dict1._key) < 0);
                    dict = dictGoesLeftOfDict1
                               ? dict1.Balanced(dict, dict1._right)
                               : dict1.Balanced(dict1._left, dict);
                }
                return dict;
            }
        }

        private TreeDictionary<TKey, TValue, TComparer> AddRecursive(TKey key, TValue value, Func<TValue, TValue> combiner, ref bool needRebalance) {
            if (_count == 0) {
                needRebalance = true;
                return TreeDictionary.Single<TKey, TValue, TComparer>(key, value);
            }
            int comparison = _comparer.Compare(key, _key);
            if (comparison == 0) {
                needRebalance = false;
                return ReplaceValue(combiner(_value));
            }
            if (comparison < 0) {
                var dict = _left.AddRecursive(key, value, combiner, ref needRebalance);
                return needRebalance
                           ? Balance(dict, _right)
                           : Balanced(dict, _right);
            }
            else {
                var dict = _right.AddRecursive(key, value, combiner, ref needRebalance);
                return needRebalance
                           ? Balance(_left, dict)
                           : Balanced(_left, dict);
            }
        }

        private TreeDictionary<TKey, TValue, TComparer> AddRecursiveShortcut(TKey key, TValue value, Func<TValue, TValue> combiner) {
            try {
                bool needRebalance = false;
                return AddRecursiveShortcut1(key, value, combiner, ref needRebalance);
            }
            catch (ReturnThisException) {
                return this;
            }
        }

        private TreeDictionary<TKey, TValue, TComparer> AddRecursiveShortcut1(TKey key, TValue value, Func<TValue, TValue> combiner, ref bool needRebalance) {
            if (_count == 0) {
                needRebalance = true;
                return TreeDictionary.Single<TKey, TValue, TComparer>(key, value);
            }
            int comparison = _comparer.Compare(key, _key);
            if (comparison == 0) {
                var newValue = combiner(_value);
                if (EqualityComparer<TValue>.Default.Equals(_value, value))
                    throw new ReturnThisException();
                needRebalance = false;
                return ReplaceValueDontCheckEquality(newValue);
            }
            if (comparison < 0) {
                var dict = _left.AddRecursiveShortcut1(key, value, combiner, ref needRebalance);
                return needRebalance
                           ? Balance(dict, _right)
                           : Balanced(dict, _right);
            }
            else {
                var dict = _right.AddRecursiveShortcut1(key, value, combiner, ref needRebalance);
                return needRebalance
                           ? Balance(_left, dict)
                           : Balanced(_left, dict);
            }
        }

        /// <summary>
        /// Adds the specified key with the specified value. If the key is
        /// already present, replaces the current value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The resulting dictionary.
        /// </returns>
        public TreeDictionary<TKey, TValue, TComparer> Add(TKey key, TValue value) {
            // TODO: Inline _after_ comparing performance:
            return Add(key, value, _ => value);
        }

        public TreeDictionary<TKey, TValue, TComparer> AddStack(TKey key, TValue value) {
            // TODO: Inline _after_ comparing performance:
            return AddStack(key, value, _ => value);
        }

        public TreeDictionary<TKey, TValue, TComparer> AddShortcut(TKey key, TValue value) {
            // TODO: Inline _after_ comparing performance:
            return AddRecursiveShortcut(key, value, _ => value);
        }

        /// <summary>
        /// Removes the specified key and the associated value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resulting dictionary.</returns>
        public TreeDictionary<TKey, TValue, TComparer> Remove(TKey key) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified key with the given function. If the dictionary
        /// doesn't contain the key, it is returned unchanged; if 
        /// <code>updater(key, currentValue)</code> returns <code>None</code>, the key is removed;
        /// if it returns <code>Some(newValue)</code>, the current value is replaced with newValue.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="updater">The updating function.</param>
        /// <returns>The resulting dictionary.</returns>
        public TreeDictionary<TKey, TValue, TComparer> Update(
            TKey key, Func<TKey, TValue, Optional<TValue>> updater) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified key with the given function.  If the
        /// dictionary doesn't contain the key, <code>updater(key, None)</code>
        /// is called to provide the new value; if it does, 
        /// <code>updater(key, Some(currentValue))</code> is. If the call
        /// returns <code>None</code>, the key is removed; if it returns 
        /// <code>Some(newValue)</code>, the key is associated with newValue.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="updater">The updating function.</param>
        /// <returns>The resulting dictionary.</returns>
        public TreeDictionary<TKey, TValue, TComparer> Update(
            TKey key, Func<TKey, Optional<TValue>, Optional<TValue>> updater) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the specified key with the specified value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="combiner">
        /// The function to be called if the given key is already present. The
        /// arguments are the key, the current value, and the added value. The
        /// result is inserted in place of the current value.
        /// </param>
        /// <returns>
        /// The resulting dictionary.
        /// </returns>
        public Tuple<TreeDictionary<TKey, TValue, TComparer>, Optional<TValue>> LookupAndUpdate(TKey key, TValue value, Func<TKey, Optional<TValue>, TValue, Optional<TValue>> combiner) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the keys. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The keys.</value>
        public IEnumerable<TKey> Keys {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the values. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<TValue> Values {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Retrieves the minimum key, associated value, and the dictionary
        /// containing all other elements.
        /// </summary>
        /// <returns>The tuple of the minimum key, associated value and the
        /// dictionary containing all other elements, if the dictionary is
        /// non-empty; <c>None</c> otherwise.</returns>
        public Optional<Tuple<TKey, TValue, TreeDictionary<TKey, TValue, TComparer>>> TakeMinKey() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the maximum key, associated value, and the dictionary
        /// containing all other elements.
        /// </summary>
        /// <returns>The tuple of the maximum key, associated value and the
        /// dictionary containing all other elements, if the dictionary is
        /// non-empty; <c>None</c> otherwise.</returns>
        public Optional<Tuple<TKey, TValue, TreeDictionary<TKey, TValue, TComparer>>> TakeMaxKey() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the minimum key and the associated value.
        /// </summary>
        /// <returns>The tuple of the minimum key, associated value and the
        /// dictionary containing all other elements, if the dictionary is
        /// non-empty; <c>None</c> otherwise.</returns>
        public Optional<Tuple<TKey, TValue>> MinKey() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the maximum key and the associated value.
        /// </summary>
        /// <returns>The tuple of the maximum key, associated value and the
        /// dictionary containing all other elements, if the dictionary is
        /// non-empty; <c>None</c> otherwise.</returns>
        public Optional<Tuple<TKey, TValue>> MaxKey() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Splits dictionary on the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A tuple, where the first element contains all keys less
        /// than <paramref name="key"/>, the second element is <c>this[key]</c>,
        /// and the third element contains all keys greater than 
        /// <paramref name="key"/></returns>
        public Tuple<TreeDictionary<TKey, TValue, TComparer>, Optional<TValue>, TreeDictionary<TKey, TValue, TComparer>> Split(TKey key) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the dictionary containing all keys present in one of the
        /// dictionaries with the same values. If a key is present in both dictionaries, 
        /// the value is obtained by using the <paramref name="combiner"/>.
        /// </summary>
        /// <param name="otherDict">
        /// The other dictionary.
        /// </param>
        /// <param name="combiner">
        /// The function used for combining values of duplicate keys.
        /// </param>
        /// <returns>
        /// The union of two dictionaries.
        /// </returns>
        public TreeDictionary<TKey, TValue, TComparer> Union(
            TreeDictionary<TKey, TValue, TComparer> otherDict, Func<TKey, TValue, TValue, TValue> combiner) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the dictionary containing all keys present in this
        /// dictionary, but not in the other. If a key is present in both
        /// dictionaries, the value is obtained by using the 
        /// <paramref name="combiner"/>.
        /// </summary>
        /// <param name="otherDict">
        /// The other dictionary.
        /// </param>
        /// <param name="combiner">
        /// The function used for combining values of duplicate keys.
        /// </param>
        /// <returns>
        /// The difference of two dictionaries.
        /// </returns>
        public TreeDictionary<TKey, TValue, TComparer> Difference(
            TreeDictionary<TKey, TValue, TComparer> otherDict, Func<TKey, TValue, TValue, TValue> combiner) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the dictionary containing all keys present in both
        /// dictionaries. The value is obtained by using the 
        /// <paramref name="combiner"/>.
        /// </summary>
        /// <param name="otherDict">
        /// The other dictionary.
        /// </param>
        /// <param name="combiner">
        /// The function used for combining values of duplicate keys.
        /// </param>
        /// <returns>
        /// The difference of two dictionaries.
        /// </returns>
        public TreeDictionary<TKey, TValue, TComparer> Intersect(
            TreeDictionary<TKey, TValue, TComparer> otherDict, Func<TKey, TValue, TValue, TValue> combiner) {
            throw new NotImplementedException();
        }

        private const int DELTA = 5;
        private const int RATIO = 2;

        /// <summary>
        /// Called instead of the constructor when <paramref name="left"/> and 
        /// <paramref name="right"/> may be unbalanced.
        /// </summary>
        private TreeDictionary<TKey, TValue, TComparer> ReplaceValue(TValue value) {
            return
                EqualityComparer<TValue>.Default.Equals(_value, value)
                    ? this
                    : ReplaceValueDontCheckEquality(value);
        }

        /// <summary>
        /// Called instead of the constructor when <paramref name="left"/> and 
        /// <paramref name="right"/> may be unbalanced.
        /// </summary>
        private TreeDictionary<TKey, TValue, TComparer> ReplaceValueDontCheckEquality(TValue value) {
            return new TreeDictionary<TKey, TValue, TComparer>(_count, _key, value, _left, _right);
        }

        /// <summary>
        /// Called instead of the constructor when <paramref name="left"/> and 
        /// <paramref name="right"/> may be unbalanced.
        /// </summary>
        private static TreeDictionary<TKey, TValue, TComparer> Balance(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            int countLeft = left.Count();
            int countRight = right.Count();
            int count = 1 + countLeft + countRight;
            if (countLeft + countRight <= 1)
                return new TreeDictionary<TKey, TValue, TComparer>(count, key, value, left, right);
            if (countLeft >= DELTA * countRight) // >= in Data.Map
                return RotateRight(key, value, left, right);
            if (countRight >= DELTA * countLeft) // >= in Data.Map
                return RotateLeft(key, value, left, right);
            // Balanced already
            return new TreeDictionary<TKey, TValue, TComparer>(count, key, value, left, right);
        }

        private TreeDictionary<TKey, TValue, TComparer> Balance(TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return ReferenceEquals(_left, left) && ReferenceEquals(_right, right)
                       ? this
                       : Balance(_key, _value, left, right);
        }

        private static TreeDictionary<TKey, TValue, TComparer> Balanced(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return new TreeDictionary<TKey, TValue, TComparer>(key, value, left, right);
        }

        /// <summary>
        /// Called instead of the constructor when <paramref name="left"/> and 
        /// <paramref name="right"/> may be unbalanced.
        /// </summary>
        private TreeDictionary<TKey, TValue, TComparer> Balanced(TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return EqualityComparer<TValue>.Default.Equals(_value, value)
                && ReferenceEquals(_left, left) && ReferenceEquals(_right, right)
                       ? this
                       : Balanced(_key, value, left, right);
        }

        /// <summary>
        /// Called instead of the constructor when <paramref name="left"/> and 
        /// <paramref name="right"/> may be unbalanced.
        /// </summary>
        private TreeDictionary<TKey, TValue, TComparer> Balanced(
            TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return ReferenceEquals(_left, left) && ReferenceEquals(_right, right)
                       ? this
                       : Balanced(_key, _value, left, right);
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateLeft(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return right._left.Count() < RATIO * right._right.Count()
                       ? RotateSingleLeft(key, value, left, right)
                       : RotateDoubleLeft(key, value, left, right);
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateRight(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return left._right.Count() < RATIO * left._left.Count()
                       ? RotateSingleRight(key, value, left, right)
                       : RotateDoubleRight(key, value, left, right);
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateSingleLeft(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return Balanced(
                right._key, right._value,
                Balanced(key, value, left, right._left),
                right._right);
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateSingleRight(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return Balanced(
                left._key, left._value,
                left._left,
                Balanced(key, value, left._right, right));
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateDoubleLeft(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            TreeDictionary<TKey, TValue, TComparer> rightLeft = right._left;
            return Balanced(
                rightLeft._key, rightLeft._value,
                Balanced(key, value, left, rightLeft._left),
                Balanced(right._key, right._value, rightLeft._right, right._right));
        }

        private static TreeDictionary<TKey, TValue, TComparer> RotateDoubleRight(
            TKey key, TValue value, TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            var leftRight = left._right;
            return Balanced(
                leftRight._key, leftRight._value,
                Balanced(left._key, left._value, left._left, leftRight._left),
                Balanced(key, value, leftRight._right, right));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of
        /// the same type. Dictionaries are considered equal, if they have the
        /// same keys and values.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the 
        /// <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        /// </param>
        public bool Equals(TreeDictionary<TKey, TValue, TComparer> other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _count == other._count && this.SequenceEqual(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TreeDictionary<TKey, TValue, TComparer>)) return false;
            return Equals((TreeDictionary<TKey, TValue, TComparer>) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int result = _count;
                result = (result * 397) ^ _key.GetHashCode();
                result = (result * 397) ^ (_value != null ? _value.GetHashCode() : 0);
                if (_left != null) {
                    result = (result * 397) ^ _left._key.GetHashCode();
                    result = (result * 397) ^ _left._value.GetHashCode();
                }
                if (_right != null) {
                    result = (result * 397) ^ _right._key.GetHashCode();
                    result = (result * 397) ^ _right._value.GetHashCode();
                }
                return result;
            }
        }

        /// <summary>
        /// Determines whether two instances of 
        /// <see cref="TreeDictionary&lt;TKey, TValue, TComparer&gt;"/> are
        /// equal (that is, have same keys and values).
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// <c>true</c> if the arguments are equal; <c>false</c> otherwise.
        /// </returns>
        public static bool operator ==(TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two instances of 
        /// <see cref="TreeDictionary&lt;TKey, TValue, TComparer&gt;"/> are
        /// unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// <c>true</c> if the arguments are not equal; <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(TreeDictionary<TKey, TValue, TComparer> left, TreeDictionary<TKey, TValue, TComparer> right) {
            return !Equals(left, right);
        }
    }

    internal class ReturnThisException : Exception { }

    public static class TreeDictionary {
        internal static int Count<TKey, TValue, TComparer>(this TreeDictionary<TKey, TValue, TComparer> dict) where TComparer : IComparer<TKey>, new() {
            return dict != null ? dict.Count : 0;
        }

        public static TreeDictionary<TKey, TValue, TComparer> Empty<TKey, TValue, TComparer>() 
            where TComparer : IComparer<TKey>, new() {
            return TreeDictionary<TKey, TValue, TComparer>.Empty;
        }

        public static TreeDictionary<TKey, TValue, DefaultComparer<TKey>> Empty<TKey, TValue>() {
            return TreeDictionary<TKey, TValue, DefaultComparer<TKey>>.Empty;
        }

        public static TreeDictionary<TKey, TValue, TComparer> Single<TKey, TValue, TComparer>(
            TKey key, TValue value) where TComparer : IComparer<TKey>, new() {
            return new TreeDictionary<TKey, TValue, TComparer>(
                1, key, value, Empty<TKey, TValue, TComparer>(), Empty<TKey, TValue, TComparer>());
        }

        public static TreeDictionary<TKey, TValue, DefaultComparer<TKey>> Single<TKey, TValue>(
        TKey key, TValue value) {
            return new TreeDictionary<TKey, TValue, DefaultComparer<TKey>>(
                1, key, value, Empty<TKey, TValue>(), Empty<TKey, TValue>());
        }
    }
}