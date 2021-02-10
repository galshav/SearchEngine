using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Framework
{
    public class SearchEngine
    {
        public Dictionary<string, List<int>> Index { get; }

        public SearchEngine(Dictionary<string, List<int>> index)
        {
            Index = index;
        }

        public SearchEngine(List<string> dataset) : 
            this(index: Framework.Index.CreateIndex(dataset))
        {
        }

        public static async Task<List<string>> FetchDatasetAsync(string dataSetPath)
        {
            ConsoleLogger.Info($"Reading dataset from {dataSetPath}");
            var data = await File.ReadAllLinesAsync(dataSetPath);
            return new List<string>(data);
        }

        /*
         * I didn't provide any input mechanisem for filters, so the following is just an example
         * Of how to receive and print result.
         */
        public void Start()
        {
            ConsoleLogger.Info("Starting search engine instance.");

            /*
             * Get here some search filters and call the Search() method.
             * Some possible providers:
             *  1. PIPE.
             *  2. Socket.
             *  3. FileSystemWatcher OnCreate event.
             *  4. Shared memory.
             *  5. Queue.
             * The logic should be called in some loop manner.
             */

            // Call the Search logic on the received filter.
            // Search(searchFilter);
            // print result, enumerate list of indices and print book[i].
            
        }

        public List<int> Search(Node searchFilter)
        {
            // Leaf reached.
            if (null == searchFilter.Left &&
                null == searchFilter.Right)
            {
                try
                {
                    return Index[$"{searchFilter.Data}"];
                }

                // It's not realy an exception, the filter just contain unindexed word.
                catch (KeyNotFoundException error)
                {
                    ConsoleLogger.Info($"{searchFilter.Data} not found in index.");
                    return new List<int>();
                }
            }

            // Search left and right branch.
            var leftIndices  = Search(searchFilter: searchFilter.Left);
            var rightIndices = Search(searchFilter: searchFilter.Right);

            if ("&&" == (string)searchFilter.Data)
            {
                // One of the branches doesn't contain given word.
                if (null == leftIndices || null == rightIndices)
                {
                    return new List<int>();
                }

                // Extract the similar indices.
                IEnumerable<int> intersections = leftIndices.AsQueryable().Intersect(rightIndices);
                return intersections.ToList();
            }

            if ("||" == (string)searchFilter.Data)
            {
                // Get all unique indices.
                var final = new List<int>();
                final.AddRange(leftIndices);
                final.AddRange(rightIndices);
                var hashSet = new HashSet<int>(final);
                return hashSet.ToList();
            }

            throw new ArgumentException($"Operand {searchFilter.Data} not supported, only logical OR/AND");
        }
    }
}
