using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using Lab3Q1;
using static Lab3Q1.HelperFunctions;

namespace WordCounttesting
{
    class Program
    {
        static void Main(string[] args)
        {
          // map and mutex for thread safety
          Mutex mutex = new Mutex();
          Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();
           Dictionary<string, int> wcountsMultiThread = new Dictionary<string, int>();


            var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
           };

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================

            //string line;  // for storing each line read from the file
            //string character = "";  // empty character to start
            //System.IO.StreamReader file = new System.IO.StreamReader(filenames[1]);

            // while ((line = file.ReadLine()) != null)
            // {

            //Console.WriteLine("{0} --", line);
            // int wordcount = Lab3Q1.HelperFunctions.WordCount(ref line, 0);
            // if ((wordcount != 0) & (!wcountsSingleThread.ContainsKey(line)))
            //wcountsSingleThread.Add(line, wordcount);


            // }
            foreach (string i in filenames)
            {
               
                CountCharacterWords(i, mutex, wcountsSingleThread);
        
            }
            List<Tuple<int, string>> sortedlist = new List<Tuple<int, string>>();
            sortedlist = SortCharactersByWordcount(wcountsSingleThread);
            PrintListofTuples(sortedlist);
            Console.WriteLine( "SingleThread is Done!");
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================
            
            //int num_threads = 12;
            List<Thread> threads = new List<Thread>();
            foreach (string i in filenames)
            {   
                    Thread thread1 = new Thread(() => CountCharacterWords(i, mutex, wcountsMultiThread));
                    thread1.Start();
                    threads.Add(thread1);

            }
                foreach (Thread t in threads)
                    t.Join();

            List<Tuple<int, string>> sortedlist_multi = new List<Tuple<int, string>>();
            
            sortedlist_multi = SortCharactersByWordcount(wcountsMultiThread);
            PrintListofTuples(sortedlist_multi);
           

            


            Console.WriteLine( "MultiThread is Done!");
           //return 0;
        }
    }
}
