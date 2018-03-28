﻿using System;
using System.Numerics;
using CSharpPractice.Algorithms;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class KaratsubaTest
    {
        [Test]
        public void TestKaratsubaMultiplication()
        {
            var firstNumber = BigInteger.Parse("3141592653589793238462643383279502884197169399375105820974944592");
            var secondNumber = BigInteger.Parse("2718281828459045235360287471352662497757247093699959574966967627");

            BigInteger result = Karatsuba.Multiply(firstNumber, secondNumber);
            Console.WriteLine(result);
        }
    }
}
