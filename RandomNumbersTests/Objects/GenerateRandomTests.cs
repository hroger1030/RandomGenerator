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
using System;

namespace RandomNumbersTests
{
    [TestFixture]
    public class GenerateRandomTests
    {
        private readonly int NUMBER_OF_TESTS = 10000;
        private readonly RandomGenerator _Rand = new RandomGenerator();

        [Test]
        [Category("Int")]
        public void Int_TestRange_Overload1()
        {
            int ceiling = 64;
            int floor = 0;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            int buffer;

            for (int i = 0; i < 10000; i++)
            {
                buffer = _Rand.Int(ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(max_value <= ceiling, $"Max value of {max_value} outside of expected range");
            Assert.IsTrue(min_value >= floor, $"Max value of {min_value} outside of expected range");
        }

        [Test]
        [Category("Int")]
        public void Int_TestRange_Overload2()
        {
            int ceiling = 64;
            int floor = 23;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            int buffer;

            for (int i = 0; i < 10000; i++)
            {
                buffer = _Rand.Int(floor, ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(max_value <= ceiling, $"Max value of {max_value} outside of expected range");
            Assert.IsTrue(min_value >= floor, $"Max value of {min_value} outside of expected range");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange()
        {
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte();

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.IsTrue(max_value <= byte.MaxValue, $"Max value of {max_value} outside of expected range");
            Assert.IsTrue(min_value >= byte.MinValue, $"Max value of {min_value} outside of expected range");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange_Overload1()
        {
            byte ceiling = 64;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte(ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.IsTrue(max_value <= ceiling, $"Max value of {max_value} outside of expected range");
            Assert.IsTrue(min_value >= byte.MinValue, $"Max value of {min_value} outside of expected range");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange_Overload2()
        {
            byte ceiling = 64;
            byte floor = 4;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte(floor,ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.IsTrue(max_value <= ceiling, $"Max value of {max_value} outside of expected range");
            Assert.IsTrue(min_value >= floor, $"Max value of {min_value} outside of expected range");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange_Overload3()
        {
            int count = 11;
            var output = _Rand.ByteArray(count);

            Assert.IsTrue(output.Length == count, $"Length of array is {output.Length}, not 11");
        }

        [Test]
        [Category("Color")]
        public void GenerateRandColor()
        {
            string buffer = _Rand.ColorString();
            Assert.IsTrue(buffer.Length == 7);

            Console.Write(buffer);
        }

        [Test]
        [Category("Color")]
        public void GenerateRandColorTint()
        {
            string buffer = _Rand.ColorString(0.7f, 0f, 0f, 0.25f);
            Assert.IsTrue(buffer.Length == 7);
        }

        [Test]
        [Category("Circle")]
        public void RandomPointInACircle()
        {
            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                var output = _Rand.RandomPointInUnitCircle(0, 0, 1);

                // test: x^2 + y^2 <= 1
                float test_value = (output.Item1 * output.Item1) + (output.Item2 * output.Item2);
                Assert.IsTrue((test_value >= 0 && test_value <= 1), $"Test value {test_value} out of excpected range");
            }
        }
    }
}
