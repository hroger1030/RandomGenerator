/*
The MIT License (MIT)

Copyright (c) 2010 Roger Hill

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge,
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using NUnit.Framework;
using RandomNumbers;
using System.Collections.Generic;
using System.Linq;

namespace RandomNumbersTests
{
    [TestFixture]
    public class CollectionRandomizerTests
    {
        [Test]
        [Category("CollectionRandomizer")]
        public void ShuffleList_PreservesAllElements()
        {
            var original = Enumerable.Range(1, 30).ToList();
            var shuffled = new List<int>(original);

            shuffled.ShuffleList();

            Assert.That(shuffled, Is.EquivalentTo(original));
        }

        [Test]
        [Category("CollectionRandomizer")]
        public void ShuffleList_ChangesOrder()
        {
            var original = Enumerable.Range(1, 30).ToList();
            var shuffled = new List<int>(original);

            shuffled.ShuffleList();

            Assert.That(shuffled, Is.Not.EqualTo(original), "ShuffleList did not change the order of a 30-element list");
        }

        [Test]
        [Category("CollectionRandomizer")]
        public void CryptoShuffleList_PreservesAllElements()
        {
            var original = Enumerable.Range(1, 30).ToList();
            var shuffled = new List<int>(original);

            shuffled.CryptoShuffleList();

            Assert.That(shuffled, Is.EquivalentTo(original));
        }

        [Test]
        [Category("CollectionRandomizer")]
        public void CryptoShuffleList_ChangesOrder()
        {
            var original = Enumerable.Range(1, 30).ToList();
            var shuffled = new List<int>(original);

            shuffled.CryptoShuffleList();

            Assert.That(shuffled, Is.Not.EqualTo(original), "CryptoShuffleList did not change the order of a 30-element list");
        }
    }
}
