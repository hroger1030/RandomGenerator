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
using NUnit.Framework.Internal;
using RandomNumbers.Dice;
using System;

namespace RandomNumbersTests
{
    [TestFixture]
    public class DiceFormulaTests
    {
        [Test]
        [Category("DiceFormula")]
        public void DiceFormula_Ctor_Success()
        {
            var formula = new DiceFormula(eDiceType.D2, 1, 2, 3);

            Assert.IsTrue(formula.DiceType == eDiceType.D2);
            Assert.IsTrue(formula.Rolls == 1);
            Assert.IsTrue(formula.Bonus == 2);
            Assert.IsTrue(formula.Multiplier == 3);
        }

        [Test]
        [Category("DiceFormula")]
        [TestCase(0)]
        [TestCase(-1)]
        public void DiceFormula_Ctor_BadRolls(int rolls)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DiceFormula(eDiceType.D2, rolls, 2, 3));
        }

        [Test]
        [Category("DiceFormula")]
        [TestCase(0)]
        [TestCase(-1)]
        public void DiceFormula_Ctor_BadMultiplier(int multiplier)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DiceFormula(eDiceType.D2, 1, 2, multiplier));
        }

        [Test]
        [Category("DiceFormula")]
        [TestCase("d6", eDiceType.D6, 1, 0, 1)]
        [TestCase("D6", eDiceType.D6, 1, 0, 1)]
        [TestCase("1d6", eDiceType.D6, 1, 0, 1)]
        [TestCase("4d6", eDiceType.D6, 4, 0, 1)]
        [TestCase("d20+4", eDiceType.D20, 1, 4, 1)]
        [TestCase("3d6-1", eDiceType.D6, 3, -1, 1)]
        [TestCase("1d6-1", eDiceType.D6, 1, -1, 1)]
        [TestCase("1d6x2", eDiceType.D6, 1, 0, 2)]
        [TestCase("d6x2", eDiceType.D6, 1, 0, 2)]
        [TestCase("1d6+1x2", eDiceType.D6, 1, 1, 2)]
        public void DiceFormula_CtorStringInput_Success(string input, eDiceType expectedDiceType, int expectedRolls, int expectedBonus, int expectedMultiplier)
        {
            var formula = new DiceFormula(input);

            Assert.IsTrue(formula.DiceType == expectedDiceType);
            Assert.IsTrue(formula.Rolls == expectedRolls);
            Assert.IsTrue(formula.Bonus == expectedBonus);
            Assert.IsTrue(formula.Multiplier == expectedMultiplier);
        }

        [Test]
        [Category("DiceFormula")]
        [TestCase("1D6", eDiceType.D6, 1, 0, 1)]
        [TestCase("1D6", eDiceType.D6, 1, 0, 1)]
        [TestCase("1D6", eDiceType.D6, 1, 0, 1)]
        [TestCase("4D6", eDiceType.D6, 4, 0, 1)]
        [TestCase("1D20+4", eDiceType.D20, 1, 4, 1)]
        [TestCase("3D6-1", eDiceType.D6, 3, -1, 1)]
        [TestCase("1D6-1", eDiceType.D6, 1, -1, 1)]
        [TestCase("1D6x2", eDiceType.D6, 1, 0, 2)]
        [TestCase("1D6x2", eDiceType.D6, 1, 0, 2)]
        [TestCase("1D6+1x2", eDiceType.D6, 1, 1, 2)]
        public void DiceFormula_ToString_Success(string expectedOutput, eDiceType expectedDiceType, int expectedRolls, int expectedBonus, int expectedMultiplier)
        {
            var formula = new DiceFormula(expectedDiceType, expectedRolls, expectedBonus, expectedMultiplier);
            var buffer = formula.ToString();

            Assert.IsTrue(buffer == expectedOutput);
        }
    }
}
