/* (C) Alexey Romanov 2008 */

using System;

namespace FP {
    public delegate R Continuation<T, R>(Func<T, R> computation);

    public static class Continuations {
        public static Continuation<T, R> FromArgument<T, R>(T arg) {
            return Wrap<T, R>(arg);
        }

        public static Continuation<T, R> Wrap<T, R>(T arg) {
            return comp => comp(arg);
        }

        public static R Run<T, R>(this Continuation<T, R> continuation, Func<T, R> final) {
            return continuation(final);
        }

        public static Continuation<T, R> Map<T, R>(this Continuation<T, R> continuation, Func<R, R> func) {
            return comp => func(continuation(comp));
        }
    }
}