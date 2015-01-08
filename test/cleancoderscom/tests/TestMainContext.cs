using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace cleancoderscom.tests.testparent
{
    public class TestMainContext
    {
       
        [SetUp]
        public void Setup()
        {
            System.Console.WriteLine("TestMainContext.Setup");
        }

        public class TestMain : TestMainContext
        {
            [Test]
            public void Test1Main()
            {
                System.Console.WriteLine("TestMain.Test1Main");
            }
        }

        public class TestClass1Context : TestMainContext
        {
            [SetUp]
            public void Setup()
            {
                System.Console.WriteLine("TestClass1Context.Setup"); 
            }

            public class TestClass1 : TestClass1Context
            {
                [Test]
                public void Test1Class1()
                {
                    System.Console.WriteLine("TestClass1.Test1Class1"); 
                }
            }

            public class TestClass1_1 : TestClass1Context
            {
                [SetUp]
                public void Setup()
                {
                    System.Console.WriteLine("TestClass1_1.Setup");
                }

                [Test]
                public void Test1Class1_1()
                {
                    System.Console.WriteLine("TestClass1_1.Test1Class1_1"); 
                }

                [Test]
                public void Test2Class1_1()
                {
                    System.Console.WriteLine("TestClass1_1.Test2Class1_1");
                }
            }

        }

    }

    
}
