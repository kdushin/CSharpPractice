using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AlgorithmBasics.Algorithms.Graphs;

namespace AlgorithmBasics
{
    class Program
    {
        private static Random _random = new Random();
        private const int MaxLength = 100;
        private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789 ";
        private const string ChunksPath = "C:\\output\\";
        
        public static void Main(string[] args)
        {
            var sw = new Stopwatch();
            Console.WriteLine("Starting to separate file into chunks");
            sw.Start();
            string chunksPath = SeparateFileIntoChunks(@"C:\Projects\CSharpPractice\AlgorithmBasics\CourseTasks\unsortedBigFile.txt", 90);
            sw.Stop();
            Console.WriteLine($"Finished to separate file into chunks and sort. Time - {sw.ElapsedMilliseconds}");

            // MergeChunks(chunksPath);
            
            using (var process = Process.GetCurrentProcess())
            {
                Console.WriteLine();
                Console.WriteLine("Application stopped!");
                Console.WriteLine("Execution time: {0:G}\r\nGen-0_count: {1}, Gen-1_count: {2}, Gen-2_count: {3}\r\nPeak WorkSet: {4:n0} \r\n",
                    sw.ElapsedMilliseconds,
                    GC.CollectionCount(0).ToString(),
                    GC.CollectionCount(1).ToString(),
                    GC.CollectionCount(2).ToString(),
                    process.PeakWorkingSet64);
            }
        }

        private static string SeparateFileIntoChunks(string filePath, int chunkSizeMb)
        {
            var stopWatch = new Stopwatch();
            long chunkMaxSize = 1024 * 1024 * chunkSizeMb; // MB
            using (var streamReader = new StreamReader(filePath))
            {
                long chunkNumber = 1;
                long previousChunkPosition = 0;

                var strings = new List<string>();
                while (streamReader.Peek() >= 0)
                {
                    strings.Add(streamReader.ReadLine());
                    
                    if (streamReader.BaseStream.Position - previousChunkPosition >= chunkMaxSize && streamReader.Peek() >= 0)
                    {
                        SortAndWriteDataChunk(strings, chunkNumber, stopWatch);
                        strings = new List<string>();
                        previousChunkPosition = streamReader.BaseStream.Position;
                        chunkNumber++;
                    }
                }

                if (strings.Any())
                {
                    SortAndWriteDataChunk(strings, chunkNumber, stopWatch);
                    strings = null;
                }

                return ChunksPath;
            }
        }

        // inject into separate function
        private static void SortAndWriteDataChunk(List<string> chunkData, long chunkNumber, Stopwatch sw)
        {
            sw.Restart();
            var result = new SortedDictionary<string, List<int>>();
            foreach (string line in chunkData)
            {
                string[] lineObjects = line.Split('.');
                if (result.ContainsKey(lineObjects[1]))
                {
                    result[lineObjects[1]].Add(int.Parse(lineObjects[0]));
                }
                else
                {
                    result.Add(lineObjects[1], new List<int>{int.Parse(lineObjects[0])});
                }
            }
            // TODO: just write sorted tuple
            using (var streamWriter = new StreamWriter($"{ChunksPath}{GetChunkFileName(chunkNumber)}"))
            {
                foreach (KeyValuePair<string,List<int>> kvp in result)
                {
                    kvp.Value.Sort();
                    streamWriter.WriteLine($"{kvp.Key}.{string.Join(",", kvp.Value)}");
                }
            }
            sw.Stop();
            Console.WriteLine($"Chunk {chunkNumber} was saved. Elapsed milliseconds - {sw.ElapsedMilliseconds}");
        }

        private static string GetChunkFileName(long chunkNumber)
        {
            return $"Chunk-{chunkNumber}.txt";
        }
        
        private static void MergeChunks(string outputPath)
        {
            string[] chunkPaths = Directory.GetFiles(outputPath); 
            var readers = new StreamReader[chunkPaths.Length];
            for (int i = 0; i < chunkPaths.Length; i++)
            {
                readers[i] = new StreamReader(chunkPaths[i]);
            }

            var x = new List<KeyValuePair<string, List<int>>>();
            
            foreach (StreamReader reader in readers)
            {
                string line = reader.ReadLine();
                string[] rawElements = line.Split('.');
                var z = x.FirstOrDefault(e => e.Key == rawElements[0]);
                
            }


            using (var writer = new StreamWriter("C:\\output\\sortedResult.txt"))
            {
                
            }
        }
        
        private static void GenerateFile(string filePath, long linesCount)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                for (int i = 0; i < linesCount; i++)
                {
                    streamWriter.WriteLine($"{_random.Next()}.{GenerateString(_random.Next(MaxLength))}");
                }
            }
        }

        private static string GenerateString(int length)
        {
            var buffer = new char[length];
            for(var i = 0; i < length; ++i)
            {
                buffer[i] = Chars[_random.Next(Chars.Length)];
            }
            return new string(buffer);
        }
        
        // private static IDictionary<string, uint> GetTopWordsParallelMapReduce()  
        // {
        //     var result = new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase);
        //     Parallel.ForEach(
        //         // FILE PATH
        //         source: File.ReadLines("InputFile.FullName"),
        //         parallelOptions: new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
        //         localInit: () => new Dictionary<string, uint>(StringComparer.InvariantCultureIgnoreCase),
        //         body: (line, state, index, localDic) =>
        //         {
        //             foreach (var word in line.Split(Separators, StringSplitOptions.RemoveEmptyEntries))
        //             {
        //                 if (!IsValidWord(word)) { continue; }
        //                 TrackWordsOccurrence(localDic, word);
        //             }
        //             return localDic;
        //         },
        //         localFinally: localDic =>
        //         {
        //             lock (result)
        //             {
        //                 foreach (var pair in localDic)
        //                 {
        //                     var key = pair.Key;
        //                     if (result.ContainsKey(key))
        //                     {
        //                         result[key] += pair.Value;
        //                     }
        //                     else
        //                     {
        //                         result[key] = pair.Value;
        //                     }
        //                 }
        //             }
        //         }
        //     );
        //
        //     return result
        //         .OrderByDescending(kv => kv.Value)
        //         .Take((int)TopCount)
        //         .ToDictionary(kv => kv.Key, kv => kv.Value);
        // }

        private void Foo()
        {
            for (int i = 0; i < 1; i++)
            {
                Dictionary<int, List<int>> dict = MinimumCuts.ParseGraph("CourseTasks\\kargerMinCut.txt");

                Console.WriteLine($"Started! {DateTime.Now}");
                int result = MinimumCuts.FindWithAdjacencyList(dict);
                Console.WriteLine($"Finished! {DateTime.Now}");
                Console.WriteLine($"MinCut = {result}");
            }
            Console.ReadLine();
        }
    }
}
