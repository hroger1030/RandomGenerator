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
using RandomNumbers.Dice;
using System;

namespace RandomNumbersTests
{
    [TestFixture]
    public class WeightedTableTests
    {
        [Test]
        [Category("WeightedTable")]
        public void WeightedTableAddAndRemove()
        {
            string[] items = new string[] { "Item 1", "Item 2", "Item 3", "Item 4" };

            var t1 = new WeightedTable<string>();

            float current_weight = 1f;

            foreach (var item in items)
            {
                t1.AddEntry(item, current_weight);
                current_weight += 1f;
            }

            Assert.IsTrue(t1.TotalWeight == 10f);

            int count = 0;

            while (t1.TableList.Count > 0)
            {
                t1.SelectRandomItem(true);
                count++;
            }

            Assert.IsTrue(count == items.Length);
        }

        [Test]
        [Category("WeightedTable")]
        public void AddEntry_ThrowsForZeroOrNegativeWeight()
        {
            var table = new WeightedTable<string>();

            Assert.Throws<ArgumentException>(() => table.AddEntry("item", 0f));
            Assert.Throws<ArgumentException>(() => table.AddEntry("item", -1f));
        }

        [Test]
        [Category("WeightedTable")]
        public void SelectRandomItem_ThrowsWhenTableIsEmpty()
        {
            var table = new WeightedTable<string>();

            Assert.Throws<Exception>(() => table.SelectRandomItem());
        }

        [Test]
        [Category("WeightedTable")]
        public void SelectRandomItem_WithoutRemoval_LeavesTableUnchanged()
        {
            var table = new WeightedTable<string>();
            table.AddEntry("a", 1f);
            table.AddEntry("b", 1f);
            table.AddEntry("c", 1f);

            table.SelectRandomItem(false);

            Assert.AreEqual(3, table.TotalEnties);
            Assert.AreEqual(3f, table.TotalWeight);
        }

        [Test]
        [Category("WeightedTable")]
        public void Reset_ClearsTableAndWeight()
        {
            var table = new WeightedTable<string>();
            table.AddEntry("a", 1f);
            table.AddEntry("b", 2f);

            table.Reset();

            Assert.AreEqual(0, table.TotalEnties);
            Assert.AreEqual(0f, table.TotalWeight);
        }

        [Test]
        [Category("WeightedTable")]
        public void SortTable_OrdersEntriesByDescendingWeight()
        {
            var table = new WeightedTable<string>();
            table.AddEntry("low", 1f);
            table.AddEntry("high", 10f);
            table.AddEntry("mid", 5f);

            table.SortTable();

            Assert.AreEqual("high", table.TableList[0].Key);
            Assert.AreEqual("mid", table.TableList[1].Key);
            Assert.AreEqual("low", table.TableList[2].Key);
        }

        [Test]
        [Category("WeightedTable")]
        public void ConvertToPercentileTable_CoversFullRange()
        {
            var table = new WeightedTable<string>();
            table.AddEntry("a", 50f);
            table.AddEntry("b", 50f);

            var percentileTable = table.ConvertToPercentileTable(eDiceType.D100);

            Assert.AreEqual(2, percentileTable.Count);
            Assert.IsTrue(percentileTable[0].Key.StartsWith("1"));
            Assert.IsTrue(percentileTable[^1].Key.EndsWith("100"));
        }

        [Test]
        [Category("WeightedTable")]
        public void ConvertToPercentileTable_ThrowsWhenTableIsTooLargeForScale()
        {
            var table = new WeightedTable<string>();

            for (int i = 0; i < 10; i++)
                table.AddEntry($"item{i}", 1f);

            Assert.Throws<ArgumentOutOfRangeException>(() => table.ConvertToPercentileTable(eDiceType.D6));
        }

        [Test]
        [Category("WeightedTable")]
        public void ToString_IncludesEntryCountAndContent()
        {
            var table = new WeightedTable<string>();
            table.AddEntry("item", 5f);

            string result = table.ToString();

            Assert.IsTrue(result.Contains("1 items"));
            Assert.IsTrue(result.Contains("item"));
        }
    }
}
