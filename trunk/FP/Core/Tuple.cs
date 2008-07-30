// Author of the template: _FRED_
// Modified by Alexey Romanov

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FP.Core
{
                    #region struct Tuple<T1, T2>
    /// <summary> An immutable tuple with 2 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2) {
            this._item1 = item1;
            this._item2 = item2;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return obj is Tuple<T1, T2> && Equals((Tuple<T1, T2>)obj);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2> left, Tuple<T1, T2> right) {
            return left.Equals(right);
          }

    public static bool operator !=(Tuple<T1, T2> left, Tuple<T1, T2> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2>> Members

    public bool Equals(Tuple<T1, T2> other) {
      return           EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) ;           }

    #endregion IEquatable<Tuple<T1, T2>> Members
  }

  #endregion struct Tuple<T1, T2>
  
                  #region struct Tuple<T1, T2, T3>
    /// <summary> An immutable tuple with 3 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2, T3 item3) {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }

        public T3 Item3 {
      [DebuggerStepThrough]
      get { return _item3; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2, Item3);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return obj is Tuple<T1, T2, T3> && Equals((Tuple<T1, T2, T3>)obj);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1)
                              ^ Tuple.RotateRight(EqualityComparer<T3>.Default.GetHashCode(Item3), 2);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right) {
            return left.Equals(right);
          }

    public static bool operator !=(Tuple<T1, T2, T3> left, Tuple<T1, T2, T3> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2, T3>> Members

    public bool Equals(Tuple<T1, T2, T3> other) {
      return           EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) 
               &&  EqualityComparer<T3>.Default.Equals(Item3, other.Item3) ;           }

    #endregion IEquatable<Tuple<T1, T2, T3>> Members
  }

  #endregion struct Tuple<T1, T2, T3>
  
                  #region class Tuple<T1, T2, T3, T4>
    /// <summary> An immutable tuple with 4 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public sealed class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
            this._item4 = item4;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }

        public T3 Item3 {
      [DebuggerStepThrough]
      get { return _item3; }
    }

        public T4 Item4 {
      [DebuggerStepThrough]
      get { return _item4; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2, Item3, Item4);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return Equals(obj as Tuple<T1, T2, T3, T4>);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1)
                              ^ Tuple.RotateRight(EqualityComparer<T3>.Default.GetHashCode(Item3), 2)
                              ^ Tuple.RotateRight(EqualityComparer<T4>.Default.GetHashCode(Item4), 3);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2, T3, T4> left, Tuple<T1, T2, T3, T4> right) {
            return Equals(left, right);
          }

    public static bool operator !=(Tuple<T1, T2, T3, T4> left, Tuple<T1, T2, T3, T4> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2, T3, T4>> Members

    public bool Equals(Tuple<T1, T2, T3, T4> other) {
      return  other != null           &&  EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) 
               &&  EqualityComparer<T3>.Default.Equals(Item3, other.Item3) 
               &&  EqualityComparer<T4>.Default.Equals(Item4, other.Item4) ;           }

    #endregion IEquatable<Tuple<T1, T2, T3, T4>> Members
  }

  #endregion sealed class Tuple<T1, T2, T3, T4>
  
                  #region class Tuple<T1, T2, T3, T4, T5>
    /// <summary> An immutable tuple with 5 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public sealed class Tuple<T1, T2, T3, T4, T5> : IEquatable<Tuple<T1, T2, T3, T4, T5>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;
        private readonly T5 _item5;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
            this._item4 = item4;
            this._item5 = item5;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }

        public T3 Item3 {
      [DebuggerStepThrough]
      get { return _item3; }
    }

        public T4 Item4 {
      [DebuggerStepThrough]
      get { return _item4; }
    }

        public T5 Item5 {
      [DebuggerStepThrough]
      get { return _item5; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2, Item3, Item4, Item5);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return Equals(obj as Tuple<T1, T2, T3, T4, T5>);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1)
                              ^ Tuple.RotateRight(EqualityComparer<T3>.Default.GetHashCode(Item3), 2)
                              ^ Tuple.RotateRight(EqualityComparer<T4>.Default.GetHashCode(Item4), 3)
                              ^ Tuple.RotateRight(EqualityComparer<T5>.Default.GetHashCode(Item5), 4);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2, T3, T4, T5> left, Tuple<T1, T2, T3, T4, T5> right) {
            return Equals(left, right);
          }

    public static bool operator !=(Tuple<T1, T2, T3, T4, T5> left, Tuple<T1, T2, T3, T4, T5> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2, T3, T4, T5>> Members

    public bool Equals(Tuple<T1, T2, T3, T4, T5> other) {
      return  other != null           &&  EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) 
               &&  EqualityComparer<T3>.Default.Equals(Item3, other.Item3) 
               &&  EqualityComparer<T4>.Default.Equals(Item4, other.Item4) 
               &&  EqualityComparer<T5>.Default.Equals(Item5, other.Item5) ;           }

    #endregion IEquatable<Tuple<T1, T2, T3, T4, T5>> Members
  }

  #endregion sealed class Tuple<T1, T2, T3, T4, T5>
  
                  #region class Tuple<T1, T2, T3, T4, T5, T6>
    /// <summary> An immutable tuple with 6 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public sealed class Tuple<T1, T2, T3, T4, T5, T6> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;
        private readonly T5 _item5;
        private readonly T6 _item6;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
            this._item4 = item4;
            this._item5 = item5;
            this._item6 = item6;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }

        public T3 Item3 {
      [DebuggerStepThrough]
      get { return _item3; }
    }

        public T4 Item4 {
      [DebuggerStepThrough]
      get { return _item4; }
    }

        public T5 Item5 {
      [DebuggerStepThrough]
      get { return _item5; }
    }

        public T6 Item6 {
      [DebuggerStepThrough]
      get { return _item6; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2, Item3, Item4, Item5, Item6);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return Equals(obj as Tuple<T1, T2, T3, T4, T5, T6>);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1)
                              ^ Tuple.RotateRight(EqualityComparer<T3>.Default.GetHashCode(Item3), 2)
                              ^ Tuple.RotateRight(EqualityComparer<T4>.Default.GetHashCode(Item4), 3)
                              ^ Tuple.RotateRight(EqualityComparer<T5>.Default.GetHashCode(Item5), 4)
                              ^ Tuple.RotateRight(EqualityComparer<T6>.Default.GetHashCode(Item6), 5);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2, T3, T4, T5, T6> left, Tuple<T1, T2, T3, T4, T5, T6> right) {
            return Equals(left, right);
          }

    public static bool operator !=(Tuple<T1, T2, T3, T4, T5, T6> left, Tuple<T1, T2, T3, T4, T5, T6> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2, T3, T4, T5, T6>> Members

    public bool Equals(Tuple<T1, T2, T3, T4, T5, T6> other) {
      return  other != null           &&  EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) 
               &&  EqualityComparer<T3>.Default.Equals(Item3, other.Item3) 
               &&  EqualityComparer<T4>.Default.Equals(Item4, other.Item4) 
               &&  EqualityComparer<T5>.Default.Equals(Item5, other.Item5) 
               &&  EqualityComparer<T6>.Default.Equals(Item6, other.Item6) ;           }

    #endregion IEquatable<Tuple<T1, T2, T3, T4, T5, T6>> Members
  }

  #endregion sealed class Tuple<T1, T2, T3, T4, T5, T6>
  
                  #region class Tuple<T1, T2, T3, T4, T5, T6, T7>
    /// <summary> An immutable tuple with 7 fields. </summary>
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    public sealed class Tuple<T1, T2, T3, T4, T5, T6, T7> : IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7>>
    {
    #region Fields
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;
        private readonly T5 _item5;
        private readonly T6 _item6;
        private readonly T7 _item7;
        #endregion Fields

    #region Constructors\Finalizer
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
            this._item4 = item4;
            this._item5 = item5;
            this._item6 = item6;
            this._item7 = item7;
          }
    #endregion Constructors\Finalizer

    #region Properties

        public T1 Item1 {
      [DebuggerStepThrough]
      get { return _item1; }
    }

        public T2 Item2 {
      [DebuggerStepThrough]
      get { return _item2; }
    }

        public T3 Item3 {
      [DebuggerStepThrough]
      get { return _item3; }
    }

        public T4 Item4 {
      [DebuggerStepThrough]
      get { return _item4; }
    }

        public T5 Item5 {
      [DebuggerStepThrough]
      get { return _item5; }
    }

        public T6 Item6 {
      [DebuggerStepThrough]
      get { return _item6; }
    }

        public T7 Item7 {
      [DebuggerStepThrough]
      get { return _item7; }
    }
    
    #endregion Properties
    
    #region Methods

    public string ToString(IFormatProvider provider) {
      return Tuple.ToString(provider, Item1, Item2, Item3, Item4, Item5, Item6, Item7);
    }

    #endregion Methods

    #region Overrides

    public override bool Equals(object obj) {
            return Equals(obj as Tuple<T1, T2, T3, T4, T5, T6, T7>);
          }

    public override int GetHashCode() {
                    return EqualityComparer<T1>.Default.GetHashCode(Item1)
                              ^ Tuple.RotateRight(EqualityComparer<T2>.Default.GetHashCode(Item2), 1)
                              ^ Tuple.RotateRight(EqualityComparer<T3>.Default.GetHashCode(Item3), 2)
                              ^ Tuple.RotateRight(EqualityComparer<T4>.Default.GetHashCode(Item4), 3)
                              ^ Tuple.RotateRight(EqualityComparer<T5>.Default.GetHashCode(Item5), 4)
                              ^ Tuple.RotateRight(EqualityComparer<T6>.Default.GetHashCode(Item6), 5)
                              ^ Tuple.RotateRight(EqualityComparer<T7>.Default.GetHashCode(Item7), 6);                   }

    public override string ToString() {
      return ToString(null);
    }

    #endregion Overrides
    
    #region Operators

    public static bool operator ==(Tuple<T1, T2, T3, T4, T5, T6, T7> left, Tuple<T1, T2, T3, T4, T5, T6, T7> right) {
            return Equals(left, right);
          }

    public static bool operator !=(Tuple<T1, T2, T3, T4, T5, T6, T7> left, Tuple<T1, T2, T3, T4, T5, T6, T7> right) {
      return !(left == right);
    }

    #endregion Operators

    #region IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Members

    public bool Equals(Tuple<T1, T2, T3, T4, T5, T6, T7> other) {
      return  other != null           &&  EqualityComparer<T1>.Default.Equals(Item1, other.Item1) 
               &&  EqualityComparer<T2>.Default.Equals(Item2, other.Item2) 
               &&  EqualityComparer<T3>.Default.Equals(Item3, other.Item3) 
               &&  EqualityComparer<T4>.Default.Equals(Item4, other.Item4) 
               &&  EqualityComparer<T5>.Default.Equals(Item5, other.Item5) 
               &&  EqualityComparer<T6>.Default.Equals(Item6, other.Item6) 
               &&  EqualityComparer<T7>.Default.Equals(Item7, other.Item7) ;           }

    #endregion IEquatable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Members
  }

  #endregion sealed class Tuple<T1, T2, T3, T4, T5, T6, T7>
  
    #region class Tuple

  public static class Tuple
  {
    #region Methods
    
    #region Helpers

    internal static int RotateRight(int value, int places) {
      if((places &= 0x1F) == 0) {
        return value;
      }//if

      var mask = ~0x7FFFFFFF >> (places - 1);
      return ((value >> places) & ~mask) | ((value << (32 - places)) & mask);
    }

    internal static string ToString(IFormatProvider provider, params object[] values) {
      if(values != null) {
        const char start = '(';
        const char end = ')';
        const string separator = ", ";

        return start + String.Join(separator, Array.ConvertAll(values, value => Convert.ToString(value, provider))) + end;
      }//if
      return String.Empty;
    }

    #endregion Helpers

    #region New(…)

    	              public static Tuple<T1, T2> New<T1, T2>(T1 item1, T2 item2) {
      return new Tuple<T1, T2>(item1, item2);
    }

    	              public static Tuple<T1, T2, T3> New<T1, T2, T3>(T1 item1, T2 item2, T3 item3) {
      return new Tuple<T1, T2, T3>(item1, item2, item3);
    }

    	              public static Tuple<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) {
      return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
    }

    	              public static Tuple<T1, T2, T3, T4, T5> New<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) {
      return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
    }

    	              public static Tuple<T1, T2, T3, T4, T5, T6> New<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) {
      return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
    }

    	              public static Tuple<T1, T2, T3, T4, T5, T6, T7> New<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) {
      return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
    }
    
    #endregion New(…)

    #endregion Methods
  }
  
  #endregion class Tuple
}
