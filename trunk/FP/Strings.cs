using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FP {
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for objects that 
    /// implement <see cref="IEnumerable{T}"/>. 
    /// </summary>
    /// <remarks>
    /// The source of each method includes the Haskell version as a comment at the end.
    /// Some methods are not safe to use with mutable collections. These cases are mentioned in the 
    /// documentation for the method. The order of arguments has generally been inverted compared 
    /// to the Haskell versions to facilitate method chaining.
    /// 
    /// See also the Remarks for <see cref="Enumerable"/>.
    /// </remarks>
    public static class Strings {
        /// <summary>
        /// Converts a sequence of <see cref="char"/>s to a <see cref="string"/>.
        /// This should be called <c>ToString</c>, but this is impossible for the
        /// obvious reason.
        /// </summary>
        /// <param name="charSequence">The char sequence.</param>
        /// <returns></returns>
        public static string ToStringProper(
            this IEnumerable<char> charSequence) {
            return Switch.ExprOn<IEnumerable<char>, string>(
                charSequence)
                .Case<string>(s => s)
                .Default(cs => {
                             var builder = new StringBuilder();
                             foreach (char c in charSequence)
                                 builder.Append(c);
                             return builder.ToString();
                         });
        }

        //TODO:
        //-- | 'lines' breaks a string up into a list of strings at newline
        //-- characters.  The resulting strings do not contain newlines.
        //lines			:: String -> [String]
        //lines ""		=  []
        //lines s			=  let (l, s') = break (== '\n') s
        //			   in  l : case s' of
        //					[]     	-> []
        //					(_:s'') -> lines s''
        //
        //-- | 'unlines' is an inverse operation to 'lines'.
        //-- It joins lines, after appending a terminating newline to each.
        //unlines			:: [String] -> String
        //unlines [] = []
        //unlines (l:ls) = l ++ '\n' : unlines ls
        //
        //-- | 'words' breaks a string up into a list of words, which were delimited
        //-- by white space.
        //words			:: String -> [String]
        //words s			=  case dropWhile {-partain:Char.-}isSpace s of
        //				"" -> []
        //				s' -> w : words s''
        //				      where (w, s'') = 
        //                                             break {-partain:Char.-}isSpace s'
        //
        //-- | 'unwords' is an inverse operation to 'words'.
        //-- It joins words with separating spaces.
        //unwords			:: [String] -> String
        //unwords []		=  ""
        //unwords [w]		= w
        //unwords (w:ws)		= w ++ ' ' : unwords ws
    }
}