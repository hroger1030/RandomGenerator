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
        public const int ARTIFICIAL_FLOAT_PRECISION = 1000000000;
        public const string ASCII_ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private static Random _Random = new();

        public RandomGenerator() { }

        /// <summary>
        /// This constructor exists for testing purposes. Passing a
        /// number in here will ensure that you get the same set of
        /// reandom numbers each time it is initialized.
        /// </summary>
        public RandomGenerator(int seed)
        {
            _Random = new Random(seed);
        }

        #region basic types

        public bool Bool()
        {
            return _Random.Next(0, 1) == 1;
        }

        public byte Byte()
        {
            return (byte)Int(0, byte.MaxValue);
        }

        public byte Byte(byte max)
        {
            return (byte)Int(0, max);
        }

        public byte Byte(byte min, byte max)
        {
            return (byte)Int(min, max);
        }

        public byte[] ByteArray(int count)
        {
            if (count < 1)
                throw new ArgumentException($"Cannot generate {count} bytes, it is less than 1");

            byte[] output = new byte[count];
            _Random.NextBytes(output);
            return output;
        }

        public char Char()
        {
            return (char)Int(char.MinValue, char.MaxValue);
        }

        public char Char(char max)
        {
            return (char)Int(0, char.MaxValue);
        }

        public char Char(char min, char max)
        {
            return (char)Int(min, max);
        }

        /// <summary>
        /// Returns a random value between 0 and 1, exclusive.
        /// </summary>
        public double Double()
        {
            byte[] buffer = new byte[sizeof(double)];
            _Random.NextBytes(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        /// Returns a random value between 0 and max parameters, inclusive.
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public double Double(double max)
        {
            return _Random.NextDouble() * max;
        }

        /// <summary>
        /// Returns a random value between min and max parameters, inclusive.
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public double Double(double min, double max)
        {
            return (_Random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a random value between 0 and 1, exclusive.
        /// </summary>
        public float Float()
        {
            return (float)_Random.NextDouble();
        }

        /// <summary>
        /// Returns a random value between 0 and max parameters, exclusive.
        /// </summary>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public float Float(float max)
        {
            return (float)(_Random.NextDouble() * max);
        }

        /// <summary>
        /// Returns a random value between min and max parameters, exclusive.
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, exclusive</param>
        public float Float(float min, float max)
        {
            return (float)((_Random.NextDouble() * (max - min)) + min);
        }

        /// <summary>
        /// Returns a random value between int min and int max, inclusive
        /// </summary>
        public int Int()
        {
            return _Random.Next(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// Returns a random value between 0 and max, inclusive
        /// </summary>
        /// <param name="max">The upper bound of the range, inclusive</param>
        public int Int(int max)
        {
            return _Random.Next(0, max + 1);
        }

        /// <summary>
        /// Returns a random value between min and max parameters, inclusive.
        /// </summary>
        /// <param name="min">The lower bound of the range, inclusive</param>
        /// <param name="max">The upper bound of the range, inclusive</param>
        public int Int(int min, int max)
        {
            return _Random.Next(min, max + 1);
        }

        public long Long()
        {
            byte[] buf = new byte[8];
            _Random.NextBytes(buf);
            return (long)BitConverter.ToUInt64(buf, 0);
        }

        public long Long(long max)
        {
            return Long(0, max);
        }

        public long Long(long min, long max)
        {
            //Working with ulong so that modulo works correctly with values > long.MaxValue
            ulong uRange = (ulong)(max - min);

            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                _Random.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + min;
        }

        public short Short()
        {
            return (short)Int(short.MinValue, short.MaxValue);
        }

        public short Short(short max)
        {
            return (short)Int(0, max);
        }

        public short Short(short min, short max)
        {
            return (short)Int(min, max);
        }

        public ulong ULong()
        {
            byte[] buf = new byte[8];
            _Random.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }

        #endregion

        #region math & geometry

        /// <summary>
        /// Returns a random value between 0 and 1, inclusive
        /// </summary>
        public double UnitInterval()
        {
            return (_Random.Next(0, ARTIFICIAL_FLOAT_PRECISION + 1)) / ((double)ARTIFICIAL_FLOAT_PRECISION);
        }

        /// <summary>
        /// Returns a value between 0 and 2pi - epsilon, inclusive.
        /// </summary>
        public float Facing()
        {
            return (float)Double(0, ((2 * Math.PI - float.Epsilon)));
        }

        #endregion

        #region strings

        /// <summary>
        /// Generates a string of N length populated with unicode characters.
        /// </summary>
        public string UnicodeString(int length)
        {
            byte[] buffer = new byte[length * 2];

            for (int i = 0; i < length * 2; i += 2)
            {
                int chr = _Random.Next(0xD7FF);
                buffer[i] = (byte)(chr & 0xFF);
                buffer[i + 1] = (byte)((chr & 0xFF00) >> 8);
            }

            return Encoding.Unicode.GetString(buffer);
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
                sb.Append(wordList[_Random.Next(wordList.Length - 1)]);
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

            return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
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

            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
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

            red = Clamp(red + Float(-delta, delta));
            green = Clamp(green + Float(-delta, delta));
            blue = Clamp(blue + Float(-delta, delta));

            return string.Format("#{0:X2}{1:X2}{2:X2}", (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
        }

        public float Clamp(float value)
        {
            return Clamp(value, 0, 1);
        }

        public float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
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
            int element_number = _Random.Next(0, dictionary.Count);

            if (remove)
            {
                V value = dictionary.ElementAt(element_number).Value;
                dictionary.Remove(dictionary.ElementAt(element_number).Key);
                return value;
            }
            else
            {
                return dictionary.ElementAt(element_number).Value;
            }
        }

        /// <summary>
        /// Returns a random value selected from an enumeration.
        /// </summary>
        public T EnumValue<T>() where T : struct, IConvertible
        {
            Array buffer = Enum.GetValues(typeof(T));
            return (T)buffer.GetValue(_Random.Next(buffer.Length));
        }

        /// <summary>
        /// Takes existing class, and randomizes all atomic values
        /// </summary>
        public T Object<T>() where T : class, new()
        {
            T output = new();

            foreach (var property_info in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (!property_info.CanWrite)
                    continue;

                Type property_type = property_info.PropertyType;
                string property_name = property_info.PropertyType.FullName; // change to .Name? shorter

                // in the event that we are looking at a nullable type, we need to look at the underlying type.
                if (property_type.IsGenericType && property_type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    property_name = Nullable.GetUnderlyingType(property_type).ToString();

                if (property_type.IsEnum)
                {
                    // todo: this is a hack. Need to figure out how to get the type of the enum?
                }

                var output_value = new object();

                switch (property_name)
                {
                    case "System.Int32":
                        output_value = Int();
                        break;

                    case "System.String":
                        output_value = String(10);
                        break;

                    case "System.Double":
                        output_value = Double();
                        break;

                    case "System.Float":
                        output_value = Float();
                        break;

                    case "System.Boolean":
                        output_value = Bool();
                        break;

                    case "System.DateTime":
                        output_value = RandomDateTime();
                        break;

                    case "System.Guid":
                        output_value = Guid.NewGuid();
                        break;

                    case "System.Single":
                        output_value = Float();
                        break;

                    case "System.Decimal":
                        output_value = (decimal)Double();
                        break;

                    case "System.Byte":
                        output_value = Byte();
                        break;

                    case "System.Byte[]":
                        byte[] buffer = new byte[Int(1, 10)];
                        _Random.NextBytes(buffer);
                        output_value = buffer;
                        break;

                    case "System.Char":
                        output_value = Char();
                        break;

                    case "System.UInt32":
                        output_value = (uint)Int();
                        break;

                    case "System.Int64":
                        output_value = (long)((ulong)(Int() << 32) | (ulong)Int());
                        break;

                    case "System.UInt64":
                        output_value = (ulong)(Int() << 32) | (ulong)Int();
                        break;

                    case "System.Object":
                        output_value = new object();
                        break;

                    case "System.Int16":
                        output_value = (short)Char();
                        break;

                    case "System.UInt16":
                        output_value = (ushort)Char();
                        break;

                    default:
                        throw new Exception($"Column {property_name} has an unknown data type: {property_type}.");
                }

                property_info.SetValue(property_name, output_value, null);
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
        /// Generates a random value in the specified range that is distributed normally.
        /// </summary>
        /// <param name="lowerBound">inclusive lowest value</param>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distruibuted random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public double NormallyDistributedDouble(double lowerBound, double upperBound, int rolls)
        {
            double lower_roll_bound = lowerBound / rolls;
            double upper_roll_bound = upperBound / rolls;
            double sum = 0.0;

            while (rolls > 0)
            {
                sum += Double(lower_roll_bound, upper_roll_bound);
                rolls--;
            }

            return sum;
        }

        /// <summary>
        /// Generates a random value between 0 and the upper bound that is distributed normally.
        /// </summary>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distruibuted random factors that contribute
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
        /// <param name="rolls">the number of normally distruibuted random factors that contribute
        /// to the final value. Greater values will push the average towards the mean.</param>
        public float NormallyDistributedFloat(float lowerBound, float upperBound, int rolls)
        {
            return (float)NormallyDistributedDouble(lowerBound, upperBound, rolls);
        }

        /// <summary>
        /// Generates a random value between 0 and the upper bound that is distributed normally.
        /// </summary>
        /// <param name="upperBound">inclusive highest value</param>
        /// <param name="rolls">the number of normally distruibuted random factors that contribute
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
        /// <param name="rolls">the number of normally distruibuted random factors that contribute
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
            // turns out the intregral of a Uniform Distribution is a stretched Arctan function. Who knew?
            //p --> peak + scale * tan(pi * (p - 0.5))
            double result = (peak + scale) * Math.Tan(Math.PI * (_Random.NextDouble() - 0.5));

            return result;
        }

        /// <summary>
        /// Generates a random number between 0.0 and 1.0 that is exponentially biased 
        /// towards 0.0 using a base 10 Logarithm.
        /// </summary>
        public double ExponentiallyDistributedDouble()
        {
            // generate a random number between 1 and 10 inclusive
            // we use log10, so log10(1) = 0, log10(10) = 1
            double random_number = (_Random.NextDouble() * 9) + 1;

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
            double random_number = (_Random.NextDouble() * (logBase - 1)) + 1;

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
            int hour = _Random.Next(0, 23);
            int mins = _Random.Next(0, 59);
            int secs = _Random.Next(0, 59);

            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, hour, mins, secs);
        }

        public DateTime RandomDateTime()
        {
            long ticks = Long(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);
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
