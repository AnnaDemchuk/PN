using NUnit.Framework;
using PN;
using System;
using System.Collections.Generic;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase("5*2+10", 20)]
        [TestCase("(20-(1+0))-5^2", -6)]
        [TestCase("3+4*2/(1-5)^2", 3.5)]
        [TestCase("3+4*2/(1-1)", double.PositiveInfinity)]
        [TestCase("5-5/0", double.NegativeInfinity)]
        public void StartTest(string exp, double expectedResult)
        {
            Transform transform = new Transform();
            double actualResult = transform.Start(exp);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("3*2-7", new[] { "3", "2", "*", "7", "-" })]
        [TestCase("(1+2)*5+6", new[] { "1", "2", "+", "5", "*", "6", "+" })]
        [TestCase("2^2/8", new[] { "2", "2", "^", "8", "/" })]
        public void MakeNotationTest(string expression, string[] expectedResult)
        {
            Transform transform = new Transform();
            List<string> actualResult = transform.MakeNotation(expression);
            Assert.AreEqual(expectedResult.ToList(), actualResult);
        }

        [TestCase(new[] { "6", "2", "/", "5", "-" }, -2)]
        [TestCase(new[] { "8", "2", "2", "^", "*" }, 32)]
        [TestCase(new[] { "2", "4", "/", "2", "+" }, 2.5)]

        public void CountResult(string[] tmpList, double expectedResult)
        {
            Transform transform = new Transform();
            double actualResult = transform.CountResult(tmpList.ToList());
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
