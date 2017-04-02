using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomNumbers;

namespace RandomNumbersTests
{
    [TestClass]
    public class GenerateRandomTests
    {
        [TestMethod]
        [TestCategory("RandomGenerator")]
        public void GenerateRandInt ()
        {
            var random = new RandomGenerator();

            int min_value = 50;
            int max_value = 50;
            int buffer;

            for (int i = 0; i < 3000; i++)
            {
                buffer = random.Int(1,101);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(max_value ==  101);
            Assert.IsTrue(min_value ==  1);
        }

        [TestMethod]
        [TestCategory("RandomGenerator")]
        public void GenerateRandColor()
        {
            var random = new RandomGenerator();

            string buffer = random.ColorString();
            Assert.IsTrue(buffer.Length == 7);
        }

        [TestMethod]
        [TestCategory("RandomGenerator")]
        public void GenerateRandColorTint()
        {
            var random = new RandomGenerator();

            string buffer = random.ColorString(0.7f,0f,0f,0.25f);
            Assert.IsTrue(buffer.Length == 7);
        }
    }
}
