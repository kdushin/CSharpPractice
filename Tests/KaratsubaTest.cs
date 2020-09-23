using System;
using System.Numerics;
using AlgorithmBasics.Algorithms;
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

            BigInteger actualResult = Karatsuba.Multiply(firstNumber, secondNumber);
            BigInteger expectedResult = BigInteger.Parse("8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184");
            Console.WriteLine(actualResult);
            Console.WriteLine(expectedResult);
            
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
