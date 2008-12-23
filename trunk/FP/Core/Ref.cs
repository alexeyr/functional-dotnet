/*
* Ref.cs is part of functional-dotnet project
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
using System.Reflection;

namespace FP.Core {
    /// <summary>
    /// The type of mutable references to <typeparamref name="T"/> which use locks to
    /// make access atomic.
    /// </summary>
    /// <typeparam name="T">The type of values.</typeparam>
    /// <remarks>Type <typeparamref name="T"/> should be immutable or at least the
    /// value should not be mutated. In this case the use of this type is thread-safe.
    /// All value changes except directly mutating the value are atomic.
    /// 
    /// Similar to Alice ML's/OCaml's ref type and Clojure's atom type.</remarks>
    internal class Ref<T> : RefBase<T> {
        protected T _value;

        /// <summary>
        /// Value of the reference.
        /// </summary>
        public override T Value {
            get { return _value; }
            set { Store(value); }
        }

        /// <summary>
        /// Creates a reference with the specified initial value.
        /// </summary>
        /// <param name="value">The value.</param>
        public Ref(T value) : this(value, null) {}

        /// <summary>
        /// Creates a reference with the specified initial value and validator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validator">The validator function.</param>
        /// <exception cref="RefValidationException">when <paramref name="value"/> doesn't
        /// pass <paramref name="validator"/>.</exception>
        public Ref(T value, Action<T> validator) : base(value, validator) {
            _value = value;
        }

        /// <summary>
        /// Stores the specified new value.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>The old value.</returns>
        /// <exception cref="RefValidationException">when <paramref name="newValue"/> doesn't
        /// pass <see cref="RefBase{T}.Validator"/>.</exception>
        public override T Store(T newValue) {
            Validate(newValue);
            lock (this) {
                T tmp = _value;
                _value = newValue;
                return tmp;
            }
        } // Store(newValue)

        /// <summary>
        /// Atomically sets <see cref="Value"/> to <paramref name="newValue"/> if and only
        /// if the current value of the atom is identical to <see cref="oldValue"/>
        /// according to <c>Value.Equals(oldValue)</c> and <c>Validator(newValue)</c>
        /// succeeds.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns><c>true</c> if the change happened, else <c>false</c>.</returns>
        /// <exception cref="RefValidationException">when <paramref name="newValue"/> doesn't
        /// pass <see cref="RefBase{T}.Validator"/>.</exception>
        public override bool CompareAndSet(T oldValue, T newValue) {
            Validate(newValue);
            lock (this) {
                if (!Value.Equals(oldValue))
                    return false;
                Value = newValue;
                return true;
            }
        } // CompareAndSet(oldValue, newValue)

        // Modify()
    }

    /// <summary>
    /// Utility class with static methods for <see cref="Ref{T}"/>.
    /// </summary>
    public static class Ref {
        /// <summary>
        /// Atomically increments the value of the specified reference.
        /// </summary>
        /// <param name="r">The reference.</param>
        public static void Increment(this IRef<int> r) {
            r.Modify(n => n + 1);
        }

        /// <summary>
        /// Atomically decrements the value of the specified reference.
        /// </summary>
        /// <param name="r">The reference.</param>
        public static void Decrement(this IRef<int> r) {
            r.Modify(n => n - 1);
        }

        /// <summary>
        /// Creates a reference with the specified initial value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static IRef<T> New<T>(T value) {
            return New(value, null);
        }

        /// <summary>
        /// Creates a reference with the specified initial value and validator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validator">The validator function.</param>
        /// <remarks>If <paramref name="value"/> doesn't pass <paramref name="validator"/>,
        /// this will throw an exception.</remarks>
        public static IRef<T> New<T>(T value, Action<T> validator) {
            // Thanks to Mark Gravell! 
            // http://stackoverflow.com/questions/388718/how-to-make-a-net-generic-method-behave-differently-for-value-types-and-referen
            return Cache<T>.CachedConstructor(value, validator);
        }

        // ReSharper disable UnusedPrivateMember
        private static IRef<T> NewRef<T>(T value, Action<T> validator) where T : class {
            return new RefObject<T>(value, validator);
        }

        private static IRef<T> NewVal<T>(T value, Action<T> validator) {
            return new Ref<T>(value, validator);
        }

        private static IRef<bool> NewBool(bool value, Action<bool> validator) {
            return new RefBool(value, validator);
        }

        private static IRef<double> NewDouble(double value, Action<double> validator) {
            return new RefDouble(value, validator);
        }

        private static IRef<float> NewFloat(float value, Action<float> validator) {
            return new RefFloat(value, validator);
        }

        private static IRef<int> NewInt(int value, Action<int> validator) {
            return new RefInt(value, validator);
        }

        private static IRef<IntPtr> NewIntPtr(IntPtr value, Action<IntPtr> validator) {
            return new RefIntPtr(value, validator);
        }

        private static IRef<long> NewLong(long value, Action<long> validator) {
            return new RefLong(value, validator);
        }
        // ReSharper restore UnusedPrivateMember

        private static class Cache<T> {
            internal static readonly Func<T, Action<T>, IRef<T>> CachedConstructor;
            static Cache() {
                Type refType = typeof(Ref);
                Type tType = typeof(T);
                const BindingFlags privateStatic = BindingFlags.NonPublic | BindingFlags.Static;
                MethodInfo method = 
                    !tType.IsValueType ? refType.GetMethod("NewRef", privateStatic).MakeGenericMethod(tType) :
                    tType == typeof(bool) ? refType.GetMethod("NewBool", privateStatic) :
                    tType == typeof(double) ? refType.GetMethod("NewDouble", privateStatic) :
                    tType == typeof(float) ? refType.GetMethod("NewFloat", privateStatic) :
                    tType == typeof(int) ? refType.GetMethod("NewInt", privateStatic) :
                    tType == typeof(IntPtr) ? refType.GetMethod("NewIntPtr", privateStatic) :
                    tType == typeof(long) ? refType.GetMethod("NewLong", privateStatic) :
                    refType.GetMethod("NewVal", privateStatic).MakeGenericMethod(tType);
                CachedConstructor = (Func<T, Action<T>, IRef<T>>)Delegate.CreateDelegate(typeof(Func<T, Action<T>, IRef<T>>), method);
            }
        }
    }
}