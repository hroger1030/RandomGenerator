using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RandomNumbers;

namespace RandomNumbersTests
{
    [TestClass]
    public class WeightedTable
    {
        [TestMethod]
        [TestCategory("RandomGenerator")]
        public void WeightedTableAddAndRemove()
        {
            string[] items = new string[] { "Item 1","Item 2","Item 3","Item 4" };

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
    }
}
