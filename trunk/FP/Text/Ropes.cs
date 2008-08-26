namespace FP.Text {
    /// <summary>
    /// Utility class for Ropes.
    /// </summary>
    public static class Ropes {
        public static StringRope ToRope(this string s) {
            return new StringRope(s);
        }

        public static ArrayRope<TChar> ToRope<TChar>(this TChar[] array) {
            return new ArrayRope<TChar>(array);
        }
    }
}