using System;

using NUnit.Framework;
using RandomNumbers;

namespace RandomNumbersTests
{
    [TestFixture]
    public class GenerateRandomTests
    {
        RandomGenerator _Rand = new RandomGenerator();

        [Test]
        [Category("RandomNumbers")]
        public void GenerateRandInt()
        {
            int min_value = 50;
            int max_value = 50;
            int buffer;

            for (int i = 0; i < 3000; i++)
            {
                buffer = _Rand.Int(1, 101);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(max_value == 101);
            Assert.IsTrue(min_value == 1);
        }

        [Test]
        [Category("RandomNumbers")]
        public void GenerateRandColor()
        {
            string buffer = _Rand.ColorString();
            Assert.IsTrue(buffer.Length == 7);

            Console.Write(buffer);
        }

        [Test]
        [Category("RandomNumbers")]
        public void GenerateRandColorTint()
        {
            string buffer = _Rand.ColorString(0.7f, 0f, 0f, 0.25f);
            Assert.IsTrue(buffer.Length == 7);
        }

        [Test]
        [Category("RandomNumbers")]
        public void RandomPointInACircle()
        {
            for (int i = 0; i < 100; i++)
            {
                var output = _Rand.RandomPointInACircle(0, 0, 1);

                // test: x^2 + y^2 <= 1
                float test_value = (output.Item1 * output.Item1) + (output.Item2 * output.Item2);
                Assert.IsTrue((test_value >= 0 && test_value <= 1), $"Test value out of excpected range {test_value}");
            }
        }
    }
}
