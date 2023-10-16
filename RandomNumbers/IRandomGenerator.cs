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

namespace RandomNumbers
{
    public interface IRandomGenerator
    {
        bool Bool();

        byte Byte();
        byte Byte(byte max);
        byte Byte(byte min, byte max);
        byte[] ByteArray(int count);

        char Char();
        char Char(char max);
        char Char(char min, char max);

        T CollectionValue<T>(IList<T> collection, bool remove);

        string RGBColorString();
        string RGBAColorString();
        string ColorString(float red, float green, float blue, float variance);

        V DictionaryValue<K, V>(IDictionary<K, V> dictionary, bool remove);

        double Double();
        double Double(double max);
        double Double(double min, double max);

        T EnumValue<T>() where T : struct, IConvertible;

        double ExponentiallyDistributedDouble();
        double ExponentiallyDistributedDouble(double logBase);
        double ExponentiallyDistributedDouble(double logBase, double min, double max);

        float Facing();

        float Float();
        float Float(float max);
        float Float(float min, float max);

        int Int();
        int Int(int max);
        int Int(int min, int max);

        long Long();
        long Long(long max);
        long Long(long min, long max);

        double NormallyDistributedDouble(double upperBound, int rolls);
        double NormallyDistributedDouble(double lowerBound, double upperBound, int rolls);
        float NormallyDistributedFloat(float upperBound, int rolls);
        float NormallyDistributedFloat(float lowerBound, float upperBound, int rolls);

        int NormallyDistributedInt(int upperBound, int rolls);
        int NormallyDistributedInt(int lowerBound, int upperBound, int rolls);

        T Object<T>() where T : class, new();

        DateTime RandomDateTime();

        string Sentence(int max_string_length);

        short Short();
        short Short(short max);
        short Short(short min, short max);

        string String(int length);
        string String(int length, string character_set);
        string String(int min_length, int max_length);

        ulong ULong();

        string UnicodeString(int length);

        double UniformDistributedDouble(double peak, double scale);

        double UnitInterval();
    }
}