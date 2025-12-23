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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RandomNumbers
{
    public class RandomGenerator : IRandomGenerator
    {
        public const string ASCII_ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private Random _Rand = new();

        public RandomGenerator() { }

        public RandomGenerator(int seed)
        {
            _Rand = new Random(seed);
        }

        #region basic types

        public bool Bool()
        {
            return _Rand.Next(2) == 1;
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, 255).
        /// </summary>
        public byte Byte()
        {
            return (byte)_Rand.Next(byte.MinValue, byte.MaxValue + 1);
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public byte Byte(byte max)
        {
            return Byte(byte.MinValue, max);
        }

        /// <summary>
        /// Returns a random value between [min, max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public byte Byte(byte min, byte max)
        {
            return (byte)_Rand.Next(min, max + 1);
        }

        /////////////////////////////////////////////////////////

        public byte[] ByteArray(int count)
        {
            if (count < 1)
                throw new ArgumentException($"Cannot generate {count} bytes, it is less than 1");

            byte[] output = new byte[count];
            _Rand.NextBytes(output);

            return output;
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, 65535).
        /// </summary>
        public char Char()
        {
            return (char)_Rand.Next(char.MinValue, char.MaxValue + 1);
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public char Char(char max)
        {
            return (char)_Rand.Next(char.MinValue, max + 1);
        }

        /// <summary>
        /// Returns a random value between [min, max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public char Char(char min, char max)
        {
            return (char)_Rand.Next(min, max + 1);
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, 1).
        /// </summary>
        public double Double()
        {
            return _Rand.NextDouble();
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public double Double(double max)
        {
            if (double.IsNaN(max))
                throw new ArgumentOutOfRangeException("max cannot be NaN");

            if (double.IsInfinity(max))
                throw new ArgumentOutOfRangeException(nameof(max), "max cannot be infinity");

            if (max < 0)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be > 0");

            return _Rand.NextDouble() * max;
        }

        /// <summary>
        /// Returns a random value between min and max parameters.
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public double Double(double min, double max)
        {
            if (double.IsNaN(min))
                throw new ArgumentException("min cannot be NaN");

            if (double.IsNaN(max))
                throw new ArgumentOutOfRangeException("max cannot be NaN");

            if (double.IsInfinity(min))
                throw new ArgumentOutOfRangeException(nameof(max), "min cannot be infinity");

            if (double.IsInfinity(max))
                throw new ArgumentOutOfRangeException(nameof(max), "max cannot be infinity");

            if (max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be >= min.");

            return (_Rand.NextDouble() * (max - min)) + min;
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, 1).
        /// </summary>
        public float Float()
        {
            return _Rand.NextSingle();
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public float Float(float max)
        {
            if (float.IsNaN(max))
                throw new ArgumentException("max cannot be NaN");

            if (float.IsInfinity(max))
                throw new ArgumentOutOfRangeException(nameof(max), "max cannot be infinity");

            if (max < 0)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be >= 0");

            return _Rand.NextSingle() * max;
        }

        /// <summary>
        /// Returns a random value between [min,max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public float Float(float min, float max)
        {
            if (float.IsNaN(min))
                throw new ArgumentException("min cannot be NaN");

            if (float.IsNaN(max))
                throw new ArgumentException("max cannot be NaN");

            if (float.IsInfinity(min))
                throw new ArgumentOutOfRangeException(nameof(min), "min cannot be infinity");

            if (float.IsInfinity(max))
                throw new ArgumentOutOfRangeException(nameof(max), "max cannot be infinity");

            if (max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be >= min.");

            return ((_Rand.NextSingle() * (max - min)) + min);
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [min, int.MaxValue).
        /// </summary>
        public int Int()
        {
            return _Rand.Next();
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public int Int(int max)
        {
            return _Rand.Next(0, max);
        }

        /// <summary>
        /// Returns a random value between [min, max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public int Int(int min, int max)
        {
            if (max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "max must be >= min.");

            return _Rand.Next(min, max);
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, long.MaxValue).
        /// </summary>
        public long Long()
        {
            return _Rand.NextInt64();
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public long Long(long max)
        {
            return _Rand.NextInt64(max);
        }

        /// <summary>
        /// Returns a random value between [min, max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public long Long(long min, long max)
        {
            return _Rand.NextInt64(min, max);
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, short.MaxValue).
        /// </summary>
        public short Short()
        {
            return (short)_Rand.Next(short.MinValue, short.MaxValue + 1);
        }

        /// <summary>
        /// Returns a random value between [0, max).
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public short Short(short max)
        {
            return (short)_Rand.Next(short.MinValue, max + 1);
        }

        /// <summary>
        /// Returns a random value between [min, max).
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public short Short(short min, short max)
        {
            return (short)_Rand.Next(min, max + 1);
        }

        /////////////////////////////////////////////////////////

        /// <summary>
        /// Returns a random value between [0, ulong.MaxValue).
        /// </summary>
        public ulong ULong()
        {
            Span<byte> buffer = stackalloc byte[8];
            _Rand.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        #endregion

        #region math & geometry

        /// <summary>
        /// Returns a random float value between [0,1].
        /// </summary>
        public float UnitFloat()
        {
            Span<byte> buffer = stackalloc byte[4];
            _Rand.NextBytes(buffer);
            var randVal = BitConverter.ToUInt32(buffer);
            return randVal / (float)long.MaxValue;
        }

        /// <summary>
        /// Returns a random double value between [0,1].
        /// </summary>
        public double UnitDouble()
        {
            Span<byte> buffer = stackalloc byte[8];
            _Rand.NextBytes(buffer);
            var randVal = BitConverter.ToUInt64(buffer);
            return randVal / (double)long.MaxValue;
        }

        /// <summary>
        /// Returns a value between [0,2pi).
        /// </summary>
        public float Facing()
        {
            return (float)(_Rand.NextSingle() * Math.Tau);
        }

        #endregion

        #region strings

        /// <summary>
        /// Generates a string of N length populated with random unicode characters.
        /// </summary>
        public string UnicodeString(int length)
        {
            char[] buffer = new char[length * 2]; // Worst-case: all supplementary characters take 2 chars each
            int charPos = 0;     // position in char array
            int charsAdded = 0;  // counts actual number of characters

            for (int i = 0; i < length; i++)
            {
                int codePoint;

                // Generate code points in two ranges to skip surrogates efficiently
                // 0x0000–0xD7FF and 0xE000–0x10FFFF
                if (_Rand.NextDouble() < 0.844) // roughly proportional sizes: 0xD800 / 0x110000
                {
                    codePoint = _Rand.Next(0xD800);
                    buffer[charPos++] = (char)codePoint;
                }
                else
                {
                    codePoint = 0xE000 + _Rand.Next(0x10FFFF - 0xE000 + 1);

                    // Supplementary character → surrogate pair
                    codePoint -= 0x10000;
                    buffer[charPos++] = (char)((codePoint >> 10) + 0xD800);
                    buffer[charPos++] = (char)((codePoint & 0x3FF) + 0xDC00);
                }

                charsAdded++;
            }

            return new string(buffer, 0, charPos);
        }

        public string String(int length)
        {
            return String(length, ASCII_ALPHABET);
        }

        public string String(int minLength, int maxLength)
        {
            int length = Int(minLength, maxLength);
            return String(length, ASCII_ALPHABET);
        }

        public string String(int length, string characterSet)
        {
            var output = new char[length];

            for (int i = 0; i < length; i++)
                output[i] = characterSet[Int(characterSet.Length - 1)];

            return new string(output);
        }

        public string Sentence(int sentenceLength)
        {
            if (sentenceLength < 1)
                throw new ArgumentException("Max string length cannot be less than 2");

            var output = new StringBuilder(sentenceLength);
            int remainingCharacters = sentenceLength;
            string buffer;

            while (remainingCharacters > 10)
            {
                buffer = String(1, 7);
                output.Append(buffer);
                output.Append(' ');

                remainingCharacters -= (buffer.Length + 1);
            }

            output.Append(String(1, remainingCharacters - 1));
            output.Append('.');

            return output.ToString();
        }

        public string TextContent(int wordCount, string[] wordList)
        {
            if (wordCount < 1)
                throw new ArgumentException(nameof(wordCount), "Max string length cannot be less than 1");

            if (wordList == null || wordList.Length == 0)
                throw new ArgumentNullException(nameof(wordList));

            var sb = new StringBuilder();

            for (int i = 0; i <= wordCount; i++)
            {
                sb.Append(' ');
                sb.Append(wordList[_Rand.Next(wordList.Length - 1)]);
            }

            sb.Append('.');
            return sb.ToString();
        }

        /// <summary>
        /// Generates a completely random RGB color string (ex: "#ff7700") 
        /// </summary>
        public string RGBColorString()
        {
            byte r = Byte();
            byte g = Byte();
            byte b = Byte();

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        /// <summary>
        /// Generates a completely random RGBA color string (ex: "#ff770077") 
        /// </summary>
        public string RGBAColorString()
        {
            byte r = Byte();
            byte g = Byte();
            byte b = Byte();
            byte a = Byte();

            return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
        }

        /// <summary>
        /// Generates a completely random RGB color string (ex: "#ff7700") that
        /// is centered around an input color, varying up to half the input variance.
        /// </summary>
        public string ColorString(float red, float green, float blue, float variance)
        {
            if (red < 0 || red > 1f || green < 0 || green > 1f || blue < 0 || blue > 1f)
                throw new ArgumentException("Input color values must be between 0 and 1, inclusive");

            if (variance < 0 || variance > 1f)
                throw new ArgumentException("Variance must be between 0 and 1, inclusive");

            float delta = variance / 2;

            red = UnitRangeClamp(red + Float(-delta, delta));
            green = UnitRangeClamp(green + Float(-delta, delta));
            blue = UnitRangeClamp(blue + Float(-delta, delta));

            var bRed = (byte)(red * 255);
            var bGreen = (byte)(green * 255);
            var bBlue = (byte)(blue * 255);

            return $"#{bRed:X2}{bGreen:X2}{bBlue:X2}";
        }

        /// <summary>
        /// Clamps a value between [0,1]
        /// </summary>
        public float UnitRangeClamp(float value)
        {
            return (value < 0) ? 0 : (value > 1) ? 1 : value;
        }

        #endregion

        #region RandomObjects

        /// <summary>
        /// Returns a random element from a collection.
        /// </summary>
        public T CollectionValue<T>(IList<T> collection, bool remove)
        {
            if (collection == null || collection.Count == 0)
                throw new ArgumentException("Collection cannot be null or empty");

            int index = Int(collection.Count - 1);
            T item = collection[index];

            if (remove)
                collection.RemoveAt(index);

            return item;
        }

        public V DictionaryValue<K, V>(IDictionary<K, V> dictionary, bool remove)
        {
            int elementNumber = _Rand.Next(0, dictionary.Count);

            if (remove)
            {
                V value = dictionary.ElementAt(elementNumber).Value;
                dictionary.Remove(dictionary.ElementAt(elementNumber).Key);
                return value;
            }
            else
            {
                return dictionary.ElementAt(elementNumber).Value;
            }
        }

        /// <summary>
        /// Returns a random value selected from an enumeration.
        /// </summary>
        public T EnumValue<T>() where T : struct, IConvertible
        {
            Array buffer = Enum.GetValues(typeof(T));
            return (T)buffer.GetValue(_Rand.Next(buffer.Length));
        }

        /// <summary>
        /// Takes existing class, and randomizes all atomic values
        /// </summary>
        public T Object<T>() where T : class, new()
        {
            T output = new();

            foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (!propInfo.CanWrite)
                    continue;

                Type propType = propInfo.PropertyType;
                string propName = propInfo.PropertyType.FullName; // change to .Name? shorter

                // in the event that we are looking at a nullable type, we need to look at the underlying type.
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propName = Nullable.GetUnderlyingType(propType).ToString();

                if (propType.IsEnum)
                {
                    // todo: this is a hack. Need to figure out how to get the type of the enum?
                }

                object outputValue;

                switch (propName)
                {
                    case "System.Int32":
                        outputValue = Int();
                        break;

                    case "System.String":
                        outputValue = String(10);
                        break;

                    case "System.Double":
                        outputValue = Double();
                        break;

                    case "System.Float":
                        outputValue = Float();
                        break;

                    case "System.Boolean":
                        outputValue = Bool();
                        break;

                    case "System.DateTime":
                        outputValue = RandomDateTime();
                        break;

                    case "System.Guid":
                        outputValue = Guid.NewGuid();
                        break;

                    case "System.Single":
                        outputValue = Float();
                        break;

                    case "System.Decimal":
                        outputValue = (decimal)Double();
                        break;

                    case "System.Byte":
                        outputValue = Byte();
                        break;

                    case "System.Byte[]":
                        outputValue = ByteArray(Int(1, 10));
                        break;

                    case "System.Char":
                        outputValue = Char();
                        break;

                    case "System.UInt32":
                        outputValue = (uint)Int();
                        break;

                    case "System.Int64":
                        outputValue = Long();
                        break;

                    case "System.UInt64":
                        outputValue = ULong();
                        break;

                    case "System.Object":
                        outputValue = new object();
                        break;

                    case "System.Int16":
                        outputValue = Short();
                        break;

                    case "System.UInt16":
                        outputValue = (ushort)Short();
                        break;

                    default:
                        throw new Exception($"Column {propName} has an unknown data type: {propType}.");
                }

                propInfo.SetValue(propName, outputValue, null);
            }

            return output;
        }

        #endregion

        #region Nonuniform distribution

        /// <summary>
        /// Generates a random value between 0 and the upper bound that is distributed normally.
        /// </summary>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public double NormallyDistributedDouble(double upperBound, int rolls)
        {
            return NormallyDistributedDouble(0, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random value in the specified range that is distributed normally, 
        /// using an Irwin-Hall approximation.
        /// </summary>
        /// <param name="lowerBound">inclusive lowest value</param>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public double NormallyDistributedDouble(double lowerBound, double upperBound, int rolls)
        {
            if (rolls < 1)
                throw new ArgumentOutOfRangeException(nameof(rolls));

            double lowerRollBound = lowerBound / rolls;
            double upperRollBound = upperBound / rolls;
            double sum = 0.0;

            while (rolls > 0)
            {
                sum += this.Double(lowerRollBound, upperRollBound);
                rolls--;
            }

            return sum;
        }

        /// <summary>
        /// Generates a random value between 0 and the upper bound that is distributed normally.
        /// </summary>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public float NormallyDistributedFloat(float upperBound, int rolls)
        {
            return NormallyDistributedFloat(0f, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random value in the specified range that is distributed normally.
        /// </summary>
        /// <param name="lowerBound">inclusive lowest value</param>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public float NormallyDistributedFloat(float lowerBound, float upperBound, int rolls)
        {
            return (float)NormallyDistributedDouble(lowerBound, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random value between 0 and the upper bound that is distributed normally.
        /// </summary>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public int NormallyDistributedInt(int upperBound, int rolls)
        {
            return NormallyDistributedInt(0, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random value in the specified range that is distributed normally.
        /// </summary>
        /// <param name="lowerBound">inclusive lowest value</param>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distributed random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public int NormallyDistributedInt(int lowerBound, int upperBound, int rolls)
        {
            return (int)NormallyDistributedDouble(lowerBound, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random number in a normal distribution between 0.0 and 1, inclusive.
        /// </summary>
        public double UniformDistributedDouble(double peak, double scale)
        {
            // based off of article:
            // http://blogs.msdn.com/b/ericlippert/archive/2012/02/21/generating-random-non-uniform-data-in-c.aspx
            // turns out the integral of a Uniform Distribution is a stretched Arctan function. Who knew?
            //p --> peak + scale * tan(pi * (p - 0.5))
            double result = (peak + scale) * Math.Tan(Math.PI * (_Rand.NextDouble() - 0.5));

            return result;
        }

        /// <summary>
        /// Generates a true normally distributed (Gaussian) random value using the Box–Muller 
        ///  transform, centered on the specified mean with the given standard deviation.
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="standardDeviation"></param>
        /// <returns></returns>
        public double GaussianNormalDistribution(double mean, double standardDeviation)
        {
            double u1 = 1.0 - _Rand.NextDouble();
            double u2 = 1.0 - _Rand.NextDouble();

            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + standardDeviation * randStdNormal;
        }

        /// <summary>
        /// Generates a random number between 0.0 and 1.0 that is exponentially biased 
        /// towards 0.0 using a base 10 Logarithm.
        /// </summary>
        public double ExponentiallyDistributedDouble()
        {
            // generate a random number between 1 and 10 inclusive
            // we use log10, so log10(1) = 0, log10(10) = 1
            double random_number = (_Rand.NextDouble() * 9) + 1;

            // get log of random value.
            double output = Math.Log10(random_number);

            // return inverted value.
            return 1.0 - output;
        }

        /// <summary>
        /// Generates a random number between 0.0 and 1.0 that is exponentially biased 
        /// towards 0.0 using a base X Logarithm.
        /// </summary>
        /// <param name="logBase">base of log to use for computation. Value must be
        /// greater than 0. The larger the base, the more probably values close to
        /// 0 will be generated.</param>
        public double ExponentiallyDistributedDouble(double logBase)
        {
            if (logBase < double.Epsilon)
                throw new ArgumentException("Log base must be greater than double.Epsilon");

            // generate a random number between 1 and X inclusive
            // we use logX, so logX(1) = 0, logX(X) = 1
            double random_number = (_Rand.NextDouble() * (logBase - 1)) + 1;

            // get log of random value using specified base.
            double output = Math.Log(random_number, logBase);

            // return inverted value.
            return 1.0 - output;
        }

        /// <summary>
        /// Generates a random number between 0.0 and 1.0 that is exponentially biased 
        /// towards 0.0 using a base X Logarithm.
        /// </summary>
        /// <param name="logBase">base of log to use for computation. Value must be
        /// greater than 0. The larger the base, the more probably values close to
        /// 0 will be generated.</param>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, inclusive</param> 
        public double ExponentiallyDistributedDouble(double logBase, double min, double max)
        {
            if (min > max)
                throw new ArgumentException("Minimum value is greater than maximum value");

            return (ExponentiallyDistributedDouble() * (max - min)) + min;
        }

        #endregion

        #region Time

        public DateTime RandomTime()
        {
            int hour = _Rand.Next(0, 23);
            int mins = _Rand.Next(0, 59);
            int secs = _Rand.Next(0, 59);

            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, hour, mins, secs);
        }

        public DateTime RandomDateTime()
        {
            long ticks = _Rand.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);
            return new DateTime(ticks, DateTimeKind.Utc);
        }

        public DateTime RandomDateTime(DateTime min, DateTime max)
        {
            long ticks;

            if (min < max)
                ticks = Long(min.Ticks, max.Ticks);
            else
                ticks = Long(max.Ticks, min.Ticks);

            return new DateTime(ticks, DateTimeKind.Local);
        }

        #endregion
    }
}
