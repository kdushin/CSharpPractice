using System;
using System.Numerics;


namespace CSharpPractice.Algorithms
{
    public static class Karatsuba
    {
        public static BigInteger Multiply(BigInteger num1, BigInteger num2)
        {
            int n = (int)Math.Ceiling(Math.Max(BigInteger.Log(num1, 2),
                BigInteger.Log(num2, 2)));

            // Base case for recursion
            // if Max of 2 numbers is less than or equal to 10 then just multiply them.
            if (n <= 3) return num1 * num2;

            n = (n + 1) / 2;
            BigInteger b = num1 >> n;          // second half of num1
            BigInteger a = num1 - (b << n);    // first half of num1
            BigInteger d = num2 >> n;          // second half of num2
            BigInteger c = num2 - (d << n);    // first half of num2

            // Recursive calls
            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            BigInteger abcd = Multiply(a + b, c + d);

            return ac + ((abcd - ac - bd) << n) + (bd << (2 * n));
        }
    }
}
