using App.Framework;
using System;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogger.Info($"Starting application.");
            
            // Create the index.
            List<string> book = new List<string>();
            book.Add("this is sentence #1");
            book.Add("this is sentence #2");
            var index = Framework.Index.CreateIndex(data: book);

            string wordToSearchFor = "this";

            // Get the indices of the sentences that the given word is appeared in.
            var indices = SearchWordInIndex(index, wordToSearchFor);

            foreach (int retrivedIndex in indices)
            {
                Console.WriteLine(book[retrivedIndex]);
            }
        }

       public static List<int> SearchWordInIndex(Dictionary<string, List<int>> index, string word) => index[word];        

       
    }
}
