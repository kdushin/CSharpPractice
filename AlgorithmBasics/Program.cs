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
            int chunkSizeMb = 100;
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Starting to separate file into chunks");
            
            sw.Start();
            string chunksPath = SeparateFileIntoChunks(@"C:\Projects\CSharpPractice\AlgorithmBasics\CourseTasks\unsortedBigFile.txt", chunkSizeMb);
            sw.Stop();
            
            var sortMilliseconds = sw.ElapsedMilliseconds;
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Finished to separate file into chunks (size = {chunkSizeMb} mb) and sort. Elapsed time ms: {sw.ElapsedMilliseconds}");

            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Starting to merge sorted chunks");
            
            sw.Restart();
            MergeChunks(chunksPath);
            sw.Stop();
            
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. All chunks were merged. Elapsed time ms: {sw.ElapsedMilliseconds}");

            using (var process = Process.GetCurrentProcess())
            {
                Console.WriteLine();
                Console.WriteLine("Application stopped!");
                Console.WriteLine("Execution time: {0:G} ms\r\nGen-0_count: {1}, Gen-1_count: {2}, Gen-2_count: {3}\r\nPeak WorkSet: {4:n0} \r\n",
                    sw.ElapsedMilliseconds + sortMilliseconds,
                    GC.CollectionCount(0).ToString(),
                    GC.CollectionCount(1).ToString(),
                    GC.CollectionCount(2).ToString(),
                    process.PeakWorkingSet64);
            }
        }

        private static string SeparateFileIntoChunks(string filePath, int chunkSizeMb)
        {
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
                        SortAndWriteDataChunk(strings, chunkNumber);
                        strings = new List<string>();
                        previousChunkPosition = streamReader.BaseStream.Position;
                        chunkNumber++;
                    }
                }

                if (strings.Any())
                {
                    SortAndWriteDataChunk(strings, chunkNumber);
                    strings = null;
                }

                return ChunksPath;
            }
        }

        private static void SortAndWriteDataChunk(List<string> chunkData, long chunkNumber)
        {
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
                    foreach (int i in kvp.Value)
                    {
                        streamWriter.WriteLine($"{kvp.Key}.{i}");   
                    }
                }
            }
        }

        private static void MergeChunks(string outputPath)
        {
            Queue<string> filesQueue = new Queue<string>();
            foreach (string file in Directory.EnumerateFiles(outputPath))
            {
                filesQueue.Enqueue(file);
            }

            while (filesQueue.Count >= 2)
            {
                filesQueue.Enqueue(MergeFiles(filesQueue.Dequeue(), filesQueue.Dequeue()));
            }
        }

        private static string MergeFiles(string path1, string path2)
        {
            string chunkNumber = GetChunkNumber(path1) + GetChunkNumber(path2);
            string resultFilePath = $"{ChunksPath}\\{GetChunkFileName(chunkNumber)}";
            
            using (var reader1 = new StreamReader(path1))
            using (var reader2 = new StreamReader(path2))
            using (var outputWriter = new StreamWriter(resultFilePath))
            {
                int count = 0;
                string line1 = reader1.EndOfStream ? null : reader1.ReadLine();
                string line2 = reader2.EndOfStream ? null : reader2.ReadLine();
                while (line1 != null && line2 != null)
                {
                    if (string.CompareOrdinal(line1, line2) <= 0)
                    {
                        outputWriter.WriteLine(line1);
                        line1 = reader1.EndOfStream ? null : reader1.ReadLine();
                    }
                    else
                    {
                        outputWriter.WriteLine(line2);
                        line2 = reader2.EndOfStream ? null : reader2.ReadLine();
                    }

                    count++;
                }

                if (line1 != null) { outputWriter.WriteLine(line1); }
                if (line2 != null) { outputWriter.WriteLine(line2); }

                while (!reader1.EndOfStream)
                {
                    outputWriter.WriteLine(reader1.ReadLine());
                }
                while (!reader2.EndOfStream)
                {
                    outputWriter.WriteLine(reader2.ReadLine());
                }
            }
            File.Delete(path1);
            File.Delete(path2);
            return resultFilePath;
        }

        private static void TestResults(string path)
        {
            var dict = new Dictionary<string, long>();
            using (var reader = new StreamReader(path))
            {
                var prevLine = reader.ReadLine();
                AddOrUpdate(dict, prevLine);
                while (!reader.EndOfStream)
                {
                    var currLine = reader.ReadLine();
                    if (currLine == null)
                    {
                        throw new Exception();
                    }
                    AddOrUpdate(dict, currLine);
                }
            }

            Console.WriteLine("Resulted dictionary");
            foreach (KeyValuePair<string,long> keyValuePair in dict)
            {
                Console.WriteLine($"Key - {keyValuePair.Key}. Count - {keyValuePair.Value}");
            }
        }

        private static void AddOrUpdate(Dictionary<string, long> dict, string line)
        {
            if (dict.ContainsKey(line))
            {
                dict[line]++;
            }
            else
            {
                dict.Add(line, 1);
            }
        }

        private static string GetChunkNumber(string path)
        {
            // path template C:\output\Chunk-1.txt
            return path.Split('\\')
                       .Last()
                       .Split('.')
                       .First()
                       .Split('-')
                       .Last();
        }
        
        private static string GetChunkFileName(string chunkNumber)
        {
            return $"Chunk-{chunkNumber}.txt";
        }
        
        private static string GetChunkFileName(long chunkNumber)
        {
            return $"Chunk-{chunkNumber}.txt";
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
