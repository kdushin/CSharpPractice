using AlgorithmBasics.DataStructures.Heap;

namespace AlgorithmBasics.TestAssignments
{
    /// <summary>
    /// <para>Goal:</para>
    /// Provide methods for sorting big txt file in ascending order (file size is greater than RAM).
    /// </summary>
    public static class ExternalSortAssignment
    {
        private static readonly Random Random = new Random();
        private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Generates example file with specified strings count, maximum size for each string, and maximum number
        /// </summary>
        /// <param name="path">Output path where generated file will be created.</param>
        /// <param name="stringsCount">overall strings count for file</param>
        /// <param name="maxStringSize">max size for each string</param>
        /// <param name="maxIntNumber">Boundaries for maximum number in "Number" part</param>
        public static void GenerateFile(string path, int stringsCount, int maxStringSize, int maxIntNumber)
        {
            using (var writer = new StreamWriter(path))
            {
                for (int i = 0; i < stringsCount; i++)
                {
                    writer.WriteLine(GenerateRandomLine(stringSize: maxStringSize, maxInt: maxIntNumber));
                }
            }
        }
        
        private static string GenerateRandomLine(int stringSize, int maxInt = Int32.MaxValue)
        {
            char[] buffer = new char[stringSize];
            for (int i = 0; i < buffer.Length; i++)
            {
                int index = Random.Next(Chars.Length);
                buffer[i] = Chars[index];
            }
            return $"{Random.Next(maxInt)}. {new string(buffer)}";
        }

        /// <summary>
        /// <para>Divide input file into N number of chunks of specified size</para>
        /// <para>Sort each chunk and then merge all chunks into one file using "merge" step from merge sort.</para>
        /// <para>Sorting criteria: Compare "String" if equal then compare numbers.</para>
        /// <para>Each file line has the following format: "Number. String".</para>
        /// <param name="inputFilePath">Path to unsorted file</param>
        /// <param name="chunksDirectory">Directory path where all chunks should be created. Directory path should be empty!</param>
        /// <param name="chunkSizeMb">Max size per chunk in megabytes</param>
        /// <param name="sortedFilePath">Path where to save sorted file.</param>
        /// </summary>
        /// <example>
        /// Sorting example per file
        /// <para>Input file has the following lines:</para>
        /// <code>
        /// 1. Apple
        /// 99. Banana
        /// 2. Cherry
        /// 10. Banana
        /// 3. Apple
        /// 1000. Foo
        /// </code>
        /// Sorted file should be:
        /// <code>
        /// 1. Apple
        /// 3. Apple
        /// 10. Banana
        /// 99. Banana
        /// 2. Cherry
        /// 1000. Foo
        /// </code>
        /// </example>
        public static void Run(string inputFilePath, string chunksDirectory, int chunkSizeMb, string sortedFilePath)
        {
            SeparateFileIntoChunks(inputFilePath, chunksDirectory, chunkSizeMb);
            MergeChunks(chunksDirectory, sortedFilePath);
        }
        
        private static void SeparateFileIntoChunks(string filePath, string outputPath, int chunkSizeMb)
        {
            Directory.CreateDirectory(outputPath);
            long chunkMaxSize = 1024 * 1024 * chunkSizeMb; // MB
            using (var streamReader = new StreamReader(filePath))
            {
                int chunkNumber = 1;
                long previousChunkPosition = 0;

                var strings = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    strings.Add(streamReader.ReadLine());
                    
                    if (streamReader.BaseStream.Position - previousChunkPosition >= chunkMaxSize && 
                        streamReader.Peek() >= 0)
                    {
                        SortAndWriteDataChunk(outputPath, strings, chunkNumber);
                        strings = new List<string>();
                        previousChunkPosition = streamReader.BaseStream.Position;
                        chunkNumber++;
                    }
                }

                if (strings.Any())
                {
                    SortAndWriteDataChunk(outputPath, strings, chunkNumber);
                }
            }
        }

        private static void SortAndWriteDataChunk(string chunksPath, List<string> chunkData, long chunkNumber)
        {
            var result = new List<(int,string)>(chunkData.Count);
            foreach (string line in chunkData)
            {
                string[] lineObjects = line.Split('.');
                result.Add((int.Parse(lineObjects[0]), lineObjects[1]));
            }
            
            result.Sort((x, y) =>
            {
                var res = string.CompareOrdinal(x.Item2, y.Item2);
                return res == 0 ? x.Item1.CompareTo(y.Item1) : res;
            });
            
            using (var streamWriter = new StreamWriter($"{chunksPath}\\Chunk-{chunkNumber}.txt"))
            {
                foreach ((int number, string str) in result)
                {
                    streamWriter.WriteLine($"{number}.{str}");
                }
            }
        }
        
        private static void MergeChunks(string chunksPath, string outputPath)
        {
            string[] filePaths = Directory.GetFiles(chunksPath);
            var readersHeap = new MinHeap<LineHandler>(filePaths.Length);
            var readersMap = new Dictionary<string, StreamReader>(filePaths.Length);
            
            foreach (string filePath in filePaths)
            {
                var reader = new StreamReader(filePath);
                if (!reader.EndOfStream)
                {
                    readersMap.Add(filePath, reader);
                    readersHeap.Insert(new LineHandler(filePath, reader.ReadLine()));
                }
            }
            
            using (var writer = new StreamWriter(outputPath))
            {
                LineHandler min = readersHeap.ExtractMin();
                while (min != null)
                {
                    writer.WriteLine(min.Line);
                    if (readersMap[min.ChunkPath].EndOfStream)
                    {
                        readersMap[min.ChunkPath].Close();
                        File.Delete(min.ChunkPath);
                    }
                    else
                    {
                        readersHeap.Insert(new LineHandler(min.ChunkPath, readersMap[min.ChunkPath].ReadLine()));
                    }
                    min = readersHeap.ExtractMin();
                }
            }
        }
        
        public static void TestResults(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string firstLine = reader.ReadLine();
                string secondLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    if (firstLine == null || secondLine == null)
                    {
                        Console.WriteLine("break;");
                        break;
                    }
                    
                    var x = new LineHandler("1", firstLine);
                    var y = new LineHandler("2", secondLine);
                    if (x.CompareTo(y) > 0)
                    {
                        throw new Exception($"Line {x.Line} is greater than {y.Line}");
                    }

                    firstLine = reader.ReadLine();
                    secondLine = reader.ReadLine();
                }
            }
            Console.WriteLine($"File {path} has been checked!");
        }
    }
    
    public class LineHandler : IComparable<LineHandler>, IIndexable<LineHandler>
    {
        public string ChunkPath { get; }
        public string Line { get; }
        
        private readonly string _strValue;
        
        private readonly int _number;

        public LineHandler(string chunkPath, string line)
        {
            Line = line;
            if (Line != null)
            {
                string[] values = Line.Split('.');
                _number = int.Parse(values[0]);
                _strValue = values[1];
            }
            ChunkPath = chunkPath;
        }
        
        public int CompareTo(LineHandler other)
        {
            var res = string.CompareOrdinal(this._strValue, other._strValue);
            return res != 0 ? res : this._number.CompareTo(other._number);
        }

        public int GetIndex()
        {
            return _strValue.GetHashCode();
        }
    }
}