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
using System.Text;

namespace RandomNumbers.Dice
{
    public class DiceFormula
    {
        public eDiceType DiceType { get; set; } = eDiceType.D6;
        public int Rolls { get; set; } = 1;
        public int Bonus { get; set; } = 0;
        public int Multiplier { get; set; } = 1;

        public DiceFormula() { }

        public DiceFormula(eDiceType diceType, int rolls, int bonus, int multiplier)
        {
            // bonus can be any value, no checks needed.

            if (rolls < 1)
                throw new ArgumentOutOfRangeException(nameof(rolls), "rolls cannot be less than 1");

            if (multiplier < 1)
                throw new ArgumentOutOfRangeException(nameof(bonus), "multiplier cannot be less than 1");

            DiceType = diceType;
            Rolls = rolls;
            Bonus = bonus;
            Multiplier = multiplier;
        }

        /// <summary>
        /// Parses strings containing dice formulas. Acceptable input formats are d6, 2d4, 3d6+2, 4d4+2x10
        /// </summary>
        /// <param name="input">string containing dice formula</param>
        public DiceFormula(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            Reset();

            input = input.Trim();
            input = input.ToLower();

            string buffer;

            for (int i = input.Length - 1, j = input.Length; i > -1; i--)
            {
                if (input[i] == 'x')
                {
                    buffer = input.Substring(i + 1);
                    if (int.TryParse(buffer, out int multiplier))
                    {
                        if (multiplier < 1)
                            multiplier = 1;

                        Multiplier = multiplier;
                    }

                    j = i;
                }
                else if (input[i] == '+')
                {
                    buffer = input.Substring(i + 1, j - i - 1);
                    if (int.TryParse(buffer, out int bonus))
                    {
                        Bonus = bonus;
                        j = i;
                    }
                }
                else if (input[i] == '-')
                {
                    buffer = input.Substring(i, j - i);
                    if (int.TryParse(buffer, out int bonus))
                    {
                        Bonus = bonus;
                        j = i;
                    }
                }
                else if (input[i] == 'd')
                {
                    buffer = input.Substring(i, j - i);
                    DiceType = (eDiceType)Enum.Parse(typeof(eDiceType), buffer, true);

                    if (i > 0)
                    {
                        buffer = input.Substring(0, i);
                        if (int.TryParse(buffer, out int rolls))
                        {
                            if (rolls < 1)
                                rolls = 1;

                            Rolls = rolls;
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            DiceType = eDiceType.D20;
            Rolls = 1;
            Bonus = 0;
            Multiplier = 1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Rolls);
            sb.Append(DiceType.ToString());

            if (Bonus > 0)
                sb.Append("+" + Bonus);
            else if (Bonus < 0)
                sb.Append(Bonus);

            if (Multiplier != 1)
                sb.Append("x" + Multiplier.ToString());

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            DiceFormula other = (DiceFormula)obj;

            if (!Equals(DiceType, other.DiceType))
                return false;

            if (!Equals(Rolls, other.Rolls))
                return false;

            if (!Equals(Bonus, other.Bonus))
                return false;

            if (!Equals(Multiplier, other.Multiplier))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DiceType, Rolls, Bonus, Multiplier);
        }
    }
}
