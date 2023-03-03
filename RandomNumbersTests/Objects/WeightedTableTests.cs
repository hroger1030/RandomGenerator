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

namespace RandomNumbersTests
{
    [TestFixture]
    public class WeightedTable
    {
        [Test]
        [Category("RandomNumbers")]
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
    }
}
