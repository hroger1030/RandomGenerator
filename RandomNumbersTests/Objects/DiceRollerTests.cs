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
using RandomNumbers.Dice;

namespace RandomNumbersTests
{
    [TestFixture]
    public class DiceRollerTests
    {
        private readonly int NUMBER_OF_TESTS = 10000;
        private readonly DiceRoller _Roller = new DiceRoller();

        [Test]
        [Category("DiceRoller")]
        public void Roll_SingleDie_StaysWithinBounds()
        {
            int sides = 6;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                int buffer = _Roller.Roll(sides);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(1, min_value, "Roll(sides) never rolled the lowest face");
            Assert.AreEqual(sides, max_value, "Roll(sides) never rolled the highest face");
        }

        [Test]
        [Category("DiceRoller")]
        public void Roll_MultipleDice_StaysWithinBounds()
        {
            int rolls = 3;
            int sides = 6;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                int buffer = _Roller.Roll(rolls, sides);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(min_value >= rolls, $"Minimum roll of {min_value} is below the lowest possible total of {rolls}");
            Assert.IsTrue(max_value <= rolls * sides, $"Maximum roll of {max_value} exceeds the highest possible total of {rolls * sides}");
        }

        [Test]
        [Category("DiceRoller")]
        public void Roll_WithFinalBonus_ShiftsTotal()
        {
            int rolls = 2;
            int sides = 4;
            int finalBonus = 10;

            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Roller.Roll(rolls, sides, finalBonus);
                Assert.IsTrue(buffer >= rolls + finalBonus && buffer <= (rolls * sides) + finalBonus);
            }
        }

        [Test]
        [Category("DiceRoller")]
        public void Roll_WithBonusPerRollAndFinalBonus_ShiftsTotal()
        {
            int rolls = 2;
            int sides = 4;
            int bonusPerRoll = 1;
            int finalBonus = 10;

            int expectedMin = (rolls * (1 + bonusPerRoll)) + finalBonus;
            int expectedMax = (rolls * (sides + bonusPerRoll)) + finalBonus;

            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Roller.Roll(rolls, sides, bonusPerRoll, finalBonus);
                Assert.IsTrue(buffer >= expectedMin && buffer <= expectedMax, $"{buffer} outside of expected range [{expectedMin},{expectedMax}]");
            }
        }

        [Test]
        [Category("DiceRoller")]
        public void Roll_ByDiceType_StaysWithinBounds()
        {
            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Roller.Roll(eDiceType.D6);
                Assert.IsTrue(buffer >= 1 && buffer <= 6);
            }
        }

        [Test]
        [Category("DiceRoller")]
        public void RollDice_ByDiceTypeWithRolls_StaysWithinBounds()
        {
            int rolls = 3;

            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Roller.RollDice(rolls, eDiceType.D6);
                Assert.IsTrue(buffer >= rolls && buffer <= rolls * 6);
            }
        }

        [Test]
        [Category("DiceRoller")]
        public void RollDice_ByDiceTypeWithFinalBonus_ShiftsTotal()
        {
            int rolls = 2;
            int finalBonus = 5;

            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Roller.RollDice(rolls, eDiceType.D4, finalBonus);
                Assert.IsTrue(buffer >= rolls + finalBonus && buffer <= (rolls * 4) + finalBonus);
            }
        }

        [Test]
        [Category("DiceRoller")]
        [TestCase(eDiceType.D2, 1.5f)]
        [TestCase(eDiceType.D3, 2.0f)]
        [TestCase(eDiceType.D4, 2.5f)]
        [TestCase(eDiceType.D6, 3.5f)]
        [TestCase(eDiceType.D8, 4.5f)]
        [TestCase(eDiceType.D10, 5.5f)]
        [TestCase(eDiceType.D12, 6.5f)]
        [TestCase(eDiceType.D20, 10.5f)]
        [TestCase(eDiceType.D30, 15.5f)]
        [TestCase(eDiceType.D100, 50.5f)]
        [TestCase(eDiceType.D1000, 500.5f)]
        [TestCase(eDiceType.D10000, 5000.5f)]
        public void GetDiceMedian_MatchesExpectedValue(eDiceType diceType, float expectedMedian)
        {
            Assert.AreEqual(expectedMedian, _Roller.GetDiceMedian(diceType));
        }
    }
}
