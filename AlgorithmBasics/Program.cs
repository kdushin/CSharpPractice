using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AlgorithmBasics.Algorithms.Graphs;
using AlgorithmBasics.DataStructures.Heap;

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
            // string chunksPath = SeparateFileIntoChunks(@"C:\Projects\CSharpPractice\AlgorithmBasics\CourseTasks\unsortedBigFile.txt", chunkSizeMb);
            // string chunksPath = SeparateFileIntoChunks(@"C:\Projects\CSharpPractice\AlgorithmBasics\CourseTasks\unsortedSmallFile.txt", chunkSizeMb);
            // string chunksPath = SeparateFileIntoChunks("C:\\Projects\\CSharpPractice\\AlgorithmBasics\\CourseTasks\\BigFileSameString.txt", chunkSizeMb);
            sw.Stop();
            
            var sortMilliseconds = sw.ElapsedMilliseconds;
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Finished to separate file into chunks (size = {chunkSizeMb} mb) and sort. Elapsed time ms: {sw.ElapsedMilliseconds}");

            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Starting to merge sorted chunks");
            sw.Restart();
            
            MergeChunks(ChunksPath);
            
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
            var sw = new Stopwatch();
            sw.Start();
            
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
            using (var streamWriter = new StreamWriter($"{ChunksPath}{GetChunkFileName(chunkNumber)}"))
            {
                foreach (KeyValuePair<string,List<int>> kvp in result)
                {
                    kvp.Value.Sort();
                    foreach (int i in kvp.Value)
                    {
                        streamWriter.WriteLine($"{i:D20}.{kvp.Key}");   
                    }
                }
            }
            
            sw.Stop();
            Console.WriteLine($"{chunkNumber} was wrote. TimeMs - {sw.ElapsedMilliseconds}");
        }

        private static void MergeChunks(string outputPath)
        {
            string[] filePaths = Directory.GetFiles(outputPath);
            var readersHeap = new MinHeap<ChunkReaderHelper>(filePaths.Length);

            foreach (string filePath in filePaths)
            {
                var reader = new StreamReader(filePath);
                if (!reader.EndOfStream)
                {
                    readersHeap.Insert(new ChunkReaderHelper(reader, filePath));
                }
            }
            
            using (var writer = new StreamWriter($"{ChunksPath}{Guid.NewGuid():N}.txt"))
            {
                ChunkReaderHelper min = readersHeap.ExtractMin();
                while (min != default)
                {
                    WriteLine(writer, min.GetLine());
                    min.SetLine();
                    if (min.HasRecords)
                    {
                        readersHeap.Insert(min);
                    }
                    else
                    {
                        min.Reader.Close();
                        //File.Delete(min.FilePath);
                    }
                    min = readersHeap.ExtractMin();
                }
            }
        }
        
        private static void WriteLine(StreamWriter writer, string line)
        {
            string[] parts = line.Split('.');
            writer.WriteLine($"{parts[0].TrimStart('0')}.{parts[1]}");
        }
        
        private static int CompareKeyValuePairs(KeyValuePair<long, string> first, KeyValuePair<long, string> second)
        {
            if (first.Value != second.Value)
            {
                return string.CompareOrdinal(first.Value, second.Value);
            }
            return first.Key.CompareTo(second.Key);
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

    public class ChunkReaderHelper : IComparable<ChunkReaderHelper>
    {
        public const string End = "EmptyReader";

        public StreamReader Reader { get; }

        public string FilePath { get; }

        public bool HasRecords => _line != End;

        private string _line = null;

        public ChunkReaderHelper(StreamReader reader, string filePath)
        {
            Reader = reader;
            _line = reader.ReadLine();
            FilePath = filePath;
        }

        public string GetLine() => _line;

        public void SetLine()
        {
            if (_line == End) { return; }
            _line = Reader.EndOfStream ? End : Reader.ReadLine();
        }

        public int CompareTo(ChunkReaderHelper other)
        {
            return string.CompareOrdinal(GetLine(), other.GetLine());
        }
    }
}
