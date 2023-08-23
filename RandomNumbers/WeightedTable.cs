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

using RandomNumbers.Dice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomNumbers
{
    public partial class WeightedTable<T>
    {
        protected static Random _Random;
        protected List<KeyValuePair<T, float>> _TableList;
        protected float _TotalWeight;
        protected bool _Sorted;

        public List<KeyValuePair<T, float>> TableList
        {
            get { return _TableList; }
        }

        /// <summary>
        /// Returns the sum of all the table weights.
        /// </summary>
        public float TotalWeight
        {
            get { return _TotalWeight; }
        }

        /// <summary>
        /// Returns number of entries in table.
        /// </summary>
        public int TotalEnties
        {
            get { return _TableList.Count; }
        }

        public WeightedTable() : this(null) { }

        public WeightedTable(IEnumerable<KeyValuePair<T, float>> items)
        {
            _Random = new Random();
            _TableList = new List<KeyValuePair<T, float>>();
            _TotalWeight = 0;
            _Sorted = false;

            if (items == null)
                return;

            foreach (var kvp in items)
                AddEntry(kvp.Key, kvp.Value);
        }

        public void Reset()
        {
            _TableList.Clear();
            _TotalWeight = 0;
        }

        public void AddEntry(T item, int weight)
        {
            AddEntry(item, (float)weight);
        }

        public void AddEntry(T item, double weight)
        {
            AddEntry(item, (float)weight);
        }

        public void AddEntry(T item, float weight)
        {
            if (weight < float.Epsilon)
                throw new ArgumentException("Cannot add an item with a weight of 0 or less");

            _TableList.Add(new KeyValuePair<T, float>(item, weight));
            _TotalWeight += weight;
            _Sorted = false;
        }

        public T SelectRandomItem(bool removeSelectedItem = false)
        {
            if (_TableList.Count < 1)
                throw new Exception("Table is empty, populate table before selecting value");

            SortTable();

            int selected_index = -1;
            float random_roll = (float)_Random.NextDouble() * _TotalWeight;

            for (int i = 0; i < _TableList.Count; i++)
            {
                random_roll -= _TableList[i].Value;

                if (random_roll < 1)
                {
                    selected_index = i;
                    break;
                }
            }

            if (selected_index == -1)
            {
                // somehow we scanned the whole table w/o getting an item. return last item.
                selected_index = _TableList.Count - 1;
            }

            KeyValuePair<T, float> selected_item = _TableList[selected_index];

            if (removeSelectedItem)
            {
                _TableList.RemoveAt(selected_index);
                _TotalWeight -= selected_item.Value;
            }

            return selected_item.Key;
        }

        /// <summary>
        /// Converts the table to a percentile table.
        /// </summary>
        public List<KeyValuePair<string,string>> ConvertToPercentileTable(eDiceType tableScale)
        {
            SortTable();

            var output = new List<KeyValuePair<string, string>>();
            int lowerBound = 1;
            int upperBound; 
            string range;

            // check total count against table scale.
            // fail if table is too large to convert to tableScale.

            foreach (var kvp in _TableList)
            {
                var currentDelta = kvp.Value / _TotalWeight * (int)tableScale;
                var roundedDelta = Math.Max((int)Math.Round(currentDelta, 0), 1);

                upperBound = lowerBound + roundedDelta - 1;

                //check to see if we have excceded the upper bound of the table
                if (lowerBound > (int)tableScale)
                    throw new ArgumentOutOfRangeException($"Table is too large to convert to a {tableScale} table");

                if (lowerBound >= upperBound)
                    range = $"{lowerBound}";
                else
                    range = $"{lowerBound} - {upperBound}";

                var newKvp = new KeyValuePair<string, string>(range, kvp.Key.ToString() + $" raw({currentDelta})" );
                output.Add(newKvp);
                lowerBound += roundedDelta;
            }

            return output;
        }

        public void SortTable()
        {
            if (!_Sorted)
            {
                _TableList = _TableList.OrderByDescending(i => i.Value).ToList();
                _Sorted = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"WeightedTable Content ({_TableList.Count} items)");

            foreach (var kvp in _TableList)
                sb.AppendLine($"Value: {kvp.Key}, Weight: {kvp.Value}");

            return sb.ToString();
        }
    }
}