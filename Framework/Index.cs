using System;
using System.Collections.Generic;

namespace App.Framework {
    public static class Index
    {
        public static Dictionary<string, List<int>> CreateIndex(List<string> data)
        {
            // initializations.
            Dictionary<string, List<int>> index = new Dictionary<string, List<int>>();

            // indexing.
            for (int i = 0; i < data.Count; ++i)
            {
                var sentence = data[i];
                var words = sentence.Split(separator: " ");
                HashSet<string> hashedSet = new HashSet<string>(words);
                foreach (string currentWord in hashedSet)
                {
                    if (index.ContainsKey(currentWord))
                    {
                        index[currentWord].Add(i);
                        continue;
                    }

                    index[currentWord] = new List<int>();
                    index[currentWord].Add(i);
                }
            }

            return index;
        }
    }
}
