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
        char Char();
        char Char(char max);
        char Char(char min, char max);
        T CollectionValue<T>(IList<T> collection, bool remove);
        string ColorString();
        string ColorString(float red, float green, float blue, float variance);
        V DictionaryValue<K, V>(IDictionary<K, V> dictionary, bool remove);
        double Double();
        double Double(double max);
        double Double(double min, double max);
        double Double(int rolls, double min, double max);
        T EnumValue<T>() where T : struct, IConvertible;
        double ExponentiallyDistributedDouble();
        double ExponentiallyDistributedDouble(double log_base);
        double ExponentiallyDistributedDouble(double log_base, double min, double max);
        float Facing();
        float Float();
        float Float(float max);
        float Float(float min, float max);
        float Float(int rolls, float min, float max);
        int Int();
        int Int(int max);
        int Int(int min, int max);
        long Long();
        long Long(long max);
        long Long(long min, long max);
        double NormallyDistributedDouble(double upper_bound, int rolls);
        double NormallyDistributedDouble(double lower_bound, double upper_bound, int rolls);
        float NormallyDistributedFloat(float upper_bound, int rolls);
        float NormallyDistributedFloat(float lower_bound, float upper_bound, int rolls);
        int NormallyDistributedInt(int upper_bound, int rolls);
        int NormallyDistributedInt(int lower_bound, int upper_bound, int rolls);
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
        double UnitDouble();
        float UnitFloat();
    }
}