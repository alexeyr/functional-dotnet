/* (c) Alexey Romanov 2008
 * Functional .Net
 * Adapted from http://community.bartdesmet.net/blogs/bart/archive/2008/03/30/a-functional-c-type-switch.aspx
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FP
{
    /// <summary>
    /// A helper class to provide generic parameter inference.
    /// You can use <c>Switch.On(t)</c> instead of <c>new Switch{T}(t)</c>.
    /// </summary>
    public static class Switch
    {
        /// <summary>
        /// Switches on the specified object.
        /// </summary>
        /// <param name="value">The object we are switching on.</param>
        /// <returns>The switch.</returns>
        public static Switch<T> On<T>(T value)
        {
            return new Switch<T>(value);
        }
    }

    /// <summary>
    /// A generic functional switch.
    /// </summary>
    /// <typeparam name="T">The type of the object we are switching on.</typeparam>
    public class Switch<T>
    {
        private bool _break;
        private bool _badType;
        private readonly object _object;
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch{T}"/> class.
        /// </summary>
        /// <param name="value">The object we are switching on.</param>
        public Switch(T value)
        {
            _value = value;
            _object = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch{T}"/> class.
        /// </summary>
        /// <param name="value">The object we are switching on.</param>
        /// <param name="o">The object we we are switching on, as <see cref="object"/>.</param>
        private Switch(T value, object o)
        {
            _value = value;
            _object = o;
        }

        /// <summary>
        /// Breaks the chain.
        /// </summary>
        /// <typeparam name="T1">The type of the new switch.</typeparam>
        /// <returns>A switch which does nothing in any case.</returns>
        public static Switch<T1> Break<T1>()
        {
            return new Switch<T1>(default(T1)) {_break = true};
        }


        /// <summary>
        /// In case the value we are switching on equals <paramref name="t"/>, do <paramref name="action"/>.
        /// </summary>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<T> Case(T t, Action<T> action)
        {
            return Case(x => x.Equals(t), action);
        }

        /// <summary>
        /// In case the value we are switching on equals <paramref name="t"/>, do <paramref name="action"/>
        /// and fall through to the next case.
        /// </summary>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<T> CaseWithFallthrough(T t, Action<T> action)
        {
            return CaseWithFallthrough(x => x.Equals(t), action);
        }

        /// <summary>
        /// In case the value we are switching on satisfies <paramref name="predicate"/>, do <paramref name="action"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<T> Case(Func<T, bool> predicate, Action<T> action)
        {
            if (_break || _badType)
                return this;
            if (predicate(_value))
            {
                action(_value);
                _break = true;
            }
            return this;
        }

        /// <summary>
        /// In case the value we are switching on satisfies <paramref name="predicate"/>, do <paramref name="action"/>
        /// and fall through to the next case.
        /// </summary>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<T> CaseWithFallthrough(Func<T, bool> predicate, Action<T> action)
        {
            if (_break || _badType)
                return this;
            if (predicate(_value))
                action(_value);
            return this;
        }

        /// <summary>
        /// In case we have reached this, do <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to do.</param>
        public void Default(Action<T> action)
        {
            if (!(_break || _badType))
                action(_value);
        }

        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/> and equals <paramref name="t"/>, do <paramref name="action"/>.
        /// </summary>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<TNew> Case<TNew>(TNew t, Action<TNew> action)
        {
            return Case(x => x.Equals(t), action);
        }

        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/> and equals <paramref name="t"/>, do <paramref name="action"/>
        /// and fall through to the next case.
        /// </summary>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<TNew> CaseWithFallthrough<TNew>(TNew t, Action<TNew> action)
        {
            return CaseWithFallthrough(x => x.Equals(t), action);
        }

        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/> and satisfies <paramref name="predicate"/>, do <paramref name="action"/>.
        /// </summary>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<TNew> Case<TNew>(Func<TNew, bool> predicate, Action<TNew> action)
        {
            bool goodType = _object is TNew;
            return new Switch<TNew>(goodType ? (TNew)_object : default(TNew), _object) { _badType = !goodType }.
                Case(predicate, action);
        }

        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/> and satisfies <paramref name="predicate"/>, do <paramref name="action"/>
        /// and fall through to the next case.
        /// </summary>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="action">The action to do in this case.</param>
        public Switch<TNew> CaseWithFallthrough<TNew>(Func<TNew, bool> predicate, Action<TNew> action)
        {
            bool goodType = _object is TNew;
            return new Switch<TNew>(goodType ? (TNew)_object : default(TNew), _object) { _badType = !goodType }
                .CaseWithFallthrough(predicate, action);
        }

        /// <summary>
        /// In case we have reached this, if the value we are switching on has type <typeparam name="TNew"/>, do <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="TNew">The type of the new value.</typeparam>
        /// <param name="action">The action to do.</param>
        public void Default<TNew>(Action<TNew> action)
        {
            if (_object is TNew)
                new Switch<TNew>((TNew) _object, _object).Default(action);
        }
    }

    /// <summary>
    /// A generic functional switch expression (as opposed to a statement).
    /// </summary>
    /// <typeparam name="T">The type of the object we are switching on.</typeparam>
    /// <typeparam name="R">The type of the result.</typeparam>
    public class Switch<T, R>
    {
        private readonly T _value;
        private bool _badType;
        private readonly object _object;
        private bool _hasResult;
        private R _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch{T}"/> class.
        /// </summary>
        /// <param name="value">The object we are switching on.</param>
        public Switch(T value)
        {
            _value = value;
            _object = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch{T}"/> class.
        /// </summary>
        /// <param name="value">The object we are switching on.</param>
        /// <param name="o">The object we we are switching on, as <see cref="object"/>.</param>
        private Switch(T value, object o)
        {
            _value = value;
            _object = o;
        }

        /// <summary>
        /// Gets a value indicating whether this switch has a result.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this switch "expression" has a result; otherwise, <c>false</c>.
        /// </value>
        public bool HasResult
        {
            get { return _hasResult; }
        }

        /// <summary>
        /// Gets the result of this switch.
        /// </summary>
        /// <value>The result, if <see cref="HasResult"/> is <c>true</c>; <c>default(R)</c> otherwise.</value>
        public R Result
        {
            get { return _result; }
            private set 
            {
                _hasResult = true;
                _result = value;
            }
        }

        /// <summary>
        /// In case the value we are switching on equals <paramref name="t"/>, apply 
        /// <paramref name="function"/> to it and return the result.
        /// </summary>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="function">The action to do in this case.</param>
        public Switch<T, R> Case(T t, Func<T, R> function)
        {
            return Case(x => x.Equals(t), function);
        }

        /// <summary>
        /// In case the value we are switching on satisfies <paramref name="predicate"/>, apply 
        /// <paramref name="function"/> to it and return the result.
        /// </summary>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="function">The action to do in this case.</param>
        public Switch<T, R> Case(Func<T, bool> predicate, Func<T, R> function)
        {
            if (!HasResult && !_badType && predicate(_value))
                Result = function(_value);
            return this;
        }

        /// <summary>
        /// In case we have reached this, apply 
        /// <paramref name="function"/> to the value we are switching on and return the result.
        /// </summary>
        /// <param name="function">The action to do in this case.</param>
        public R Default(Func<T, R> function)
        {
            if (!HasResult)
                Result = function(_value);
            return Result;
        }

        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/>
        /// and equals <paramref name="t"/>, apply 
        /// <paramref name="function"/> to it and return the result.
        /// </summary>
        /// <typeparam name="TNew">The type of the new value.</typeparam>
        /// <param name="t">The value to compare the value we are switching on to.</param>
        /// <param name="function">The action to do in this case.</param>
        public Switch<TNew, R> Case<TNew>(TNew t, Func<TNew, R> function)
        {
            return Case(x => x.Equals(t), function);
        }
        
        /// <summary>
        /// In case the value we are switching on has type <typeparam name="TNew"/>
        /// and satisfies <paramref name="predicate"/>, apply 
        /// <paramref name="function"/> to it and return the result.
        /// </summary>
        /// <typeparam name="TNew">The type of the new value.</typeparam>
        /// <param name="predicate">The predicate to test the value we are switching on.</param>
        /// <param name="function">The action to do in this case.</param>
        public Switch<TNew, R> Case<TNew>(Func<TNew, bool> predicate, Func<TNew, R> function)
        {
            bool goodType = _object is TNew;
            return new Switch<TNew, R>(goodType ? (TNew)_object : default(TNew), _object) { _badType = !goodType }.
                Case(predicate, function);
        }

        /// <summary>
        /// In case we have reached this, if the value we are switching on has type <typeparam name="TNew"/>, apply 
        /// <paramref name="function"/> to it and return the result.
        /// </summary>
        /// <typeparam name="TNew">The type of the new value.</typeparam>
        /// <param name="function">The action to do in this case.</param>
        public R Default<TNew>(Func<TNew, R> function)
        {
            if (_object is TNew)
                return new Switch<TNew, R>((TNew)_object, _object).Default(function);
        }
    }

}
