using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace SVG_from_Script
{
    public static class FindReplace
    {
        // REF: https://www.rosettacode.org/wiki/Count_occurrences_of_a_substring#C.23
        public static int CountSubStrings(string OriginalText, string FindText)
        {
            int count = 0;

            if (OriginalText.ToLower().Contains(FindText.ToLower()))
            {
                for (int i = 0; i < OriginalText.Length; i++)
                {
                    if (OriginalText.Substring(i).Length >= FindText.Length)
                    {
                        bool equals = OriginalText.ToLower().Substring(i, FindText.Length).Equals(FindText.ToLower());
                        if (equals)
                        {
                            count++;
                            i += FindText.Length - 1;  // Fix: Don't count overlapping matches
                        }
                    }
                }
            }
            return count;
        }

        // REF: https://stackoverflow.com/questions/26933069/how-to-implement-find-replace-next-in-a-string-on-c
        //string yourOriginalString = "ab cd ab cd ab cd";
        //string pattern = "ab";
        //string yourNewDescription = "123";
        //int startingPositionOffset = 0;
        //int yourOriginalStringLength = yourOriginalString.Length;

        //MatchCollection match = Regex.Matches(yourOriginalString, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

        //foreach (Match m in match)
        //{
        //    yourOriginalString = yourOriginalString.Substring(0, m.Index+startingPositionOffset) + yourNewDescription + yourOriginalString.Substring(m.Index + startingPositionOffset+ m.Length);
        //    startingPositionOffset = yourOriginalString.Length - yourOriginalStringLength;
        //}

        public static ReplaceResults ReplaceAll(string OriginalText, string FindText, string ReplacementText)
        {
            ReplaceResults retval = new ReplaceResults
            {
                OccuranceCount = 0,
                ResultingText = OriginalText
            };

            int startingPositionOffset = 0;
            int originalStringLength = OriginalText.Length;

            MatchCollection match = Regex.Matches(retval.ResultingText, FindText, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            retval.OccuranceCount = match.Count;

            foreach (Match m in match)
            {
                retval.ResultingText = retval.ResultingText.Substring(0, m.Index + startingPositionOffset) + ReplacementText + retval.ResultingText.Substring(m.Index + startingPositionOffset + m.Length);
                startingPositionOffset = retval.ResultingText.Length - originalStringLength;
            }


            return retval;
        }
    }

    public struct ReplaceResults
    {
        public int OccuranceCount;
        public string ResultingText;
    }
}
