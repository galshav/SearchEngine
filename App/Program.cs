using App.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace App
{
    class Program
    {
        public enum ErrorCode
        {
            Success =  0,
            Failure = -1,
        }

        /*
         * The search engine app built to run as independent docker image app.
         * Given a predefined dataset and dynamic search filters, the app prints
         * to the console all sentences within the dataset that match the search filter.
         *  Input:
         *      1. Predefined dataset contains sentences.
         *      2. Stream of filters.
         *  Output: None.
         */
        static void Main(string[] args)
        {
            try
            {
                ConsoleLogger.Info("Starting search engine application.");
                var book = SearchEngine.FetchDatasetAsync(dataSetPath: args[0]).Result;
                var searchEngine = new SearchEngine(dataset: book);
                searchEngine.Start();
                System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            }

            catch (Exception error)
            {
                ConsoleLogger.Error($"{error}");
                Environment.Exit((int)ErrorCode.Failure);
            }
        }
    }
}
