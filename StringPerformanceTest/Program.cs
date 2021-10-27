using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Order;

namespace StringPerformanceTest
{
 
    [MemoryDiagnoser]
    public class Benchmark
    {
        string[] Lines;

        public int NumberOfLines;

       [Params("Bacon", "pork", "prosciutto")]
        public string SearchValue;

       [Params("Files/Bacon10.txt", "Files/Bacon25.txt", "Files/Bacon50.txt")]
        public string FileToRead;

        [GlobalSetup]
        public void GlobalSetup()
        {
            string fileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileToRead);
            Lines = File.ReadAllLines(fileLocation);
            NumberOfLines = Lines.Count();
        }


        [Benchmark]
        public int CountOccurrences()
        {
            int counter = 0;
            for (int i = 0; i < NumberOfLines; i++)
            {
                //jit compiler And operator with 1 to test if our element is odd
                if ((i & 1) != 1)//skip the odd number index, as i checked all the words are in the even numbers,might as well used it to have a performance speed.
                {
                    if (Lines[i].Contains(SearchValue))//search for the word
                    {
                        counter++;//increment the counter if we have word in that list
                    }
                }
            }
            return counter;
        }


        //[Benchmark]
        //public int CountOccurrences()
        //{
        //    int counter = 0;
        //    for (int i = 0; i < NumberOfLines; i++)
        //    {
        //        //using modulus
        //        if (i % 2 == 0 && Lines[i].Contains(SearchValue))
        //        {
        //            counter++;
        //        }
        //    }
        //    return counter;
        //}

 
 

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();

            
        }
    }
}
