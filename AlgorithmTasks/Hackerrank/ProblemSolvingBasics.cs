using System.Globalization;

namespace AlgorithmTasks.Hackerrank;

public class ProblemSolvingBasics
{
    public static void MiniMaxSum(List<int> arr)
    {
        var longs = arr.Select(i => (long)i).ToList();
        longs.Sort();
        Console.WriteLine($"{longs.Take(arr.Count-1).Sum()} {longs.Skip(1).Sum()}");
    }

    /// <summary>
    /// Convert time from 12 to 24 hour system
    /// </summary>
    /// <param name="s">A single string  that represents a time in -hour clock format (i.e.: hh:mm:ssAM or hh:mm:ssPM).</param>
    /// <returns>24 hours format - hh:mm:ss</returns>
    public static string TimeConversion(string s)
    {
        if (s.Contains("-12:")) return $"00:{s[4]}{s[5]}:{s[7]}{s[8]}";
        
        var time = DateTime.ParseExact(s, "h:mm:sstt", CultureInfo.InvariantCulture);
        var output = time.ToString("HH:mm:ss"); 
        Console.WriteLine(output);
        return output;
    } 
    
    /// <summary>
    /// For each query string, determine how many times it occurs in the list of input strings.
    /// </summary>
    /// <returns>an array of the results</returns>
    public static List<int> MatchingStrings(List<string> strings, List<string> queries)
    {
        var dict = new Dictionary<string, int>();

        foreach (var s in strings)
        {
            if (dict.ContainsKey(s))
            {
                dict[s]++;
            }
            else
            {
                dict.Add(s,1);
            }
        }

        return queries.Select(q => dict.TryGetValue(q, out var value) ? value : 0).ToList();
    }

    public static int LonelyInteger(List<int> a)
    {
        return a.Aggregate((x, y) => x ^ y);
    }
}