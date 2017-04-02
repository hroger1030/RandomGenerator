using System;

namespace RandomNumbers.Dice
{
    public class Die
    {
        private static Random _Random = new Random();

        public Die() { }

        public Die(int seed)
        {
            _Random = new Random(seed);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="dice_sides">Number of sides dice posesses.</param>
        public int Roll(int dice_sides)
        {
            return Roll(1, dice_sides, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_sides">Number of sides dice posesses.</param>
        public int Roll(int rolls, int dice_sides)
        {
            return Roll(rolls, dice_sides, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_sides">Number of sides dice posesses.</param>
        /// <param name="final_bonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, int dice_sides, int final_bonus)
        {
            return Roll(rolls, dice_sides, 0, final_bonus);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_sides">Number of sides dice posesses.</param>
        /// <param name="bonus_per_roll">Bonus that is applied to each roll.</param>
        /// <param name="final_bonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, int dice_sides, int bonus_per_roll, int final_bonus)
        {
            int total = 0;

            while (rolls > 0)
            {
                total += _Random.Next(1, (int)dice_sides + 1) + bonus_per_roll;
                rolls--;
            }

            total += final_bonus;

            return total;
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="dice_type">Strongly typed dice type to roll.</param>
        public int Roll(eDiceType dice_type)
        {
            return Roll(1, (int)dice_type, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_type">Strongly typed dice type to roll.</param>
        public int RollDice(int rolls, eDiceType dice_type)
        {
            return Roll(rolls, (int)dice_type, 0, 0);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_type">Strongly typed dice type to roll.</param>
        /// <param name="final_bonus">Bonus that is applied to final sum of rolls.</param> 
        public int RollDice(int rolls, eDiceType dice_type, int final_bonus)
        {
            return Roll(rolls, (int)dice_type, 0, final_bonus);
        }

        /// <summary>
        /// Simulates rolling a N sided dice.
        /// </summary>
        /// <param name="rolls">Number of times to roll dice.</param>
        /// <param name="dice_type">Strongly typed dice type to roll.</param>
        /// <param name="bonus_per_roll">Bonus that is applied to each roll.</param>
        /// <param name="final_bonus">Bonus that is applied to final sum of rolls.</param> 
        public int Roll(int rolls, eDiceType dice_type, int bonus_per_roll, int final_bonus)
        {
            return Roll(rolls, (int)dice_type, bonus_per_roll, final_bonus);
        }

        public float GetDiceMedian(eDiceType dice_type)
        {
            switch (dice_type)
            {
                case eDiceType.D2: return 0.5f;
                case eDiceType.D3: return 2.0f;
                case eDiceType.D4: return 2.5f;
                case eDiceType.D6: return 3.5f;
                case eDiceType.D8: return 4.5f;
                case eDiceType.D10: return 5.5f;
                case eDiceType.D12: return 6.5f;
                case eDiceType.D20: return 10.5f;
                case eDiceType.D30: return 15.5f;
                case eDiceType.D100: return 50.5f;
                case eDiceType.D1000: return 500.5f;

                default:
                    throw new Exception("Unknown eDiceType value " + dice_type.ToString());
            }
        }
    }
}
