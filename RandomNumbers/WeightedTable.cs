using System;
using System.Collections.Generic;
using System.Linq;

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

        public T SelectRandomItem(bool removeSelectedItem)
        {
            if (_TableList.Count < 1)
                throw new Exception("Table is empty, populate table before selecting value");

            if (!_Sorted)
            {
                _TableList = _TableList.OrderByDescending(i => i.Value).ToList();
                _Sorted = true;
            }

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

        public override string ToString()
        {
            return $"WeightedTable ({_TableList.Count} items))";
        }
    }
}