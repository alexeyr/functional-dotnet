using System;
using FP.Collections.Immutable;
using Xunit;

namespace FPTests {
    public class VectorTests {
        private Vector<int> _emptyVector = Vector<int>.Empty;
        private Random _rnd = new Random();

        [Fact]
        public void InfiniteBounds() {
            for (int i = 0; i < 100; i++) {
                int index = _rnd.Next();
                Assert.Equal(0, _emptyVector[index]);
            }
        }

        [Fact]
        public void SingleElementVector() {
            for (int i = 0; i < 100; i++) {
                int value = _rnd.Next();
                Vector<int> vector = _emptyVector.SetAt(0, value);
                Assert.Equal(value, vector[0]);
            }            
        }

        [Fact]
        public void ReplaceSingleElement() {
            var vector = Vector.New("Haskell", "Ocaml", "Scala", "Ruby");
            for (int i = 0; i < 4; i++) {
                var vector2 = vector.SetAt(i, "C#");
                for (int j = 0; j < 4; j++) {
                    if (j == i)
                        Assert.Equal("C#", vector2[j]);
                    else
                        Assert.Equal(vector[j], vector2[j]);
                }
            }
        }

        [Fact]
        public void StoreManyElements() {
            const int LENGTH = 10000;
            var array = new int[LENGTH];
            var vector = _emptyVector;
            for (int i = 0; i < LENGTH; i++) {
                int index = _rnd.Next(LENGTH);
                int value = _rnd.Next();
                array[index] = value;
                vector = vector.SetAt(index, value);
            }
            for (int i = 0; i < LENGTH; i++)
                Assert.Equal(array[i], vector[i]);
        }
    }
}