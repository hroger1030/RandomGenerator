using System;

using NUnit.Framework;
using RandomNumbers;

namespace RandomNumbersTests
{
    [TestFixture]
    public class GenerateRandomTests
    {
        [Test]
        [Category("RandomNumbers")]
        public void GenerateRandInt()
        {
            var random = new RandomGenerator();

            int min_value = 50;
            int max_value = 50;
            int buffer;

            for (int i = 0; i < 3000; i++)
            {
                buffer = random.Int(1, 101);

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
            var random = new RandomGenerator();

            string buffer = random.ColorString();
            Assert.IsTrue(buffer.Length == 7);

            Console.Write(buffer);
        }

        [Test]
        [Category("RandomNumbers")]
        public void GenerateRandColorTint()
        {
            var random = new RandomGenerator();

            string buffer = random.ColorString(0.7f, 0f, 0f, 0.25f);
            Assert.IsTrue(buffer.Length == 7);
        }
    }
}
