using System;
using System.Text;

namespace RandomNumbers.Dice
{
    public class cDiceFormula
    {
        protected eDiceType _DiceType;
        protected int _Rolls;
        protected int _Bonus;
        protected int _Multiplier;

        public eDiceType DiceType
        {
            get { return _DiceType; }
            set { _DiceType = value; }
        }
        public int Rolls
        {
            get { return _Rolls; }
            set { _Rolls = value; }
        }
        public int Bonus
        {
            get { return _Bonus; }
            set { _Rolls = value; }
        }
        public int Multiplier
        {
            get { return _Multiplier; }
            set { _Multiplier = value; }
        }

        public cDiceFormula()
        {
            Reset();
        }

        public cDiceFormula(eDiceType dice_type, int rolls, int bonus, int multiplier)
        {
            if (rolls < 1)
                throw new ArgumentException("rolls cannot be less than 1");

            _DiceType = dice_type;
            _Rolls = rolls;
            _Bonus = bonus;
            _Multiplier = multiplier;
        }

        /// <summary>
        /// Parses strings containing dice formulas. Acceptable input formats are d6, 2d4, 3d6+2, 4d4+2x10
        /// </summary>
        /// <param name="input">string containing dice formula</param>
        public cDiceFormula(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty");

            Reset();

            input = input.Trim();
            input = input.ToLower();

            string buffer;

            for (int i = input.Length - 1, j = input.Length; i > -1; i--)
            {
                if (input[i] == 'x')
                {
                    buffer = input.Substring(i + 1);
                    int.TryParse(buffer, out _Multiplier);
                    if (_Multiplier < 1)
                        _Multiplier = 1;

                    j = i;
                }
                else if (input[i] == '+')
                {
                    buffer = input.Substring(i + 1, j - i - 1);
                    int.TryParse(buffer, out _Bonus);

                    j = i;
                }
                else if (input[i] == '-')
                {
                    buffer = input.Substring(i, j - i);
                    int.TryParse(buffer, out _Bonus);

                    j = i;
                }
                else if (input[i] == 'd')
                {
                    buffer = input.Substring(i, j - i);
                    _DiceType = (eDiceType)Enum.Parse(typeof(eDiceType), buffer, true);

                    if (i > 0)
                    {
                        buffer = input.Substring(0, i);
                        int.TryParse(buffer, out _Rolls);
                        if (_Rolls < 1)
                            _Rolls = 1;
                    }
                }
            }
        }

        public void Reset()
        {
            _DiceType = eDiceType.D6;
            _Rolls = 3;
            _Bonus = 0;
            _Multiplier = 1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_Rolls.ToString());
            sb.Append(_DiceType.ToString());

            if (_Bonus > 0)
                sb.Append("+" + _Bonus);
            else if (_Bonus < 0)
                sb.Append("+" + _Bonus);
            else
                sb.Append("+" + _Bonus);

            if (_Multiplier != 1)
                sb.Append("x" + _Multiplier.ToString());

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            cDiceFormula other = (cDiceFormula)obj;

            if (!Equals(_DiceType, other._DiceType))
                return false;

            if (!Equals(_Rolls, other._Rolls))
                return false;

            if (!Equals(_Bonus, other._Bonus))
                return false;

            if (!Equals(_Multiplier, other._Multiplier))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_DiceType.GetHashCode() * 17) ^ (_Rolls * 5023) ^ (_Bonus * 2647) ^ (_Multiplier * 641);
             }
        }
    }
}
