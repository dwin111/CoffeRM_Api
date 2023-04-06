using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChekTesting
{
    public class TestTestCheck
    {
        [Fact]
        public void Test1() 
        {
            int exemple = 4;

            int requect = Add(2,2);

            Assert.Equal(exemple, requect);

        }
        [Fact]
        public void Test2()
        {
            int exemple = 5;

            int requect = Add(2, 3);

            Assert.Equal(exemple, requect);

        }
        [Fact]
        public void Test3()
        {
            int exemple = 100;

            int requect = Add(2, 98);

            Assert.Equal(exemple, requect);

        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
