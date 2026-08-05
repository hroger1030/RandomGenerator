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
using RandomNumbers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomNumbersTests
{
    // Sample POCO used to exercise RandomGenerator.Object<T>().
    public class SamplePoco
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
        public double DoubleValue { get; set; }
        public bool BoolValue { get; set; }
    }

    public class UnsupportedPropertyPoco
    {
        public TimeSpan SpanValue { get; set; }
    }

    public enum SampleEnum
    {
        First,
        Second,
        Third
    }

    [TestFixture]
    public class GenerateRandomTests
    {
        private readonly int NUMBER_OF_TESTS = 10000;
        private readonly RandomGenerator _Rand = new RandomGenerator();

        #region basic types

        [Test]
        [Category("Bool")]
        public void Bool_BothValuesObserved()
        {
            bool sawTrue = false;
            bool sawFalse = false;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                if (_Rand.Bool())
                    sawTrue = true;
                else
                    sawFalse = true;
            }

            Assert.IsTrue(sawTrue, "Bool() never returned true");
            Assert.IsTrue(sawFalse, "Bool() never returned false");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange()
        {
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte();

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.AreEqual(byte.MaxValue, max_value, "Byte() never reached its upper bound");
            Assert.AreEqual(byte.MinValue, min_value, "Byte() never reached its lower bound");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange_Overload1()
        {
            byte ceiling = 64;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte(ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Byte(max) never reached its upper bound");
            Assert.AreEqual(byte.MinValue, min_value, "Byte(max) never reached its lower bound");
        }

        [Test]
        [Category("Byte")]
        public void Byte_TestRange_Overload2()
        {
            byte ceiling = 64;
            byte floor = 4;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            byte buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Byte(floor, ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Byte(min, max) never reached its upper bound");
            Assert.AreEqual(floor, min_value, "Byte(min, max) never reached its lower bound");
        }

        [Test]
        [Category("ByteArray")]
        public void ByteArray_TestLength()
        {
            int count = 11;
            var output = _Rand.ByteArray(count);

            Assert.IsTrue(output.Length == count, $"Length of array is {output.Length}, not {count}");
        }

        [Test]
        [Category("ByteArray")]
        [TestCase(0)]
        [TestCase(-1)]
        public void ByteArray_ThrowsForCountLessThanOne(int count)
        {
            Assert.Throws<ArgumentException>(() => _Rand.ByteArray(count));
        }

        [Test]
        [Category("Char")]
        public void Char_TestRange_Overload1()
        {
            char ceiling = (char)64;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            char buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Char(ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Char(max) never reached its upper bound");
            Assert.AreEqual(char.MinValue, min_value, "Char(max) never reached its lower bound");
        }

        [Test]
        [Category("Char")]
        public void Char_TestRange_Overload2()
        {
            char ceiling = (char)64;
            char floor = (char)4;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            char buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Char(floor, ceiling);

                if (buffer > max_value)
                    max_value = buffer;

                if (buffer < min_value)
                    min_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Char(min, max) never reached its upper bound");
            Assert.AreEqual(floor, min_value, "Char(min, max) never reached its lower bound");
        }

        [Test]
        [Category("Double")]
        public void Double_TestRange()
        {
            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.Double();
                Assert.IsTrue(buffer >= 0d && buffer < 1d, $"{buffer} outside of expected range [0,1)");
            }
        }

        [Test]
        [Category("Double")]
        public void Double_TestRange_Overload1()
        {
            double ceiling = 5.5d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.Double(ceiling);
                Assert.IsTrue(buffer >= 0d && buffer < ceiling, $"{buffer} outside of expected range [0,{ceiling})");
            }
        }

        [Test]
        [Category("Double")]
        public void Double_TestRange_Overload2()
        {
            double floor = -3d;
            double ceiling = 5.5d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.Double(floor, ceiling);
                Assert.IsTrue(buffer >= floor && buffer < ceiling, $"{buffer} outside of expected range [{floor},{ceiling})");
            }
        }

        [Test]
        [Category("Double")]
        public void Double_SingleArg_ThrowsForInvalidInput()
        {
            Assert.Catch<ArgumentException>(() => _Rand.Double(double.NaN));
            Assert.Catch<ArgumentException>(() => _Rand.Double(double.PositiveInfinity));
            Assert.Catch<ArgumentException>(() => _Rand.Double(-1d));
        }

        [Test]
        [Category("Double")]
        public void Double_TwoArg_ThrowsForInvalidInput()
        {
            Assert.Catch<ArgumentException>(() => _Rand.Double(double.NaN, 1d));
            Assert.Catch<ArgumentException>(() => _Rand.Double(0d, double.NaN));
            Assert.Catch<ArgumentException>(() => _Rand.Double(double.NegativeInfinity, 1d));
            Assert.Catch<ArgumentException>(() => _Rand.Double(0d, double.PositiveInfinity));
            Assert.Catch<ArgumentException>(() => _Rand.Double(5d, 1d));
        }

        [Test]
        [Category("Float")]
        public void Float_TestRange()
        {
            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                float buffer = _Rand.Float();
                Assert.IsTrue(buffer >= 0f && buffer < 1f, $"{buffer} outside of expected range [0,1)");
            }
        }

        [Test]
        [Category("Float")]
        public void Float_TestRange_Overload1()
        {
            float ceiling = 5.5f;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                float buffer = _Rand.Float(ceiling);
                Assert.IsTrue(buffer >= 0f && buffer < ceiling, $"{buffer} outside of expected range [0,{ceiling})");
            }
        }

        [Test]
        [Category("Float")]
        public void Float_TestRange_Overload2()
        {
            float floor = -3f;
            float ceiling = 5.5f;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                float buffer = _Rand.Float(floor, ceiling);
                Assert.IsTrue(buffer >= floor && buffer < ceiling, $"{buffer} outside of expected range [{floor},{ceiling})");
            }
        }

        [Test]
        [Category("Float")]
        public void Float_SingleArg_ThrowsForInvalidInput()
        {
            Assert.Catch<ArgumentException>(() => _Rand.Float(float.NaN));
            Assert.Catch<ArgumentException>(() => _Rand.Float(float.PositiveInfinity));
            Assert.Catch<ArgumentException>(() => _Rand.Float(-1f));
        }

        [Test]
        [Category("Float")]
        public void Float_TwoArg_ThrowsForInvalidInput()
        {
            Assert.Catch<ArgumentException>(() => _Rand.Float(float.NaN, 1f));
            Assert.Catch<ArgumentException>(() => _Rand.Float(0f, float.NaN));
            Assert.Catch<ArgumentException>(() => _Rand.Float(float.NegativeInfinity, 1f));
            Assert.Catch<ArgumentException>(() => _Rand.Float(0f, float.PositiveInfinity));
            Assert.Catch<ArgumentException>(() => _Rand.Float(5f, 1f));
        }

        [Test]
        [Category("Int")]
        public void Int_TestRange_Overload1()
        {
            int ceiling = 64;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            int buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Int(ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling - 1, max_value, "Int(max) never reached its upper bound");
            Assert.AreEqual(0, min_value, "Int(max) never reached its lower bound");
        }

        [Test]
        [Category("Int")]
        public void Int_TestRange_Overload2()
        {
            int ceiling = 64;
            int floor = 23;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;
            int buffer;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                buffer = _Rand.Int(floor, ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling - 1, max_value, "Int(min, max) never reached its upper bound");
            Assert.AreEqual(floor, min_value, "Int(min, max) never reached its lower bound");
        }

        [Test]
        [Category("Int")]
        public void Int_MinMax_ThrowsWhenMaxLessThanMin()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _Rand.Int(10, 5));
        }

        [Test]
        [Category("Long")]
        public void Long_TestRange_Overload1()
        {
            long ceiling = 64;
            long min_value = long.MaxValue;
            long max_value = long.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                long buffer = _Rand.Long(ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling - 1, max_value, "Long(max) never reached its upper bound");
            Assert.AreEqual(0, min_value, "Long(max) never reached its lower bound");
        }

        [Test]
        [Category("Long")]
        public void Long_TestRange_Overload2()
        {
            long ceiling = 64;
            long floor = 23;
            long min_value = long.MaxValue;
            long max_value = long.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                long buffer = _Rand.Long(floor, ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling - 1, max_value, "Long(min, max) never reached its upper bound");
            Assert.AreEqual(floor, min_value, "Long(min, max) never reached its lower bound");
        }

        [Test]
        [Category("Long")]
        public void Long_NoArg_NeverNegative()
        {
            for (int i = 0; i < NUMBER_OF_TESTS; i++)
                Assert.IsTrue(_Rand.Long() >= 0, "Long() returned a negative value");
        }

        [Test]
        [Category("Short")]
        public void Short_NoArg_ObservesBothSigns()
        {
            short min_value = short.MaxValue;
            short max_value = short.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                short buffer = _Rand.Short();

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.IsTrue(min_value < 0, "Short() never returned a negative value");
            Assert.IsTrue(max_value > 0, "Short() never returned a positive value");
        }

        [Test]
        [Category("Short")]
        public void Short_TestRange_Overload1()
        {
            short ceiling = 64;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                short buffer = _Rand.Short(ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Short(max) never reached its upper bound");
            Assert.AreEqual(0, min_value, "Short(max) went below its documented lower bound of 0");
        }

        [Test]
        [Category("Short")]
        public void Short_TestRange_Overload2()
        {
            short ceiling = 64;
            short floor = 4;
            int min_value = int.MaxValue;
            int max_value = int.MinValue;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                short buffer = _Rand.Short(floor, ceiling);

                if (buffer < min_value)
                    min_value = buffer;

                if (buffer > max_value)
                    max_value = buffer;
            }

            Assert.AreEqual(ceiling, max_value, "Short(min, max) never reached its upper bound");
            Assert.AreEqual(floor, min_value, "Short(min, max) never reached its lower bound");
        }

        [Test]
        [Category("ULong")]
        public void ULong_ProducesVariedValues()
        {
            var seen = new HashSet<ulong>();

            for (int i = 0; i < 20; i++)
                seen.Add(_Rand.ULong());

            Assert.IsTrue(seen.Count > 1, "ULong() returned the same value every time");
        }

        #endregion

        #region math & geometry

        [Test]
        [Category("UnitFloat")]
        public void UnitFloat_TestRangeAndDistribution()
        {
            double sum = 0d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                float buffer = _Rand.UnitFloat();
                Assert.IsTrue(buffer >= 0f && buffer <= 1f, $"{buffer} outside of expected range [0,1]");
                sum += buffer;
            }

            double average = sum / NUMBER_OF_TESTS;
            Assert.IsTrue(average > 0.4d && average < 0.6d, $"Average of {average} suggests UnitFloat() is not uniformly distributed across [0,1]");
        }

        [Test]
        [Category("UnitDouble")]
        public void UnitDouble_TestRangeAndDistribution()
        {
            double sum = 0d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.UnitDouble();
                Assert.IsTrue(buffer >= 0d && buffer <= 1d, $"{buffer} outside of expected range [0,1]");
                sum += buffer;
            }

            double average = sum / NUMBER_OF_TESTS;
            Assert.IsTrue(average > 0.4d && average < 0.6d, $"Average of {average} suggests UnitDouble() is not uniformly distributed across [0,1]");
        }

        [Test]
        [Category("Facing")]
        public void Facing_TestRange()
        {
            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                float buffer = _Rand.Facing();
                Assert.IsTrue(buffer >= 0f && buffer < Math.Tau, $"{buffer} outside of expected range [0,2π)");
            }
        }

        [Test]
        [Category("UnitRangeClamp")]
        public void UnitRangeClamp_ClampsToRange()
        {
            Assert.AreEqual(0f, _Rand.UnitRangeClamp(-0.5f));
            Assert.AreEqual(1f, _Rand.UnitRangeClamp(1.5f));
            Assert.AreEqual(0.5f, _Rand.UnitRangeClamp(0.5f));
        }

        #endregion

        #region strings

        [Test]
        [Category("String")]
        public void String_TestLength()
        {
            string buffer = _Rand.String(20);

            Assert.AreEqual(20, buffer.Length);
            Assert.IsTrue(buffer.All(c => RandomGenerator.ASCII_ALPHABET.Contains(c)));
        }

        [Test]
        [Category("String")]
        public void String_TestLengthRange()
        {
            for (int i = 0; i < 200; i++)
            {
                string buffer = _Rand.String(5, 15);
                Assert.IsTrue(buffer.Length >= 5 && buffer.Length < 15, $"Length {buffer.Length} outside of expected range [5,15)");
            }
        }

        [Test]
        [Category("String")]
        public void String_TestCustomCharacterSet()
        {
            string characterSet = "AB";
            string buffer = _Rand.String(30, characterSet);

            Assert.AreEqual(30, buffer.Length);
            Assert.IsTrue(buffer.All(c => c == 'A' || c == 'B'));
        }

        [Test]
        [Category("UnicodeString")]
        public void UnicodeString_ProducesNonEmptyString()
        {
            int length = 50;
            string buffer = _Rand.UnicodeString(length);

            Assert.IsTrue(buffer.Length >= length && buffer.Length <= length * 2, $"Length {buffer.Length} outside of expected range [{length},{length * 2}]");
        }

        [Test]
        [Category("Sentence")]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(100)]
        public void Sentence_EndsWithPeriod(int sentenceLength)
        {
            string buffer = _Rand.Sentence(sentenceLength);

            Assert.IsTrue(buffer.EndsWith("."), $"'{buffer}' does not end with a period");
        }

        [Test]
        [Category("TextContent")]
        public void TextContent_UsesOnlySuppliedWords()
        {
            string[] wordList = new string[] { "alpha", "bravo", "charlie" };
            string buffer = _Rand.TextContent(5, wordList);

            Assert.IsTrue(buffer.EndsWith("."), $"'{buffer}' does not end with a period");

            var words = buffer.TrimEnd('.').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Assert.IsTrue(words.Length > 0, "TextContent produced no words");
            Assert.IsTrue(words.All(w => wordList.Contains(w)), "TextContent produced a word not present in the supplied word list");
        }

        [Test]
        [Category("TextContent")]
        [TestCase(0)]
        [TestCase(-1)]
        public void TextContent_ThrowsForInvalidWordCount(int wordCount)
        {
            Assert.Catch<ArgumentException>(() => _Rand.TextContent(wordCount, new string[] { "a" }));
        }

        [Test]
        [Category("TextContent")]
        public void TextContent_ThrowsForNullOrEmptyWordList()
        {
            Assert.Throws<ArgumentNullException>(() => _Rand.TextContent(3, null));
            Assert.Throws<ArgumentNullException>(() => _Rand.TextContent(3, Array.Empty<string>()));
        }

        [Test]
        [Category("Color")]
        public void GenerateRand24BitColor()
        {
            string buffer = _Rand.RGBColorString();

            Assert.IsTrue(buffer.Length == 7);
            Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(buffer, "^#[0-9A-F]{6}$"));
        }

        [Test]
        [Category("Color")]
        public void GenerateRand32BitColor()
        {
            string buffer = _Rand.RGBAColorString();

            Assert.IsTrue(buffer.Length == 9);
            Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(buffer, "^#[0-9A-F]{8}$"));
        }

        [Test]
        [Category("Color")]
        public void GenerateRandColorTint()
        {
            string buffer = _Rand.ColorString(0.7f, 0f, 0f, 0.25f);

            Assert.IsTrue(buffer.Length == 7);
            Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(buffer, "^#[0-9A-F]{6}$"));
        }

        [Test]
        [Category("Color")]
        public void ColorString_ThrowsForOutOfRangeComponents()
        {
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(-0.1f, 0f, 0f, 0.25f));
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(1.1f, 0f, 0f, 0.25f));
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(0f, -0.1f, 0f, 0.25f));
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(0f, 0f, 1.1f, 0.25f));
        }

        [Test]
        [Category("Color")]
        public void ColorString_ThrowsForOutOfRangeVariance()
        {
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(0.5f, 0.5f, 0.5f, -0.1f));
            Assert.Throws<ArgumentException>(() => _Rand.ColorString(0.5f, 0.5f, 0.5f, 1.1f));
        }

        #endregion

        #region RandomObjects

        [Test]
        [Category("Collections")]
        public void CollectionValue_ReturnsElementWithoutRemoving()
        {
            var list = new List<string> { "a", "b", "c" };
            string result = _Rand.CollectionValue(list, false);

            Assert.IsTrue(list.Contains(result));
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        [Category("Collections")]
        public void CollectionValue_RemovesSelectedElement()
        {
            var list = new List<string> { "a", "b", "c" };
            string result = _Rand.CollectionValue(list, true);

            Assert.IsFalse(list.Contains(result));
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        [Category("Collections")]
        public void CollectionValue_ThrowsForNullOrEmptyCollection()
        {
            Assert.Throws<ArgumentException>(() => _Rand.CollectionValue<string>(null, false));
            Assert.Throws<ArgumentException>(() => _Rand.CollectionValue(new List<string>(), false));
        }

        [Test]
        [Category("Collections")]
        public void DictionaryValue_ReturnsValueWithoutRemoving()
        {
            var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            int result = _Rand.DictionaryValue(dict, false);

            Assert.IsTrue(dict.Values.Contains(result));
            Assert.AreEqual(3, dict.Count);
        }

        [Test]
        [Category("Collections")]
        public void DictionaryValue_RemovesSelectedEntry()
        {
            var dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 }, { "c", 3 } };
            int result = _Rand.DictionaryValue(dict, true);

            Assert.AreEqual(2, dict.Count);
            Assert.IsFalse(dict.Values.Contains(result) && dict.Values.Count(v => v == result) > 1);
        }

        [Test]
        [Category("Enum")]
        public void EnumValue_ReturnsDefinedValue()
        {
            var definedValues = Enum.GetValues(typeof(SampleEnum)).Cast<SampleEnum>().ToList();

            for (int i = 0; i < 50; i++)
            {
                var value = _Rand.EnumValue<SampleEnum>();
                Assert.IsTrue(definedValues.Contains(value));
            }
        }

        [Test]
        [Category("Object")]
        public void Object_PopulatesWritableProperties()
        {
            var result = _Rand.Object<SamplePoco>();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.StringValue);
            Assert.AreEqual(10, result.StringValue.Length);
        }

        [Test]
        [Category("Object")]
        public void Object_ThrowsForUnsupportedPropertyType()
        {
            Assert.Throws<Exception>(() => _Rand.Object<UnsupportedPropertyPoco>());
        }

        #endregion

        #region Nonuniform distribution

        [Test]
        [Category("Distribution")]
        public void GaussianNormalDistribution_ApproximatesMean()
        {
            double mean = 50d;
            double standardDeviation = 10d;
            double sum = 0d;
            int samples = 5000;

            for (int i = 0; i < samples; i++)
                sum += _Rand.GaussianNormalDistribution(mean, standardDeviation);

            double average = sum / samples;
            Assert.IsTrue(Math.Abs(average - mean) < 3d, $"Average of {average} is too far from expected mean of {mean}");
        }

        [Test]
        [Category("Distribution")]
        public void NormallyDistributedDouble_TestRange()
        {
            double lowerBound = 10d;
            double upperBound = 100d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.NormallyDistributedDouble(lowerBound, upperBound, 3);
                Assert.IsTrue(buffer >= lowerBound && buffer <= upperBound, $"{buffer} outside of expected range [{lowerBound},{upperBound}]");
            }
        }

        [Test]
        [Category("Distribution")]
        [TestCase(0)]
        [TestCase(-1)]
        public void NormallyDistributedDouble_ThrowsForInvalidRolls(int rolls)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _Rand.NormallyDistributedDouble(0d, 10d, rolls));
        }

        [Test]
        [Category("Distribution")]
        public void NormallyDistributedFloat_TestRange()
        {
            float upperBound = 100f;

            for (int i = 0; i < 1000; i++)
            {
                float buffer = _Rand.NormallyDistributedFloat(upperBound, 3);
                Assert.IsTrue(buffer >= 0f && buffer <= upperBound, $"{buffer} outside of expected range [0,{upperBound}]");
            }
        }

        [Test]
        [Category("Distribution")]
        public void NormallyDistributedInt_TestRange()
        {
            int upperBound = 100;

            for (int i = 0; i < 1000; i++)
            {
                int buffer = _Rand.NormallyDistributedInt(upperBound, 3);
                Assert.IsTrue(buffer >= 0 && buffer <= upperBound, $"{buffer} outside of expected range [0,{upperBound}]");
            }
        }

        [Test]
        [Category("Distribution")]
        public void UniformDistributedDouble_NeverReturnsNaN()
        {
            for (int i = 0; i < 1000; i++)
            {
                double buffer = _Rand.UniformDistributedDouble(0d, 1d);
                Assert.IsFalse(double.IsNaN(buffer), "UniformDistributedDouble returned NaN");
            }
        }

        [Test]
        [Category("Distribution")]
        public void ExponentiallyDistributedDouble_TestRangeAndBias()
        {
            double sum = 0d;

            for (int i = 0; i < NUMBER_OF_TESTS; i++)
            {
                double buffer = _Rand.ExponentiallyDistributedDouble();
                Assert.IsTrue(buffer >= 0d && buffer <= 1d, $"{buffer} outside of expected range [0,1]");
                sum += buffer;
            }

            double average = sum / NUMBER_OF_TESTS;
            Assert.IsTrue(average < 0.5d, $"Average of {average} does not show expected bias toward 0");
        }

        [Test]
        [Category("Distribution")]
        public void ExponentiallyDistributedDouble_WithLogBase_TestRange()
        {
            for (int i = 0; i < 1000; i++)
            {
                double buffer = _Rand.ExponentiallyDistributedDouble(5d);
                Assert.IsTrue(buffer >= 0d && buffer <= 1d, $"{buffer} outside of expected range [0,1]");
            }
        }

        [Test]
        [Category("Distribution")]
        public void ExponentiallyDistributedDouble_ThrowsForLogBaseTooSmall()
        {
            Assert.Throws<ArgumentException>(() => _Rand.ExponentiallyDistributedDouble(0d));
            Assert.Throws<ArgumentException>(() => _Rand.ExponentiallyDistributedDouble(-1d));
        }

        [Test]
        [Category("Distribution")]
        public void ExponentiallyDistributedDouble_WithRange_TestRange()
        {
            double min = 10d;
            double max = 20d;

            for (int i = 0; i < 1000; i++)
            {
                double buffer = _Rand.ExponentiallyDistributedDouble(5d, min, max);
                Assert.IsTrue(buffer >= min && buffer <= max, $"{buffer} outside of expected range [{min},{max}]");
            }
        }

        [Test]
        [Category("Distribution")]
        public void ExponentiallyDistributedDouble_WithRange_ThrowsWhenMinGreaterThanMax()
        {
            Assert.Throws<ArgumentException>(() => _Rand.ExponentiallyDistributedDouble(5d, 20d, 10d));
        }

        #endregion

        #region Time

        [Test]
        [Category("Time")]
        public void RandomTime_ComponentsWithinBounds()
        {
            for (int i = 0; i < 1000; i++)
            {
                var buffer = _Rand.RandomTime();

                Assert.IsTrue(buffer.Hour >= 0 && buffer.Hour <= 23, $"Hour {buffer.Hour} out of range");
                Assert.IsTrue(buffer.Minute >= 0 && buffer.Minute <= 59, $"Minute {buffer.Minute} out of range");
                Assert.IsTrue(buffer.Second >= 0 && buffer.Second <= 59, $"Second {buffer.Second} out of range");
            }
        }

        [Test]
        [Category("Time")]
        public void RandomDateTime_NoArg_ProducesVariedValues()
        {
            var seen = new HashSet<long>();

            for (int i = 0; i < 20; i++)
                seen.Add(_Rand.RandomDateTime().Ticks);

            Assert.IsTrue(seen.Count > 1, "RandomDateTime() returned the same value every time");
        }

        [Test]
        [Category("Time")]
        public void RandomDateTime_WithRange_StaysWithinBounds()
        {
            var min = new DateTime(2000, 1, 1);
            var max = new DateTime(2010, 1, 1);

            for (int i = 0; i < 1000; i++)
            {
                var buffer = _Rand.RandomDateTime(min, max);
                Assert.IsTrue(buffer >= min && buffer <= max, $"{buffer} outside of expected range [{min},{max}]");
            }
        }

        [Test]
        [Category("Time")]
        public void RandomDateTime_WithReversedRange_StaysWithinBounds()
        {
            var min = new DateTime(2000, 1, 1);
            var max = new DateTime(2010, 1, 1);

            for (int i = 0; i < 1000; i++)
            {
                var buffer = _Rand.RandomDateTime(max, min);
                Assert.IsTrue(buffer >= min && buffer <= max, $"{buffer} outside of expected range [{min},{max}]");
            }
        }

        #endregion
    }
}
