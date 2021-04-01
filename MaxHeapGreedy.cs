using System.Collections.Generic;
using System.Text;

namespace Data_structures
{
    public static class MaxHeapGreedy
    {
        //https://leetcode.com/problems/reorganize-string/
        public static string ReorganizeString(string S)
        {
            //recursive use up and alternate place the most common letters
            var dic = new Dictionary<char, int>();

            // add or update the char counts dictionary
            for (int i = 0; i < S.Length; i++)
            {
                if (!dic.TryAdd(S[i], 1))
                {
                    dic[S[i]] += 1;
                }
            }

            //Max heap to keep track of max and next max char
            //The custom Comparer allows us to sort based on char count in dictionary
            //Since SortedSet can contain only distict values we cannot return 0 to the Comparer
            //If two char counts are same then compare some other random unique to keep all chars in the set
            var maxHeap = new SortedSet<char>(dic.Keys, Comparer<char>.Create((a, b) =>
            {
                if (dic[a] == dic[b]) return a.CompareTo(b);
                return dic[a] - dic[b] > 0 ? 1 : -1;
            }));




            var sb = new StringBuilder(string.Empty);
            while (maxHeap.Count > 1)
            {
                var max = maxHeap.Max;
                maxHeap.Remove(max);
                var nextMax = maxHeap.Max;
                maxHeap.Remove(nextMax);

                sb.Append($"{max}{nextMax}");

                //update counts in dictionary; Add char back to max heap if more left in the original string
                dic[max] -= 1;
                dic[nextMax] -= 1;

                if (dic[max] > 0)
                {
                    maxHeap.Add(max);
                }

                if (dic[nextMax] > 0)
                {
                    maxHeap.Add(nextMax);
                }
            }

            if (maxHeap.Count == 1 && dic[maxHeap.Max] <= 1)
            {
                sb.Append(maxHeap.Max);
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}
