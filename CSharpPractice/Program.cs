using System;
using System.CodeDom;

class Program {
    
    public int solution(int[] array)
    {
        var deepestPitLength = DeepestPit(new[] {1, 5, 4, 3, 2, 3, 5, 6, 1, 4, 8, 9});
    }
    
    public static int DeepestPit(int[] array)
    {
        int P = 0, Q = 0, R = 0;
        var maxDepth = -1;
        var previousValue = array[0];
        var i = 1;
        var firstTime = true;
 
        while(i < array.Length)
        {
            var next = array[i];
            if (previousValue > next )
            {
                P = previousValue;
                i++;
                   
                while (i < array.Length && previousValue > array[i])
                {
                    previousValue = array[i];
                    i++;
                }
 
                if (i == array.Length)
                {
                    return maxDepth;
                }
 
                Q = i - 1;
            }
 
            var firstSequenceValue = array[P] - array[Q];
 
            R = i++;
 
            previousValue = array[R];
 
            var secondSequenceValue = array[R] - array[Q];
 
            if (firstTime)
            {
                maxDepth = Math.Min(firstSequenceValue, secondSequenceValue);
                firstTime = false;
            }
            else
            {
                maxDepth = Math.Max(maxDepth, Math.Min(firstSequenceValue, secondSequenceValue));
            }
               
 
            while (i < array.Length && previousValue < array[i])
            {
                R = i;
                var currentSequenceValue = Math.Min(firstSequenceValue, array[R] - array[Q]);
                if (maxDepth < currentSequenceValue) maxDepth = currentSequenceValue;
 
                previousValue = array[i];
                i++;
            }
        }
 
        return maxDepth;
    }

    public int solution(int[] A, int[] B)
    {
        int result = 0;
        int length = A.Length;
        int upperBound = 1000000000;
    
        int j = length - 1;
        if (length > 1)
        {
            int i;
            for (i = 0; i < length; i++)
            {
                if (GetNumber(i, A, B) > 1) break;
            }
    
            while (j > i)
            {
                double v = GetNumber(j, A, B) / (GetNumber(j, A, B) - 1);
    
                while (GetNumber(i, A, B) < v && i < j)
                {
                    i++;
                }
    
                if (i == j) break;
                
                result += (j - i);
                
                if (result >= upperBound) return upperBound;
    
                j--;
            }
        }
    
        return result;
    }

    private static double GetNumber(int i, int[] A, int[] B)
    {
        return A[i] + (double)(B[i] / 1000000);
    }
}