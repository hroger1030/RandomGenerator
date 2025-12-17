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

using System;

namespace RandomNumbers.Dice
{
    public class DiceRoller
    {
        private static Random _Rand = new();

        public DiceRoller() { }

        public DiceRoller(int seed)
        {
            _Rand = new Random(seed);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="diceSides">Number of sides dice possesses.</param>
        public int Roll(int diceSides)
        {
            return Roll(1, diceSides, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceSides">Number of sides dice possesses.</param>
        public int Roll(int rolls, int diceSides)
        {
            return Roll(rolls, diceSides, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceSides">Number of sides dice possesses.</param>
        /// <param name="finalBonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, int diceSides, int finalBonus)
        {
            return Roll(rolls, diceSides, 0, finalBonus);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceSides">Number of sides dice possesses.</param>
        /// <param name="bonusPerRoll">Bonus that is applied to each roll.</param>
        /// <param name="finalBonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, int diceSides, int bonusPerRoll, int finalBonus)
        {
            int total = 0;

            while (rolls > 0)
            {
                total += _Rand.Next(1, (int)diceSides + 1) + bonusPerRoll;
                rolls--;
            }

            total += finalBonus;

            return total;
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="diceType">Strongly typed dice type to roll.</param>
        public int Roll(eDiceType diceType)
        {
            return Roll(1, (int)diceType, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceType">Strongly typed dice type to roll.</param>
        public int RollDice(int rolls, eDiceType diceType)
        {
            return Roll(rolls, (int)diceType, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceType">Strongly typed dice type to roll.</param>
        /// <param name="finalBonus">Bonus that is applied to final sum of rolls.</param> 
        public int RollDice(int rolls, eDiceType diceType, int finalBonus)
        {
            return Roll(rolls, (int)diceType, 0, finalBonus);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="diceType">Strongly typed dice type to roll.</param>
        /// <param name="bonusPerRoll">Bonus that is applied to each roll.</param>
        /// <param name="finalBonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, eDiceType diceType, int bonusPerRoll, int finalBonus)
        {
            return Roll(rolls, (int)diceType, bonusPerRoll, finalBonus);
        }

        public float GetDiceMedian(eDiceType diceType)
        {
            return diceType switch
            {
                eDiceType.D2 => 0.5f,
                eDiceType.D3 => 2.0f,
                eDiceType.D4 => 2.5f,
                eDiceType.D6 => 3.5f,
                eDiceType.D8 => 4.5f,
                eDiceType.D10 => 5.5f,
                eDiceType.D12 => 6.5f,
                eDiceType.D20 => 10.5f,
                eDiceType.D30 => 15.5f,
                eDiceType.D100 => 50.5f,
                eDiceType.D1000 => 500.5f,
                _ => throw new Exception("Unknown eDiceType value " + diceType.ToString()),
            };
        }
    }
}
