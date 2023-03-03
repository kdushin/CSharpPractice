using System.Numerics;

namespace AlgorithmBasics.Algorithms.GreedyAlgorithms
{
    public class SchedulingJobs
    {
        public static BigInteger ComputeJobsCompletionTimeRatio(string filePath)
        {
            List<WeightedJob> jobs = Parse(filePath);

            jobs.Sort((x, y) =>
            {
                var yRatio = (float)y.Weight / y.Length;
                var xRatio = (float)x.Weight / x.Length;
                return yRatio.CompareTo(xRatio);
            });
            
            return ComputeWeightedSum(jobs);
        }

        public static BigInteger ComputeJobsCompletionTimeDifference(string filePath)
        {
            List<WeightedJob> jobs = Parse(filePath);
            
            jobs.Sort((x, y) =>
            {
                int res = (y.Weight - y.Length).CompareTo(x.Weight - x.Length);
                return res == 0 ? y.Weight.CompareTo(x.Weight) : res;
            });
            
            return ComputeWeightedSum(jobs);
        }
        
        private static List<WeightedJob> Parse(string filePath)
        {
            var lst = new List<WeightedJob>(int.Parse(File.ReadLines(filePath).First()));
            
            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                string[] items = line.Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (items.Any())
                {
                    lst.Add(new WeightedJob(weight: int.Parse(items[0]), length: int.Parse(items[1])));
                }
            }

            return lst;
        }

        private static BigInteger ComputeWeightedSum(List<WeightedJob> jobs)
        {
            var lengthSum = 0;
            BigInteger weightedSum = 0;

            foreach (WeightedJob job in jobs)
            {
                lengthSum += job.Length;
                weightedSum += job.Weight * lengthSum;
            }

            return weightedSum;
        }
    }

    public class WeightedJob
    {
        public int Weight { get; set; }
        public int Length { get; set; }

        public WeightedJob(int weight, int length)
        {
            Weight = weight;
            Length = length;
        }
    }
}   