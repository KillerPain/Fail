using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Items
{
    public class Levenshtein
    {
        private int[,] dist = new int[100, 100];
        public int calculate(string inputWord, string archiveWord)
        {
            if (inputWord.Length == 0 || archiveWord.Length == 0) return Math.Max(inputWord.Length, archiveWord.Length);
            for (int i = 0; i <= inputWord.Length; ++i)
                dist[i, 0] = i;
            for (int i = 0; i <= archiveWord.Length; ++i)
                dist[0, i] = i;         
            for (int i = 1; i <= inputWord.Length; i++)
                for (int j = 1; j <= archiveWord.Length; j++)
                {
                    int cost = 0;
                    if (inputWord[i - 1] != archiveWord[j - 1]) cost = 2;
                    dist[i, j] = Math.Min(dist[i - 1, j] + 1, dist[i, j - 1] + 1);
                    dist[i, j] = Math.Min(dist[i, j], dist[i - 1, j - 1] + cost);
                }
            return dist[inputWord.Length, archiveWord.Length];
        }               
    }
}
